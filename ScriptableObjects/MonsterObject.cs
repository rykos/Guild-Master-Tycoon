using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Monster-", menuName = "Create new Monster")]
public class MonsterObject : ScriptableObject
{
    public Sprite Avatar;
    public string Name;
    public SkillObject[] SkillObjects;
}

public enum MonsterClass
{
    None,
    Solider,
    Skeleton,
    Zombie,
    Ghost
}