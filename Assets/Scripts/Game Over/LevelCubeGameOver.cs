using UnityEngine;
using System.Collections;

public class LevelCubeGameOver : MonoBehaviour {

    public GameObject cube;
    public GameObject mesh;
    public Vector3 endPosition;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(endPosition, 0.1f);
    }
}
