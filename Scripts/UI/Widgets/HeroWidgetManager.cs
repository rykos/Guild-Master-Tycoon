﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroWidgetManager : MonoBehaviour, IUIWidget
{
    public Image HeroAvatar;
    public SliderWidget HeroHealthBar;
    //
    private Entity entity;

    public void Rebuild()
    {
        if (this.HeroAvatar != null)
        {
            this.HeroAvatar.sprite = this.entity.Avatar;
        }
        if (this.HeroHealthBar != null)
        {
            this.HeroHealthBar.SetData(this.entity.GetHealthPercentage());
        }
    }

    /// <param name="data">Entity</param>
    public void SetData(object data)
    {
        this.entity = (Entity)data;
        this.Rebuild();
    }
}
