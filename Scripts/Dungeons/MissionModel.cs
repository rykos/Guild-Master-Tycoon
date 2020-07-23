using System;
using System.Collections.Generic;

public class MissionModel
{
    public DungeonModel Dungeon;//Selected dungeon
    public DateTime StartedTime;//Starting time
    public DateTime FinishTime;//Finished time
    public List<HeroModel> Heroes;//Heroes dispatched to this mission
    public Perks Perks = new Perks();//Perks applied to this mission

    public MissionModel(DungeonModel dungeon, DateTime finishTime, List<HeroModel> heroes)
    {
        this.Dungeon = dungeon;
        this.StartedTime = DateTime.Now;
        this.FinishTime = finishTime;
        this.Heroes = heroes;
    }
}
