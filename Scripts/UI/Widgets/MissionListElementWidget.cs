using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MissionListElementWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public TextMeshProUGUI Text;
    public TimerWidget TimerWidget;
    //
    private MissionModel missionModel;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void Rebuild()
    {
        this.Text.text = this.missionModel.Dungeon.Name;
        TimerWidget.SetData(this.missionModel.FinishTime);
    }

    public void SetData(object data)
    {
        this.missionModel = data as MissionModel;
        this.Rebuild();
    }
}
