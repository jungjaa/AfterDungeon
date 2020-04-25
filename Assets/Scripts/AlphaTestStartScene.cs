using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaTestStartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SceneManager.LoadScene("AlphaTestStartScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string SceneNum)
    {
        SceneManager.LoadScene(SceneNum);
    }
}
