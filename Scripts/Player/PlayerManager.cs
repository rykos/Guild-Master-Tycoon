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
    public delegate void ShopManagerChanged();
    public event ShopManagerChanged ShopManagerChangedEvent;
    //
    public static PlayerManager Instance;
    public PlayerModel PlayerModel;
    public ShopManager ShopManager;
    //

    private void Awake()
    {
        if (Instance == null)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        Instance = this;
        PlayerModel = new PlayerModel();
        ShopManager = new ShopManager();
        PlayerModel.HeroesChangedEvent += OnHeroesChange;
        DontDestroyOnLoad(this);
    }

    private void OnHeroesChange()
    {
        this.HeroesList.Rebuild(this.PlayerModel.Heroes);
    }

    public void BuyHero(HeroModel hero)
    {
        this.ShopManager.RemoveHero(hero);
        this.PlayerModel.AddHero(hero);
        this.ShopManagerChangedEvent?.Invoke();
    }
    public void AddHeroToShop(HeroModel hero)
    {
        this.ShopManager.AddHero(hero);
        this.ShopManagerChangedEvent?.Invoke();
    }
}

/// <summary>
/// Hold player data
/// </summary>
public class PlayerModel
{
    public event HeroesChanged HeroesChangedEvent;//Called on Heroes Change
    public delegate void HeroesChanged();
    public event ItemBagChanged ItemBagChangedEvent;//Called on ItemBag Change
    public delegate void ItemBagChanged();
    //
    public List<HeroModel> Heroes = new List<HeroModel>();
    public ItemBag ItemBag;
    public Wallet Wallet;

    public PlayerModel()
    {
        this.ItemBag = new ItemBag();
        this.Wallet = new Wallet(999);
    }

    #region Wrapers
    public void AddItemToBag(Item item)
    {
        this.ItemBag.AddItem(item);
        this.ItemBagChangedEvent?.Invoke();
    }

    public void RemoveItemFromBag(Item item)
    {
        this.ItemBag.RemoveItem(item);
        this.ItemBagChangedEvent?.Invoke();
    }

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