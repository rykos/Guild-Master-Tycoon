using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonResultPage : MonoBehaviour, IUIWidget
{
    private DungeonModel dungeon;

    public void Rebuild()
    {
        throw new System.NotImplementedException();
    }

    public void SetData(object data)
    {
        this.dungeon = data as DungeonModel;
        print(this.dungeon.Name);
    }
}
