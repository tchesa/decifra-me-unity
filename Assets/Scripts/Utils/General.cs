using UnityEngine;
using System.Collections;

public struct Constants
{
    public const int ShuffleInteractions = 380;
	public const float ShuffligDelay = 0.025f;
    public const float EndAnimation = 12.5f;
    public const float UnpauseDelay = 1.5f;
	public const float EletrifiedSoundBreak = 0.2f;
	public static string FileDirectory = Application.persistentDataPath;
}

public struct Scenes
{
    public const int MainMenu = 1;
    public const int Game1 = 2;
    public const int Game2 = 3;
    public const int Game3 = 4;
    public const int Game4 = 5;
    public const int Game5 = 6;
    public const int Game6 = 7;
    public const int Game7 = 8;
    public const int Game8 = 9;
    public const int Game9 = 10;
    public const int MainMenuWithoutIntro = 11;
    public const int MainMenuLevelSelect = 12;
    public const int GameOver = 13;
    public const int Video = 14;
    public const int Credits = 15;
}

public class General : MonoBehaviour
{
    #region Singleton Design Pattern
    private static General instance = null;
    public static General Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (General)FindObjectOfType(typeof(General));
                if (instance == null)
                    instance = (new GameObject("General")).AddComponent<General>();
            }
            return instance;
        }
    }
    #endregion

    public static MainBlock MainBlock;
	public static ConnectionIconsGen ConnectionIconsGen;
	public float time; // Tempo de duração da fase
	public int cubesLeft; // Cubos restantes para a conclusão da fase
	public int moves; // Movimentos feitos pelo jogador na fase
	Block[] blocks;
    public bool paused;
    public bool shuffleOnStart = true;
    public bool end = false;
	public float eletrifiedSoundBreak;

    public TextMesh txtCubesLeft, txtTime, txtSeconds;
	
    public static float invokeFrequence = 0.1f;

    void Awake()
    {
        MainBlock = GameObject.Find("General Cube").GetComponent<MainBlock>();
		ConnectionIconsGen = GameObject.Find("ConnectionIconsGen").GetComponent<ConnectionIconsGen>();
		
		time = -(Constants.EndAnimation);
    }

    void Start()
    {
        MainBlock.Front.InvokeRepeating("_Update", 0.1f, invokeFrequence);
        MainBlock.Back.InvokeRepeating("_Update", 0.1f, invokeFrequence);
        MainBlock.Left.InvokeRepeating("_Update", 0.1f, invokeFrequence);
        MainBlock.Right.InvokeRepeating("_Update", 0.1f, invokeFrequence);
        MainBlock.Top.InvokeRepeating("_Update", 0.1f, invokeFrequence);
        MainBlock.Bot.InvokeRepeating("_Update", 0.1f, invokeFrequence);

        blocks = General.MainBlock.GetComponentsInChildren<Block>();
		
		moves = 0;
		eletrifiedSoundBreak = Constants.EletrifiedSoundBreak;
    }
	
	void Update()
	{
		if(eletrifiedSoundBreak > 0f) eletrifiedSoundBreak -= Time.deltaTime;
	
		if(cubesLeft > 0 && !paused) time += Time.deltaTime;
        float _cubesLeft = cubesLeft;

		cubesLeft = 0;
		foreach(Block block in blocks)
		{
			if(!block.electrified) cubesLeft++;
		}

        if (cubesLeft != _cubesLeft)
        {
            txtCubesLeft.renderer.material.color = Color.white;
        }

        if (cubesLeft != 0)
        {
            txtCubesLeft.renderer.material.color = new Color(Mathf.Clamp(txtCubesLeft.renderer.material.color.r - Time.deltaTime * 0.5f, 0.5f, 1),
                                                            Mathf.Clamp(txtCubesLeft.renderer.material.color.r - Time.deltaTime * 0.5f, 0.5f, 1),
                                                            Mathf.Clamp(txtCubesLeft.renderer.material.color.r - Time.deltaTime * 0.5f, 0.5f, 1),
                                                            1);
        }

        txtCubesLeft.text = "" + cubesLeft;
        txtTime.text = string.Format("{0}.{1:D2}", (int)(time / 60), (int)(time % 60));
        txtSeconds.text = "." + (int)(((time % 60) * 10)%10);
	}

    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        StartCoroutine(IUnpause());
    }

    IEnumerator IUnpause()
    {
        yield return new WaitForSeconds(Constants.UnpauseDelay);

        paused = false;
    }
}
