using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroTile : MonoBehaviour, IPointerClickHandler
{
    public Image IconImage;
    public TextMeshProUGUI NameTMP;
    public GameObject Level;
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
    }

    /// <summary>
    /// Open SubView
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        HeroViewController.heroViewController.EnableSubView(this.hero);
    }
}
