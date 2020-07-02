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
        this.currentView = view;
    }
    public void DisableView(GameObject view)
    {
        view.SetActive(false);
        this.currentView = null;
    }

    private void Rebuild()
    {
        this.statsController.Build(hero);
    }
}
