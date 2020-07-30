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
    public GameState ActiveGameState = new GameState();
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

    public void Rebuild()
    {
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

    ///<summary>Helps to manage game state</summary>
    public class GameState
    {
        public EntityWidgetManager ActiveEntity;//Currently active entity
        public EntityWidgetManager Target;//Active target of user action
        private Dictionary<Entity, EntityWidgetManager> entityToWidgetMap = new Dictionary<Entity, EntityWidgetManager>();

        public void TargetEntity(EntityWidgetManager entityWidget)
        {
            if (this.Target != null)
            {
                this.Target.IsHighlighted = false;
            }
            if (this.Target == entityWidget)//Clicked same target twice
            {
                this.Target = null;
            }
            else//Different target
            {
                entityWidget.IsHighlighted = true;
                this.Target = entityWidget;
            }
        }
        public void TargetEntity(Entity entity)
        {
            this.TargetEntity(this.entityToWidgetMap[entity]);
        }

        public void SetActiveEntity(EntityWidgetManager entityWidget)
        {
            if (this.ActiveEntity != null)//Deactivate old entity
            {
                this.ActiveEntity.IsActive = false;
            }
            //Activate new one
            this.ActiveEntity = entityWidget;
            this.ActiveEntity.IsActive = true;
        }
        public void SetActiveEntity(Entity entity)
        {
            this.SetActiveEntity(this.entityToWidgetMap[entity]);
        }

        ///<summary>Creates a link between Entity and its Widget</summary>
        public void CreateLink(Entity entity, EntityWidgetManager entityWidgetManager)
        {
            this.entityToWidgetMap.Add(entity, entityWidgetManager);
        }
        public void RemoveLink(Entity entity)
        {
            this.entityToWidgetMap.Remove(entity);
        }
    }

    ///<summary>Contains game logic</summary>
    public class Game
    {
        public Entity GetActiveEntity { get => this.activeEntity; }
        private List<Entity> entities;
        private List<HeroModel> heroes;
        private List<MonsterModel> monsters;
        private Entity activeEntity;//Entity that holds current turn

        public Game(List<HeroModel> heroes, List<MonsterModel> monsters)
        {
            this.heroes = heroes;
            this.monsters = monsters;
            this.entities = new List<Entity>();
            this.entities.AddRange(this.heroes);
            this.entities.AddRange(this.monsters);
            this.SortOnSpeed();
        }

        private void SortOnSpeed()
        {
            this.entities.Sort();
        }

        public void SelectNextEntity()
        {
            if (this.activeEntity != null)
            {
                int id = this.entities.IndexOf(this.activeEntity);
                this.activeEntity = this.entities[id + 1];
            }
            else
            {
                this.activeEntity = this.entities[0];
            }
        }
    }
}