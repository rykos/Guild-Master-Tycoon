using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Fight
{
    private MissionModel missionModel;
    public bool? won;
    public List<FightTurn> Turns = new List<FightTurn>();

    public Fight(MissionModel missionModel)
    {
        this.missionModel = missionModel;
        //
        this.PrepareEntities();
        while (true)
        {
            MonsterModel mm = this.PickMonster();
            HeroModel hm = this.PickCharacter();
            if (mm != default && hm != default)
            {
                Turn(hm, mm);
            }
            else//If no monster is left alive
            {
                break;
            }
        }
        if (this.missionModel.Heroes.Where(x => x.CurrentHealth > 0).Count() > this.missionModel.Dungeon.Monsters.Where(x => x.CurrentHealth > 0).Count())
        {
            this.won = true;
        }
        else
        {
            this.won = false;
        }
    }
    private void PrepareEntities()
    {
        this.Rebuild(this.missionModel.Heroes);
        this.Rebuild(this.missionModel.Dungeon.Monsters);
        this.missionModel.Heroes.Sort((a, b) => a.MaxHealth.CompareTo(b.MaxHealth));
        this.missionModel.Dungeon.Monsters.Sort(((a, b) => a.MaxHealth.CompareTo(b.MaxHealth)));
        //
        foreach (HeroModel hm in this.missionModel.Heroes)
        {
            hm.ClearPerks();
            hm.Skills.PassiveUseAll();
            this.missionModel.Perks.AddPerks(hm.Perks);
        }
    }

    private void Rebuild(IEnumerable entities)
    {
        foreach (object entity in entities)
        {
            ((Entity)entity).Build();
        }
    }
    private HeroModel PickCharacter()
    {
        return this.missionModel.Heroes.FirstOrDefault(x => x.CurrentHealth > 0);
    }
    private MonsterModel PickMonster()
    {
        return this.missionModel.Dungeon.Monsters.FirstOrDefault(x => x.CurrentHealth > 0);
    }

    private void Turn(HeroModel hero, MonsterModel monster)
    {
        if (hero.CurrentHealth > 0)//Hero alive, take turn
        {
            double damage = hero.DealDamage();
            monster.TakeDamage(damage);
            RegisterTurn(hero, monster, damage);
        }
        if (monster.CurrentHealth > 0)//Monster alive, take turn
        {
            double damage = monster.DealDamage();
            hero.TakeDamage(damage);
            RegisterTurn(monster, hero, damage);
        }
    }

    private void RegisterTurn(Entity attackingEntity, Entity targetEntity, double damage)
    {
        this.Turns.Add(new FightTurn(attackingEntity, targetEntity, damage));
    }
}
