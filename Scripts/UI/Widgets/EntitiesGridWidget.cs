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
            this.missionLayoutManager.ActiveGameState.RemoveLink(children[i].GetComponent<EntityWidgetManager>().GetEntity);//yes
            Destroy(children[i]);
        }
    }

    private void CreateChild(Entity entity)
    {
        GameObject child = Instantiate(this.EntityPrefab, transform);
        children.Add(child);
        EntityWidgetManager childEWM = child.GetComponent<EntityWidgetManager>();
        childEWM.SetData(entity);
        //Assign on click action to entities
        childEWM.OnClick = () => { this.EntityClicked(entity); };
        childEWM.OnHeld = () => { this.EntityHeld(entity); };
        this.missionLayoutManager.ActiveGameState.CreateLink(entity, childEWM);
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

    private void EntityHeld(Entity entity)
    {
        Debug.Log($"{entity.Name} held");
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
        this.missionLayoutManager.ActiveGameState.TargetEntity(entity);
    }
}