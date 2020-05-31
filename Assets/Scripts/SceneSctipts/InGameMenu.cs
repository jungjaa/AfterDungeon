using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : UICluster
{
    private bool Esc;

    protected override void Update()
    {
        base.Update();
    }


    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }
}
