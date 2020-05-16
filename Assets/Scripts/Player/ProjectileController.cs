using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private bool isGoingRight;
    private float speed;

    private bool isPlayerThere;
    private bool isFlying;
    private float endX;

    private float elaspedtime;
    private PlayerMovement player;
    public float limitTime = 999f;

    public void Initialize(bool isGoingRight, float speed, float distance, PlayerMovement person)
    {
        this.isGoingRight = isGoingRight;
        this.speed = speed;
        this.player = person;

        elaspedtime = 0f;

        isPlayerThere = true;
        isFlying = true;
        endX = transform.position.x + (isGoingRight ? distance : -distance);
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(0.45f,0,0), new Vector2(1f,0.15f));
    }
    */

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        rb2D.velocity = isGoingRight ? new Vector2(speed, 0) : new Vector2(-speed, 0);
    }

    private void FixedUpdate()
    {
        ActionCheck();
        if (isFlying == false)
        {
            elaspedtime += Time.deltaTime;
            if (elaspedtime > limitTime)
            {
                player.FireEnd();
                Destroy(gameObject);
            }
            rb2D.velocity = Vector2.zero;
            rb2D.bodyType = RigidbodyType2D.Static;

            return;
        }

        if (isGoingRight && transform.position.x > endX)
        {
            isFlying = false;
            transform.position = new Vector2(endX, transform.position.y);
        }

        if (!isGoingRight && transform.position.x < endX)
        {
            isFlying = false;
            transform.position = new Vector2(endX, transform.position.y);
        }
    }
    private void ActionCheck()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position - new Vector3(0.45f, 0, 0), new Vector2(1f, 0.15f), 0);
        for(int i=0;i<colls.Length;i++)
        {
            if(colls[i].gameObject.GetComponent<ContactArrow>() != null)
            {
                colls[i].gameObject.GetComponent<ContactArrow>().OnLodgingEnterAction(this.gameObject);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            if (!isPlayerThere)
            {
                coll.gameObject.GetComponent<PlayerMovement>().ProjectileJump();
                Destroy(gameObject);
            }           
        }
        else if (coll.tag != "Player" && coll.tag != "Item")// 추가된 부분
        {
            isFlying = false;
        }

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (isPlayerThere) isPlayerThere = false;
            bc2D.isTrigger = false; // 추가된 부분
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//추가된 부분
    {
        isFlying = false;
        if (collision.collider.tag == "Player")
        {
            if (!isPlayerThere)
            {
                collision.collider.gameObject.GetComponent<PlayerMovement>().ProjectileJump();
                Destroy(gameObject);
            }
        }
        if (collision.collider.GetComponent<ContactArrow>() != null)
        {
            collision.collider.GetComponent<ContactArrow>().OnLodgingEnterAction(this.gameObject);
            //Debug.Log("Arrow on Lever");
        }

    }

    private void OnDestroy()
    {
        player.FireEnd();
    }

    public void SetLimit(float time)
    {
        limitTime = time;
    }
}
