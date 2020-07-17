using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroWidgetManager : MonoBehaviour, IUIWidget
{
    public Image HeroAvatar;
    public SliderWidget HeroHealthBar;
    //
    private EntityState entityState;

    public void Rebuild()
    {
        if (this.HeroAvatar != null)
        {
            this.HeroAvatar.sprite = this.entityState.Entity.Avatar;
        }
        if (this.HeroHealthBar != null)
        {
            this.HeroHealthBar.SetData(this.entityState.HealthPercentage);
        }
    }

    /// <param name="data">Entity</param>
    public void SetData(object data)
    {
        if (data.GetType() == typeof(HeroModel) || data.GetType() == typeof(MonsterModel))
        {
            this.entityState = new EntityState(((Entity)data));
        }
        else
        {
            this.entityState = (EntityState)data;
        }
        this.Rebuild();
    }
}
