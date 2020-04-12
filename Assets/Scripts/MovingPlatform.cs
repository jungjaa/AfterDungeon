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

    [Tooltip("지면을 벗어났을 때 유효 점프 인정 시간")]
    [SerializeField] private float mildJumpTime;

    [SerializeField] private GameObject rider;
    [SerializeField] private GameObject movingObjectPrefab;
    [SerializeField] private GameObject movingObject;
    public Status status = Status.off;
    private Vector3 direction;
    Rigidbody2D rb2D;

    private MovingPlatform destin;

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
                accel = new Vector2(0f, velocity * velocity / (2 * (endPoint.transform.position.x - transform.position.x)));

            movingObject = Instantiate(movingObjectPrefab, transform.position, Quaternion.identity);
            rb2D = movingObject.GetComponent<Rigidbody2D>();
            destin = endPoint;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isItStart)
        {
            if (OutOfBound(destin))// 정해진 범위보다 더 움직였을 경우
            {
                rb2D.velocity = new Vector2(0f, 0f);
                movingObject.transform.position = destin.transform.position;
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
                    StartCoroutine(ChangeStatus(Status.off));
                }
            }
            else
            {
                if (status == Status.forward)
                {
                    rb2D.velocity += accel * Time.deltaTime;
                }
                else if (status == Status.backward)
                {
                    rb2D.velocity = direction * returnVelocity;
                }
            }
        }
               
    }
    
    private bool OutOfBound(MovingPlatform endPoint2)
    {
        if(direction==Vector3.up)
        {
            if (movingObject.transform.position.y > endPoint2.transform.position.y)
                return true;
            else
                return false;
            
        }
        else if (direction == Vector3.down)
        {
            if (movingObject.transform.position.y < endPoint2.transform.position.y)
                return true;
            else
                return false;

        }
        else if(direction == Vector3.right)
        {
            if (movingObject.transform.position.x > endPoint2.transform.position.x)
                return true;
            else
                return false;

        }
        else
        {
            if (movingObject.transform.position.x < endPoint2.transform.position.x)
                return true;
            else
                return false;

        }
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
        this.status = Status.off;
        yield return new WaitForSeconds(0.5f);
        this.status = finalstatus;
    }
}
public enum Direction
{
    x,y
}

public enum Status
{
    forward, off, backward
}