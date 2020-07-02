using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesList : MonoBehaviour
{
    public GameObject HeroTilePrefab;
    public GameObject HeroListContent;

    private void Start()
    {
        Stats[] eqStats = new Stats[] { new Stats(10, 0, 0), new Stats(0, 10, 0), new Stats(0, 0, 10), new Stats(5, 5, 5) };
        HeroModel hero = new HeroModel() { Name = "Janusz", Level = new Level(5) { Exp = 79}, Stats = new Stats(eqStats) };
        HeroModel hero2 = new HeroModel() { Name = "Mariusz", Level = new Level(2) { Exp = 150}, Stats = new Stats(2,5,6) };
        var newTile = Instantiate(HeroTilePrefab, HeroListContent.transform);
        newTile.GetComponent<HeroTile>().Hero = hero;
        newTile.GetComponent<HeroesList>();
        var newTile2 = Instantiate(HeroTilePrefab, HeroListContent.transform);
        newTile2.GetComponent<HeroTile>().Hero = hero2;
    }
}
