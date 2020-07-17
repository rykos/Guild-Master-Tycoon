using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero-", menuName = "Create Hero")]
public class HeroObject : ScriptableObject
{
    public Sprite Avatar;
    public string Name;
    public HeroClass Class;
}
