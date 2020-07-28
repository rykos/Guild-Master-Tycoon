using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntitiesGridWidget : MonoBehaviour, IUIWidget
{
    public MissionLayoutManager missionLayoutManager;
    public GameObject EntityPrefab;
    //
    private List<Entity> entities;
    private List<GameObject> children = new List<GameObject>();

    public void Rebuild()
    {
        this.DestroyChildren();
        foreach (Entity entity in this.entities)
        {
            this.CreateChild(entity);
        }
    }

    ///<param name="data">List<Entity></param> 
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
        child.GetComponent<EntityWidgetManager>().OnClick = () => { this.EntityClicked(entity); };
    }

    private void EntityClicked(Entity entity)
    {
        if (entity.GetType() == typeof(HeroModel))
        {
            missionLayoutManager.SetDetails($"{((HeroModel)entity).Name}: show details of this hero, also skills");
        }
        else
        {
            missionLayoutManager.SetDetails($"{((MonsterModel)entity).Name}: show details of this monster");
        }
    }
}