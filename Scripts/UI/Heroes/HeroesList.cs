using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesList : MonoBehaviour, IUIWidget
{
    public GameObject HeroTilePrefab;
    public GameObject HeroListContent;
    private HeroModel[] heroes;//Currently displayed heroes

    public void SetData(HeroModel[] heroes)
    {
        this.heroes = heroes;
        this.Rebuild();
    }

    public void Rebuild()
    {
        this.DestroyChildren();
        foreach (HeroModel hero in this.heroes)
        {
            this.BuildTile(hero);
        }
    }

    private void DestroyChildren(Transform[] children = null)
    {
        if (children == null)
        {
            for (int i = HeroListContent.transform.childCount; i > 0; i--)
            {
                Destroy(HeroListContent.transform.GetChild(i - 1).gameObject);
            }
        }
    }

    private void Start()
    {
        //Stats[] eqStats = new Stats[] { new Stats(10, 0, 0), new Stats(0, 10, 0), new Stats(0, 0, 10), new Stats(5, 5, 5) };
        //HeroModel hero = new HeroModel() { Name = "Janusz", Level = new Level(5) { Exp = 79}, Stats = new Stats(eqStats) };
        //HeroModel hero2 = new HeroModel() { Name = "Mariusz", Level = new Level(2) { Exp = 150}, Stats = new Stats(2,5,6) };
        //var newTile = Instantiate(HeroTilePrefab, HeroListContent.transform);
        //newTile.GetComponent<HeroTile>().Hero = hero;
        //newTile.GetComponent<HeroesList>();
        //var newTile2 = Instantiate(HeroTilePrefab, HeroListContent.transform);
        //newTile2.GetComponent<HeroTile>().Hero = hero2;
    }

    private void BuildTile(HeroModel hero)
    {
        GameObject newTile = Instantiate(this.HeroTilePrefab, HeroListContent.transform);
        newTile.GetComponent<HeroTile>().Hero = hero;
    }
}
