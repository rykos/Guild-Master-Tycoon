using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroViewStatsController : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Name;
    public LevelWidgetController LevelWidget;
    public StatsWidgetController StatsWidgetController;
    //
    private HeroModel hero;
    public void Build(HeroModel hero)
    {
        this.hero = hero;
        this.Name.text = hero.Name;
        this.LevelWidget.SetHero(hero);
        this.StatsWidgetController.Hero = hero;
    }
}
