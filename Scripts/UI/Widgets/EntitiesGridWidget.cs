using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntitiesGridWidget : MonoBehaviour, IUIWidget
{
    public MissionLayoutManager missionLayoutManager;
    public GameObject EntityPrefab;
    //
    private List<Entity> entities;
    private List<GameObject> children = new List<GameObject>();//entities represented as (GameObject)EntityWidgets

    public void Rebuild()
    {
        this.DestroyChildren();
        foreach (Entity entity in this.entities)
        {
            this.CreateChild(entity);
        }
    }

    public void SetData(object data)
    {
        //Generic use is just better, keeping it becouse of interface
        throw new System.Exception();
    }
    public void SetData<T>(List<T> data)
    {
        this.entities = data.Cast<Entity>().ToList();
        this.Rebuild();
    }
    public void SetData<T>(IEnumerable<T> data)
    {
        this.entities = data.Cast<Entity>().ToList();
        this.Rebuild();
    }

    private void DestroyChildren()
    {
        for (int i = this.children.Count - 1; i > 0; i++)
        {
            Destroy(children[i]);
        }
    }

    private void CreateChild(Entity entity)
    {
        GameObject child = Instantiate(this.EntityPrefab, transform);
        children.Add(child);
        child.GetComponent<EntityWidgetManager>().SetData(entity);
        //Assign on click action to entities
        child.GetComponent<EntityWidgetManager>().OnClick = () => { this.EntityClicked(entity); };
    }

    private void EntityClicked(Entity entity)
    {
        if (entity.GetType() == typeof(HeroModel))
        {
            this.HeroEntityClicked(entity);
        }
        else
        {
            this.MonsterEntityClicked(entity);
        }
    }

    private void HeroEntityClicked(Entity entity)
    {
        //missionLayoutManager.SetDetails($"{((HeroModel)entity).Name}: show details of this hero, also skills");
        TargetEntity(entity);
    }

    private void MonsterEntityClicked(Entity entity)
    {
        //missionLayoutManager.SetDetails($"{((MonsterModel)entity).Name}: show details of this monster");
        TargetEntity(entity);
    }

    private void TargetEntity(Entity entity)
    {
        //Change target entity border to show the selected entity
        //Save target entity to memory
        //Could map entities to GameObjects, would be faster
        EntityWidgetManager entityWidget = children.First(e => (Entity)e.GetComponent<EntityWidgetManager>().GetEntity == entity).GetComponent<EntityWidgetManager>();
        entityWidget.IsHighlighted = true;
        if (this.missionLayoutManager.ActiveGameState.Target != null)
        {
            this.missionLayoutManager.ActiveGameState.Target.IsHighlighted = false;
        }
        this.missionLayoutManager.ActiveGameState.Target = entityWidget;
    }
}