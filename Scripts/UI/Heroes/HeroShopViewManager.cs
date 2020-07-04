using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShopViewManager : MonoBehaviour
{
    public HeroesList heroesList;

    private void OnEnable()
    {
        heroesList.SetData(PlayerManager.Instance.ShopManager.Heroes.ToArray());
    }
    private void OnDisable()
    {

    }
}
