using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedHeroFrameWidget : MonoBehaviour, IUIWidget
{
    public Image HeroIcon;
    //
    private HeroModel hero;

    public void Rebuild()
    {
        if (this.HeroIcon != null)
        {
            this.HeroIcon.sprite = hero.Avatar;
        }
    }

    /// <param name="data">Heromodel</param>
    public void SetData(object data)
    {
        this.hero = (HeroModel)data;
        this.Rebuild();
    }
}

