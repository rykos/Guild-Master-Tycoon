using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLayoutManager : MonoBehaviour
{
    private bool? won;

    public void OnFinishButtonClicked()
    {
        //Redirect to result page if won
        Destroy(gameObject);
        if (won == null)
        {
            this.Skip();
        }
        if (won == true)
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


}
