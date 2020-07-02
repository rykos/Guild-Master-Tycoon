using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShopViewManager : MonoBehaviour
{
    public HeroesList heroesList;

    private void OnEnable()
    {
        heroesList.SetData(new HeroModel[] { new HeroModel() { Name = "Kupny Heros", Level = new Level(25), Stats = new Stats(50, 29, 60) } });
    }
    private void OnDisable()
    {

    }
}
