using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEquipmentWidget : MonoBehaviour, IUIWidget
{
    public ItemWidget Head, Chest, Legs;
    //
    private Equipment equipment;

    private void Start()
    {
        Equipment eq = new Equipment();
        eq.EquipItem(NewItem(ItemType.Head));
        eq.EquipItem(NewItem(ItemType.Chest));
        eq.EquipItem(NewItem(ItemType.Legs));
        this.SetData(eq);
    }

    private Item NewItem(ItemType type = ItemType.None)
    {
        ItemType itemType = (type == ItemType.None) ? (ItemType)Random.Range(1, System.Enum.GetNames(typeof(ItemType)).Length) : type;
        Item item = new Item("Embeded Something", itemType, new Stats(10, 5, 15), 10, 15)
        {
            Image = AssetManager.Instance.RandomIcon(itemType)
        };
        return item;
    }

    public void SetData(Equipment eq)
    {
        this.equipment = eq;
        this.Rebuild();
    }

    public void Rebuild()
    {
        this.Head.SetData(equipment.GetItemOfType(ItemType.Head));
        this.Chest.SetData(equipment.GetItemOfType(ItemType.Chest));
        this.Legs.SetData(equipment.GetItemOfType(ItemType.Legs));
    }
}
