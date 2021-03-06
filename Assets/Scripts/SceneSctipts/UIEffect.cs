﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEffect : MonoBehaviour, IPointerEnterHandler
{
    public UICluster controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Activate()
    {

    }
    public virtual void DeActivate()
    {

    }
    public virtual void Select()
    {

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(controller!=null)
            controller.IndexChange(this);
    }

}
