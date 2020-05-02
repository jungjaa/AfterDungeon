using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPlatform : LodgingPlatform
{
    [SerializeField] private LayerMask playerLayer;
    private bool targetState = true;

    public void ChangeState()
    {
        if (targetState) Deactivate();
        else Activate();
    }

    private void Activate()
    {
        targetState = true;
        StartCoroutine(TryToActivate());
    }

    private void Deactivate()
    {
        targetState = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator TryToActivate()
    {
        BoxCollider2D myColl = GetComponent<BoxCollider2D>();

        Collider2D coll = Physics2D.OverlapBox(transform.localPosition, myColl.size, 0, playerLayer);

        while (coll != null)
        {
            if (!targetState) yield break;

            yield return new WaitForFixedUpdate();
            coll = Physics2D.OverlapBox(transform.localPosition, myColl.size, 0, playerLayer);
        }

        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Collider2D>().enabled = true;
    }
}
