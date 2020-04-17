using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRegion : MonoBehaviour
{
    [Header("주의: CameraController의 위치는 0,0,0이어야 함")]
    [Tooltip("카메라 이동 방식\n x고정, y고정, 모두 고정")]
    public CameraType cameratype;
    private float width;
    private float height;
    private Vector3 position;




    public Vector3 Center // 카메라 고정점(cameratype이 center일 경우) 이거나 카메라 이동 기준점
    {
        get
        {
            return transform.GetChild(2).position;
        }
    }
    public Vector3 MinPoint
    {
        get
        {
            //Debug.Log(transform.position);
            //Debug.Log(transform.GetChild(0).position);
            return transform.GetChild(0).position;
        }
    }
    public Vector3 MaxPoint
    {
        get
        {
            return transform.GetChild(1).position;
        }
    }

    public float Width   { get {    return width;  }  }
    public float Height  { get {    return height; }  }

    public float Min
    {
        get
        {
            if (cameratype == CameraType.XFreeze)
                return MinPoint.y;
            else
                return MinPoint.x;
        }
    }
    public float Max
    {
        get
        {
            if (cameratype == CameraType.XFreeze)
                return MaxPoint.y;
            else
                return MaxPoint.x;
        }
    }
    /*
    void Update()
    {
        Debug.Log(transform.lossyScale + " "+ gameObject);
        Debug.Log(GetComponent<MeshRenderer>().bounds.size + " " + gameObject);
    }
    */
    private void Awake()
    {
        position = transform.position;
        width = (GetComponent<MeshRenderer>().bounds.size.x / 2);
        height = (GetComponent<MeshRenderer>().bounds.size.y / 2);
        
    }
    private void Start()
    {
    }



}

public enum CameraType
{
    XFreeze,YFreeze,Center
}
