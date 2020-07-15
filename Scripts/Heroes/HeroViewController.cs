using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//Main view HeroView
public class HeroViewController : MonoBehaviour
{
    public static HeroViewController heroViewController;
    public GameObject HeroView;//Owned Hero View
    public GameObject HeroViewShop;//Hero available in shop View
    public HeroesList heroesList;

    private void Awake()
    {
        HeroViewController.heroViewController = this;
    }
    private void OnEnable()
    {
        heroesList.SetData(PlayerManager.Instance.PlayerModel.Heroes);
    }

    public void EnableSubView(HeroModel heroModel, bool shopEnv = false)
    {
        GameObject view;
        if (shopEnv)
        {
            view = this.HeroViewShop;//Show version
        }
        else
        {
            view = this.HeroView;//Standard version
        }
        view.GetComponent<HeroViewManager>().Hero = heroModel;//Provide data model
        view.SetActive(true);//Enable view
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
