using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles HeroView, Displays data using HeroModel
/// </summary>
public class HeroViewManager : MonoBehaviour
{
    public HeroViewStatsController statsController;
    public HeroViewEquipmentController equipmentController;
    public HeroViewSkillsController skillsController;
    //
    public HeroModel Hero
    {
        get { return this.hero; }
        set
        {
            this.hero = value;
            Rebuild();
        }
    }
    private HeroModel hero;
    //
    private GameObject currentView;

    private void OnEnable()
    {
        EnableView(statsController.gameObject);
    }
    private void OnDisable()
    {
        DisableView(currentView);
    }

    public void EnableView(GameObject view)
    {
        if (currentView != null)
        {
            DisableView(currentView);
        }
        view.SetActive(true);
        view.GetComponent<IUIWidget>()?.SetData(hero);
        this.currentView = view;
    }
    public void DisableView(GameObject view)
    {
        view.SetActive(false);
        this.currentView = null;
    }

    /// <summary>User clicked buy button on hero</summary>
    public void BuyButtonClicked(GameObject view)
    {
        BuyHero(view);
    }

    public void BuyHero(GameObject view)
    {
        ReturnState rs = PlayerManager.Instance.BuyHero(this.hero);
        if (rs == ReturnState.Success)
        {
            DisableView(view);
        }
        else
        {
            print(rs);
        }
    }

    private void Rebuild()
    {
        this.statsController.SetData(hero);
    }
}
