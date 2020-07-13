using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonResultPage : MonoBehaviour, IUIWidget
{
    public ListWidget HeroesListWidget;
    public ItemsGridWidget ItemsGridWidget;
    public IntegerDisplayWidget IntegerDisplayWidget;
    //
    private MissionModel dungeonMission;

    public void Rebuild()
    {
        List<HeroResultModel> heroResults = new List<HeroResultModel>();
        foreach (HeroModel hero in this.dungeonMission.Heroes)
        {
            Level grantedExp = GrantExp();
            Level oldLevel = hero.Level;
            hero.Level.AddExp(grantedExp.Exp);
            heroResults.Add(new HeroResultModel(hero, oldLevel, grantedExp));
        }
        this.HeroesListWidget.SetData(heroResults);
        //
        List<Item> newItems = new List<Item>() { GenerateItem(), GenerateItem(), GenerateItem(), GenerateItem() };
        this.ItemsGridWidget.SetData(newItems);
        PlayerManager.Instance.PlayerModel.AddItemsToBag(newItems);
        //
        float x = Random.Range(50, 400);
        PlayerManager.Instance.PlayerModel.Wallet.Money += x;
        this.IntegerDisplayWidget.SetData(x);
    }

    private Item GenerateItem()
    {
        Level lvl = new Level((uint)Random.Range(5, 10));
        ItemType itemType = (ItemType)Random.Range(1, System.Enum.GetNames(typeof(ItemType)).Length);
        Item item = new Item("Embeded Something", itemType, new Stats(lvl * 10, lvl * 5, lvl * 15), lvl, (uint)Mathf.Floor(lvl * 1.2f))
        {
            Image = AssetManager.Instance.RandomIcon(itemType)
        };
        return item;
    }

    private Level GrantExp()
    {
        return new Level(0, Random.Range(400, 500));
    }

    /// <param name="data">MissionModel</param>
    public void SetData(object data)
    {
        this.dungeonMission = data as MissionModel;
        this.Rebuild();
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }
}

public class DungeonResultModel
{
    public DungeonModel dungeon;
    public List<HeroModel> heroes;

    public DungeonResultModel(DungeonModel dungeon, List<HeroModel> heroes)
    {
        this.dungeon = dungeon;
        this.heroes = heroes;
    }
}

public class HeroResultModel
{
    public HeroModel Hero;
    public Level OldLevel;
    public Level DeltaExp;

    public HeroResultModel(HeroModel hero, Level oldLevel, Level deltaExp)
    {
        this.Hero = hero;
        this.OldLevel = oldLevel;
        this.DeltaExp = deltaExp;
    }
}