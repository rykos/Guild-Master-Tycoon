using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles HeroView, Displays data using HeroModel
/// </summary>
public class HeroViewManager : MonoBehaviour
{
    public HeroViewStatsController statsController;
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

    private void Rebuild()
    {
        this.statsController.Build(hero);
    }
}
