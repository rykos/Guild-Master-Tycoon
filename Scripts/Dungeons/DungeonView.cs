using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dungeon selected, select heroes and go
/// </summary>
public class DungeonView : MonoBehaviour, IUIWidget
{
    public static DungeonView Instance;
    public HeroesList HeroesList;
    public SelectedHeroesWidget SelectedHeroesWidget;
    //
    private DungeonModel dungeon;
    private List<HeroModel> selectedHeroes = new List<HeroModel>();

    private void Awake()
    {
        Instance = this;
    }

    public void Rebuild()
    {
        print($"Rebuild with dungeon {dungeon.Name}");
    }

    public void SetData(object data)
    {
        this.dungeon = data as DungeonModel;
        this.Rebuild();
    }

    private void OnEnable()
    {
        this.HeroesList.SetData(PlayerManager.Instance.PlayerModel.Heroes.ToArray());
    }
    private void OnDisable()
    {
        
    }

    public void SelectHero(HeroModel hero)
    {
        if (!this.selectedHeroes.Contains(hero))
        {
            this.selectedHeroes.Add(hero);
            this.UpdateWidget();
        }
        else
        {
            this.DeselectHero(hero);
        }
    }
    public void DeselectHero(HeroModel hero)
    {
        this.selectedHeroes.Remove(hero);
        this.UpdateWidget();
    }

    private void UpdateWidget()
    {
        this.SelectedHeroesWidget.SetData(this.selectedHeroes);
    }
}
