using System.Collections.Generic;
using UnityEngine;

public static class Generator
{
    public static Level RandomLevel(uint lvl, uint deviation)
    {
        uint level = (uint)Random.Range(lvl - deviation, lvl + deviation);
        return new Level(level);
    }

    public static string RandomName()
    {
        string[] NamesDatabase = new string[] { "Knight", "Mage", "Rogue", "Druid", "Warrior"};
        return NamesDatabase[Random.Range(0, NamesDatabase.Length)];
    }

    public static string RandomIconPath()
    {
        string[] IconsDatabase = new string[] { "/Knight.jpg", "/Mage.jpg", "/Rogue.jpg", "/Druid.jpg", "/Warrior.jpg" };
        return IconsDatabase[Random.Range(0, IconsDatabase.Length)];
    }


}