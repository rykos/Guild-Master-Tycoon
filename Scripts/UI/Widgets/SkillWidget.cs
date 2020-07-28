using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Abilities;

public class SkillWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public Image Icon;
    public SkillDetailsWidget SkillDetailsWidget;
    //
    public delegate void ClickHandler();
    public ClickHandler OnClick;
    public Skill Skill { get => this.skill; }
    private Skill skill;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.OnClick == null)//By default point to skill details
        {
            this.OpenSkillDetails();
        }
        this.OnClick.Invoke();
    }

    public string GetName()
    {
        return this.skill.GetName();
    }

    private void OpenSkillDetails()
    {
        Instantiate(this.SkillDetailsWidget, GameObject.Find("/Canvas").transform).GetComponent<SkillDetailsWidget>().SetData(this.skill);
    }
}
