using UnityEngine;
using Abilities;
using TMPro;

public class MissionActionPanelWidget : MonoBehaviour
{
    public RectTransform SkillsContainer;//Contains skills widgets
    public RectTransform StatsContainer;//Contains Entity details
    public GameObject SkillWidgetPrefab;
    public MissionLayoutManager MissionLayoutManager;
    //
    private MonsterModel monster;//Context MonsterModel, Clicked on monster to view its stats
    private HeroModel hero;//Context HeroModel, current turn hero
    private Skills skills;//Available skills

    public void SetSkills(Skills skills)
    {
        this.skills = skills;
        this.RebuildSkills();
    }

    private void RebuildSkills()
    {
        foreach (Skill skill in skills.Abilities)
        {
            GameObject child = Instantiate(this.SkillWidgetPrefab, this.SkillsContainer);//Generate widget
            SkillWidget sw = child.GetComponent<SkillWidget>();
            sw.SetData(skill);//Assign data to widget
            sw.OnClick = () => { UseSkill(sw.Skill); };//Set OnClick delegate
            sw.OnHeld = () => { 
                this.MissionLayoutManager.ShowDetails(skill);
            };
        }
    }

    public void UseSkill(Skill skill)
    {
        Debug.Log($"Using skill {skill.GetName()}");
    }

    public void SetEntity(Entity entity)
    {
        if (entity.MasterType == typeof(HeroModel))
        {
            this.hero = (HeroModel)entity;
        }
        else//Monster model
        {
            this.monster = (MonsterModel)entity;
        }
    }

    public void SetView(RectTransform viewTransform)
    {
        if (viewTransform == SkillsContainer)
        {
            viewTransform.gameObject.SetActive(true);
            this.StatsContainer.gameObject.SetActive(false);
        }
        else
        {
            viewTransform.gameObject.SetActive(true);
            this.SkillsContainer.gameObject.SetActive(false);
        }
    }
}