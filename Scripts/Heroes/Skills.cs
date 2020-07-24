using System;
using System.Collections.Generic;

namespace Abilities
{
    public class Skills
    {
        public List<Skill> Abilities = new List<Skill>();
        private HeroModel hero;
        private int skillPoints;//Skill points left to allocate

        public Skills(HeroModel hero)
        {
            this.hero = hero;
        }

        public void AddSkill(SkillObject skillObject, int level = 1)
        {
            this.Abilities.Add(new Skill(this.hero, skillObject, level));
            this.skillPoints = (int)hero.Level.Lvl;
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
        public void PassiveUseAll()
        {
            foreach (Skill ability in this.Abilities)
            {
                ability.PassiveUse();
            }
        }
        public void ActiveUseAll()
        {
            foreach (Skill ability in this.Abilities)
            {
                ability.ActiveUse();
            }
        }
    }
}