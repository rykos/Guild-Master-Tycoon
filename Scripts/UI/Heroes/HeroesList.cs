using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesList : MonoBehaviour
{
    public GameObject HeroTilePrefab;
    public GameObject HeroListContent;

    private void Start()
    {
        HeroModel hero = new HeroModel() { Name = "Janusz", Level = new Level(5)};
        var newTile = Instantiate(HeroTilePrefab, HeroListContent.transform);
        newTile.GetComponent<HeroTile>().Hero = hero;
    }
}
