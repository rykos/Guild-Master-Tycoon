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

    public void SetData(Item item, ItemActionType itemActionType)
    {
        this.item = item;
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
        //PlayerManager.Instance.
        //
        this.CloseItemDetails();
    }
}
