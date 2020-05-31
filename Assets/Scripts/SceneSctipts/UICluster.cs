using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICluster : MonoBehaviour
{

    [SerializeField] protected List<UIEffect> uiList; // 버튼 등 실제 이벤트를 담당하는 역할
    [SerializeField] protected List<UIEffect> otherUI; // 텍스트나 패널 등을 등록하는 역할
    public bool isOn;
    public int index;

    protected bool up;
    protected bool down;
    protected bool enter;

    protected virtual void Start()
    {
       index = 0;
       uiList[0].Activate();
       uiList[0].controller = this;
       for(int i=1;i<uiList.Count;i++)
       {
            uiList[i].DeActivate();
            uiList[i].controller = this;
       }
        ActivateAll(isOn);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isOn)
        {
            up = Input.GetKeyDown(KeyCode.UpArrow);
            down = Input.GetKeyDown(KeyCode.DownArrow);
            enter = Input.GetKeyDown(KeyCode.Return);
            if (up)
            {
                uiList[index].DeActivate();
                index = (index - 1) >= 0 ? index - 1 : 0;
                uiList[index].Activate();
            }
            else if (down)
            {
                uiList[index].DeActivate();
                index = (index + 1) < uiList.Count ? index + 1 : uiList.Count - 1;
                uiList[index].Activate();
            }
            else if (enter)
            {
                uiList[index].Select();
            }
        }
    }

    public void IndexChange(UIEffect part)
    {
        uiList[index].DeActivate();
        index = uiList.IndexOf(part);
        uiList[index].Activate();
    }

    public virtual void ActivateAll(bool On)
    {
        isOn = On;
        for(int i=0;i<uiList.Count;i++)
        {
            uiList[i].gameObject.SetActive(On);
        }
        if (otherUI != null)
        {
            for (int i = 0; i < otherUI.Count; i++)
            {
                otherUI[i].gameObject.SetActive(On);
            }
        }
    }

}
