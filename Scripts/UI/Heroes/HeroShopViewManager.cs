using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShopViewManager : MonoBehaviour, IUIWidget
{
    public HeroesList heroesList;

    private void OnEnable()
    {
        Rebuild();
        PlayerManager.Instance.ShopManagerChangedEvent += Rebuild;
    }
    private void OnDisable()
    {
        PlayerManager.Instance.ShopManagerChangedEvent -= Rebuild;
    }

    public void Rebuild()
    {
        heroesList.SetData(PlayerManager.Instance.ShopManager.Heroes.ToArray());
    }

    public void SetData(object data)
    {
        //this.heroesList = (HeroesList)data;
    }
}
