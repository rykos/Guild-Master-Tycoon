using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonListElementWidget : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI Name;
    //
    private DungeonModel dungeon;

    public void Rebuild()
    {
        if (this.Name != null)
        {
            this.Name.text = this.dungeon.Name;
        }
    }

    public void SetData(object data)
    {
        this.dungeon = data as DungeonModel;
        this.Rebuild();
    }
}
