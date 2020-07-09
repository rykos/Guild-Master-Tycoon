using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedHeroesWidget : MonoBehaviour, IUIWidget
{
    public GameObject HeroFramePrefab;
    //
    private List<HeroModel> heroes = new List<HeroModel>();

    public void Rebuild()
    {
        for (int i = transform.childCount - 1; i > -1; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (HeroModel hero in this.heroes)
        {
            Instantiate(this.HeroFramePrefab, transform);
        }
    }

    public void SetData(object data)
    {
        this.heroes = data as List<HeroModel>;
        this.Rebuild();
    }
}
