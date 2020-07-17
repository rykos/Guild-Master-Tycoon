using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour
{
    public HeroObject[] Heroes;
    //
    public Sprite[] Helmets;
    public Sprite[] Chests;
    public Sprite[] Boots;
    public Sprite[] Weapons_Melee;
    public Sprite[] Weapons_Magic;
    //
    public static AssetManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public Sprite RandomIcon(ItemType itemType)
    {
        switch (itemType) 
        {
            case ItemType.Head:
                return Helmets[Random.Range(0, Helmets.Length)];
            case ItemType.Chest:
                return Chests[Random.Range(0, Chests.Length)];
            case ItemType.Legs:
                return Boots[Random.Range(0, Boots.Length)];
            case ItemType.WeaponMelee:
                return Weapons_Melee[Random.Range(0, Weapons_Melee.Length)];
            case ItemType.WeaponMagic:
                return Weapons_Magic[Random.Range(0, Weapons_Magic.Length)];
        }
        return null;
    }

    public HeroObject RandomHeroObject()
    {
        return this.Heroes[Random.Range(0, this.Heroes.Length)];
    }
}