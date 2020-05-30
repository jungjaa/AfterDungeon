using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCluster : UICluster
{
    [SerializeField] UICluster saveCluster;

    public void GoNextCluster()
    {
        ActivateAll(false);
        saveCluster.ActivateAll(true);
    }
}
