using UnityEngine;
using System.Collections;

public class RotateCubeMesh : MonoBehaviour
{

    Vector3 mouseClick; // Variável auxiliar que armazena a posição do clique do mouse no mesh
    Vector3 defaultRotation; // Variável auxiliar que armazena a rotação do cubo antes que ele começa a ser rotacionado
    public Transform mainBlock;
    public float rotationFactor = 1;

    public enum Rotation { HORIZONTAL, VERTICAL };
    public Rotation rotation;

    void OnMouseDown()
    {
        // Para a movimentação iTween atual do cubo, se existir
        if (mainBlock.gameObject.GetComponent<iTween>()) Destroy(mainBlock.gameObject.GetComponent<iTween>());

        General.MainBlock.SelectedGrid = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast com origem na MainCamera em direção a posiçao do mouse
        RaycastHit hit; // Colisão do Raycast
        if (Physics.Raycast(ray, out hit, 100))
        {
            mouseClick = hit.point - transform.position; // Armazena a posiçao onde o mouse clicou no bloco
            defaultRotation = mainBlock.eulerAngles;
        }

        General.ConnectionIconsGen.HideIcons();
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast com origem na MainCamera em direção a posiçao do mouse
        RaycastHit hit; // Colisão do Raycast
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 mouseDrag = hit.point - transform.position; // Armazena a posiçao onde o mouse clicou no bloco

            // Aplica a rotação inicial do cubo e rotaciona o cubo em relação à sua rotação inicial
            mainBlock.eulerAngles = defaultRotation;
            if (rotation == Rotation.HORIZONTAL)
                mainBlock.Rotate(new Vector3(0, (mouseDrag - mouseClick).x * rotationFactor, 0), Space.World);
            else
                mainBlock.Rotate(new Vector3((mouseDrag - mouseClick).y * -rotationFactor, 0, 0), Space.World);
        }
    }

    void OnMouseUp()
    {
        // Ajusta a rotação do cubo
        iTween.RotateTo(mainBlock.gameObject, iTween.Hash("rotation", new Vector3(Mathf.Round(mainBlock.eulerAngles.x / 90) * 90, Mathf.Round(mainBlock.eulerAngles.y / 90) * 90, Mathf.Round(mainBlock.eulerAngles.z / 90) * 90),
                                                          "easeType", iTween.EaseType.easeOutQuint,
                                                          "speed", 70,
                                                          //"onstart", "StartTween",
                                                          //"onstarttarget", this.gameObject,
                                                          "oncomplete", "EndTween",
                                                          "oncompletetarget", this.gameObject));
    }
    /*
    void StartTween() 
    {
        
    }
    */
    void EndTween()
    {
        General.MainBlock.VerifySelectedGrid();
        General.ConnectionIconsGen.UpdateIcons();
    }
}
