using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroViewStatsController : MonoBehaviour, IUIWidget
{
    public Image Image;
    public TextMeshProUGUI Name;
    public LevelWidgetController LevelWidget;
    public StatsWidgetController StatsWidgetController;
    //
    private HeroModel hero;

    public void Rebuild()
    {
        this.Name.text = hero.Name;
        this.LevelWidget.SetHero(hero);
        this.StatsWidgetController.Hero = hero;
    }

    public void SetData(object hero)
    {
        this.hero = (HeroModel)hero;
        this.Rebuild();
    }
}
