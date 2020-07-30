using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Abilities;
using Utils;

public class SkillWidget : MonoBehaviour, IUIWidget, IPointerDownHandler, IPointerUpHandler
{
    public Image Icon;
    public SkillDetailsWidget SkillDetailsWidget;
    public float HoldTreshold;//Point at which click becomes hold
    //
    public delegate void ClickHandler();
    public ClickHandler OnClick;
    public ClickHandler OnHeld;
    public Skill Skill { get => this.skill; }
    private Skill skill;
    private Utils.Input inputHandler;

    public void Rebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.skill.skillObject.Icon;
        }
    }

    public void SetData(object data)
    {
        this.skill = (Skill)data;
        this.Rebuild();
    }

    public string GetName()
    {
        return this.skill.GetName();
    }

    private void OpenSkillDetails()
    {
        Instantiate(this.SkillDetailsWidget, GameObject.Find("/Canvas").transform).GetComponent<SkillDetailsWidget>().SetData(this.skill);
    }

    private void Clicked()
    {
        Debug.Log("Clicked");
        this.OnClick?.Invoke();
    }
    private void Held()
    {
        Debug.Log("Held");
        this.OnHeld?.Invoke();
    }

    private void Awake()
    {
        this.inputHandler = new Utils.Input(this.HoldTreshold, this.Clicked, this.Held);
    }
    private void Update()
    {
        this.inputHandler.Tick(Time.deltaTime);
    }

    private bool isClicked = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        this.inputHandler.isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.inputHandler.isClicked = false;
    }
}
