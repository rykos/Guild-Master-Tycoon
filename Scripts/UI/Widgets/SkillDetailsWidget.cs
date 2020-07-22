using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Abilities;

public class SkillDetailsWidget : MonoBehaviour, IPointerClickHandler, IUIWidget
{
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    //
    private Skill skill;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void Rebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.skill.skillObject.Icon;
        }
        if (this.Name != null)
        {
            this.Name.text = this.skill.skillObject.Name;
        }
        if (this.Description != null)
        {
            this.Description.text = this.skill.GetDescription();
        }
    }

    public void SetData(object data)
    {
        this.skill = (Skill)data;
        this.Rebuild();
    }

    public void IncreaseButtonClicked()
    {
        this.skill.LevelUp();
        this.Rebuild();
    }
}
