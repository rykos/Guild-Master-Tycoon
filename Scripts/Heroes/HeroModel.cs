[System.Serializable]
public class HeroModel
{
    public string IconPath;
    public string Name;
    public Equipment Equipment;
    public Level Level;
    public Stats BaseStats;
    public Stats FinalStats;

    public void EquipItem(Item item)
    {
        this.Equipment.EquipItem(item);
        RecalculateStats();
    }
    public void UnequipItem(Item item)
    {
        this.Equipment.UnequipItem(item);
        RecalculateStats();
    }
    private void RecalculateStats()
    {
        this.FinalStats = this.BaseStats + this.Equipment.GetStats();
    }
    public Stats GetStats()
    {
        if (this.FinalStats == default)
        {
            this.FinalStats = this.BaseStats;
        }
        return this.FinalStats;
    }
}
