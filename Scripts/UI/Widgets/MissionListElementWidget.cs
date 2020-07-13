using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MissionListElementWidget : MonoBehaviour, IUIWidget, IPointerClickHandler
{
    public TextMeshProUGUI Text;
    public TimerWidget TimerWidget;
    //
    private MissionModel missionModel;

    public void OnPointerClick(PointerEventData eventData)
    {
        int span = (missionModel.FinishTime - System.DateTime.Now).Seconds;
        print($"{span}s");
        if (!(span > 0))
        {
            OpenResultPage();
            PlayerManager.Instance.DungeonManager.EndMission(this.missionModel);
            Destroy(gameObject);
        }
    }

    private void OpenResultPage()
    {
        ListWidget x = gameObject.GetComponentInParent<ListWidget>();
        var resPage = Instantiate(x.ActivableElement, GameObject.Find("/Canvas").transform);
        resPage.GetComponent<IUIWidget>()?.SetData(this.missionModel);
    }

    public void Rebuild()
    {
        this.Text.text = this.missionModel.Dungeon.Name;
        TimerWidget.SetData(this.missionModel.FinishTime);
    }

    public void SetData(object data)
    {
        this.missionModel = data as MissionModel;
        this.Rebuild();
    }
}
