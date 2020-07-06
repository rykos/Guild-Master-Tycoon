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
        Stats stats = hero.GetStats();
        if (Attack != null)
        {
            Attack.text = stats.Attack.ToString();
        }
        if (Defense != null)
        {
            Defense.text = stats.Defense.ToString();
        }
        if (Health != null)
        {
            Health.text = stats.Health.ToString();
        }
    }

    public void SetData(object o) { }
}

public interface IUIWidget
{
    void Rebuild();
    void SetData(object data);
}