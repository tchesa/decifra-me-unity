using UnityEngine;
using System.Collections;

public class ConnectionIconsGen : MonoBehaviour
{
    public bool gizmos = true;
    public float distance = 0f;
    public int gridSize = 0;
    public float prefabScale = 0.4f;
	public float delay = 0.05f;
    public GameObject connectionIconPrefab;

    public ConnectionIcon[] topIcons;
    public ConnectionIcon[] botIcons;
    public ConnectionIcon[] leftIcons;
    public ConnectionIcon[] rightIcons;
	
	public Block[] debugBlocks;
	
    void Start() 
    {

        // Inicializando os vetores
        topIcons =   new ConnectionIcon[gridSize];
        botIcons =   new ConnectionIcon[gridSize];
        leftIcons =  new ConnectionIcon[gridSize];
        rightIcons = new ConnectionIcon[gridSize];
		
		debugBlocks = new Block[gridSize];

        // Instanciando icones do topo
        for (int i = 0; i < gridSize; i++)
        {
            GameObject g = Instantiate(connectionIconPrefab, new Vector3(transform.position.x - (i - gridSize/2) - 0.5f, this.transform.position.y + (gridSize / 2) + distance, this.transform.position.z), transform.rotation) as GameObject;
            g.transform.localScale = Vector3.one * prefabScale;
            g.transform.parent = this.transform;
            topIcons[i] = g.GetComponent<ConnectionIcon>();
			//yield return new WaitForSeconds(delay);
        }
		// Instanciando icones da direita
        for (int i = 0; i < gridSize; i++)
        {
            GameObject g = Instantiate(connectionIconPrefab, new Vector3(this.transform.position.y - (gridSize / 2) - distance, transform.position.x - (i - gridSize/2) - 0.5f, this.transform.position.z), transform.rotation) as GameObject;
            g.transform.localScale = Vector3.one * prefabScale;
            g.transform.parent = this.transform;
            rightIcons[i] = g.GetComponent<ConnectionIcon>();
			//yield return new WaitForSeconds(delay);
        }
        // Instanciando icones de baixo
        for (int i = 0; i < gridSize; i++)
        {
            GameObject g = Instantiate(connectionIconPrefab, new Vector3(transform.position.x + (i - gridSize/2) + 0.5f, this.transform.position.y - (gridSize / 2) - distance, this.transform.position.z), transform.rotation) as GameObject;
            g.transform.localScale = Vector3.one * prefabScale;
            g.transform.parent = this.transform;
            botIcons[gridSize - 1 - i] = g.GetComponent<ConnectionIcon>();
			//yield return new WaitForSeconds(delay);
        }
        // Instanciando icones da esquerda
        for (int i = 0; i < gridSize; i++)
        {
            GameObject g = Instantiate(connectionIconPrefab, new Vector3(this.transform.position.y + (gridSize / 2) + distance, transform.position.x + (i - gridSize/2) + 0.5f, this.transform.position.z), transform.rotation) as GameObject;
            g.transform.localScale = Vector3.one * prefabScale;
            g.transform.parent = this.transform;
            leftIcons[gridSize - 1 - i] = g.GetComponent<ConnectionIcon>();
			//yield return new WaitForSeconds(delay);
        }

        Invoke("UpdateIcons", Constants.EndAnimation + 2.5f);
    }

