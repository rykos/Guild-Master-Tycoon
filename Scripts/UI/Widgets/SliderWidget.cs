using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderWidget : MonoBehaviour, IUIWidget
{
    public Slider Slider;
    private float iTime = 0;
    private float iSpeed = 2;
    private float targetValue;
    private float startingValue;

    private void Update()
    {
        if (iTime <= 1)
        {
            iTime += Time.deltaTime * iSpeed;
            Slider.value = Mathf.Lerp(startingValue, targetValue, iTime);
        }
    }

    public void Rebuild()
    {
        throw new System.NotImplementedException();
    }

    public void SetData(object data)
    {
        //this.Slider.value = (float)data;
        startingValue = this.Slider.value;
        targetValue = (float)data;
        this.iTime = 0;
    }

    public void SetData(float targetValue, float startingValue)
    {
        this.startingValue = startingValue;
        this.targetValue = targetValue;
        this.iTime = 0;
    }

    public void SetDataInstant(float value)
    {
        this.Slider.value = value;
    }
}
