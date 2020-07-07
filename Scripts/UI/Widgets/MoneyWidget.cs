using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyWidget : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI MoneyText;
    //
    private PlayerModel player;
    private string activeString;

    private void Start()
    {
        this.player = PlayerManager.Instance.PlayerModel;
    }

    private void FixedUpdate()
    {
        if (this.player != null)
        {
            this.Rebuild();
        }
    }

    public void Rebuild()
    {
        string newString = player.Wallet.Money.ToString();
        if (newString != activeString)
        {
            this.MoneyText.text = newString;
            activeString = newString;
        }
    }

    public void SetData(object data)
    {
        throw new System.NotImplementedException();
    }
}
