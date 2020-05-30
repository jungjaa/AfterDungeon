using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICluster : MonoBehaviour
{
    [SerializeField]
    protected List<UIEffect> uiList;
    public bool isOn;
    public int index;

    bool up;
    bool down;
    bool enter;

    void Start()
    {
       index = 0;
       uiList[0].Activate();
       uiList[0].controller = this;
       for(int i=1;i<uiList.Count;i++)
       {
            uiList[i].DeActivate();
            uiList[i].controller = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            up = Input.GetKeyDown(KeyCode.UpArrow);
            down = Input.GetKeyDown(KeyCode.DownArrow);
            enter = Input.GetKeyDown(KeyCode.KeypadEnter);
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

    public void ActivateAll(bool On)
    {
        for(int i=0;i<uiList.Count;i++)
        {
            uiList[i].gameObject.SetActive(false);
        }
    }
}
