using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroTileDungeon : MonoBehaviour
{
    public TextMeshProUGUI ActionButtonText;
    public Image ActionButtonImage;
    public Sprite GreenButtonSprite, RedButtonSprite;
    public bool HeroSelected;

    public void OnActionClick()
    {
        this.SelectHero();
    }

    private void SelectHero()
    {
        ReturnState rs = DungeonView.Instance.SelectHero(GetComponent<HeroTile>().Hero);
        if (rs == ReturnState.Success)
        {
            this.ActionButtonText.text = "Deselect";
            this.ActionButtonImage.sprite = RedButtonSprite;
        }
        else
        {
            this.ActionButtonText.text = "Select";
            this.ActionButtonImage.sprite = GreenButtonSprite;
        }
    }
}
