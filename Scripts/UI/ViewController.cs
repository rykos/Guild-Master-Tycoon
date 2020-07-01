using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public void DisableSubView(GameObject subView)
    {
        subView.SetActive(false);
    }

    public void EnableSubView(GameObject subView)
    {
        subView.SetActive(true);
    }
}
