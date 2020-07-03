using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShopViewManager : MonoBehaviour
{
    public HeroesList heroesList;

    private void OnEnable()
    {
        HeroModel hero = new HeroModel() { Name = "Kupny Heros", Level = new Level(25) { Exp = 25 * 80 }, BaseStats = new Stats(1, 1, 1) };
        hero.Equipment = new Equipment();
        hero.EquipItem(Item.Build(Name: "Boots", ItemType.Legs, new Stats(20, 10, 5), 1, 15));
        hero.EquipItem(Item.Build(Name: "Chest", ItemType.Chest, new Stats(40, 10, 5), 1, 15));
        hero.EquipItem(Item.Build(Name: "Chest", ItemType.Chest, new Stats(41, 10, 5), 1, 15));
        heroesList.SetData(new HeroModel[] { hero });
    }
    private void OnDisable()
    {

    }
}
