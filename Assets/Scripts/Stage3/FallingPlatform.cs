using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : ResetableObject
{
    private float elapsed = 0f;

    private bool isFalling = false;
    private FallingPlatform left = null;
    private FallingPlatform right = null;

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform weightChecker;

    private Vector2 checkBox = new Vector2(0.5f,0.5f);
    private Vector2 weightBox = new Vector2(0.95f, 0.2f);

    [Tooltip("추락 상호작용할 Layer")]
    [SerializeField] private LayerMask whatIsWeight;

    private Rigidbody2D rb2D;
    private Vector2 originPos;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(weightChecker.position, weightBox);
        /*
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(leftPoint.position, checkBox);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(rightPoint.position, checkBox);
        */
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        originPos = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        left = CheckSidePlatform(leftPoint.position);
        right = CheckSidePlatform(rightPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFalling)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y - 1.1772f); //만약 player의 gravityscale이 달라진다면 이것도 수정해야 함... 6 기준임
            VelocityLimit();
            elapsed += Time.deltaTime;
            if(elapsed>5)
            {
                isFalling = false;
                rb2D.velocity = new Vector2(0,0);
            }
        }
        else if(WeightChecking())
        {
            RecursiveFalling();
        }

    }

    private void VelocityLimit()
    {
        if (rb2D.velocity.y < (-1) * 20/*jumpVelocity.y*/ + 0.2f /* * rb2D.gravityScale / 6*/)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, (-1) * 20/*jumpVelocity.y*/ + 0.2f /* * rb2D.gravityScale / 6*/);
        }
    }
    private bool WeightChecking()
    {
        List<Collider2D> colliders = new List<Collider2D>();

        Collider2D[] colls = Physics2D.OverlapBoxAll(weightChecker.position, weightBox, 0, whatIsWeight);
        foreach (Collider2D coll in colls)
        {
            colliders.Add(coll);
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject != gameObject && colliders[i].gameObject.GetComponent<Rigidbody2D>()!=null)
            {
                if (colliders[i].gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 && colliders[i].gameObject.transform.position.y>=transform.position.y)
                    return true;
            }

        }
        return false;

    }

    protected void RecursiveFalling()
    {
        if (isFalling)
            return;

        isFalling = true;
        if (left != null)
            left.RecursiveFalling();
        if (right != null)
            right.RecursiveFalling();
    }

    FallingPlatform CheckSidePlatform(Vector2 checkPosition)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        Collider2D[] colls = Physics2D.OverlapBoxAll(checkPosition, checkBox,0);
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

    public override void Reset()
    {
        isFalling = false;
        rb2D.velocity = Vector2.zero;
        elapsed = 0;
        transform.position = originPos;
    }

}
