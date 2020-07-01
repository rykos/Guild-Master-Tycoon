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

    public static implicit operator uint(Level level)
    {
        return level.Lvl;
    }
    public override string ToString()
    {
        return this.Lvl.ToString();
    }
}