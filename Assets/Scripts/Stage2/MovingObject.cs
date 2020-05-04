using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Vector2 playerBox = new Vector2(0.92f, 0.2f);

    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerChecker;

    [Tooltip("발판이 멈춘 이후에도 점프를 할 수 있는 시간")]
    public float mildPlatformTime;
    private float lastPlayerTime;
    [SerializeField] private GameObject player;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerChecker.position, playerBox);

    }

    public GameObject PlayerChecking()
    {
        GameObject curPlayer = player;
        player = null;
        Collider2D[] colls = Physics2D.OverlapBoxAll(playerChecker.position, playerBox, 0, whatIsPlayer);
        int i;
        for(i=0;i<colls.Length;i++)
        {
            if (colls[i].tag == "Player")
            {
                player = colls[i].gameObject;
                lastPlayerTime = Time.time;
                if (curPlayer == null)
                    animator.SetTrigger("PlayerOn");
                break;
            }
        }
        //Debug.Log(transform.position + " " + player);
        //Debug.Log("cur: " + curPlayer + "new: " + player);
        if(curPlayer!=null && player==null)
        {
            Debug.Log("playerOff");
            animator.SetTrigger("PlayerOff");
        }
        if (Time.time - lastPlayerTime <= mildPlatformTime)
            return player;
        return null;
    }

    public void SetObjectIdle()
    {
        animator.SetTrigger("Idle");
    }
}
