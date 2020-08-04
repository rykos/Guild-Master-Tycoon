using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;

[CreateAssetMenu(fileName = "Skill-", menuName = "Create new Skill")]
public class SkillObject : ScriptableObject
{
    public Sprite Icon;
    public GameObject Animation;
    public string Name;
    public string Description;
    public float Value;//Starting value for level 1
    public float IncrementValue;//Value to increment for each level
    public SkillEnum Skill;
    public SkillTargetType TargetType;
}