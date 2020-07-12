using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGridWidget : MonoBehaviour, IUIWidget
{
    public GameObject GridContainer;
    public GameObject ItemSlotPrefab;
    public ItemActionType DefaultItemAction;//Default item action
    private HeroModel context;//Interaction context
    private ItemBag playerBag;
    private List<Item> Items;

    private void OnEnable()
    {
        //Context does not exist yet
        PlayerManager.Instance.PlayerModel.ItemBagChangedEvent += Rebuild;
    }
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerModel.ItemBagChangedEvent -= Rebuild;
    }

    private void BuildItem(Item item)
    {
        GameObject newSlot = Instantiate(ItemSlotPrefab, GridContainer.transform);
        newSlot.GetComponent<ItemWidget>().SetData(item, context, this.DefaultItemAction);//Building items with context
    }


    /// <param name="o">List of items/HeroModel Context</param>
    public void SetData(object o)
    {
        if (o.GetType() == typeof(HeroModel))
        {
            this.context = (HeroModel)o;
            playerBag = PlayerManager.Instance.PlayerModel.ItemBag;
            this.Items = playerBag.Items;
            this.Rebuild();
        }
        else
        {
            this.context = null;
            this.Items = o as List<Item>;
            this.Rebuild();
        }
    }

    private void DestroyItems()
    {
        for (int i = GridContainer.transform.childCount - 1; i > -1; i--)
        {
            Destroy(GridContainer.transform.GetChild(i).gameObject);
        }
    }

    public void Rebuild()
    {
        DestroyItems();
        foreach (Item item in this.Items)
        {
            BuildItem(item);
        }
    }
}
