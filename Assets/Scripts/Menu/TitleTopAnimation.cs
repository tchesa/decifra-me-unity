using UnityEngine;
using System.Collections;

public class TitleTopAnimation : MonoBehaviour
{
    public string finalText;
    public float delayIn;
    public float timeLocked;

    float[] _animationTime;
    float[] _startTime;
    char[] _finalText;
    char[] text;
    public float speed = 1;

    bool hide = true;
    float _time;

    void Start()
    {
        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];
        _time = 0f;

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = ' ';
        }

        GetComponent<TextMesh>().text = new string(text);
    }

    /*void Update()
    {
        if (hide && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)) && timeLocked < _time)
        {
            StartCoroutine(FadeIn());
            hide = false;
        }

        _time += Time.deltaTime;
    }*/

    public void WakeUp() 
    {
        StartCoroutine(FadeIn());
    }

    public void ChangeName(string name)
    {
        StartCoroutine(IChangeName(name));
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delayIn);

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = ' ';
        }

        for (int i = 0; i < _finalText.Length; i++)
        {
            text[i] = '*';
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        for (int i = 0; i < _finalText.Length; i++)
        {
            text[i] = _finalText[i];
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }
    }

    IEnumerator FadeOut()
    {
        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = _finalText[i];
        }

        GetComponent<TextMesh>().text = new string(text);

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = _finalText[i];
        }

        for (int i = 0; i < _finalText.Length; i++)
        {
            text[i] = '*';
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        for (int i = 0; i < _finalText.Length; i++)
        {
            text[i] = ' ';
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }
    }
    
    IEnumerator IChangeName(string name) 
    {
        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = _finalText[i];
        }

        GetComponent<TextMesh>().text = new string(text);

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = _finalText[i];
        }

        for (int i = 0; i < _finalText.Length; i++)
        {
            text[i] = '*';
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        for (int i = 0; i < _finalText.Length; i++)
        {
            text[i] = ' ';
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        finalText = name;
        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];

        delayIn = 0f;

        StartCoroutine(FadeIn());
    }
}
