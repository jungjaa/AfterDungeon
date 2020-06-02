using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataAdmin : MonoBehaviour
{
    public static DataAdmin instance;

    /*
    private int slotNum;

    private int mapWorldNum;
    private int mapStageNum;

    private int InWorldNum;
    private int InStageNum;
    */

    private Dictionary<DataType, int> InGameData = new Dictionary<DataType, int>();
    private Dictionary<string, int> WorldNum = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                Destroy(instance.gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        InGameData.Add(DataType.map_world, -999);
        InGameData.Add(DataType.map_stage, -999);
        InGameData.Add(DataType.game_world, -999);
        InGameData.Add(DataType.game_stage, -999);
        InGameData.Add(DataType.slotNum, 1);
        InGameData.Add(DataType.deathNum, 0);

        WorldNum.Add("0_Re", 0);
        WorldNum.Add("1", 1);
        WorldNum.Add("2-1", 2);
        WorldNum.Add("2-2", 3);
        WorldNum.Add("3", 4);
        WorldNum.Add("Test", 999);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetWorldNum(string worldName)
    {
        return WorldNum[worldName];
    }
    public int GetData(DataType datatype)
    {
        return InGameData[datatype];
    }
    public void SetData(DataType datatype, int data)
    {
        InGameData[datatype] = data;
    }
    public void IncrementData(DataType datatype)
    {
        InGameData[datatype]++;
    }
}
public enum DataType { map_world, map_stage, game_world, game_stage, slotNum, deathNum};

public enum SaveType { WorldMap, Stage};
