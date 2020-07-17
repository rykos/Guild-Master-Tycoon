using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroTile : MonoBehaviour, IPointerClickHandler
{
    public bool ShopEnv;
    public Image IconImage;
    public TextMeshProUGUI NameTMP;
    public LevelWidgetController levelWidgetController;
    //
    public HeroModel Hero
    {
        get { return this.hero; }
        set
        {
            this.hero = value;
            RebuildTile();
        }
    }
    private HeroModel hero;
    private HeroViewController heroViewController;

    public void RebuildTile()
    {
        NameTMP.text = this.hero.Name;
        levelWidgetController.SetHero(hero);
        IconImage.sprite = this.hero.Avatar;
    }

    /// <summary>
    /// Open SubView
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.ShopEnv)
        {
            HeroViewController.heroViewController.EnableSubView(this.hero, true);
        }
        else
        {
            HeroViewController.heroViewController.EnableSubView(this.hero);
        }
    }
}
