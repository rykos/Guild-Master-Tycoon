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
        dungeonManager.OnDungeonManagerChanged += Rebuild;
        this.Rebuild();
    }
    private void OnDisable()
    {
        dungeonManager.OnDungeonManagerChanged -= Rebuild;
    }

    private void Rebuild()
    {
        this.DungeonsListWidget.SetData(dungeonManager.ActiveDungeons);
    }
}
