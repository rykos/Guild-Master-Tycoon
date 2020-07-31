using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utils;

public class EntityWidgetManager : MonoBehaviour, IUIWidget, IPointerDownHandler, IPointerUpHandler
{
    public float HeldTreshold;
    public Color SelectedBorderTint;
    public Color ActiveBorderTint;
    public Image Avatar;
    public Image Border;
    public SliderWidget HealthBar;
    //
    public delegate void ClickHandler();
    public ClickHandler OnClick;
    public ClickHandler OnHeld;
    public Entity GetEntity { get => this.entity; }
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
    public bool IsActive
    {
        get => isActive;
        set
        {
            this.isActive = value;
            this.Rebuild();
        }
    }
    private bool isActive;
    private Utils.Input input;

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
        this.entity = (Entity)data;
        this.Rebuild();
    }

    private void Clicked()
    {
        this.OnClick?.Invoke();
    }

    private void Held()
    {
        this.OnHeld?.Invoke();
    }

    private void Awake()
    {
        this.input = new Utils.Input(this.HeldTreshold, this.Clicked, this.Held);
    }

    private void Update()
    {
        this.input.Tick(Time.deltaTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.input.isClicked = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.input.isClicked = true;
    }
}
