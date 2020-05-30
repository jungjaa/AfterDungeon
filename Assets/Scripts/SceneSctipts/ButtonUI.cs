using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : UIEffect
{
    [SerializeField] Color onTextColor;
    [SerializeField] Color offTextColor;

    [SerializeField] int onFontSize;
    [SerializeField] int offFontSize;

    [SerializeField] Text text;
    // Start is called before the first frame update
    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        text.color = onTextColor;
        text.fontSize = onFontSize;
    }

    public override void DeActivate()
    {
        text.color = offTextColor;
        text.fontSize = offFontSize;
    }

    public override void Select()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}
