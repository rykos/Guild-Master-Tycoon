using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill-", menuName = "Create new Skill")]
public class SkillObject : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description;
    public SkillEnum Skill;
    public int Level = 1;
}

public enum SkillEnum
{
    None,
    MoreGold,
    MoreHealth,
    MoreDamage
}