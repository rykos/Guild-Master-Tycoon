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

    /// <summary>
    /// TO_DO! Rebuild only delta
    /// </summary>
    public void Rebuild()
    {
        this.DestroyChildren();
        foreach (HeroModel hero in this.heroes)
        {
            this.BuildTile(hero);
        }
    }
    public void Rebuild(List<HeroModel> heroes)
    {
        this.heroes = heroes.ToArray();
        this.Rebuild();
    }
    public void SetData(object o) { }

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

    private void BuildTile(HeroModel hero)
    {
        GameObject newTile = Instantiate(this.HeroTilePrefab, HeroListContent.transform);
        newTile.GetComponent<HeroTile>().Hero = hero;
    }
}
