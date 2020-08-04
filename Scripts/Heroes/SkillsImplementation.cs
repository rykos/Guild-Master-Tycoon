using System.Collections.Generic;

namespace Abilities
{
    public struct Bash : IAbility//Basic ass hit
    {
        public string GetDescription(Skill skill)
        {
            return $"Bashes target for {skill.GetValue}";
        }

        public void ActiveUse(Entity caster, Entity target, IEnumerable<Entity> friendly, IEnumerable<Entity> enemy, Skill skill)
        {
            target.TakeDamage(new Damage(skill.GetValue * 2, DamageType.Blunt));
        }

        public void PassiveUse(Entity caster, Entity[] targets, Skill skill)
        {

        }
    }

    public struct WhirlWind : IAbility
    {
        public void ActiveUse(Entity caster, Entity target, IEnumerable<Entity> friendly, IEnumerable<Entity> enemy, Skill skill)
        {
            foreach (Entity t in enemy)
            {
                t.Buffs.Add(new Buff("Bleeding", "Bleeds for 1 every turn", true, 2, (c) =>
                {
                    c.TakeDamage(new Damage(skill.GetValue * 0.25f, DamageType.Bleed));
                }));
            }
        }

        public string GetDescription(Skill skill)
        {
            return $"Inflicts bleeding {skill.GetValue * 0.25} to all targets for 2 turns";
        }

        public void PassiveUse(Entity caster, Entity[] targets, Skill skill)
        {
            throw new System.NotImplementedException();
        }
    }

    public struct Heal : IAbility
    {
        public string GetDescription(Skill skill)
        {
            return $"Heals target for {skill.GetValue} radiant";
        }

        public void ActiveUse(Entity caster, Entity target, IEnumerable<Entity> friendly, IEnumerable<Entity> enemy, Skill skill)
        {
            target.TakeHealing(new Damage(skill.GetValue, DamageType.Radiant));
        }

        public void PassiveUse(Entity caster, Entity[] targets, Skill skill)
        {
            
        }
    }


    public enum SkillEnum
    {
        None,
        Heal,
        Bash,
        WhirlWind,
    }
}