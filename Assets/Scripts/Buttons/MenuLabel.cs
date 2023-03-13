using UnityEngine;
using System.Collections;

public class MenuLabel : MonoBehaviour
{
    string finalText;
    public float delayIn, delayOut;
    public bool fadeOutEnabled = true;
    public float timeLocked;
    public bool inOut = true;

    float[] _animationTime;
    float[] _startTime;
    char[] _finalText;
    char[] text;
    public float speed = 1;
    public float colorSpeed;

    Color mainColor;
	
    public void SetFinalText(string newText)
    {
        finalText = newText;
    }

    void Start()
    {
        mainColor = GetComponent<Renderer>().material.color;
        finalText = GetComponent<TextMesh>().text;              
        GetComponent<TextMesh>().text = "";
    }

    void Update()
    {
        GetComponent<TextMesh>().GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<TextMesh>().GetComponent<Renderer>().material.color, mainColor, Time.deltaTime * colorSpeed);
	}

    public void WakeUp()
    {
        StartCoroutine(FadeIn());
        if (gameObject.GetComponent<Collider>()) GetComponent<Collider>().enabled = true;
    }

    public void Sleep()
    {
        StartCoroutine(FadeOut());
        if(gameObject.GetComponent<Collider>()) GetComponent<Collider>().enabled = false;
    }

    void OnMouseDown() 
    {
        if(inOut) StartCoroutine(FadeInOut());
        GetComponent<TextMesh>().GetComponent<Renderer>().material.color = Color.red;
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delayIn);

		_finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = ' ';
        }

        GetComponent<TextMesh>().text = new string(text);

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
    }

    IEnumerator FadeInOut()
    {
        _finalText = finalText.ToCharArray();

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
}