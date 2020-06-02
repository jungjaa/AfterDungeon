using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : UICluster
{
    private bool Esc;

    protected override void Update()
    {
        base.Update();
    }


    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }

    public void Restart()
    {
        FindObjectOfType<Player>().GetFalseDamage();
    }

    public void SaveAndExit()
    {
        Saver.SaveData(DataAdmin.instance.GetData(DataType.slotNum));
        SceneManager.LoadScene("Main");
    }

    public void ChapterRestart()
    {
        Player player = FindObjectOfType<Player>();
        player.SetSpawnPos(FindObjectOfType<SpawnController>().transform.GetChild(0).GetComponent<SpawnRegion>().spawnPositionObject.transform.position, 0, 0, 0);
        player.GetFalseDamage();
    }
}
