using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HeroViewController : MonoBehaviour
{
    public static HeroViewController heroViewController;
    public GameObject HeroView;

    private void Awake()
    {
        HeroViewController.heroViewController = this;
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
