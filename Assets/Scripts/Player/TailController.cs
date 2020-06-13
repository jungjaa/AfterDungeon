using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    [SerializeField] private Sprite normalTail;
    [SerializeField] private Sprite fallingTail;
    private float elapsed = 0f;
    private bool isOn = false;
    private float limit;
    private Rigidbody2D rb2D;

    private SpriteRenderer spr;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }
    void Update()
    {
        if(isOn)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x -2f < -4 ? rb2D.velocity.x -2f: -4, rb2D.velocity.y-2f);
            elapsed += Time.deltaTime;
            if(elapsed>limit)
            {
                isOn = false;
                gameObject.SetActive(false);
            }
            if(elapsed>limit*0.2 && spr.sprite != fallingTail)
            {
                spr.sprite = fallingTail;
            }
            spr.color = new Color(1, 1, 1, (limit - elapsed*0.8f) / limit);
        }
    }
    public void Initiate(float dashtime)
    {
        if (!isOn)
        {
            spr.sprite = normalTail;
            spr.color = new Color(1f, 1f, 1f, 1f);
            limit = dashtime;
            elapsed = 0f;
            isOn = true;
        }
    }

    public void End(Vector3 position)
    {
        isOn = false;
        spr = GetComponent<SpriteRenderer>(); // 오류 방지용
        spr.color = new Color(1f, 1f, 1f, 0f);
        transform.position = position;
        gameObject.SetActive(false);
    }

}
