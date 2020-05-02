using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContactArrow : MonoBehaviour
{
    public abstract void OnLodgingEnterAction(GameObject arrow);

    public abstract void OnLodgingStayAction(GameObject arrow);

    public abstract void OnLodgingExitAction(GameObject arrow);
}
