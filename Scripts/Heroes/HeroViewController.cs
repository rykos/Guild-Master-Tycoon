using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//Main view HeroView
public class HeroViewController : MonoBehaviour
{
    public static HeroViewController heroViewController;
    public GameObject HeroView;
    public HeroesList heroesList;

    private void Awake()
    {
        HeroViewController.heroViewController = this;
    }
    private void OnEnable()
    {
        heroesList.SetData(new HeroModel[] { new HeroModel() { Name = "Adam", Level = new Level(5), BaseStats = new Stats(5, 6, 7) } });
    }
    private void OnDisable()
    {
        
    }

    public void EnableSubView(HeroModel heroModel)
    {
        this.HeroView.SetActive(true);
        HeroView.GetComponent<HeroViewManager>().Hero = heroModel;
    }

    public void EnableSubView(GameObject subView)
    {
        subView.SetActive(true);
    }

    public void DisableSubView(GameObject subView)
    {
        subView.SetActive(false);
    }
}
