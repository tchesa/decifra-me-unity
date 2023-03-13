using UnityEngine;
using System.Collections;

public class FadeInOutStringAnimation : MonoBehaviour
{
    public string finalText;
	public float animationTime;
	public float startTime;
    public float idleTime;
    public float delay;

	float[] _animationTime;
	float[] _startTime;
	char[] _finalText;
	char[] text;
    public float speed = 1;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeIn());
    }
	
	IEnumerator FadeIn()
	{
        _finalText = finalText.ToCharArray();
		text = new char[_finalText.Length];
		_animationTime = new float[text.Length];
		_startTime = new float[text.Length];

		for(int i=0; i<text.Length; i++)
		{
			text[i] = ' ';
			_startTime[i] = Random.value * startTime;
			_animationTime[i] = Random.value * animationTime;
		}
		
		for(float i=0; i<animationTime + startTime; i+=Time.deltaTime * speed)
		{
			for(int j=0; j<text.Length; j++)
			{
				if(i>_startTime[j])
				{
					if(i<_animationTime[j] + _startTime[j] - Time.deltaTime) text[j] = (char)((int)(Random.value * 94) + 32);
					else text[j] = _finalText[j];
				}
			}
			GetComponent<TextMesh>().text = new string(text);
			yield return new WaitForSeconds(Time.deltaTime * speed);
		}
        yield return new WaitForSeconds(idleTime);
        StartCoroutine(FadeOut());
	}

    IEnumerator FadeOut()
    {
        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];
        _animationTime = new float[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = _finalText[i];
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
                    else text[j] = ' ';
                }
            }
            GetComponent<TextMesh>().text = new string(text);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        Destroy(gameObject);
    }
}
