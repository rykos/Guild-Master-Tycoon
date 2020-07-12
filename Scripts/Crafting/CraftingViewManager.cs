using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingViewManager : MonoBehaviour
{
    public ItemsGridWidget ItemsGridWidget;//Player Items

    private void OnEnable()
    {
        this.Rebuild();
        PlayerManager.Instance.PlayerModel.ItemBagChangedEvent += Rebuild;//Attach listener
    }
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerModel.ItemBagChangedEvent -= Rebuild;//Detach
    }

    private void Rebuild()
    {
        ItemsGridWidget.SetData(PlayerManager.Instance.PlayerModel.ItemBag.Items);//Rebuild Player Inventory
    }
}
