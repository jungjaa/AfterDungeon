using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWaySensor : MonoBehaviour
{
    public PlatformEffector2D pe2D;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            int curLayer = pe2D.colliderMask;
            int playerLayer = 1 << LayerMask.NameToLayer("Player");
            curLayer = curLayer | playerLayer;
            pe2D.colliderMask -= playerLayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            int curLayer = pe2D.colliderMask;
            int playerLayer = 1 << LayerMask.NameToLayer("Player");
            pe2D.colliderMask = curLayer | playerLayer;
        }
    }
}
