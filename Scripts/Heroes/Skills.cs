using System.Collections.Generic;

namespace Abilities
{
    public class Skills
    {
        public List<Skill> Abilities = new List<Skill>();

        public void AddSkill(SkillObject skillObject, int level = 1)
        {
            this.Abilities.Add(new Skill(skillObject, level));
        }

        public void PassiveUseAll()
        {
            foreach (Skill ability in this.Abilities)
            {
                ability.Ability.PassiveUse();
            }
        }
        public void ActiveUseAll()
        {
            foreach (Skill ability in this.Abilities)
            {
                ability.Ability.ActiveUse();
            }
        }
    }
}