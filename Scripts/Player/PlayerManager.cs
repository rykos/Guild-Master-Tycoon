using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Editor
    public HeroesList HeroesList;//Owned heroes list
    #endregion
    //
    public static PlayerManager Instance;
    public PlayerModel PlayerModel;
    public ShopManager ShopManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Initialize();
        }
    }

    private void Start()
    {
        //this.PlayerModel.EqupItem(NewItem(ItemType.Chest));
        //this.PlayerModel.EqupItem(NewItem(ItemType.Head));
        //this.PlayerModel.EqupItem(NewItem(ItemType.Legs));
    }

    private void Initialize()
    {
        Instance = this;
        PlayerModel = new PlayerModel();
        ShopManager = new ShopManager();
        PlayerModel.HeroesChangedEvent += OnHeroesChange;
        DontDestroyOnLoad(this);
    }

    private Item NewItem(ItemType type = ItemType.None)
    {
        ItemType itemType = type;
        Item item = new Item("Embeded Something", itemType, new Stats(10, 5, 15), 10, 15)
        {
            Image = AssetManager.Instance.RandomIcon(itemType)
        };
        return item;
    }

    private void OnHeroesChange()
    {
        this.HeroesList.Rebuild(this.PlayerModel.Heroes);
    }
}

/// <summary>
/// Hold player data
/// </summary>
public class PlayerModel
{
    //public event ItemsChanged ItemsChangedEvent;//Called on Equipment Items Changed
    //public delegate void ItemsChanged();
    public event HeroesChanged HeroesChangedEvent;//Called on Heroes Changed
    public delegate void HeroesChanged();
    //
    public List<HeroModel> Heroes = new List<HeroModel>();

    #region Wrapers
    public void AddHero(HeroModel hero)
    {
        this.Heroes.Add(hero);
        HeroesChangedEvent();
    }

    public void RemoveHero(HeroModel hero)
    {
        this.Heroes.Remove(hero);
        HeroesChangedEvent();
    }
    #endregion
}