using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsWidgetController : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI Attack, Defense, Health;//Editor bound
    public HeroModel Hero
    {
        set
        {
            this.hero = value;
            this.Rebuild();
        }
    }
    private HeroModel hero;

    public void Rebuild()
    {
        if (Attack != null)
        {
            Attack.text = hero.Stats.Attack.ToString();
        }
        if (Defense != null)
        {
            Defense.text = hero.Stats.Defense.ToString();
        }
        if (Health != null)
        {
            Health.text = hero.Stats.Health.ToString();
        }
    }
}

public interface IUIWidget
{
    void Rebuild();
}