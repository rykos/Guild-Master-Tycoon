using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionLayoutManager : MonoBehaviour, IUIWidget
{
    public HeroWidgetManager HeroWidget, MonsterWidget;
    public GameObject DungeonResultPagePrefab;
    private MissionModel missionModel;
    private Fight fight;

    private float time = 0;
    private int index = 0;

    private void Update()
    {
        if (fight.won != null)
        {
            if (time > 1)
            {
                Debug.Log(this.fight.Turns[index].Log());
                time = 0;
                index++;
                if (index > this.fight.Turns.Count() - 1)
                {
                    enabled = false;
                }
            }
            time += Time.deltaTime;
        }
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
        this.HeroWidget.SetData(this.missionModel.Heroes.First());
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
    public Entity AttackingEntity, TargetEntity;
    public double DamageDealt;

    public FightTurn(Entity ae, Entity te, double damage)
    {
        this.AttackingEntity = ae;
        this.TargetEntity = te;
        this.DamageDealt = damage;
    }

    public string Log()
    {
        return $"{AttackingEntity} attacked {TargetEntity}, dealing {DamageDealt} damage";
    }
}

public class FightSimulator
{

}