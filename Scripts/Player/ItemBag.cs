using System.Collections.Generic;

public class ItemBag
{
    public List<Item> Items;

    public ItemBag()
    {
        this.Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        this.Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        this.Items.Remove(item);
    }
}
