using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerWidget : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI Text;
    //
    private bool started = false;
    private DateTime elapseAt;//At this time timer shows 0

    private readonly char[] trimCharacters = new char[] { '0', ':' };

    private void FixedUpdate()
    {
        if (started)
        {
            var time = elapseAt - DateTime.Now;
            if (time.Seconds > 0)
                this.Text.text = string.Format("{0:hh\\:mm\\:ss}", (elapseAt - DateTime.Now)).TrimStart(trimCharacters);
            else
            {
                this.Text.text = "0";
                enabled = false;
            }
        }
    }

    public void Rebuild()
    {

    }

    public void SetData(object data)
    {
        this.elapseAt = (DateTime)data;
        started = true;
    }
}
