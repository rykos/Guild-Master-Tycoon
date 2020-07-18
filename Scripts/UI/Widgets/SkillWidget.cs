using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public SkillObject Skill;
    public Image Icon;
    public SkillDetailsWidget SkillDetailsWidget;
    //
    private SkillObject skill;


    private void Start()
    {
        this.SetData(this.Skill);
    }

    public void Rebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.skill.Icon;
        }
    }

    public void SetData(object data)
    {
        this.skill = (SkillObject)AssetManager.Instance.Skills[Random.Range(0, AssetManager.Instance.Skills.Length)];
        this.Rebuild();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(this.SkillDetailsWidget, GameObject.Find("/Canvas").transform).GetComponent<SkillDetailsWidget>().SetData(this.skill);
    }
}
