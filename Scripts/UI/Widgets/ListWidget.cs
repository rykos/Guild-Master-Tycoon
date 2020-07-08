using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListWidget : MonoBehaviour, IUIWidget
{
    public GameObject ListContent;//Contains ListElements
    public GameObject ListElementPrefab;//Element to spawn
    public GameObject ActivableElement;//Element to activate on child click

    public object ElementsData;//List of data elements for widgets inside content

    private void Awake()
    {
        List<DungeonModel> dm = new List<DungeonModel>() {
            new DungeonModel() { Name = "Spanker", Rarity = Rarity.Legendary, Level = new Level(25) },
            new DungeonModel() { Name = "Bobo", Rarity = Rarity.Normal, Level = new Level(31) }
        };
        this.SetData(dm);
    }

    private void OnEnable()
    {
        this.Rebuild();
    }
    private void OnDisable()
    {
        DestroyChildren();
    }

    public void Rebuild()
    {
        DestroyChildren();
        CreateChildren();
    }

    public void SetData(object data)
    {
        this.ElementsData = data;
        this.Rebuild();
    }

    private void DestroyChildren()
    {
        for (int i = this.ListContent.transform.childCount - 1; i > -1; i--)
        {
            Destroy(this.ListContent.transform.GetChild(i).gameObject);
        }
    }
    private void CreateChildren()
    {
        foreach (var dataElement in (ElementsData as IEnumerable))
        {
            GameObject newTile = Instantiate(ListElementPrefab, this.ListContent.transform);
            newTile.GetComponent<IUIWidget>().SetData(dataElement);
        }
    }

    public void ActivateElement(object data = null)
    {
        this.ActivableElement.SetActive(true);
        this.ActivableElement.GetComponent<IUIWidget>()?.SetData(data);
    }
}
