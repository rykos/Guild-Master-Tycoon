[System.Serializable]
public struct Level
{
    public uint Lvl;
    public float Exp;
    public float ReqExp;

    public Level(uint lvl)
    {
        Lvl = lvl;
        Exp = 0;
        ReqExp = lvl * 100;
    }
    public Level(uint lvl, float exp)
    {
        Lvl = lvl;
        Exp = exp;
        ReqExp = lvl * 100;
    }
    public void AddExp(float amount)
    {
        this.Exp += amount;
        if (this.Exp > this.ReqExp)
        {
            this.LevelUp();
        }
    }
    private void LevelUp()
    {
        this.Exp -= ReqExp;
        this.Lvl++;
    }

    public float GetPercentage()
    {
        return this.Exp / this.ReqExp;
    }

    public static implicit operator uint(Level level)
    {
        return level.Lvl;
    }
    public override string ToString()
    {
        return this.Lvl.ToString();
    }
    public static Level operator +(Level a, Level b)
    {
        return new Level(a.Lvl + b.Lvl, a.Exp + b.Exp);
    }
}