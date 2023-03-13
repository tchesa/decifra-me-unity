using UnityEngine;
using System.Collections;

public class TitleIntroAnimation : MonoBehaviour
{
    public string finalText;
    public float animationTime;
    public float startTime;
    public float idleTime;
    public float inDelay, outDelay;
    public float timeLocked;

    float[] _animationTime;
    float[] _startTime;
    char[] _finalText;
    char[] text;
    bool idle = false;
    float _time;

    public float speed = 1;

    void Start()
    {
        StartCoroutine(FadeIn());
        _time = 0f;
    }

    void Update()
    {
        /*if (idle && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)) && timeLocked < _time)
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
        yield return new WaitForSeconds(inDelay);

        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];
        _animationTime = new float[text.Length];
        _startTime = new float[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = ' ';
            _startTime[i] = Random.value * startTime;
            _animationTime[i] = Random.value * animationTime;
        }

        for (float i = 0; i < animationTime + startTime; i += Time.deltaTime * speed)
        {
            for (int j = 0; j < text.Length; j++)
            {
                if (i > _startTime[j])
                {
                    if (i < _animationTime[j] + _startTime[j] - Time.deltaTime) text[j] = (char)((int)(Random.value * 94) + 32);
                    else text[j] = _finalText[j];
                }
            }
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }
        idle = true;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(outDelay);

        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];
        _animationTime = new float[text.Length];

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
