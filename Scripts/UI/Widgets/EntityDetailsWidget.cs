using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EntityDetailsWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public Image Avatar;
    public TextMeshProUGUI Text;
    private Entity entity;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void Rebuild()
    {
        if (this.Text != null)
        {
            this.Text.text = $"{this.entity.Name} HP:{this.entity.MaxHealth}";
        }
        if (this.Avatar != null)
        {
            this.Avatar.sprite = this.entity.Avatar;
        }
    }

    public void SetData(object data)
    {
        this.entity = (Entity)data;
        this.Rebuild();
    }
}