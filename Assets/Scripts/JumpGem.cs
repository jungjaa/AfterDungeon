using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGem : MonoBehaviour
{
    SpriteRenderer spr;
    public Sprite activated;
    public Sprite deActivated;

    bool isActivated;

    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        isActivated = true;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated == false)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 3f)
            {
                elapsedTime = 0;
                isActivated = true;
                spr.sprite = activated;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("player used");
                collision.GetComponent<PlayerMovement>().ProjectileJump();
                isActivated = false;
                spr.sprite = deActivated;
            }
        }
    }
}
