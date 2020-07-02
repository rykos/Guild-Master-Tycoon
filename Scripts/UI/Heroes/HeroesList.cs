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
        HeroModel hero = new HeroModel() { Name = "Janusz", Level = new Level(5), Stats = new Stats(eqStats) };
        var newTile = Instantiate(HeroTilePrefab, HeroListContent.transform);
        newTile.GetComponent<HeroTile>().Hero = hero;
    }
}
