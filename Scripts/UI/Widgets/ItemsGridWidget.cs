using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGridWidget : MonoBehaviour, IUIWidget
{
    public GameObject GridContainer;
    public GameObject ItemSlotPrefab;
    public ItemActionType DefaultItemAction;
    private HeroModel context;

    private void OnEnable()
    {
        //Context does not exist yet
    }
    private void OnDisable()
    {
        for (int i = GridContainer.transform.childCount - 1; i > -1; i--)
        {
            Destroy(GridContainer.transform.GetChild(i).gameObject);
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
        newSlot.GetComponent<ItemWidget>().SetData(item, context, this.DefaultItemAction);//Building items with context
    }

    public void SetData(object o)
    {
        this.context = (HeroModel)o;
        print($"Got new context = {context.Name}");
        for (int i = 0; i < 10; i++)
        {
            this.BuildItem();
        }
    }

    public void Rebuild()
    {

    }
}
