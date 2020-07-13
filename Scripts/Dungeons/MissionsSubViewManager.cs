using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsSubViewManager : MonoBehaviour
{
    public ListWidget MissionsListWidget;

    private void OnEnable()
    {
        this.MissionsListWidget.SetData(PlayerManager.Instance.DungeonManager.ActiveMissions);
    }
    private void OnDisable()
    {
        
    }
}
