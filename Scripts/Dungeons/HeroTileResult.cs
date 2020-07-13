using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroTileResult : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI RewardText;
    private string rewardString;
    private HeroResultModel heroResultModel;
    private LevelWidgetController levelWidgetController;
    private bool started;//Animation state
    private float interpolationValue = 0;

    private void Awake()
    {
        this.levelWidgetController = GetComponent<HeroTile>().levelWidgetController;//fetch widget from family tile
    }

    private void Update()
    {
        if (started)
        {
            this.interpolationValue += Time.deltaTime/2;
            this.RewardText.text = $"+{Mathf.Lerp(0, heroResultModel.DeltaExp.Exp, this.interpolationValue):0}xp";
            if (this.interpolationValue > 1)
            {
                started = false;
            }
        }
    }

    public void Rebuild()
    {
        if (this.RewardText != null)
        {
            this.RewardText.text = this.heroResultModel.OldLevel.Exp.ToString();
        }
        GetComponent<HeroTile>().Hero = this.heroResultModel.Hero;
        this.levelWidgetController.SetData(this.heroResultModel);
        this.started = true;
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
