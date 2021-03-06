﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;

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
    public DungeonManager DungeonManager;
    //

    private void Awake()
    {
        if (Instance == null)
        {
            Initialize();
        }
    }

    private void Start()
    {
        this.ShopManager.GenerateRandomHeroes(6);
        this.DungeonManager.GenerateDungeons(20);
    }

    private void Initialize()
    {
        Instance = this;
        this.PlayerModel = new PlayerModel();
        this.ShopManager = new ShopManager();
        this.DungeonManager = new DungeonManager();
        this.PlayerModel.HeroesChangedEvent += OnHeroesChange;
        DontDestroyOnLoad(this);
    }

    private void OnHeroesChange()
    {
        this.HeroesList.Rebuild(this.PlayerModel.Heroes);
    }

    public ReturnState BuyHero(HeroModel hero)
    {
        if (this.PlayerModel.Wallet.Money >= hero.Price)//Check if transaction is possible
        {
            this.PlayerModel.Wallet.Money -= hero.Price;//Subtract price
            this.ShopManager.RemoveHero(hero);
            this.PlayerModel.AddHero(hero);
            this.ShopManagerChangedEvent?.Invoke();
            return ReturnState.Success;
        }
        return ReturnState.Failed;
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
        this.Wallet = new Wallet(499);
    }

    #region Wrapers
    public void AddItemToBag(Item item)
    {
        this.ItemBag.AddItem(item);
        this.ItemBagChangedEvent?.Invoke();
    }
    public void AddItemsToBag(List<Item> items)
    {
        foreach (Item item in items)
        {
            this.ItemBag.AddItem(item);
        }
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
        this.Heroes.Sort((a, b) => b.Level.Lvl.CompareTo(a.Level.Lvl));
        HeroesChangedEvent();
    }
    public void RemoveHero(HeroModel hero)
    {
        this.Heroes.Remove(hero);
        HeroesChangedEvent();
    }
    #endregion
}

public enum ReturnState
{
    Failed,
    Success
}