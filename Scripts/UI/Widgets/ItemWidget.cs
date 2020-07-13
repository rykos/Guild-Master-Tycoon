using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public GameObject ItemDetailsPrefab;
    public Sprite DefaultIcon;
    public Image Icon;
    public TextMeshProUGUI Description;
    public List<ItemActionType> ItemActionsType;
    //
    private Item item;
    private object context;


    public void SetData(object o)
    {
        this.item = (Item)o;
        this.Rebuild();
    }
    public void SetData(Item item, object context, List<ItemActionType> actionsType = default)
    {
        if (actionsType != default)
        {
            this.ItemActionsType = actionsType;
        }
        if (context != null)
        {
            this.context = context;
        }
        this.item = item;
        this.Rebuild();
    }

    public void Rebuild()
    {
        if (this.item == default)
        {
            this.CleanRebuild();
            return;
        }
        if (this.Icon != null)
        {
            this.Icon.sprite = this.item.Image;
        }
        if (this.Description != null)
        {
            this.Description.text = $"Name: {item.Name}";
        }
    }
    private void CleanRebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.DefaultIcon;
        }
        if (this.Description != null)
        {
            this.Description.text = $"Name: {item.Name}";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.OpenItemDetails();
    }

    private void OpenItemDetails()
    {
        if (this.item == default)
        {
            return;
        }
        if (this.context == null)
        {
            context = gameObject.GetComponentInParent<IContext>()?.GetContext();
        }
        Instantiate(this.ItemDetailsPrefab, GameObject.Find("/Canvas").transform).
            GetComponent<ItemDetailsWidget>().SetData(this.item, this.context, this.ItemActionsType);
    }
}

public enum ItemActionType
{
    None,
    Equip,
    Unequip,
    Sell,
    Upgrade,
    Destroy
}