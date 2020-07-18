using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillDetailsWidget : MonoBehaviour, IPointerClickHandler, IUIWidget
{
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    //
    private SkillObject skillObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void Rebuild()
    {
        if (this.Icon != null)
        {
            this.Icon.sprite = this.skillObject.Icon;
        }
        if (this.Name != null)
        {
            this.Name.text = this.skillObject.Name;
        }
        if (this.Description != null)
        {
            this.Description.text = string.Format(this.skillObject.Description, "999");
        }
    }

    public void SetData(object data)
    {
        this.skillObject = (SkillObject)data;
        this.Rebuild();
    }
}
