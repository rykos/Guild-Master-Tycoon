using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionListElementWidget : MonoBehaviour
{
    public TimerWidget TimerWidget;

    private void OnEnable()
    {
        TimerWidget.SetData(System.DateTime.Now.AddSeconds(15));
    }
}
