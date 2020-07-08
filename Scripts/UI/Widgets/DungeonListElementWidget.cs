using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonListElementWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public GameObject DungeonPreview;
    //
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Rarity;
    public TextMeshProUGUI Level;
    //
    private DungeonModel dungeon;//Dungeon being represented as this widget

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponentInParent<ListWidget>().ActivateElement(this.dungeon);
    }

    public void Rebuild()
    {
        if (this.Name != null)
        {
            this.Name.text = this.dungeon.Name;
        }
        if (this.Rarity != null)
        {
            this.Rarity.text = this.dungeon.Rarity.ToString();
        }
        if (this.Level != null)
        {
            this.Level.text = this.dungeon.Level.Lvl.ToString();
        }
    }

    public void SetData(object data)
    {
        this.dungeon = data as DungeonModel;
        this.Rebuild();
    }
}
