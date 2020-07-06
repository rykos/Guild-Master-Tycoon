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

    public PlayerModel()
    {
         this.ItemBag = new ItemBag();
    }

    #region Wrapers
    public void AddItem(Item item)
    {
        this.ItemBag.AddItem(item);
        this.ItemBagChangedEvent?.Invoke();
    }

    public void RemoveItem(Item item)
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