using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EntityWidgetManager : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public Image Avatar;
    public SliderWidget HealthBar;
    //
    public delegate void ClickHandler();
    public ClickHandler OnClick;
    private EntityState entityState;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void Rebuild()
    {
        if (this.Avatar != null)
        {
            this.Avatar.sprite = this.entityState.Entity.Avatar;
        }
        if (this.HealthBar != null)
        {
            this.HealthBar.SetData(this.entityState.HealthPercentage);
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
