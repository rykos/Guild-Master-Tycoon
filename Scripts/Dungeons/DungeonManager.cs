using System.Collections.Generic;
using System.Linq;

public class DungeonManager
{
    public delegate void DungeonManagerChanged();
    public event DungeonManagerChanged DungeonManagerChangedEvent;
    //Events
    public List<DungeonModel> ActiveDungeons = new List<DungeonModel>();
    public List<MissionModel> ActiveMissions = new List<MissionModel>();
    public List<HeroModel> OccupiedHeroes = new List<HeroModel>();

    public DungeonManager()
    {
        for (int i = 1; i < 50; i++)
        {
            NewDungeon($"Dungeon id:{i}", (uint)i, (Rarity)(byte)UnityEngine.Random.Range(0, 4));
        }
    }

    public void NewDungeon(string name, uint level, Rarity rarity = default)
    {
        DungeonModel dm = new DungeonModel()
        {
            Name = name,
            Rarity = rarity,
            Level = new Level(level),
        };
        dm.GenerateMonsters();
        this.ActiveDungeons.Add(dm);
        this.DungeonManagerChangedEvent?.Invoke();
    }
    public void RemoveDungeon(DungeonModel dm)
    {
        this.ActiveDungeons.Remove(dm);
        this.DungeonManagerChangedEvent?.Invoke();
    }

    public void StartMission(MissionModel missionModel)
    {
        this.RemoveDungeon(missionModel.Dungeon);
        this.ActiveMissions.Add(missionModel);
        this.DungeonManagerChangedEvent?.Invoke();
        this.OccupiedHeroes.AddRange(missionModel.Heroes);
    }
    public void EndMission(MissionModel missionModel)
    {
        this.ActiveMissions.Remove(missionModel);
        this.DungeonManagerChangedEvent?.Invoke();
        foreach (var hero in missionModel.Heroes)
        {
            this.OccupiedHeroes.Remove(hero);
        }
    }
    public List<HeroModel> GetUnoccupiedHeroes()
    {
        List<HeroModel> heroes = new List<HeroModel>();
        foreach (HeroModel hero in PlayerManager.Instance.PlayerModel.Heroes)
        {
            if (!this.OccupiedHeroes.Contains(hero))//Unoccupied
            {
                heroes.Add(hero);
            }
        }
        return heroes;
    }
}
