using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainBlock : MonoBehaviour
{
    #region Singleton Design Pattern
    private static MainBlock instance = null;
    public static MainBlock Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (MainBlock)FindObjectOfType(typeof(MainBlock));
                if (instance == null)
                    instance = (new GameObject("_MainBlock")).AddComponent<MainBlock>();
            }
            return instance;
        }
    }
    #endregion
    
    public Grid Front, Back, Left, Right, Top, Bot; // Referencias de cada grid do cubo
    public enum CubeFace { FRONT, BACK, LEFT, RIGHT, TOP, BOT };

    public Grid SelectedGrid = null; // Referencia do grid principal (que está sendo mostrado na tela)
	
	List<Movement> Memento = new List<Movement>();
	
    // Função que verifica qual grid está de frente para a camera e referencia na variável SelectedGrid
    public void VerifySelectedGrid()
    {
        SelectedGrid = Front;
        if (Back.transform.forward.z > SelectedGrid.transform.forward.z) SelectedGrid = Back;
        if (Left.transform.forward.z > SelectedGrid.transform.forward.z) SelectedGrid = Left;
        if (Right.transform.forward.z > SelectedGrid.transform.forward.z) SelectedGrid = Right;
        if (Top.transform.forward.z > SelectedGrid.transform.forward.z) SelectedGrid = Top;
        if (Bot.transform.forward.z > SelectedGrid.transform.forward.z) SelectedGrid = Bot;
    }

    void Start()
    {
        VerifySelectedGrid();
		
		//StartCoroutine(IVerifyEletricityFromAll());
    }
	
	public IEnumerator IVerifyEletricityFromAll()
	{
		yield return new WaitForSeconds(0.2f);
		
		VerifyEletricityFromAll();
	}

    public void VerifyEletricityFromAll()
    {
        Front.VerifyEletricityFromAll();
        Back.VerifyEletricityFromAll();
        Left.VerifyEletricityFromAll();
        Right.VerifyEletricityFromAll();
        Top.VerifyEletricityFromAll();
        Bot.VerifyEletricityFromAll();
    }
	
	public void AddMovement(Vector2 from, Vector2 to, Block movementedBlock)
	{
		Memento.Add(new Movement(from, to, movementedBlock));
	}
	
	public void MementoReturn()
	{
		if(Memento.Count > 0)
		{
			Movement movement = Memento[Memento.Count-1];
			movement.MovementedBlock.ForceMovement(movement.From);
			Memento.RemoveAt(Memento.Count-1);
		}
		else 
		{
			Debug.Log("Memento Empty");
		}
	}
}

public class Movement
{
	public Vector2 From, To;
	public Block MovementedBlock;
	
	public Movement(Vector2 from, Vector2 to, Block movementedBlock)
	{
		From = from;
		To = to;
		MovementedBlock = movementedBlock;
	}
}