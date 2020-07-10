using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroTileResult : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI RewardText;
    private string rewardString;

    public void Rebuild()
    {
        if (this.RewardText != null)
        {
            this.RewardText.text = rewardString;
        }
    }

    public void SetData(object data)
    {
        this.rewardString = (string)data;
        this.Rebuild();
    }
}
