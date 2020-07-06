using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGridWidget : MonoBehaviour, IUIWidget
{
    public GameObject GridContainer;
    public GameObject ItemSlotPrefab;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            this.BuildItem();
        }
    }

    private void BuildItem()
    {
        ItemType itemType = (ItemType)Random.Range(1, System.Enum.GetNames(typeof(ItemType)).Length);
        Item item = new Item("Embeded Something", itemType, new Stats(10, 5, 15), 10, 15)
        {
            Image = AssetManager.Instance.RandomIcon(itemType)
        };
        GameObject newSlot = Instantiate(ItemSlotPrefab, GridContainer.transform);
        newSlot.GetComponent<ItemWidget>().SetData(item);
    }

    public void SetData()
    {

    }

    public void Rebuild()
    {
        
    }
}
