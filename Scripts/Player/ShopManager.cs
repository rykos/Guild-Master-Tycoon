using System.Collections.Generic;

public class ShopManager
{
    public List<HeroModel> Heroes;

    public ShopManager()
    {
        this.Heroes = new List<HeroModel>();
        GenerateRandomHeroes();
    }

    public void GenerateRandomHeroes(int amount = 5)
    {
        this.Heroes = new List<HeroModel>();
        for (int i = 0; i < amount; i++)
        {
            this.Heroes.Add(this.BuildRandomHero());
        }
    }

    private HeroModel BuildRandomHero()
    {
        HeroModel hero = HeroModel.Build(iconPath: Generator.RandomIconPath(), name: Generator.RandomName(),
            new Equipment(), Generator.RandomLevel(10, 3), stats: new Stats(5, 6, 2));
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
