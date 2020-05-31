using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private bool canControl = true;

    private PlayerMovement mover;
    private Animator animator;
    private float horizontal = 0;
    private bool jump = false;
    private bool dash = false;
    private bool respawn = false;
    private bool esc = false;

    private bool fire = false;
    private bool stillfire = false;
    private bool fireUp = false;

    private float fireButtonTime = 0f;

    private Vector2 originPos;

    [SerializeField]private InGameMenu escMenu;


    private void Awake()
    {
        fireButtonTime = 0f;
        mover = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
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
            GetDamage(0.5f);
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

    public void GetDamage(float duration = 2f)
    {
        if (!canControl) return;
        canControl = false;

        StartCoroutine(Die(duration));
    }

    public void SetSpawnPos(Vector2 value, float x = 0, float y = 0)
    {
        originPos = value;
        //transform.position = value;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x,y);
    }

    private IEnumerator Die(float duration)
    {
        animator.SetBool("Die", true);
        GetComponent<SpriteRenderer>().DOKill();
        GetComponent<SpriteRenderer>().color = Color.white;
        CanControl(false);

        yield return new WaitForSeconds(duration);
        ResetableObject.ResetAll();

        animator.SetBool("Die", false);
        transform.position = originPos;
        CanControl(true);
    }

    public void CanControl(bool canControl)
    {
        this.canControl = canControl;
        horizontal = 0;
        jump = false;
        fire = false;
    }

}
