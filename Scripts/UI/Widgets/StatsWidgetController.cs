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
            Attack.text = $"Attack: {stats.Attack}";
        }
        if (Defense != null)
        {
            Defense.text = $"Defense {stats.Defense}";
        }
        if (Health != null)
        {
            Health.text = $"Health: {stats.Health}";
        }
    }

    public void SetData(object o) 
    {
        this.hero = (HeroModel)o;
    }
}

public interface IUIWidget
{
    void Rebuild();
    void SetData(object data);
}