using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MissionLayoutManager : MonoBehaviour, IUIWidget
{
    public HeroWidgetManager HeroWidget, MonsterWidget;//Widgets displaying currently acitve entities
    public ListWidget HeroesListWidget, MonstersListWidget;//Queue for awaiting entities
    public GameObject DungeonResultPagePrefab;//Result page prefab
    private MissionModel missionModel;//Active mission
    private Fight fight;//Fight logic
    private FightSimulator fightSimulator;//Displays fight in turns

    private float time = 2;
    private int index = 0;
    private float animationTime = 0.8f;

    private void Update()
    {
        if (fight.won != null)
        {
            if (time > 1)
            {
                FightTurn currentTurn = this.fightSimulator.NextStep();
                if (currentTurn == default)
                {
                    enabled = false;
                    return;
                }
                if (currentTurn.AttackingEntity.Entity.MasterType == typeof(HeroModel))//Hero attacked
                {
                    HeroAction(currentTurn);
                    if (currentTurn.TargetEntity.HealthPercentage <= 0 && missionModel.Dungeon.Monsters.IndexOf(currentTurn.TargetEntity.Entity as MonsterModel) < missionModel.Dungeon.Monsters.Count - 1)
                    {
                        StartCoroutine(RunAfter(animationTime, () => { MonsterWidget.HeroHealthBar.SetDataInstant(1f); }));
                    }
                }
                else//Monster attacked
                {
                    EnemyAction(currentTurn);
                    if (currentTurn.TargetEntity.HealthPercentage <= 0 && missionModel.Heroes.IndexOf(currentTurn.TargetEntity.Entity as HeroModel) < missionModel.Heroes.Count - 1)
                    {
                        StartCoroutine(RunAfter(animationTime, () => { HeroWidget.HeroHealthBar.SetDataInstant(1f); }));
                    }
                }
                Debug.Log(
                    $"Attacker:{currentTurn.AttackingEntity.HealthPercentage * 100}% Target:{currentTurn.TargetEntity.HealthPercentage * 100}%");
                time = 0;
            }
            time += Time.deltaTime;
        }
    }

    IEnumerator RunAfter(float s, Action action)
    {
        yield return new WaitForSeconds(s);
        action();
    }

    private void HeroAction(FightTurn turn)
    {
        this.HeroWidget.SetData(turn.AttackingEntity);
        this.MonsterWidget.SetData(turn.TargetEntity);
    }
    private void EnemyAction(FightTurn turn)
    {
        this.MonsterWidget.SetData(turn.AttackingEntity);
        this.HeroWidget.SetData(turn.TargetEntity);
    }

    public void OnFinishButtonClicked()
    {
        //Redirect to result page if won
        Destroy(gameObject);
        if (fight.won == null)
        {
            this.Skip();
        }
        if (fight.won == true)
        {
            print("won");
            Instantiate(this.DungeonResultPagePrefab, GameObject.Find("/Canvas").transform).GetComponent<DungeonResultPage>().SetData(this.missionModel);
        }
        else
        {
            print("lost");
        }
    }

    //Skips battle
    private void Skip()
    {

    }

    public void Rebuild()
    {
        this.fight = new Fight(this.missionModel);
        this.fightSimulator = new FightSimulator(this.fight.Turns);
        this.HeroesListWidget.SetData(this.missionModel.Heroes);
        this.MonstersListWidget.SetData(this.missionModel.Dungeon.Monsters);
    }

    /// <param name="data">MissionModel</param>
    public void SetData(object data)
    {
        this.missionModel = data as MissionModel;
        this.Rebuild();
    }
}

public class Fight
{
    private MissionModel missionModel;
    public bool? won;
    public List<FightTurn> Turns = new List<FightTurn>();

    public Fight(MissionModel missionModel)
    {
        this.missionModel = missionModel;
        //
        this.PrepareEntities();
        while (true)
        {
            MonsterModel mm = this.PickMonster();
            HeroModel hm = this.PickCharacter();
            if (mm != default && hm != default)
            {
                Turn(hm, mm);
            }
            else//If no monster is left alive
            {
                break;
            }
        }
        if (this.missionModel.Heroes.Where(x => x.CurrentHealth > 0).Count() > this.missionModel.Dungeon.Monsters.Where(x => x.CurrentHealth > 0).Count())
        {
            this.won = true;
        }
        else
        {
            this.won = false;
        }
    }
    private void PrepareEntities()
    {
        this.Rebuild(this.missionModel.Heroes);
        this.Rebuild(this.missionModel.Dungeon.Monsters);
        this.missionModel.Heroes.Sort((a, b) => a.MaxHealth.CompareTo(b.MaxHealth));
        this.missionModel.Dungeon.Monsters.Sort(((a, b) => a.MaxHealth.CompareTo(b.MaxHealth)));
    }

    private void Rebuild(IEnumerable entities)
    {
        foreach (object entity in entities)
        {
            ((Entity)entity).Build();
        }
    }
    private HeroModel PickCharacter()
    {
        return this.missionModel.Heroes.FirstOrDefault(x => x.CurrentHealth > 0);
    }
    private MonsterModel PickMonster()
    {
        return this.missionModel.Dungeon.Monsters.FirstOrDefault(x => x.CurrentHealth > 0);
    }

    private void Turn(HeroModel hero, MonsterModel monster)
    {
        if (hero.CurrentHealth > 0)//Hero alive, take turn
        {
            double damage = hero.DealDamage();
            monster.TakeDamage(damage);
            RegisterTurn(hero, monster, damage);
        }
        if (monster.CurrentHealth > 0)//Monster alive, take turn
        {
            double damage = monster.DealDamage();
            hero.TakeDamage(damage);
            RegisterTurn(monster, hero, damage);
        }
    }

    private void RegisterTurn(Entity attackingEntity, Entity targetEntity, double damage)
    {
        this.Turns.Add(new FightTurn(attackingEntity, targetEntity, damage));
    }
}

public struct FightTurn
{
    public EntityState AttackingEntity, TargetEntity;
    public double DamageDealt;

    public FightTurn(Entity ae, Entity te, double damage)
    {
        this.AttackingEntity = new EntityState(ae, ae.GetHealthPercentage());
        this.TargetEntity = new EntityState(te, te.GetHealthPercentage());
        this.DamageDealt = damage;
    }

    public string Log()
    {
        return $"{AttackingEntity} attacked {TargetEntity}, dealing {DamageDealt} damage";
    }

    public static bool operator ==(FightTurn a, FightTurn b)
    {
        if (a.AttackingEntity.Entity == b.AttackingEntity.Entity && a.DamageDealt == b.DamageDealt && a.TargetEntity.Entity == b.TargetEntity.Entity)
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(FightTurn a, FightTurn b)
    {
        return !(a == b);
    }
}

public class FightSimulator
{
    List<FightTurn> turns;
    private int step = 0;

    public FightSimulator(List<FightTurn> turns)
    {
        this.turns = turns;
    }

    public FightTurn NextStep()
    {
        if (step > this.turns.Count - 1)
        {
            return default;
        }
        FightTurn turn = this.turns[step];
        this.step++;//Advance to next step
        return turn;
    }

    public FightTurn SeekNextStep()
    {
        return this.turns[step + 1];
    }
}