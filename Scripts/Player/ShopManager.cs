using System.Collections.Generic;

public class ShopManager
{
    public List<HeroModel> Heroes;

    public ShopManager()
    {
        this.Heroes = new List<HeroModel>();
    }

    public void GenerateRandomHeroes(int amount = 5)
    {
        this.Heroes = new List<HeroModel>();
        for (int i = 0; i < amount; i++)
        {
            this.Heroes.Add(this.BuildRandomHero(i));
        }
        this.Heroes.Sort((a,b) => b.Level.Lvl.CompareTo(a.Level.Lvl));
    }

    private HeroModel BuildRandomHero(int heroIndex )
    {
        HeroModel hero = HeroModel.BuildHero(iconPath: Generator.RandomIconPath(), name: Generator.RandomName(),
            new Equipment(), Generator.RandomLevel(10, 3), stats: new Stats(5, 1, 100));
        hero.Price = 100;
        HeroObject ho = (heroIndex < 4) ? AssetManager.Instance.Heroes[heroIndex] : AssetManager.Instance.RandomHeroObject();
        hero.Avatar = ho.Avatar;
        hero.Name = ho.name;
        return hero;
    }

    public void RemoveHero(HeroModel hero)
    {
        this.Heroes.Remove(hero);
    }
    public void AddHero(HeroModel hero)
    {
        this.Heroes.Add(hero);
    }
}
