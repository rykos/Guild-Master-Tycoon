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
    public DungeonResultPage DungeonResultPage;//Result page for dungeon
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
        this.SelectedHeroesWidget.SetData(selectedHeroes);
        this.HeroesList.SetData(PlayerManager.Instance.DungeonManager.GetUnoccupiedHeroes());
    }
    private void OnDisable()
    {
        //clear cache
        this.selectedHeroes = new List<HeroModel>();
    }

    public ReturnState SelectHero(HeroModel hero)
    {
        if (!this.selectedHeroes.Contains(hero))
        {
            this.selectedHeroes.Add(hero);
            this.UpdateWidget();
            return ReturnState.Success;
        }
        else
        {
            this.DeselectHero(hero);
            return ReturnState.Failed;
        }
    }
    public void DeselectHero(HeroModel hero)
    {
        this.selectedHeroes.Remove(hero);
        this.UpdateWidget();
    }

    public void StartDungeon()
    {
        if (this.selectedHeroes.Count > 0)
        {
            PlayerManager.Instance.DungeonManager.StartMission(new MissionModel(this.dungeon, System.DateTime.Now.AddSeconds(5), this.selectedHeroes));
            this.gameObject.SetActive(false);
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }

    private void UpdateWidget()
    {
        this.SelectedHeroesWidget.SetData(this.selectedHeroes);
    }
}
