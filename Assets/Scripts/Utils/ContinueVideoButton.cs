using UnityEngine;
using System.Collections;

public class ContinueVideoButton : MonoBehaviour
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

    Color targetColor = Color.white;

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
        GetComponent<TextMesh>().GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<TextMesh>().GetComponent<Renderer>().material.color, targetColor, Time.deltaTime * 5f);
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
        GetComponent<Collider>().enabled = true;
    }

    void OnMouseEnter()
    {
        targetColor = Color.red;

        AudioSource.PlayClipAtPoint(SoundArchive.Instance.restart, Camera.main.transform.position, 0.5f);
    }

    void OnMouseExit()
    {
        targetColor = Color.white;

    }

    void OnMouseDown()
    {

        AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position, 0.5f);

        FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
        fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);

        //ClassificationDataBase.Instance.SetLevelClassification(Application.loadedLevel - Scenes.Game1, 


        //fadeInstance.levelIndex = Application.loadedLevel + 1;
        fadeInstance.levelIndex = Scenes.MainMenu;
    }
}
