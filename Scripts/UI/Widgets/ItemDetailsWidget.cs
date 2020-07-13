using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDetailsWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI ActionButtonText;
    public GameObject ActionButton;
    //
    private Item item;
    private ItemActionType itemActionType;
    private object context;

    public void SetData(object o) { }
    public void SetData(Item item, object context, ItemActionType itemActionType)
    {
        this.item = item;
        this.context = context;
        this.itemActionType = itemActionType;
        Rebuild();
    }

    public void Rebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.item.Image;
        }
        if (this.Name != null)
        {
            this.Name.text = this.item.Name;
        }
        if (this.Description != null)
        {
            Stats stats = this.item.Stats;
            this.Description.text = $"AP: {stats.Attack}\nDef: {stats.Defense}\nHP: {stats.Health}";
        }
        if (this.ActionButtonText != null)
        {
            this.RebuildActionButton();
        }
    }

    private void RebuildActionButton()
    {
        switch (this.itemActionType)
        {
            case ItemActionType.Equip:
                this.ActionButtonText.text = "Equip";
                break;
            case ItemActionType.Sell:
                this.ActionButtonText.text = "Sell";
                break;
            case ItemActionType.Upgrade:
                this.ActionButtonText.text = "Upgrade";
                break;
            case ItemActionType.Destroy:
                this.ActionButtonText.text = "Destroy";
                break;
            case ItemActionType.Unequip:
                this.ActionButtonText.text = "Unequip";
                break;
            case ItemActionType.None://If there is no interaction hide button
                this.ActionButton.SetActive(false);
                return;
        }
        this.ActionButton.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.CloseItemDetails();
    }

    private void CloseItemDetails()
    {
        Destroy(this.gameObject);
    }

    public void ActionButtonClicked()
    {
        print("ActionButtonClicked()");
        //
        if (this.itemActionType == ItemActionType.Equip)
        {
            ((HeroModel)context).EquipItem(this.item);   
        }
        else if (this.itemActionType == ItemActionType.Unequip)
        {
            ((HeroModel)context).UnequipItem(this.item);
        }
        else if (this.itemActionType == ItemActionType.Sell)
        {

        }
        else if (this.itemActionType == ItemActionType.Destroy)
        {

        }
        else if (this.itemActionType == ItemActionType.Upgrade)
        {

        }
        //
        this.CloseItemDetails();
    }
}
