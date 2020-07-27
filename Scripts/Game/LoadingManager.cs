using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>Does Nothing</summary>
///Should take care of loadning information, and enstablishing connection to the servers
public class LoadingManager : MonoBehaviour
{
    public void FinishedLoading()
    {
        SceneManager.LoadScene("Main");
    }
}
