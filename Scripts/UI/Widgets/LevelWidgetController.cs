using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWidgetController : MonoBehaviour, IUIWidget
{
    public TextMeshProUGUI Level_TMP;
    public Slider ExpBar;
    //
    private HeroModel hero;
    private Level baseLevel, finalLevel;
    private bool interpolationStarted;
    private float interpolationValue = 0;

    private void Update()
    {
        if (this.interpolationStarted)
        {
            this.interpolationValue += Time.deltaTime / 2;
            this.ExpBar.value = (Mathf.Lerp(this.baseLevel.Exp, this.finalLevel.Exp, interpolationValue)/this.finalLevel.ReqExp);
            if (this.interpolationValue > 1)
            {
                this.interpolationStarted = false;
            }
        }
    }

    public void Rebuild()
    {
        throw new System.NotImplementedException();
    }

    /// <param name="data">String/percentage value</param>
    public void SetData(object data)
    {
        if (data.GetType() == typeof(string))
        {
            this.Level_TMP.text = data.ToString();
        }
        else if (data.GetType() == typeof(float))
        {
            this.ExpBar.value = (float)data;
        }
    }

    public void SetData(HeroResultModel heroResultModel)
    {
        this.baseLevel = heroResultModel.OldLevel;
        this.finalLevel = heroResultModel.Hero.Level;
        if (finalLevel.Lvl > baseLevel.Lvl)
        {
            this.baseLevel.Exp = 0;
        }
        this.interpolationStarted = true;
    }

    public void SetHero(HeroModel hero)
    {
        this.hero = hero;
        UpdateWidget();
    }

    private void UpdateWidget()
    {
        this.Level_TMP.text = hero.Level.ToString();
        this.ExpBar.value = this.hero.Level.Exp / this.hero.Level.ReqExp;
    }
}
