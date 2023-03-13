using UnityEngine;
using System.Collections;

public class PressMessageAnimation : MonoBehaviour
{
    public string finalText;
    public float delayIn, delayOut;
    public bool fadeOutEnabled = true;
    public float timeLocked;

    float[] _animationTime;
    float[] _startTime;
    char[] _finalText;
    char[] text;
    public float speed = 1;

    bool idle = false;
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

        StartCoroutine(FadeIn());
    }

    void Update()
    {
        /*if (fadeOutEnabled && idle && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)) && timeLocked < _time)
        {
            StartCoroutine(FadeOut());
            idle = false;
        }*/

        _time += Time.deltaTime;
    }

    public void WakeUp()
    {
        StartCoroutine(FadeOut());
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

        idle = true;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayOut);

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

        Destroy(gameObject);
    }
}
