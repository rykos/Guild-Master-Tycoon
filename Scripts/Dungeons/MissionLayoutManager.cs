using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MissionLayoutManager : MonoBehaviour, IUIWidget
{
    public GameObject SkillDetailsWidget;//Prefab of SkillDetailsWidget
    public GameObject EntityDetailsWidget;//Prefab of EntityDetailsWidget
    public EntitiesGridWidget HeroesGridWidget, MonstersGridWidget;
    public MissionActionPanelWidget ActionPanelWidget;
    public GameObject DungeonResultPagePrefab;//Result page prefab
    public GameState ActiveGameState = new GameState();
    private MissionModel missionModel;//Active mission
    private Game game;

    private float time = 2;
    private int index = 0;
    private float animationTime = 0.6f;

    private IEnumerator IERunAfter(float s, Action action)
    {
        yield return new WaitForSeconds(s);
        action();
    }

    public void RunAfter(float s, Action action)
    {
        StartCoroutine(IERunAfter(s, action));
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
        this.HeroesGridWidget.SetData<HeroModel>(this.missionModel.Heroes.Take(3));
        this.MonstersGridWidget.SetData<MonsterModel>(this.missionModel.Dungeon.Monsters.Take(3));
        game = new Game(this.missionModel.Heroes.Take(3).ToList(), this.missionModel.Dungeon.Monsters.Take(3).ToList(), this.ActiveGameState, this);
        game.SelectNextEntity();
    }

    /// <param name="data">MissionModel</param>
    public void SetData(object data)
    {
        this.missionModel = data as MissionModel;
        this.Rebuild();
    }

    public void ShowDetails<T>(T data)
    {
        GameObject details = Instantiate(
            (typeof(T) == typeof(Entity) ? this.EntityDetailsWidget : this.SkillDetailsWidget),
            GameObject.Find("/Canvas").transform);
        details.GetComponent<IUIWidget>().SetData((T)data);
    }

    public void PlayerUseSkill(Abilities.Skill skill)
    {
        this.game.PlayerTurn(skill);
    }

    public void PlayerTurn(Entity entity)
    {
        this.ActionPanelWidget.SetSkills(entity.Skills);
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
        private GameState gameState;
        private MissionLayoutManager missionLayoutManager;
        private List<Entity> entities;
        private List<HeroModel> heroes;
        private List<MonsterModel> monsters;
        private Entity activeEntity;//Entity that holds current turn
        private Entity getTarget { get => this.gameState.Target?.GetEntity; }

        public Game(List<HeroModel> heroes, List<MonsterModel> monsters, GameState gameState, MissionLayoutManager missionLayoutManager)
        {
            this.heroes = heroes;
            this.monsters = monsters;
            this.gameState = gameState;
            this.entities = new List<Entity>();
            this.entities.AddRange(this.heroes);
            this.entities.AddRange(this.monsters);
            this.missionLayoutManager = missionLayoutManager;
            this.SortOnSpeed();
        }

        private void SortOnSpeed()
        {
            this.entities = this.entities.OrderByDescending(x => x.FinalStats.Speed).ToList();
        }

        public void SelectNextEntity()
        {
            this.entities.RemoveAll(x => x.GetHealthPercentage() <= 0);
            if (this.activeEntity != null)
            {
                int id = this.entities.IndexOf(this.activeEntity);
                if (id > this.entities.Count - 2)
                {
                    this.activeEntity = this.entities[0];
                }
                else
                {
                    this.activeEntity = this.entities[id + 1];
                }
            }
            else
            {
                this.activeEntity = this.entities[0];
            }
            this.gameState.SetActiveEntity(this.activeEntity);
            this.HandleNewEntity();
        }

        private void HandleNewEntity()
        {
            if (this.activeEntity.MasterType == typeof(MonsterModel))//Give controll to the AI
            {
                this.missionLayoutManager.RunAfter(1f, this.AITurn);
            }
            else//Give controll to the player
            {
                this.missionLayoutManager.PlayerTurn(this.activeEntity);
            }
        }

        private void AITurn()//Retarded AI
        {
            Hit(this.activeEntity, this.heroes.ToArray(), this.activeEntity.Skills.Abilities[0]);
            this.missionLayoutManager.RunAfter(1f, () =>
            {
                Debug.Log($"{this.activeEntity.Name} ended his turn");
                this.EndTurn();
            });
        }

        public void PlayerTurn(Abilities.Skill skill)
        {
            if (this.activeEntity != null && this.getTarget != null)
            {
                if (skill.SkillTargetType == Abilities.SkillTargetType.Friendly)
                {
                    if (this.getTarget.MasterType == typeof(MonsterModel))
                    {
                        return;
                    }
                }
                else if (skill.SkillTargetType == Abilities.SkillTargetType.Enemy)
                {
                    if (this.getTarget.MasterType == typeof(HeroModel))
                    {
                        return;
                    }
                }
                //All good
                Debug.Log($"{this.activeEntity.Name} used {skill.GetName()}");
                this.Hit(this.activeEntity,
                new Entity[] { this.gameState.Target.GetEntity }, skill);
                this.EndTurn();
            }
        }

        private void EndTurn()
        {
            this.SelectNextEntity();
        }

        private void Hit(Entity caster, Entity[] targets, Abilities.Skill skill)
        {
            skill.ActiveUse(targets);
        }
    }
}