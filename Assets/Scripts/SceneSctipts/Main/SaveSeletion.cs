using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSeletion : UICluster
{
    bool pressC;
    bool back;
    [SerializeField] private UICluster mainCluster;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isOn)
        {
            pressC = Input.GetKeyDown(KeyCode.C);
            back = Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape);
            if (pressC)
            {
                uiList[index].Select();
            }
            else if (back)
            {
                ActivateAll(false);
                mainCluster.ActivateAll(true);
            }
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("0_Re");
    }
}
