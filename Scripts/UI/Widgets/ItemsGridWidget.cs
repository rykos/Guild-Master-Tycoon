using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGridWidget : MonoBehaviour, IUIWidget
{
    public GameObject GridContainer;
    public GameObject ItemSlotPrefab;
    public ItemActionType DefaultItemAction;
    private HeroModel context;
    private ItemBag playerBag;

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

    public void SetData(object o)
    {
        this.context = (HeroModel)o;
        print($"Got new context = {context.Name}");
        playerBag = PlayerManager.Instance.PlayerModel.ItemBag;
        this.Rebuild();
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
        print("Rebuild Called");
        DestroyItems();
        foreach (Item item in playerBag.Items)
        {
            BuildItem(item);
        }
    }
}
