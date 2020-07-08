using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonsViewController : MonoBehaviour
{
    public GameObject DefaultView;
    private GameObject currentView;

    public void EnableView(GameObject view)
    {
        DisableView(currentView);
        view.SetActive(true);
        this.currentView = view;
    }
    public void DisableView(GameObject view)
    {
        if (view != null)
        {
            view.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EnableView(this.DefaultView);
    }
    private void OnDisable()
    {
        DisableView(currentView);
    }
}
