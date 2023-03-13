using UnityEngine;
using System.Collections;

public class ClassificationInstantiate : MonoBehaviour {

    public GameObject OnPrefab, OffPrefab;
    public float distance;
    public float rootDelay;
    public float internalDelay;
    public float iconScale;

    bool instantiate = false;
    
    public void InstantiateIcons()
    {
        if (!instantiate)
        {
            StartCoroutine(IInstantiateIcons());
            instantiate = true;
        }
    }

    IEnumerator IInstantiateIcons()
    {
        GameObject[] obj = new GameObject[3];

        string performance = ID3Classification.Instance.ClassificarElemento(LevelStatistics.Instance.PlayerPerformance());

        switch (performance)
        {
            case "ruim":
                {
                    obj[0] = OnPrefab; obj[1] = OffPrefab; obj[2] = OffPrefab;
                    ClassificationDataBase.Instance.SetLevelClassification(Application.loadedLevel - Scenes.Game1, 0);
                    break;
                }
            case "regular":
                {
                    obj[0] = OnPrefab; obj[1] = OnPrefab; obj[2] = OffPrefab;
                    ClassificationDataBase.Instance.SetLevelClassification(Application.loadedLevel - Scenes.Game1, 1);
                    break;
                }
            case "bom":
                {
                    obj[0] = OnPrefab; obj[1] = OnPrefab; obj[2] = OnPrefab; 
                    ClassificationDataBase.Instance.SetLevelClassification(Application.loadedLevel - Scenes.Game1, 2);
                    break;
                }
            default: obj[0] = OffPrefab; obj[1] = OnPrefab; obj[2] = OnPrefab; break;
        }

        yield return new WaitForSeconds(rootDelay);

        for (int i = 0; i < 3; i++)
        {
            obj[i] = Instantiate(obj[i], transform.position, transform.rotation) as GameObject;

            obj[i].transform.Translate(distance * i - distance, 0, 0);
            obj[i].transform.Rotate(45, 90, 90, Space.World);
            obj[i].transform.localScale = Vector3.zero;

            iTween.ScaleTo(obj[i], iTween.Hash("scale", Vector3.one * iconScale,
                                               "time", 0.7f,
                                               "easeType", iTween.EaseType.easeOutQuint));

            yield return new WaitForSeconds(internalDelay);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for(int i=0; i<3; i++)
        {
            Gizmos.DrawSphere(new Vector3(transform.position.x + distance * i - distance, transform.position.y, transform.position.z), 0.1f);
        }
    }
}
