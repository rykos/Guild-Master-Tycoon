using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<HeroModel> Heroes = new List<HeroModel>();
    public delegate void HeroesChanged();
    public event HeroesChanged HeroesChangedEvent;//Called on Heroes Changed

    public void AddHero(HeroModel hero)
    {
        this.Heroes.Add(hero);
        HeroesChangedEvent();
    }

    public void RemoveHero(HeroModel hero)
    {
        throw new NotImplementedException();
        HeroesChangedEvent();
    }
}