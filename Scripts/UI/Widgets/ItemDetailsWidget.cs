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
    private List<ItemActionType> itemActionType = new List<ItemActionType>();
    private object context;

    public void SetData(object o) { throw new System.NotImplementedException(); }
    public void SetData(Item item, object context, List<ItemActionType> itemActionsType)
    {
        this.item = item;
        this.context = context;
        this.itemActionType.AddRange(itemActionsType);
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
            this.Description.text = BuildDescriptionText(stats);
        }
        if (this.ActionButtonText != null)
        {
            this.RebuildActionButton();
        }
    }

    private string BuildDescriptionText(Stats stats)
    {
        string text = $"Attack Power: {stats.Attack}\nDefense: {stats.Defense}\nHealth: {stats.Health}";
        return text;
    }

    private void RebuildActionButton()
    {
        foreach (ItemActionType iat in this.itemActionType)
        {
            if (iat == ItemActionType.Equip)
            {
                this.ActionButtonText.text = "Equip";
            }
            else if (iat == ItemActionType.Unequip)
            {
                this.ActionButtonText.text = "Unequip";
            }
            else if (iat == ItemActionType.Sell)
            {
                this.ActionButtonText.text = "Sell";
            }
            else if (iat == ItemActionType.Upgrade)
            {
                this.ActionButtonText.text = "Upgrade";
            }
            else if (iat == ItemActionType.Destroy)
            {
                this.ActionButtonText.text = "Destroy";
            }
            else if (iat == ItemActionType.None)
            {
                this.ActionButton.SetActive(false);
                return;
            }
        }
        this.ActionButton.SetActive(true);
    }

    private void ExecuteActionType(ItemActionType iat)
    {
        if (iat == ItemActionType.Equip)
        {
            ((HeroModel)context).EquipItem(this.item);
        }
        else if (iat == ItemActionType.Unequip)
        {
            ((HeroModel)context).UnequipItem(this.item);
        }
        else if (iat == ItemActionType.Sell)
        {
            print($"Sell");
        }
        else if (iat == ItemActionType.Upgrade)
        {
            print($"Upgrade");
        }
        else if (iat == ItemActionType.Destroy)
        {
            print($"Destroy");
        }
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
        foreach (ItemActionType iat in this.itemActionType)
        {
            ExecuteActionType(iat);
        }
        this.CloseItemDetails();
    }
}
