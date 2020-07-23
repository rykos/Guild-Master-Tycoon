using System;
using System.Collections.Generic;

namespace Abilities
{
    public class Skills
    {
        public List<Skill> Abilities = new List<Skill>();
        private HeroModel hero;

        public Skills(HeroModel hero)
        {
            this.hero = hero;
        }

        public void AddSkill(SkillObject skillObject, int level = 1)
        {
            this.Abilities.Add(new Skill(this.hero, skillObject, level));
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