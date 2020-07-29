using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EntityWidgetManager : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public Color SelectedBorderTint;
    public Color ActiveBorderTint;
    public Image Avatar;
    public Image Border;
    public SliderWidget HealthBar;
    //
    public delegate void ClickHandler();
    public ClickHandler OnClick;
    public bool IsHighlighted
    {
        get => this.isHighlighted;
        set
        {
            this.isHighlighted = value;
            this.Rebuild();
        }
    }
    private bool isHighlighted = false;
    public bool IsActive { get => isActive; set => isActive = value; }
    private bool isActive;
    public Entity GetEntity { get => this.entity; }

    private Entity entity;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void Rebuild()
    {
        if (this.Avatar != null)
        {
            this.Avatar.sprite = this.entity.Avatar;
        }
        if (this.HealthBar != null)
        {
            this.HealthBar.SetData(this.entity.GetHealthPercentage());
        }
        if (IsActive)
        {
            this.Border.color = this.ActiveBorderTint;
        }
        else if (isHighlighted)
        {
            this.Border.color = this.SelectedBorderTint;
        }
        else//Default white tint
        {
            this.Border.color = new Color(1, 1, 1, 1);
        }
        Debug.Log($"Rebuilded {entity.Name}, isHighlighted={isHighlighted}");
    }

    /// <param name="data">Entity</param>
    public void SetData(object data)
    {
        if (data.GetType() == typeof(HeroModel) || data.GetType() == typeof(MonsterModel) || data.GetType() == typeof(Entity))
        {
            this.entity = (Entity)data;
        }
        else
        {
            throw new System.Exception();
        }
        this.Rebuild();
    }
}
