using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private void Start()
    {
        //GetComponent<Animator>().Play();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}