using UnityEngine;

namespace Abilities
{
    public class Skill
    {
        public IAbility Ability;//This skill logic
        public readonly SkillObject skillObject;//Basic template for a skill
        private SkillModel skillModel;
        private HeroModel hero;//Hero owning this skill

        public Skill(HeroModel hero, SkillObject skillObject, int level = 0)
        {
            this.hero = hero;
            this.skillObject = skillObject;
            this.skillModel = new SkillModel(skillObject);
            this.skillModel.SetLevel(level);
            this.Initialize();
        }

        private void Initialize()
        {
            SkillLogic.AssignAbility(this);
            this.Ability.Rebuild(this.hero, this);
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
            if (hero.Skills.TrySpendSkillPoint())//Successfully spent skill point
            {
                this.skillModel.SetLevel(this.skillModel.Level + 1);
                this.Ability.Rebuild(this.hero, this);
            }
        }

        public void ActiveUse()
        {
            this.Ability.ActiveUse(this.hero, this);
        }

        public void PassiveUse()
        {
            this.Ability.PassiveUse(this.hero, this);
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
            IAbility ability = null;
            switch (skill.GetSkillType())
            {
                case SkillEnum.MoreDamage:
                    ability = new SharpWill();
                    break;
                case SkillEnum.MoreGold:
                    ability = new GoldenEye();
                    break;
                case SkillEnum.MoreHealth:
                    ability = new MoreHealth();
                    break;
            }
            skill.Ability = ability ?? throw new System.NotImplementedException();
        }
    }


    #region Abilities
    public interface IAbility
    {
        void PassiveUse(HeroModel hero, Skill skill);//Passive use before fight starts
        void ActiveUse(HeroModel hero, Skill skill);//Active use in turn
        void Rebuild(HeroModel hero, Skill skill);//Rebuild this ability values
    }

    public struct GoldenEye : IAbility//Increase amount of gold earned
    {
        private const PerkType PERK_TYPE = PerkType.MoreGold;
        public void ActiveUse(HeroModel hero, Skill skill)
        {
            Debug.Log(this.ToString() + " active use");
        }

        public void PassiveUse(HeroModel hero, Skill skill)
        {
            Debug.Log(this.ToString() + " passive use");
            hero.Perks.AddPerk(PERK_TYPE, skill.GetValue());
        }

        public void Rebuild(HeroModel hero, Skill skill)
        {
            Debug.Log(this.ToString() + " rebuild");
            hero.Perks.GetPerks.ForEach(x => Debug.Log($"{x.Type} {x.Value}"));
        }
    }

    public struct SharpWill : IAbility//Increase damage
    {
        private const PerkType PERK_TYPE = PerkType.MoreDamage;
        public void ActiveUse(HeroModel hero, Skill skill)
        {
            Debug.Log(this.ToString() + " active use");
        }

        public void PassiveUse(HeroModel hero, Skill skill)
        {
            Debug.Log(this.ToString() + " passive use");
            hero.Perks.AddPerk(PERK_TYPE, skill.GetValue());
        }

        public void Rebuild(HeroModel hero, Skill skill)
        {
            Debug.Log(this.ToString() + " rebuild");
        }
    }

    public struct MoreHealth : IAbility
    {
        public void ActiveUse(HeroModel hero, Skill skill)
        {

        }

        public void PassiveUse(HeroModel hero, Skill skill)
        {

        }

        public void Rebuild(HeroModel hero, Skill skill)
        {

        }
    }
    #endregion
}