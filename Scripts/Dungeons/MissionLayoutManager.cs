using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionLayoutManager : MonoBehaviour, IUIWidget
{
    private MissionModel missionModel;
    private Fight fight;

    public void OnFinishButtonClicked()
    {
        //Redirect to result page if won
        Destroy(gameObject);
        if (fight.won == null)
        {
            this.Skip();
        }
        if (fight.won == true)
        {
            print("won");
        }
        else
        {
            print("lost");
        }
    }

    //Skips battle
    private void Skip()
    {

    }

    public void Rebuild()
    {
        this.fight = new Fight(this.missionModel);
    }

    /// <param name="data">MissionModel</param>
    public void SetData(object data)
    {
        this.missionModel = data as MissionModel;
        this.Rebuild();
    }
}

public class Fight
{
    private MissionModel missionModel;
    public bool? won;
    public string resultLog = "";

    public Fight(MissionModel missionModel)
    {
        this.missionModel = missionModel;
        //
        this.Rebuild(this.missionModel.Heroes);
        this.Rebuild(this.missionModel.Dungeon.Monsters);
        this.missionModel.Heroes.OrderBy(x => x.MaxHealth);
        this.missionModel.Dungeon.Monsters.OrderBy(x => x.MaxHealth);
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
        Debug.Log(this.resultLog);
        Debug.Log($"Fight done Heroes:{this.missionModel.Heroes.Where(x => x.CurrentHealth > 0).Count()} | Monsters:{this.missionModel.Dungeon.Monsters.Where(x => x.CurrentHealth > 0).Count()}");
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
            this.resultLog += $"Hero turn: dealt {damage} damage\n";
        }
        if (monster.CurrentHealth > 0)//Monster alive, take turn
        {
            double damage = monster.DealDamage();
            hero.TakeDamage(damage);
            this.resultLog += $"Monster turn: dealt {damage} damage\n";
        }
    }
}
