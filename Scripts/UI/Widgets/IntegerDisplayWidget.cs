using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntegerDisplayWidget : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI Text;
    public float Speed;
    private float value;
    private bool started = false;
    private float interPoint = 0;

    public void Rebuild()
    {

    }

    private void Update()
    {
        if (started)
        {
            interPoint += Time.deltaTime * Speed;
            float tempVal = Mathf.Lerp(0, value, interPoint);
            Text.text = string.Format("{0:0}", tempVal);
            if (interPoint > 1)
            {
                enabled = false;
            }
        }
    }

    public void SetData(object data)
    {
        value = (float)data;
        this.Rebuild();
        started = true;
    }
}
