using UnityEngine;

namespace Abilities
{
    public class Skill
    {
        public IAbility Ability;//This skill logic
        public int Level;
        private SkillObject skillObject;//Basic template for a skill

        public Skill(SkillObject skillObject, int level = 0)
        {
            this.skillObject = skillObject;
            this.Level = level;
            this.Initialize();
        }

        private void Initialize()
        {
            SkillLogic.AssignAbility(this);
            this.Ability.Rebuild();
        }

        public SkillEnum GetSkillType()
        {
            return this.skillObject.Skill;
        }
    }

    public static class SkillLogic
    {
        public static void AssignAbility(Skill skill)
        {
            switch (skill.GetSkillType()) 
            {
                case SkillEnum.MoreDamage:
                    skill.Ability = new SharpWill();
                    break;
                case SkillEnum.MoreGold:
                    skill.Ability = new GoldenEye();
                    break;
                case SkillEnum.MoreHealth:
                    throw new System.NotImplementedException();
                    break;
            }
        }
    }


    #region Abilities
    public interface IAbility
    {
        public void PassiveUse();//Passive use before fight starts
        public void ActiveUse();//Active use in turn
        public void Rebuild();//Rebuild this ability values
    }

    public struct GoldenEye : IAbility//Increase amount of gold earned
    {
        public void ActiveUse()
        {
            Debug.Log(this.ToString() + " active use");
        }

        public void PassiveUse()
        {
            Debug.Log(this.ToString() + " passive use");
        }

        public void Rebuild()
        {
            Debug.Log(this.ToString() + " rebuild");
        }
    }

    public struct SharpWill : IAbility//Increase damage
    {
        public void ActiveUse()
        {
            Debug.Log(this.ToString() + " active use");
        }

        public void PassiveUse()
        {
            Debug.Log(this.ToString() + " passive use");
        }

        public void Rebuild()
        {
            Debug.Log(this.ToString() + " rebuild");
        }
    }
    #endregion
}