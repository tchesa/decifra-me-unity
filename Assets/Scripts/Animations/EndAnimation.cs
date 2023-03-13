using UnityEngine;
using System.Collections;

public class EndAnimation : MonoBehaviour {

	public FadeInStringAnim congratulations1, congratulations2;
	public ContinueButton continueButton;
	public GameObject backCollider;
    public EndStripeAnimation stripe1, stripe2, stripe3;
    public LightEndAnimation lightEndAnimation;
    public ClassificationInstantiate classificationInstantiate;

	float time;
	
	void Start()
	{
		time = 0f;
	}
	
	void Update()
	{
		time += Time.deltaTime;
		
		if(General.Instance.cubesLeft <= 0 && time > Constants.EndAnimation)
		{
            General.Instance.end = true;
			WakeUp();
		}
	}
	
	void WakeUp()
	{
		if(SoundArchive.Instance.pause)
			AudioSource.PlayClipAtPoint(SoundArchive.Instance.pause, Camera.main.transform.position);
		else
			Debug.LogWarning("Null Sound: pause");

		congratulations1.WakeUp();
        congratulations2.WakeUp();
		continueButton.WakeUp();
        stripe1.WakeUp();
        stripe2.WakeUp();
        stripe3.WakeUp();
        lightEndAnimation.WakeUp();
        classificationInstantiate.InstantiateIcons();

        backCollider.collider.enabled = true;
		Destroy(this);
	}
}
