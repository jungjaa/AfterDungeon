using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRegion : MonoBehaviour
{
    [Header("주의: SpawnController의 위치는 0,0,0이어야 함")]

    [SerializeField]
    [Tooltip("이 월드의 처음 스폰지점인가?")]private bool isFirst;
    [SerializeField]
    [Tooltip("이 월드의 세이브 스테이지인가?")] private bool isSaveStage;
    private float width;
    private float height;
    [SerializeField] public GameObject spawnPositionObject;
    private Vector2 spawnPosition;

    [SerializeField]
    [Tooltip("스테이지 번호")] private int stageNum;



    public bool IsFirst { get { return isFirst; } }
    public bool IsSaveStage { get { return isSaveStage; } }
    public float Width { get { return width; } }
    public float Height { get { return height; } }

    /*
    void Update()
    {
        Debug.Log(transform.lossyScale + " "+ gameObject);
        Debug.Log(GetComponent<MeshRenderer>().bounds.size + " " + gameObject);
    }


    private void Start()
    {
    }
    */

    private void Awake()
    {
        spawnPosition = spawnPositionObject.transform.position;
        width = GetComponent<MeshRenderer>().bounds.size.x / 2;
        height = GetComponent<MeshRenderer>().bounds.size.y / 2;

    }

    public void SetSpawn(Player player)
    {
        player.SetSpawnPos(spawnPosition, player.gameObject.GetComponent<Rigidbody2D>().velocity.x, player.gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

}
