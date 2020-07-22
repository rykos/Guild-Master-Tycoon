using UnityEngine;

namespace Abilities
{
    public class Skill
    {
        public IAbility Ability;//This skill logic
        public readonly SkillObject skillObject;//Basic template for a skill
        private SkillModel skillModel;

        public Skill(SkillObject skillObject, int level = 0)
        {
            this.skillObject = skillObject;
            this.skillModel = new SkillModel(skillObject);
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
        public int GetLevel()
        {
            return this.skillModel.Level;
        }
        public string GetDescription()
        {
            return string.Format(this.skillObject.Description, this.skillModel.ModifiedValue);
        }
        public float GetValue()
        {
            return this.skillModel.ModifiedValue;
        }

        public void LevelUp()
        {
            this.skillModel.SetLevel(this.skillModel.Level + 1);
            this.Ability.Rebuild();
        }

        public void ActiveUse(HeroModel hero)
        {
            this.Ability.ActiveUse(hero);
        }

        public void PassiveUse(HeroModel hero)
        {
            this.Ability.PassiveUse(hero);
        }
    }
    public struct SkillModel
    {
        public float ModifiedValue;
        public float BaseValue;
        public float Incrementvalue;
        public int Level;

        public SkillModel(SkillObject skillObject)
        {
            this.BaseValue = skillObject.Value;
            this.Incrementvalue = skillObject.IncrementValue;
            this.ModifiedValue = 0;
            this.Level = 0;
        }
        public void SetLevel(int level)
        {
            this.Level = level;
            if (level != 0)
                this.ModifiedValue = this.BaseValue + level * this.Incrementvalue;
            else
                this.ModifiedValue = 0;
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
        void PassiveUse(HeroModel hero);//Passive use before fight starts
        void ActiveUse(HeroModel hero);//Active use in turn
        void Rebuild();//Rebuild this ability values
    }

    public struct GoldenEye : IAbility//Increase amount of gold earned
    {
        public void ActiveUse(HeroModel hero)
        {
            Debug.Log(this.ToString() + " active use");
        }

        public void PassiveUse(HeroModel hero)
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
        public void ActiveUse(HeroModel hero)
        {
            Debug.Log(this.ToString() + " active use");
        }

        public void PassiveUse(HeroModel hero)
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