using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderWidget : MonoBehaviour, IUIWidget
{
    public Slider Slider;

    public void Rebuild()
    {
        throw new System.NotImplementedException();
    }

    public void SetData(object data)
    {
        this.Slider.value = (float)data;
    }
}
