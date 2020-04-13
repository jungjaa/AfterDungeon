using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("이것이 시작점인지 끝점인지 설정")]
    public bool isItStart;
    [Tooltip("이것이 시작점일 경우에만 끝점 설정")]
    public MovingPlatform endPoint;
    private MovingPlatform startPoint;
    [Tooltip("움직이는 방향 설정")]
    public Direction directionType;
    [Tooltip("최종 속도 설정")]
    public float velocity;
    [Tooltip("돌아가는 속도 설정")]
    public float returnVelocity;
    [Tooltip("플랫폼 크기")]
    public int size;

    [Tooltip("발판이 멈춘 이후에도 점프를 할 수 있는 시간")]
    [SerializeField] private float mildPlatformTime;

    [SerializeField] private GameObject player = null;
    [SerializeField] private MovingObject movingObjectPrefab;
    [SerializeField] private MovingObject movingObject;
    public Status status = Status.off;
    public Vector3 direction;
    Rigidbody2D rb2D;

    private MovingPlatform destin;

    [SerializeField]
    private Vector2 accel;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        if (isItStart)
        {
            if (directionType == Direction.x)
            {
                if (endPoint.transform.position.x > transform.position.x)
                    direction = Vector3.right;
                else
                    direction = Vector3.left;
            }
            else
            {
                if (endPoint.transform.position.y > transform.position.y)
                    direction = Vector3.up;
                else
                    direction = Vector3.down;
            }
            this.startPoint = this;
            if (directionType == Direction.x)
                accel = new Vector2(velocity * velocity / (2 * (endPoint.transform.position.x - transform.position.x)), 0f);
            else
                accel = new Vector2(0f, velocity * velocity / (2 * (endPoint.transform.position.y - transform.position.y)));

            movingObject = Instantiate(movingObjectPrefab, transform.position, Quaternion.identity);
            movingObject.transform.localScale *= size;
            movingObject.mildPlatformTime = this.mildPlatformTime;
            rb2D = movingObject.GetComponent<Rigidbody2D>();
            destin = endPoint;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isItStart)
        {
            StatusSetting();
            if (OutOfBound(destin))// 정해진 범위보다 더 움직였을 경우
            {
                rb2D.velocity = new Vector2(0f, 0f);
                movingObject.transform.position = destin.transform.position;
                if (player != null)
                {
                    player.GetComponent<Rigidbody2D>().velocity -= player.GetComponent<PlayerMovement>().platformVelocity;
                    player.GetComponent<PlayerMovement>().platformVelocity = new Vector3(0f, 0f, 0f);
                }
                if (status == Status.forward)
                {
                    direction = direction * (-1);
                    destin = startPoint;
                    Debug.Log("Forward end");
                    StartCoroutine(ChangeStatus(Status.backward));
                }
                else
                {
                    direction = direction * (-1);
                    destin = endPoint;
                    this.status = Status.off;
                    StartCoroutine(ChangeStatus(Status.off));
                }
            }
            else
            {
                if (status == Status.forward)
                {
                    rb2D.velocity += accel * Time.deltaTime;
                    if (player != null)
                    {
                        player.GetComponent<Rigidbody2D>().velocity += accel * Time.deltaTime;
                        player.GetComponent<PlayerMovement>().platformVelocity += accel * Time.deltaTime;
                    }
                }
                else if (status == Status.backward)
                {
                    rb2D.velocity = direction * returnVelocity;
                    if(player!=null)
                         player.GetComponent<PlayerMovement>().platformVelocity = direction * returnVelocity;
                }
            }
        }
               
    }
    
    private bool OutOfBound(MovingPlatform endPoint2)
    {
        if(direction==Vector3.up)
        {
            if (movingObject.transform.position.y > endPoint2.transform.position.y - rb2D.velocity.y*Time.deltaTime)
                return true;
            else
                return false;
            
        }
        else if (direction == Vector3.down)
        {
            if (movingObject.transform.position.y < endPoint2.transform.position.y - rb2D.velocity.y * Time.deltaTime)
                return true;
            else
                return false;

        }
        else if(direction == Vector3.right)
        {
            if (movingObject.transform.position.x > endPoint2.transform.position.x - rb2D.velocity.x * Time.deltaTime)
                return true;
            else
                return false;

        }
        else
        {
            if (movingObject.transform.position.x < endPoint2.transform.position.x - rb2D.velocity.x * Time.deltaTime)
                return true;
            else
                return false;

        }
    }

    private void StatusSetting()
    {
        GameObject curPlayer;
        curPlayer = movingObject.PlayerChecking();
        if(player==null && curPlayer!=null)
        {
            curPlayer.GetComponent<PlayerMovement>().isPlatform = true;
            curPlayer.GetComponent<PlayerMovement>().movingPlatform = this;
        }
        else if(player!=null && curPlayer==null)
        {
            player.GetComponent<PlayerMovement>().isPlatform = false;
            player.GetComponent<PlayerMovement>().movingPlatform = null;
        }

        player = curPlayer;
        if (status == Status.off && player != null)
            status = Status.forward;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        this.rider = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == rider)
            this.rider = null;
    }
    */
    IEnumerator ChangeStatus(Status finalstatus)
    {
        if (status == Status.forward)
        {
            this.status = Status.wait_jump;
            yield return new WaitForSeconds(mildPlatformTime);
            this.status = Status.wait_nojump;
            yield return new WaitForSeconds(0.5f - mildPlatformTime);
            this.status = finalstatus;
        }
        else
        {
            this.status = Status.wait_nojump;
            yield return new WaitForSeconds(0.5f);
            this.status = finalstatus;
        }
    }
}
public enum Direction
{
    x,y
}

public enum Status
{
    forward, off, backward, wait_jump, wait_nojump
}