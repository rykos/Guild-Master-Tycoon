﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public Image Icon;
    public TextMeshProUGUI Description;
    //
    private Item item;

    public void SetData(Item item)
    {
        this.item = item;
        this.Rebuild();
    }

    public void Rebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.item.Image;
        }
        if (this.Description != null)
        {
            this.Description.text = $"Name: {item.Name}";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print($"Item \"{this.item.Name} {this.item.Image.GetSpriteID()}\" click event");
    }
}
