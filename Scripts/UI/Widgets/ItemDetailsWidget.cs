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
    //
    private Item item;

    public void SetData(Item item)
    {
        this.item = item;
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.CloseItemDetails();
    }

    private void CloseItemDetails()
    {
        Destroy(this.gameObject);
    }
}
