using UnityEngine;
using System.Collections;

public class ContinueButton : MonoBehaviour
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
	
	public FadeInstantiate fade;
	
	Color mainColor = Color.black;
	
    void Start()
    {
        _finalText = finalText.ToCharArray();
        text = new char[_finalText.Length];

        for (int i = 0; i < text.Length; i++)
        {
            text[i] = ' ';
        }

        GetComponent<TextMesh>().text = new string(text);
    }
	
	void Update()
	{
		GetComponent<TextMesh>().renderer.material.color = Color.Lerp(GetComponent<TextMesh>().renderer.material.color, mainColor, Time.deltaTime * 5f);
	}

    public void WakeUp()
    {
        StartCoroutine(FadeIn());
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
		collider.enabled = true;
    }
	
	void OnMouseDown()
	{
		GetComponent<TextMesh>().renderer.material.color = Color.red;
	
		if(SoundArchive.Instance.menuButton)
			AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position, 0.5f);
		else
			Debug.LogWarning("Null Sound: menuButton");

		FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
        fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
		
		//ClassificationDataBase.Instance.SetLevelClassification(Application.loadedLevel - Scenes.Game1, 
			
		
        //fadeInstance.levelIndex = Application.loadedLevel + 1;
        if (ClassificationDataBase.Instance.Complete())
        {
            fadeInstance.levelIndex = Scenes.GameOver;
        }
        else fadeInstance.levelIndex = Scenes.MainMenuLevelSelect;
	}
}
