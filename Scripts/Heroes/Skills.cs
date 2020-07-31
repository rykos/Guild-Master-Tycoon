using System;
using System.Collections.Generic;

namespace Abilities
{
    public class Skills
    {
        public List<Skill> Abilities = new List<Skill>();
        private Entity entity;
        private int skillPoints;//Skill points left to allocate

        public Skills(Entity entity)
        {
            this.entity = entity;
        }

        public void AddSkill(SkillObject skillObject, int level = 1)
        {
            this.Abilities.Add(new Skill(this.entity, skillObject, level));
            this.skillPoints = (int)entity.Level.Lvl;
        }
        public void AddSkillPoint(int amount = 1)
        {
            this.skillPoints += amount;
        }
        /// <returns>Operation Success state</returns>
        public bool TrySpendSkillPoint()
        {
            if (this.skillPoints > 0)
            {
                this.skillPoints--;
                return true;
            }
            return false;
        }
    }
}