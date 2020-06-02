using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void LoadData(int slotNum)
    {
        if(PlayerPrefs.GetInt("saveType_" + slotNum.ToString())==(int)SaveType.Stage)
        {
            DataAdmin.instance.SetData(DataType.slotNum, slotNum);
            DataAdmin.instance.SetData(DataType.game_world, PlayerPrefs.GetInt("worldNum_" + slotNum.ToString()));
            DataAdmin.instance.SetData(DataType.game_stage, PlayerPrefs.GetInt("stageNum_" + slotNum.ToString()));
            DataAdmin.instance.SetData(DataType.deathNum, PlayerPrefs.GetInt("deathNum_" + slotNum.ToString()));
            SceneManager.LoadScene(PlayerPrefs.GetString("sceneName_" + slotNum.ToString()));
        }
        else
        {

        }
    }

    public static void SaveData(int slotNum)
    {
        PlayerPrefs.SetInt("stageNum_" + slotNum.ToString(), FindObjectOfType<Player>().stageNum);
        Debug.Log(FindObjectOfType<Player>().stageNum);
        PlayerPrefs.SetInt("worldNum_" + slotNum.ToString(), DataAdmin.instance.GetWorldNum(SceneManager.GetActiveScene().name));
        PlayerPrefs.SetInt("deathNum_" + slotNum.ToString(), DataAdmin.instance.GetData(DataType.deathNum));
        PlayerPrefs.SetInt("saveType_" + slotNum.ToString(), (int)SaveType.Stage);
        PlayerPrefs.SetString("sceneName_" + slotNum.ToString(), SceneManager.GetActiveScene().name);
    }

    public static void SaveDataOnMap(int slotNum)
    {

    }
}
