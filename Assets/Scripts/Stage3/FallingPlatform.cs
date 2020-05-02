using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float elapsed = 0f;

    private bool isFalling = false;
    private FallingPlatform left = null;
    private FallingPlatform right = null;

    public Transform leftPoint;
    public Transform rightPoint;

    private Vector2 checkBox = new Vector2(0.05f,0.05f);
    // Start is called before the first frame update
    void Start()
    {
        left = CheckSidePlatform(leftPoint.position);
        right = CheckSidePlatform(rightPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFalling)
        {
            elapsed += Time.deltaTime;
        }
        if(elapsed>2f)
        {
            Destroy(gameObject);
        }
    }

    protected void RecursiveFalling()
    {
        if (isFalling)
            return;

        if (left != null)
            left.RecursiveFalling();
        if (right != null)
            right.RecursiveFalling();

        isFalling = true;
    }

    FallingPlatform CheckSidePlatform(Vector2 checkPosition)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        Collider2D[] colls = Physics2D.OverlapBoxAll(checkPosition, checkBox, 0);
        foreach (Collider2D coll in colls)
        {
            colliders.Add(coll);
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].GetComponent<FallingPlatform>() != null)
            {
               return colliders[i].GetComponent<FallingPlatform>();
            }
        }
        return null;
    }
}
