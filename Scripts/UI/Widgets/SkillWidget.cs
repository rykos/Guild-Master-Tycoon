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
        Instantiate(this.SkillDetailsWidget, GameObject.Find("/Canvas").transform).GetComponent<SkillDetailsWidget>().SetData(this.skill);
    }
}
