using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private Sprite unPressed;
    [SerializeField] private Sprite Pressed;
    private Sprite next;
    SpriteRenderer spr;
    private float elapsed = 0f;
    private bool isChanged = false;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = unPressed;
        next = Pressed;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(!isChanged && elapsed<0.2f)
        {
            isChanged = true;
            Sprite temp = spr.sprite;
            spr.sprite = next;
            next = temp;
        }
        else if(elapsed>0.22f && isChanged)
        {
            Sprite temp = spr.sprite;
            spr.sprite = next;
            next = temp;
            isChanged = false;
        }
        else if(elapsed>1.5f)
        {
            elapsed = 0f;
        }
        
    }

}
