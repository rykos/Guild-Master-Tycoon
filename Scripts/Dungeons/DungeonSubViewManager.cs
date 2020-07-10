using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSubViewManager : MonoBehaviour
{
    public ListWidget DungeonsListWidget;
    private DungeonManager dungeonManager;

    private void OnEnable()
    {
        if (dungeonManager == null)
        {
            dungeonManager = PlayerManager.Instance.DungeonManager;
        }
        dungeonManager.DungeonManagerChangedEvent += Rebuild;
        this.Rebuild();
    }
    private void OnDisable()
    {
        dungeonManager.DungeonManagerChangedEvent -= Rebuild;
    }

    private void Rebuild()
    {
        this.DungeonsListWidget.SetData(dungeonManager.ActiveDungeons);
    }
}