	public void UpdateIcons()
    {
        #region VERIFICANDO FACE FRONT
        if (General.MainBlock.SelectedGrid.gridFace ==  MainBlock.CubeFace.FRONT)
        {
            // Verificando ligações do topo
            for (int i = 0; i < General.MainBlock.Top.gridSize; i++)
            {
                if (General.MainBlock.Top.GetBlock(i, General.MainBlock.Top.gridSize - 1) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Top.GetBlock(i, General.MainBlock.Top.gridSize - 1).portDivision.ToString().Contains("BOT")) // O bloco não tem licgação com o grid
                    topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Top.GetBlock(i, General.MainBlock.Top.gridSize - 1).electrified)
                    topIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    topIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações de baixo
            for (int i = 0; i < General.MainBlock.Bot.gridSize; i++)
            {
                if (General.MainBlock.Bot.GetBlock(i, 0) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Bot.GetBlock(i, 0).portDivision.ToString().Contains("TOP")) // O bloco não tem licgação com o grid
                    botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Bot.GetBlock(i, 0).electrified)
                    botIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    botIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da esquerda
            for (int i = 0; i < General.MainBlock.Left.gridSize; i++)
            {
                if (General.MainBlock.Left.GetBlock(General.MainBlock.Right.gridSize - 1, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Left.GetBlock(General.MainBlock.Right.gridSize - 1, i).portDivision.ToString().Contains("RIGHT")) // O bloco não tem licgação com o grid
                    leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Left.GetBlock(General.MainBlock.Right.gridSize - 1, i).electrified)
                    leftIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    leftIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da direita
            for (int i = 0; i < General.MainBlock.Right.gridSize; i++)
            {
                if (General.MainBlock.Right.GetBlock(0, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Right.GetBlock(0, i).portDivision.ToString().Contains("LEFT")) // O bloco não tem licgação com o grid
                    rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Right.GetBlock(0, i).electrified)
                    rightIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    rightIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			// Verificando rotação da face
			string ori = General.MainBlock.Front.Orientation();
			switch (ori)
			{
				case "UP" : transform.eulerAngles = new Vector3 (0f, 0f, 0f); break;
				case "RIGHT" : transform.eulerAngles = new Vector3 (0f, 0f, 90f); break;
				case "DOWN" : transform.eulerAngles = new Vector3 (0f, 0f, 180f); break;
				case "LEFT" : transform.eulerAngles = new Vector3 (0f, 0f, 270f); break;
				default : Debug.Log ("OPS"); break;
			}
        }
        #endregion

        #region VERIFICANDO FACE BACK
        if (General.MainBlock.SelectedGrid.gridFace == MainBlock.CubeFace.BACK)
        {
            // Verificando ligações do topo
            for (int i = 0; i < General.MainBlock.Top.gridSize; i++)
            {
                if (General.MainBlock.Top.GetBlock(General.MainBlock.Top.gridSize - 1 - i, 0) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Top.GetBlock(General.MainBlock.Top.gridSize - 1 - i, 0).portDivision.ToString().Contains("TOP")) // O bloco não tem licgação com o grid
                    topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Top.GetBlock(General.MainBlock.Top.gridSize - 1 - i, 0).electrified)
                    topIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    topIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações de baixo
            for (int i = 0; i < General.MainBlock.Bot.gridSize; i++)
            {
                if (General.MainBlock.Bot.GetBlock(General.MainBlock.Bot.gridSize - 1 - i, General.MainBlock.Bot.gridSize - 1) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Bot.GetBlock(General.MainBlock.Bot.gridSize - 1 - i, General.MainBlock.Bot.gridSize - 1).portDivision.ToString().Contains("BOT")) // O bloco não tem licgação com o grid
                    botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Bot.GetBlock(General.MainBlock.Bot.gridSize - 1 - i, General.MainBlock.Bot.gridSize - 1).electrified)
                    botIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    botIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da esquerda
            for (int i = 0; i < General.MainBlock.Right.gridSize; i++)
            {
                if (General.MainBlock.Right.GetBlock(General.MainBlock.Right.gridSize - 1, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Right.GetBlock(General.MainBlock.Right.gridSize - 1, i).portDivision.ToString().Contains("RIGHT")) // O bloco não tem licgação com o grid
                    leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Right.GetBlock(General.MainBlock.Right.gridSize - 1, i).electrified)
                    leftIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    leftIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da direita
            for (int i = 0; i < General.MainBlock.Left.gridSize; i++)
            {
                if (General.MainBlock.Left.GetBlock(0, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Left.GetBlock(0, i).portDivision.ToString().Contains("LEFT")) // O bloco não tem licgação com o grid
                    rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Left.GetBlock(0, i).electrified)
                    rightIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    rightIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			// Verificando rotação da face
			string ori = General.MainBlock.Back.Orientation();
			switch (ori)
			{
				case "UP" : transform.eulerAngles = new Vector3 (0f, 0f, 0f); break;
				case "RIGHT" : transform.eulerAngles = new Vector3 (0f, 0f, 90f); break;
				case "DOWN" : transform.eulerAngles = new Vector3 (0f, 0f, 180f); break;
				case "LEFT" : transform.eulerAngles = new Vector3 (0f, 0f, 270f); break;
				default : Debug.Log ("OPS"); break;
			}
        }
        #endregion

        #region VERIFICANDO FACE LEFT
        if (General.MainBlock.SelectedGrid.gridFace == MainBlock.CubeFace.LEFT)
        {
            // Verificando ligações do topo
            for (int i = 0; i < General.MainBlock.Top.gridSize; i++)
            {
                if (General.MainBlock.Top.GetBlock(0, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Top.GetBlock(0, i).portDivision.ToString().Contains("LEFT")) // O bloco não tem licgação com o grid
                    topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Top.GetBlock(0, i).electrified)
                    topIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    topIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações de baixo
            for (int i = 0; i < General.MainBlock.Bot.gridSize; i++)
            {
                if (General.MainBlock.Bot.GetBlock(0, General.MainBlock.Bot.gridSize - 1 - i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Bot.GetBlock(0, General.MainBlock.Bot.gridSize - 1 - i).portDivision.ToString().Contains("LEFT")) // O bloco não tem licgação com o grid
                    botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Bot.GetBlock(0, General.MainBlock.Bot.gridSize - 1 - i).electrified)
                    botIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    botIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da esquerda
            for (int i = 0; i < General.MainBlock.Back.gridSize; i++)
            {
                if (General.MainBlock.Back.GetBlock(General.MainBlock.Back.gridSize - 1, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Back.GetBlock(General.MainBlock.Back.gridSize - 1, i).portDivision.ToString().Contains("RIGHT")) // O bloco não tem licgação com o grid
                    leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Back.GetBlock(General.MainBlock.Back.gridSize - 1, i).electrified)
                    leftIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    leftIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da direita
            for (int i = 0; i < General.MainBlock.Front.gridSize; i++)
            {
                if (General.MainBlock.Front.GetBlock(0, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Front.GetBlock(0, i).portDivision.ToString().Contains("LEFT")) // O bloco não tem licgação com o grid
                    rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Front.GetBlock(0, i).electrified)
                    rightIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    rightIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			// Verificando rotação da face
			string ori = General.MainBlock.Left.Orientation();
			switch (ori)
			{
				case "UP" : transform.eulerAngles = new Vector3 (0f, 0f, 0f); break;
				case "RIGHT" : transform.eulerAngles = new Vector3 (0f, 0f, 90f); break;
				case "DOWN" : transform.eulerAngles = new Vector3 (0f, 0f, 180f); break;
				case "LEFT" : transform.eulerAngles = new Vector3 (0f, 0f, 270f); break;
				default : Debug.Log ("OPS"); break;
			}
        }
        #endregion

        #region VERIFICANDO FACE RIGHT
        if (General.MainBlock.SelectedGrid.gridFace == MainBlock.CubeFace.RIGHT)
        {
            // Verificando ligações do topo
            for (int i = 0; i < General.MainBlock.Top.gridSize; i++)
            {
                if (General.MainBlock.Top.GetBlock(General.MainBlock.Top.gridSize - 1, General.MainBlock.Top.gridSize - 1 - i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Top.GetBlock(General.MainBlock.Top.gridSize - 1, General.MainBlock.Top.gridSize - 1 - i).portDivision.ToString().Contains("RIGHT")) // O bloco não tem licgação com o grid
                    topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Top.GetBlock(General.MainBlock.Top.gridSize - 1, General.MainBlock.Top.gridSize - 1 - i).electrified)
                    topIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    topIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações de baixo
            for (int i = 0; i < General.MainBlock.Bot.gridSize; i++)
            {
                if (General.MainBlock.Bot.GetBlock(General.MainBlock.Bot.gridSize - 1, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Bot.GetBlock(General.MainBlock.Bot.gridSize - 1, i).portDivision.ToString().Contains("RIGHT")) // O bloco não tem licgação com o grid
                    botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Bot.GetBlock(General.MainBlock.Bot.gridSize - 1, i).electrified)
                    botIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    botIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da esquerda
            for (int i = 0; i < General.MainBlock.Front.gridSize; i++)
            {
                if (General.MainBlock.Front.GetBlock(General.MainBlock.Front.gridSize - 1, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Front.GetBlock(General.MainBlock.Front.gridSize - 1, i).portDivision.ToString().Contains("RIGHT")) // O bloco não tem licgação com o grid
                    leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Front.GetBlock(General.MainBlock.Front.gridSize - 1, i).electrified)
                    leftIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    leftIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da direita
            for (int i = 0; i < General.MainBlock.Back.gridSize; i++)
            {
                if (General.MainBlock.Back.GetBlock(0, i) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Back.GetBlock(0, i).portDivision.ToString().Contains("LEFT")) // O bloco não tem licgação com o grid
                    rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Back.GetBlock(0, i).electrified)
                    rightIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    rightIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			// Verificando rotação da face
			string ori = General.MainBlock.Right.Orientation();
			switch (ori)
			{
				case "UP" : transform.eulerAngles = new Vector3 (0f, 0f, 0f); break;
				case "RIGHT" : transform.eulerAngles = new Vector3 (0f, 0f, 90f); break;
				case "DOWN" : transform.eulerAngles = new Vector3 (0f, 0f, 180f); break;
				case "LEFT" : transform.eulerAngles = new Vector3 (0f, 0f, 270f); break;
				default : Debug.Log ("OPS"); break;
			}
        }
        #endregion
		
        #region VERIFICANDO FACE TOP
        if (General.MainBlock.SelectedGrid.gridFace == MainBlock.CubeFace.TOP)
        {
            // Verificando ligações do topo
            for (int i = 0; i < General.MainBlock.Back.gridSize; i++)
            {
                if (General.MainBlock.Back.GetBlock(General.MainBlock.Back.gridSize - 1 - i, 0) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Back.GetBlock(General.MainBlock.Back.gridSize - 1 - i, 0).portDivision.ToString().Contains("TOP")) // O bloco não tem licgação com o grid
                    topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Back.GetBlock(General.MainBlock.Back.gridSize - 1 - i, 0).electrified)
                    topIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    topIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			
            // Verificando ligações de baixo
            for (int i = 0; i < General.MainBlock.Front.gridSize; i++)
            {
                if (General.MainBlock.Front.GetBlock(i, 0) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Front.GetBlock(i, 0).portDivision.ToString().Contains("TOP")) // O bloco não tem licgação com o grid
                    botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Front.GetBlock(i, 0).electrified)
                    botIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    botIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da esquerda
            for (int i = 0; i < General.MainBlock.Left.gridSize; i++)
            {
                if (General.MainBlock.Left.GetBlock(i, 0) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Left.GetBlock(i, 0).portDivision.ToString().Contains("TOP")) // O bloco não tem licgação com o grid
                    leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Left.GetBlock(i, 0).electrified)
                    leftIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    leftIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da direita
            for (int i = 0; i < General.MainBlock.Right.gridSize; i++)
            {
                if (General.MainBlock.Right.GetBlock(General.MainBlock.Right.gridSize - 1 - i, 0) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Right.GetBlock(General.MainBlock.Right.gridSize - 1 - i, 0).portDivision.ToString().Contains("TOP")) // O bloco não tem licgação com o grid
                    rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Right.GetBlock(General.MainBlock.Right.gridSize - 1 - i, 0).electrified)
                    rightIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    rightIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			// Verificando rotação da face
			string ori = General.MainBlock.Top.Orientation();
			switch (ori)
			{
				case "UP" : transform.eulerAngles = new Vector3 (0f, 0f, 0f); break;
				case "RIGHT" : transform.eulerAngles = new Vector3 (0f, 0f, 90f); break;
				case "DOWN" : transform.eulerAngles = new Vector3 (0f, 0f, 180f); break;
				case "LEFT" : transform.eulerAngles = new Vector3 (0f, 0f, 270f); break;
				default : Debug.Log ("OPS"); break;
			}
        }
        #endregion
		
        #region VERIFICANDO FACE BOT
        if (General.MainBlock.SelectedGrid.gridFace == MainBlock.CubeFace.BOT)
        {
            // Verificando ligações do topo
            for (int i = 0; i < General.MainBlock.Front.gridSize; i++)
            {
                if (General.MainBlock.Front.GetBlock(i, General.MainBlock.Front.gridSize - 1) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Front.GetBlock(i, General.MainBlock.Front.gridSize - 1).portDivision.ToString().Contains("BOT")) // O bloco não tem licgação com o grid
                    topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Front.GetBlock(i, General.MainBlock.Front.gridSize - 1).electrified)
                    topIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    topIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações de baixo
            for (int i = 0; i < General.MainBlock.Back.gridSize; i++)
            {
                if (General.MainBlock.Back.GetBlock(General.MainBlock.Front.gridSize - 1 - i, General.MainBlock.Back.gridSize - 1) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Back.GetBlock(General.MainBlock.Front.gridSize - 1 - i, General.MainBlock.Back.gridSize - 1).portDivision.ToString().Contains("BOT")) // O bloco não tem licgação com o grid
                    botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Back.GetBlock(General.MainBlock.Front.gridSize - 1 - i, General.MainBlock.Back.gridSize - 1).electrified)
                    botIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    botIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da esquerda
            for (int i = 0; i < General.MainBlock.Left.gridSize; i++)
            {
                if (General.MainBlock.Left.GetBlock(General.MainBlock.Left.gridSize - 1 - i, General.MainBlock.Left.gridSize - 1) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Left.GetBlock(General.MainBlock.Left.gridSize - 1 - i, General.MainBlock.Left.gridSize - 1).portDivision.ToString().Contains("BOT")) // O bloco não tem licgação com o grid
                    leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Left.GetBlock(General.MainBlock.Left.gridSize - 1 - i, General.MainBlock.Left.gridSize - 1).electrified)
                    leftIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    leftIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
            // Verificando ligações da direita
            for (int i = 0; i < General.MainBlock.Right.gridSize; i++)
            {
                if (General.MainBlock.Right.GetBlock(i, General.MainBlock.Right.gridSize - 1) == null || // Se o bloco for igual a nulo ou...
                    !General.MainBlock.Right.GetBlock(i, General.MainBlock.Right.gridSize - 1).portDivision.ToString().Contains("BOT")) // O bloco não tem licgação com o grid
                    rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
                else if (General.MainBlock.Right.GetBlock(i, General.MainBlock.Right.gridSize - 1).electrified)
                    rightIcons[i].iconState = ConnectionIcon.IconState.ON;
                else
                    rightIcons[i].iconState = ConnectionIcon.IconState.OFF;
            }
			// Verificando rotação da face
			string ori = General.MainBlock.Bot.Orientation();
			switch (ori)
			{
				case "UP" : transform.eulerAngles = new Vector3 (0f, 0f, 0f); break;
				case "RIGHT" : transform.eulerAngles = new Vector3 (0f, 0f, 90f); break;
				case "DOWN" : transform.eulerAngles = new Vector3 (0f, 0f, 180f); break;
				case "LEFT" : transform.eulerAngles = new Vector3 (0f, 0f, 270f); break;
				default : Debug.Log ("OPS"); break;
			}
        }
        #endregion
	}

    public void HideIcons()
    {
        for (int i = 0; i < gridSize; i++)
        {
            topIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
            botIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
            leftIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
            rightIcons[i].iconState = ConnectionIcon.IconState.HIDDEN;
        }
    }

    void OnDrawGizmos()
    {
        if (gizmos)
        {
			Gizmos.DrawWireCube(transform.position, new Vector3(gridSize, gridSize, 0f));
            // Instanciando icones do topo
            for (int i = 0; i < gridSize; i++)
                Gizmos.DrawSphere(new Vector3(transform.position.x + (i - gridSize/2) + 0.5f, this.transform.position.y + (gridSize / 2) + distance, this.transform.position.z), 0.1f);

            // Instanciando icones de baixo
            for (int i = 0; i < gridSize; i++)
                Gizmos.DrawSphere(new Vector3(transform.position.x + (i - gridSize/2) + 0.5f, this.transform.position.y - (gridSize / 2) - distance, this.transform.position.z), 0.1f);

            // Instanciando icones da esquerda
            for (int i = 0; i < gridSize; i++)
                Gizmos.DrawSphere(new Vector3(this.transform.position.y + (gridSize / 2) + distance, transform.position.x + (i - gridSize/2) + 0.5f, this.transform.position.z), 0.1f);

            // Instanciando icones da direita
            for (int i = 0; i < gridSize; i++)
                Gizmos.DrawSphere(new Vector3(this.transform.position.y - (gridSize / 2) - distance, transform.position.x + (i - gridSize/2) + 0.5f, this.transform.position.z), 0.1f);
        }
    }
}
