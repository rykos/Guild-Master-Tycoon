using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTileDungeon : MonoBehaviour
{
    public void OnActionClick()
    {
        DungeonView.Instance.SelectHero(GetComponent<HeroTile>().Hero);
    }
}
