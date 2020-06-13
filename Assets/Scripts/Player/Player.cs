using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private bool canControl = true;

    private PlayerMovement mover;
    [SerializeField]private Animator animator;
    private float horizontal = 0;
    private bool jump = false;
    private bool dash = false;
    private bool respawn = false;
    private bool esc = false;

    private bool fire = false;
    private bool stillfire = false;
    private bool fireUp = false;

    private GameObject FadeObject;
    [SerializeField]private GameObject FadeObjectPrefab;

    private float fireButtonTime = 0f;

    private Vector2 originPos;
    public int stageNum;

    private InGameMenu escMenu;


    private void Awake()
    {
        fireButtonTime = 0f;
        mover = GetComponent<PlayerMovement>();
        FadeObject = GameObject.FindGameObjectWithTag("FadeObject");
        if(FadeObject==null)
        {
            FadeObject = Instantiate(FadeObjectPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
            FadeObject.transform.localPosition = new Vector3(0, 0, 10);
        }
        FadeObject.SetActive(false);
    }

    private void Start()
    {
        if (DataAdmin.instance.GetData(DataType.game_world) >= 0 && DataAdmin.instance.GetData(DataType.game_stage) >= 0)
        {
            SetSpawnPos(FindObjectOfType<SpawnController>().transform.GetChild(DataAdmin.instance.GetData(DataType.game_stage)).GetComponent<SpawnRegion>().spawnPositionObject.transform.position);
            transform.position = FindObjectOfType<SpawnController>().transform.GetChild(DataAdmin.instance.GetData(DataType.game_stage)).GetComponent<SpawnRegion>().spawnPositionObject.transform.position;
        }
        else
            SetSpawnPos(transform.position);
        escMenu = (InGameMenu)FindObjectOfType(typeof(InGameMenu));
    }

    private void Update()
    {

        respawn = Input.GetButtonDown("Respawn");
        esc = Input.GetKeyDown(KeyCode.Escape);
        if (esc && escMenu != null)
        {
           // Debug.Log("esc pressed");
            Time.timeScale = escMenu.isOn? 1:0;
            escMenu.ActivateAll(!escMenu.isOn);
        }
        if(respawn)
        {
            SpawnController.instance.Respawn();
            transform.position = originPos;
            GetFalseDamage(0.5f);
        }
        if (canControl && Time.timeScale>0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            jump = Input.GetButtonDown("Jump");
            dash = Input.GetButtonDown("Dash");
            fire = Input.GetButtonDown("Fire");
            stillfire = Input.GetButton("Fire");
            fireUp = Input.GetButtonUp("Fire");
            
            if (stillfire)
            {
                fireButtonTime += Time.deltaTime;
                if (fireButtonTime > 1.0f)
                    mover.SetProjectileTime(1.2f);
            }
        }
        mover.Move(horizontal, jump, dash, fire);
        if (fireUp)
        {
            mover.SetProjectileTime(fireButtonTime);
            fireButtonTime = 0f;
        }
        
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        jump = false;
        
    }

    public void GetDamage(float duration = 0.8f)
    {
        DataAdmin.instance.IncrementData(DataType.deathNum);
        if (!canControl) return;
        canControl = false;

        StartCoroutine(Die(duration));
        StartCoroutine(FadeOut());
    }
    public void GetFalseDamage(float duration = 0.8f)
    {
        if (!canControl) return;
        canControl = false;

        StartCoroutine(Die(duration));
    }

    public void SetSpawnPos(Vector2 value, float x = 0, float y = 0, int num = -999)
    {
        Debug.Log("Spawn set: "+ value);
        originPos = value;
        //transform.position = value;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x,y);
        if (num != -999)
            stageNum = num;
    }

    private IEnumerator FadeOut()
    {
        float rad = 30;

        FadeObject.SetActive(true);
        FadeObject.GetComponent<Renderer>().material.SetFloat("_CenterX", transform.position.x);
        FadeObject.GetComponent<Renderer>().material.SetFloat("_CenterY", transform.position.y);
        while (rad>5)
        {
            FadeObject.GetComponent<Renderer>().material.SetFloat("_Radius", rad);
            rad -= 50*Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        while (rad > 0)
        {
            FadeObject.GetComponent<Renderer>().material.SetFloat("_Radius", rad);
            rad -= 25 * Time.deltaTime;
            yield return null;
        }

        //FadeObject.GetComponent<Renderer>().material.SetFloat("_Radius", 30);
        //FadeObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        float rad = 0;
        transform.position = originPos;
        FadeObject.SetActive(true);
        FadeObject.GetComponent<Renderer>().material.SetFloat("_CenterX", transform.position.x);
        FadeObject.GetComponent<Renderer>().material.SetFloat("_CenterY", transform.position.y);

        while (rad < 5)
        {
            FadeObject.GetComponent<Renderer>().material.SetFloat("_Radius", rad);
            rad += 25 * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        while (rad < 30)
        {
            FadeObject.GetComponent<Renderer>().material.SetFloat("_Radius", rad);
            rad += 50 * Time.deltaTime;
            yield return null;
        }



        FadeObject.GetComponent<Renderer>().material.SetFloat("_Radius", 30);
        FadeObject.SetActive(false);
        CanControl(true);
    }

    private IEnumerator Die(float duration)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        animator.SetTrigger("Die");
        //GetComponent<SpriteRenderer>().DOKill();
        //GetComponent<SpriteRenderer>().color = Color.white;
        CanControl(false);

        yield return new WaitForSeconds(duration);
        ResetableObject.ResetAll();

        //animator.SetBool("Die", false);
        animator.SetTrigger("Respawn");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(FadeIn());
        //transform.position = originPos; FadeIn으로 이동함
        //CanControl(true); FadeIn으로 이동함
    }

    public void CanControl(bool canControl)
    {
        this.canControl = canControl;
        horizontal = 0;
        jump = false;
        fire = false;
    }

}
