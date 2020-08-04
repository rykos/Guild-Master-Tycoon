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
    private bool skillsEnabled;

    public void SetSkills(Skills skills, bool skillsEnabled)
    {
        this.skillsEnabled = skillsEnabled;
        this.skills = skills;
        this.RebuildSkills();
    }

    private void RebuildSkills()
    {
        for (int i = this.SkillsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(this.SkillsContainer.GetChild(i).gameObject);
        }
        foreach (Skill skill in skills.Abilities)
        {
            if (skill.SkillTargetType != SkillTargetType.None)
            {
                GameObject child = Instantiate(this.SkillWidgetPrefab, this.SkillsContainer);//Generate widget
                SkillWidget sw = child.GetComponent<SkillWidget>();
                sw.enabled = this.skillsEnabled;
                sw.SetData(skill);//Assign data to widget
                sw.OnClick = () => { UseSkill(sw.Skill); };//Set OnClick delegate
                sw.OnHeld = () =>
                {
                    this.MissionLayoutManager.ShowDetails(skill);
                };
            }
        }
    }

    public void UseSkill(Skill skill)
    {
        this.MissionLayoutManager.PlayerUseSkill(skill);
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