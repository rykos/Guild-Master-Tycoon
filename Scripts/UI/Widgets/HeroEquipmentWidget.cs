﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEquipmentWidget : MonoBehaviour, IUIWidget
{
    public ItemWidget Head, Chest, Legs;
    //
    private Equipment equipment;

    private void Start()
    {
        //PlayerManager.Instance.PlayerModel.ItemsChangedEvent += Rebuild;
    }
    private void OnDisable()
    {
        this.equipment.ItemsChangedEvent -= this.Rebuild;
    }

    public void SetData(object eq)
    {
        this.equipment = ((HeroModel)eq).Equipment;
        this.equipment.ItemsChangedEvent += this.Rebuild;
        this.Rebuild();
    }

    public void Rebuild()
    {
        if (this.equipment == null)
        {
            print("Equipment == null");
            return;
        }
        print($"Hero has {this.equipment.Items.Count} items");
        this.RebuildItemWidget(this.Head, this.equipment.GetItemOfType(ItemType.Head));
        this.RebuildItemWidget(this.Chest, this.equipment.GetItemOfType(ItemType.Chest));
        this.RebuildItemWidget(this.Legs, this.equipment.GetItemOfType(ItemType.Legs));
    }

    private void RebuildItemWidget(ItemWidget itemWidget, Item item)
    {
        itemWidget.SetData(item);
    }
}
