using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HeroViewController : ViewController
{
    public GameObject HeroView;

    public void EnableSubView()
    {
        this.HeroView.SetActive(true);
    }
}
