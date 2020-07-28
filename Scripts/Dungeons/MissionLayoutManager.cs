using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MissionLayoutManager : MonoBehaviour, IUIWidget
{
    public EntitiesGridWidget HeroesGridWidget, MonstersGridWidget;
    public MissionActionPanelWidget ActionPanelWidget;
    public GameObject DungeonResultPagePrefab;//Result page prefab
    private MissionModel missionModel;//Active mission

    private float time = 2;
    private int index = 0;
    private float animationTime = 0.6f;

    private void Update()
    {
        // if (fight.won != null)
        // {
        //     if (time > 1)
        //     {
        //         FightTurn currentTurn = this.fightSimulator.NextStep();
        //         if (currentTurn == default)
        //         {
        //             enabled = false;
        //             return;
        //         }
        //         UpdateMainWidgets(currentTurn);
        //         if (currentTurn.AttackingEntity.Entity.MasterType == typeof(HeroModel))//Hero attacked
        //         {
        //             if (currentTurn.TargetEntity.HealthPercentage <= 0 && missionModel.Dungeon.Monsters.IndexOf(currentTurn.TargetEntity.Entity as MonsterModel) < missionModel.Dungeon.Monsters.Count - 1)
        //             {
        //                 StartCoroutine(RunAfter(animationTime, () =>//Load next monster
        //                 {
        //                     MonsterWidget.HealthBar.SetDataInstant(1f);
        //                     UpdateMainWidget(new EntityState(NextEntity<MonsterModel>((MonsterModel)currentTurn.TargetEntity.Entity, missionModel.Dungeon.Monsters)), MonsterWidget);
        //                     MonstersListWidget.SetData(GetEntitiesAfter(this.missionModel.Dungeon.Monsters, currentTurn.TargetEntity));
        //                 }));
        //             }
        //         }
        //         else//Monster attacked
        //         {
        //             if (currentTurn.TargetEntity.HealthPercentage <= 0 && missionModel.Heroes.IndexOf(currentTurn.TargetEntity.Entity as HeroModel) < missionModel.Heroes.Count - 1)
        //             {
        //                 StartCoroutine(RunAfter(animationTime, () =>//Load next hero
        //                 {
        //                     HeroWidget.HealthBar.SetDataInstant(1f);
        //                     UpdateMainWidget(new EntityState(NextEntity<HeroModel>((HeroModel)currentTurn.TargetEntity.Entity, missionModel.Heroes)), HeroWidget);
        //                     HeroesListWidget.SetData(GetEntitiesAfter(this.missionModel.Heroes, currentTurn.TargetEntity));
        //                 }));
        //             }
        //         }
        //         time = 0;
        //     }
        //     time += Time.deltaTime;
        // }
    }

    IEnumerator RunAfter(float s, Action action)
    {
        yield return new WaitForSeconds(s);
        action();
    }

    private void UpdateMainWidgets(FightTurn turn)
    {
        // if (turn.AttackingEntity.Entity.MasterType == typeof(HeroModel))
        // {
        //     this.HeroWidget.SetData(turn.AttackingEntity);
        //     this.MonsterWidget.SetData(turn.TargetEntity);
        // }
        // else
        // {
        //     this.HeroWidget.SetData(turn.TargetEntity);
        //     this.MonsterWidget.SetData(turn.AttackingEntity);
        // }
    }
    private void UpdateMainWidget(EntityState entityState, EntityWidgetManager widget)
    {
        widget.SetData(entityState);
    }

    private List<T> GetEntitiesAfter<T>(List<T> entityList, EntityState entity)
    {
        int index = entityList.IndexOf(entityList.Find(x => (Entity)(object)x == entity.Entity));
        return entityList.GetRange(index + 2, entityList.Count - index - 2);
    }
    private Entity NextEntity<T>(T entity, List<T> entities)
    {
        int index = entities.IndexOf(entity);
        return (Entity)(object)entities[index + 1];
    }

    public void OnFinishButtonClicked()
    {
        // //Redirect to result page if won
        // Destroy(gameObject);
        // if (fight.won == null)
        // {
        //     this.Skip();
        // }
        // if (fight.won == true)
        // {
        //     print("won");
        //     Instantiate(this.DungeonResultPagePrefab, GameObject.Find("/Canvas").transform).GetComponent<DungeonResultPage>().SetData(this.missionModel);
        // }
        // else
        // {
        //     print("lost");
        // }
    }

    //Skips battle
    private void Skip()
    {

    }

    public void Rebuild()
    {
        // this.fight = new Fight(this.missionModel);
        // this.fightSimulator = new FightSimulator(this.fight.Turns);
        // this.HeroesListWidget.SetData(this.missionModel.Heroes.GetRange(1, this.missionModel.Heroes.Count - 1));
        // this.MonstersListWidget.SetData(this.missionModel.Dungeon.Monsters.GetRange(1, this.missionModel.Dungeon.Monsters.Count - 1));
        //
        this.HeroesGridWidget.SetData<HeroModel>(this.missionModel.Heroes);
        this.MonstersGridWidget.SetData<MonsterModel>(this.missionModel.Dungeon.Monsters.Take(3));
        this.ActionPanelWidget.SetSkills(this.missionModel.Heroes.First().Skills);
    }

    /// <param name="data">MissionModel</param>
    public void SetData(object data)
    {
        this.missionModel = data as MissionModel;
        this.Rebuild();
    }

    public void SetDetails(string text)
    {
        this.ActionPanelWidget.SetDetails(text);
    }
}

///<summary>Obsolete</summary>
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

///<summary>Obsolete</summary>
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