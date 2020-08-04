using UnityEngine;
using System.Collections.Generic;

namespace Abilities
{
    public class Skill
    {
        public IAbility Ability;//This skill logic
        public readonly SkillObject skillObject;//Basic template for a skill
        public readonly SkillTargetType SkillTargetType;
        private SkillModel skillModel;
        private Entity entity;//Entity owning this skill
        //
        public Entity GetEntity { get => this.entity; }
        public string GetName { get => this.skillObject.Name; }
        public string GetDescription { get => this.Ability.GetDescription(this); }
        public int GetLevel { get => this.skillModel.Level; }
        public float GetValue { get => this.skillModel.ModifiedValue; }
        public SkillEnum GetSkillType { get => this.skillObject.Skill; }

        public Skill(Entity hero, SkillObject skillObject, int level = 0)
        {
            this.entity = hero;
            this.skillObject = skillObject;
            this.skillModel = new SkillModel(skillObject);
            this.SkillTargetType = this.skillObject.TargetType;
            this.skillModel.SetLevel(level);
            this.Initialize();
        }

        private void Initialize()
        {
            SkillLogic.AssignAbility(this);
            // this.Ability.Rebuild(this.entity, this);
        }
        public void LevelUp()
        {
            if (entity.Skills.TrySpendSkillPoint())//Successfully spent skill point
            {
                this.skillModel.SetLevel(this.skillModel.Level + 1);
            }
        }

        public void ActiveUse(Entity caster, Entity target, IEnumerable<Entity> friendly, IEnumerable<Entity> enemy)
        {
            this.Ability.ActiveUse(caster, target, friendly, enemy, this);
        }

        public void PassiveUse(Entity[] targets)
        {
            this.Ability.PassiveUse(this.entity, targets, this);
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
            switch (skill.GetSkillType)
            {
                case SkillEnum.Heal:
                    ability = new Heal();
                    break;
                case SkillEnum.Bash:
                    ability = new Bash();
                    break;
                case SkillEnum.WhirlWind:
                    ability = new WhirlWind();
                    break;
            }
            skill.Ability = ability ?? throw new System.NotImplementedException();
        }
    }


    #region Abilities
    public interface IAbility
    {
        string GetDescription(Skill skill);
        void PassiveUse(Entity caster, Entity[] targets, Skill skill);//Passive use before fight starts
        void ActiveUse(Entity caster, Entity target, IEnumerable<Entity> friendly, IEnumerable<Entity> enemy, Skill skill);//Active use in turn
    }

    
    #endregion

    public enum SkillTargetType
    {
        None,
        Enemy,
        Friendly,
        Both
    }
}