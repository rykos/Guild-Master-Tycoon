using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dungeon selected, select heroes and go
/// </summary>
public class DungeonView : MonoBehaviour, IUIWidget
{
    private DungeonModel dungeon;

    public void Rebuild()
    {
        print($"Rebuild with dungeon {dungeon.Name}");
    }

    public void SetData(object data)
    {
        this.dungeon = data as DungeonModel;
        this.Rebuild();
    }
}
