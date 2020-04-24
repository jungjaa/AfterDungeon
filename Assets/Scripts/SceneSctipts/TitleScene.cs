using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    private float elapsed = 0f;

    public Text[] firstText;
    private bool first;

    public Image titleImage;
    private bool second;

    public Text secondText;
    private bool canPress;

    // Start is called before the first frame update
    void Start()
    {
        titleImage.gameObject.SetActive(false);
        secondText.color = new Color(secondText.color.r, secondText.color.b, secondText.color.g, 0);
        secondText.gameObject.SetActive(false);
        changeAlpha(0f);
        for (int i = 0; i < firstText.Length; i++)
        {
            firstText[i].gameObject.SetActive(false);
        }
        canPress = false;
        first = false;
        second = false;
        StartCoroutine(Title());
    }

    void Update()
    {
        if(first)
        {
            StartCoroutine(TitleFadeIn());
            first = false;
        }
        if(second)
        {
            canPress = true;
            StartCoroutine(PressTitle());
            second = false;
        }
        if(canPress)
        {
            bool c_pressed = Input.GetKeyDown(KeyCode.C);
            if (c_pressed)
            {
                SceneManager.LoadScene("0");
            }
        }
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
        yield return new WaitForSeconds(0.5f);
        first = true;
    }

    IEnumerator TitleFadeIn()
    {
        elapsed = 0f;
        float fadeTime = 0.4f;
        titleImage.gameObject.SetActive(true);
        while(true)
        {
            elapsed += Time.deltaTime;
            if(elapsed<fadeTime)
            {
                titleImage.color = new Color(titleImage.color.r, titleImage.color.b, titleImage.color.g, elapsed/fadeTime);
            }
            else
            {
                titleImage.color = new Color(titleImage.color.r, titleImage.color.b, titleImage.color.g, 1f);
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        second = true;
    }

    IEnumerator PressTitle()
    {
        elapsed = 0;
        secondText.gameObject.SetActive(true);

        while(true)
        {
            float fadeTime = 0.6f;
            elapsed += Time.deltaTime;
            if(elapsed< fadeTime)
            {
                secondText.color = new Color(secondText.color.r, secondText.color.b, secondText.color.g, elapsed/ fadeTime);
            }
            else if(fadeTime <= elapsed && elapsed< fadeTime + 0.6f)
            {
                secondText.color = new Color(secondText.color.r, secondText.color.b, secondText.color.g, 1f);
            }
            else if(elapsed>= fadeTime + 0.6f && elapsed< 2*fadeTime + 0.6f)
            {
                secondText.color = new Color(secondText.color.r, secondText.color.b, secondText.color.g, (2 * fadeTime + 0.6f - elapsed)/ fadeTime);
            }
            else
            {
                secondText.color = new Color(secondText.color.r, secondText.color.b, secondText.color.g, 0f);
                elapsed = 0f;
            }
            yield return null;
        }
    }

    void changeAlpha(float value)
    {
        for (int i = 0; i < firstText.Length; i++)
        {
            firstText[i].color = new Color(firstText[i].color.r, firstText[i].color.b, firstText[i].color.g, value);
        }
    }
}
