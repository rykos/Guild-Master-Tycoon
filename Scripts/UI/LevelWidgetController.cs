using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWidgetController : MonoBehaviour
{
    public TextMeshProUGUI Level_TMP;
    public Slider ExpBar;
    private HeroModel hero;

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
