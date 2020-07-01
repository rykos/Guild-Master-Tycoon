using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject CraftingView;
    public GameObject DungeonsView;
    public GameObject HeroesView;
    //
    private GameObject currentView;

    private void Awake()
    {
        this.SetView(HeroesView);
    }

    public void SetView(GameObject view)
    {
        if (view == null)
        {
            throw new System.Exception("Passed view GameObject is null");
        }
        if (view == currentView)//Reentering same view
        {
            return;
        }
        if (currentView != null)
        {
            DisableView(currentView);
        }
        EnableView(view);
    }

    private void DisableView(GameObject view)
    {
        if (this.currentView == view)
            currentView = null;
        view.SetActive(false);
    }
    private void EnableView(GameObject view)
    {
        this.currentView = view;
        view.SetActive(true);
    }
}
