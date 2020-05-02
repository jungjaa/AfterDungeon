using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : ContactArrow
{
    private List<GameObject> leverPlatforms;
    private bool isActive = false;

    private void Start()
    {
        leverPlatforms = new List<GameObject>();

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Lever Platform");
        foreach (GameObject platform in platforms)
        {
            leverPlatforms.Add(platform);
        }

        ActivatePlatform();
    }

    public override void OnLodgingEnterAction(GameObject arrow)
    {
        ActivatePlatform();
        Destroy(arrow);
        //arrow.GetComponent<ArrowController>().Disable();
    }

    public override void OnLodgingExitAction(GameObject arrow)
    {
    }

    public override void OnLodgingStayAction(GameObject arrow)
    {
    }

    private void ActivatePlatform()
    {
        foreach (GameObject leverPlatform in leverPlatforms)
        {
            leverPlatform.GetComponent<LeverPlatform>().ChangeState();
        }
    }
}
