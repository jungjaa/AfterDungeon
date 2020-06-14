using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera controlcamera;
    public float moveTime = 0.1f;
    float inverseMoveTime;

    private bool isCameraMoving = false;

    public CameraType startType; // 게임 시작때 카메라 정보, 사용하지 않을 수도 있음
    public Vector3 startPosition;

    public GameObject player;

    private float curLeft = 0; // 현재 영역의 각 변들
    private float curRight = 0;
    private float curUp = 0;
    private float curDown = 0;

    private float curWidth = -1; // 현재 영역의 너비, 높이
    private float curHeight = -1;

    private int regionNum;

    float x;
    float y; // player의 위치 저장용 변수


    CameraRegion curRegion;

    private Vector3 offset; // 카메라 이동시 offset

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controlcamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        regionNum = WhichRegion();
        inverseMoveTime = 1f / moveTime;
        x = player.transform.position.x;
        y = player.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCameraMoving == false)
        {
            //Debug.Log(offset);
            x = player.transform.position.x;
            y = player.transform.position.y;
            int num;
            //Debug.Log(curRegion.transform.position);
            //Debug.Log("Min: " + curRegion.MinPoint);
            //Debug.Log(curRegion.Min);
            // Debug.Log("Down: " + curDown);
            if ((x >= curLeft) && (x < curRight) && (y >= curDown) && (y < curUp))
            {
                 //Debug.Log("here!");
                if (curRegion.cameratype == CameraType.XFreeze)
                {
                    Vector3 next = offset + new Vector3(controlcamera.transform.position.x, y, -10f);
                    if (next.y < curRegion.Min)
                        next.y = curRegion.Min;
                    else if (next.y > curRegion.Max)
                        next.y = curRegion.Max;

                    controlcamera.transform.position = next;
                }
                else if (curRegion.cameratype == CameraType.YFreeze)
                {
                    Vector3 next = offset + new Vector3(x, controlcamera.transform.position.y, -10f);
                    if (next.x < curRegion.Min)
                        next.x = curRegion.Min;
                    else if (next.x > curRegion.Max)
                        next.x = curRegion.Max;

                    controlcamera.transform.position = next;
                }
            }
            else if ((num = WhichRegion()) != transform.childCount)
            {
                Debug.Log("region changed");
                if (curRegion.cameratype == CameraType.Center)
                {
                    StartCoroutine(Move(curRegion.Center + new Vector3(0f, 0f, -10f)));
                }
                else if ((curRegion.MinPoint - player.transform.position).magnitude < (curRegion.MaxPoint - player.transform.position).magnitude)
                {
                    SetMinOffset();
                    StartCoroutine(Move(curRegion.MinPoint + new Vector3(0f, 0f, -10f)));
                }
                else
                {
                    SetMaxOffset();
                    StartCoroutine(Move(curRegion.MaxPoint + new Vector3(0f, 0f, -10f)));
                }
            }
            else
            {
                player.GetComponent<Player>().GetDamage();
            }
        }
    }

    int WhichRegion()
    {
        int i;
        for (i = 0; i < transform.childCount; i++)
        {
            CameraRegion region = transform.GetChild(i).gameObject.GetComponent<CameraRegion>();
            float width = region.Width;
            float height = region.Height;
            float regionx = region.gameObject.transform.position.x;
            float regiony = region.gameObject.transform.position.y;

            if ((x >= regionx - width) && (x < regionx + width) && (y >= regiony - height) && (y < regiony + height))
            {
                curWidth = region.Width;
                curHeight = region.Height;
                curLeft = regionx - width;
                curRight = regionx + width;
                curUp = regiony + height;
                curDown = regiony - height;
                regionNum = i;
                curRegion = region;
                break;
            }
        }
        return i;
    }

    void SetMinOffset()
    {
        CameraType cameratype = curRegion.cameratype;
        if (cameratype == CameraType.XFreeze)
        {
            offset = new Vector3(0f, curRegion.Min - curDown, 0f);
        }
        else
        {
            offset = new Vector3(curRegion.Min - curLeft, 0f, 0f);
        }
    }
    void SetMaxOffset()
    {
        CameraType cameratype = curRegion.cameratype;
        if (cameratype == CameraType.XFreeze)
        {
            offset = new Vector3(0f, curUp - curRegion.Max, 0f);
        }
        else
        {
            offset = new Vector3(curRight - curRegion.Max, 0f, 0f);
        }
    }
    IEnumerator Move(Vector3 end)
    {
        isCameraMoving = true;
        float sqrRemainingDistance = (controlcamera.transform.position - end).sqrMagnitude;
        Time.timeScale = 0f;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(controlcamera.transform.position, end, inverseMoveTime * Time.fixedDeltaTime);
            controlcamera.transform.position = newPosition;
            sqrRemainingDistance = (controlcamera.transform.position - end).sqrMagnitude;
            //Debug.Log(camera.transform.position);
            // Debug.Log(end);
            //Debug.Log("Move!");
            yield return null;
        }
        Time.timeScale = 1f;
        isCameraMoving = false;
    }
}

/*
Debug.Log("x:"+x);
Debug.Log("y:"+y);
Debug.Log("height:" + height);
Debug.Log(regionx - width);
Debug.Log(regionx + width);
Debug.Log(regiony - height);
Debug.Log(regiony + height);
*/
