using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroTileResult : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI RewardText;
    private string rewardString;
    private HeroResultModel heroResultModel;

    public void Rebuild()
    {
        if (this.RewardText != null)
        {
            this.RewardText.text = this.heroResultModel.DeltaLevel.Exp.ToString();
        }
        GetComponent<HeroTile>().Hero = this.heroResultModel.Hero;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data">HeroResult</param>
    public void SetData(object data)
    {
        this.heroResultModel = data as HeroResultModel;
        this.Rebuild();
    }
}
