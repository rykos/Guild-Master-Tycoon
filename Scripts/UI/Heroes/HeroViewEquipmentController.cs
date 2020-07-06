using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroViewEquipmentController : MonoBehaviour, IUIWidget, IContext
{
    public HeroEquipmentWidget HeroEquipmentWidget;
    public ItemsGridWidget ItemsGridWidget;
    //
    private HeroModel hero;

    /// <param name="data">HeroModel</param>
    public void SetData(object data)
    {
        this.hero = (HeroModel)data;
        this.HeroEquipmentWidget.SetData(data);
        this.ItemsGridWidget.SetData(data);
    }
    public void Rebuild()
    {
        
    }

    private void OnEnable()
    {
        //this.SetData();
    }

    public object GetContext()
    {
        return this.hero;
    }
}

public interface IContext
{
    object GetContext();
}