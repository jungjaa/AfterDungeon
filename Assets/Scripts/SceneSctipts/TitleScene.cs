using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    private float elapsed = 0f;

    public Text[] firstText;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        changeAlpha(0f);
        for (int i = 0; i < firstText.Length; i++)
        {
            firstText[i].gameObject.SetActive(false);
        }
        first = false;
        StartCoroutine(Title());
    }

    IEnumerator Title()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < firstText.Length; i++)
        {
            firstText[i].gameObject.SetActive(true);
        }
        elapsed = 0f;
        while(elapsed<=1.5f)
        {
            elapsed += Time.deltaTime;
            changeAlpha(elapsed / 1.5f);
            yield return null;
        }
        elapsed = 0f;
        changeAlpha(1f);
        yield return new WaitForSeconds(1f);
        while (elapsed <= 1.5f)
        {
            elapsed += Time.deltaTime;
            changeAlpha((1.5f-elapsed) / 1.5f);
            yield return null;
        }
        changeAlpha(0f);
    }

    void changeAlpha(float value)
    {
        for (int i = 0; i < firstText.Length; i++)
        {
            firstText[i].color = new Color(firstText[i].color.r, firstText[i].color.b, firstText[i].color.g, value);
        }
    }
}
