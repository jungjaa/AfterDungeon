using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : ContactArrow
{
    private List<GameObject> leverPlatforms;
    private List<GameObject> leverPlatformsB;
    private bool isActive = false;

    private void Start()
    {
        leverPlatforms = new List<GameObject>();
        leverPlatformsB = new List<GameObject>();

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Lever Platform");
        foreach (GameObject platform in platforms)
        {
            leverPlatforms.Add(platform);
        }

        GameObject[] platformsB = GameObject.FindGameObjectsWithTag("Lever PlatformB");
        foreach (GameObject platform in platformsB)
        {
            leverPlatformsB.Add(platform);
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
        foreach (GameObject leverPlatform in leverPlatformsB)
        {
            leverPlatform.GetComponent<LeverPlatform>().ChangeState();
        }
    }
}
