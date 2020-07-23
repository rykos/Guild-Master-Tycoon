using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;

public class HeroViewSkillsController : MonoBehaviour, IUIWidget
{
    public GameObject SkillWidgetPrefab;
    public GameObject SkillsContainer;//Contains spawned widgets
    //
    private HeroModel hero;
    private List<GameObject> generatedSkillWidgets = new List<GameObject>();

    public void Rebuild()
    {
        this.DestroyWidgets();
        this.CreateWidgets();
    }

    public void SetData(object data)
    {
        this.hero = (HeroModel)data;
        this.Rebuild();
    }

    private void DestroyWidgets()
    {
        for (int i = this.generatedSkillWidgets.Count - 1; i > -1; i--)
        {
            Destroy(this.generatedSkillWidgets[i]);
            this.generatedSkillWidgets.RemoveAt(i);
        }
    }

    private void CreateWidgets()
    {
        foreach (Skill skill in this.hero.Skills.Abilities)
        {
            this.generatedSkillWidgets.Add(BuildWidget(skill));
        }
    }

    private GameObject BuildWidget(Skill skill)
    {
        GameObject newWidget = Instantiate(this.SkillWidgetPrefab, this.SkillsContainer.transform);
        newWidget.GetComponent<IUIWidget>().SetData(skill);
        return newWidget;
    }
}
