using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSubViewManager : MonoBehaviour
{
    public ListWidget DungeonsListWidget;

    private void OnEnable()
    {
        this.DungeonsListWidget.SetData(PlayerManager.Instance.DungeonManager.ActiveDungeons);
    }
}
