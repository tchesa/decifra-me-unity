using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    public Block selectedBlock = null;
    public int gridSize;
    Block[,] blockGrid;
    public MainBlock.CubeFace gridFace;
    public bool shuffling = false;

	public string orientation;
	
    void Awake()
    {
        blockGrid = new Block[gridSize, gridSize];
        Block[] blocks = GetComponentsInChildren<Block>();
        foreach (Block b in blocks)
        {
            blockGrid[Mathf.RoundToInt(b.transform.localPosition.x), Mathf.RoundToInt(b.transform.localPosition.y)] = b;
            b.SetPosition(new Vector2(Mathf.RoundToInt(b.transform.localPosition.x), Mathf.RoundToInt(b.transform.localPosition.y)));
            b.grid = this;
        }
    }
	
	void Start()
	{
		if(HasNonStaticBlock() && General.Instance.shuffleOnStart) StartCoroutine(ShuffleBlocks());
	}
	
    void _Update()
    {
        SetNeighborhoodOfAll();
    }

    public void SetNeighborhood(int x, int y)
    {
        if (blockGrid[x, y] == null) { Debug.Log("ERRO : Nao existe bloco na posicao."); }
        else
        {
            Block top = null, bot = null, left = null, right = null;

            
            if (blockGrid[x, y].portDivision.ToString().Contains("LEFT"))
            {
                if (x <= 0)
                {
                    switch (gridFace)
                    {
                        case MainBlock.CubeFace.FRONT:
                            {
                                if (General.MainBlock.Left.blockGrid[gridSize - 1, y] != null &&
                                    General.MainBlock.Left.blockGrid[gridSize - 1, y].portDivision.ToString().Contains("RIGHT")) 
                                {
                                    left = General.MainBlock.Left.blockGrid[gridSize - 1, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BACK:
                            {
                                if (General.MainBlock.Right.blockGrid[gridSize - 1, y] != null &&
                                    General.MainBlock.Right.blockGrid[gridSize - 1, y].portDivision.ToString().Contains("RIGHT"))
                                {
                                    left = General.MainBlock.Right.blockGrid[gridSize - 1, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.LEFT:
                            {
                                if (General.MainBlock.Back.blockGrid[gridSize - 1, y] != null &&
                                    General.MainBlock.Back.blockGrid[gridSize - 1, y].portDivision.ToString().Contains("RIGHT"))
                                {
                                    left = General.MainBlock.Back.blockGrid[gridSize - 1, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.RIGHT:
                            {
                                if (General.MainBlock.Front.blockGrid[gridSize - 1, y] != null &&
                                    General.MainBlock.Front.blockGrid[gridSize - 1, y].portDivision.ToString().Contains("RIGHT"))
                                {
                                    left = General.MainBlock.Front.blockGrid[gridSize - 1, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.TOP:
                            {
                                if (General.MainBlock.Left.blockGrid[y, x] != null &&
                                    General.MainBlock.Left.blockGrid[y, x].portDivision.ToString().Contains("TOP"))
                                {
                                    left = General.MainBlock.Left.blockGrid[y, x];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BOT:
                            {
                                if (General.MainBlock.Left.blockGrid[gridSize - y - 1, gridSize - 1] != null &&
                                    General.MainBlock.Left.blockGrid[gridSize - y - 1, gridSize - 1].portDivision.ToString().Contains("BOT"))
                                {
                                    left = General.MainBlock.Left.blockGrid[gridSize - y - 1, gridSize - 1];
                                }
                                break;
                            }
                        default: break;
                    }
                }
                else
                {
                    if (blockGrid[x - 1, y] != null &&
                        blockGrid[x - 1, y].portDivision.ToString().Contains("RIGHT"))
                    {
                        left = blockGrid[x - 1, y];
                    }
                }
            }

            if (blockGrid[x, y].portDivision.ToString().Contains("RIGHT"))
            {
                if (x >= gridSize - 1)
                {
                    switch (gridFace)
                    {
                        case MainBlock.CubeFace.FRONT:
                            {
                                if (General.MainBlock.Right.blockGrid[0, y] != null &&
                                    General.MainBlock.Right.blockGrid[0, y].portDivision.ToString().Contains("LEFT"))
                                {
                                    right = General.MainBlock.Right.blockGrid[0, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BACK:
                            {
                                if (General.MainBlock.Left.blockGrid[0, y] != null &&
                                    General.MainBlock.Left.blockGrid[0, y].portDivision.ToString().Contains("LEFT"))
                                {
                                    right = General.MainBlock.Left.blockGrid[0, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.LEFT:
                            {
                                if (General.MainBlock.Front.blockGrid[0, y] != null &&
                                    General.MainBlock.Front.blockGrid[0, y].portDivision.ToString().Contains("LEFT"))
                                {
                                    right = General.MainBlock.Front.blockGrid[0, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.RIGHT:
                            {
                                if (General.MainBlock.Back.blockGrid[0, y] != null &&
                                    General.MainBlock.Back.blockGrid[0, y].portDivision.ToString().Contains("LEFT"))
                                {
                                    right = General.MainBlock.Back.blockGrid[0, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.TOP:
                            {
                                if (General.MainBlock.Right.blockGrid[gridSize - y - 1, 0] != null &&
                                    General.MainBlock.Right.blockGrid[gridSize - y - 1, 0].portDivision.ToString().Contains("TOP"))
                                {
                                    right = General.MainBlock.Right.blockGrid[gridSize - y - 1, 0];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BOT:
                            {
                                if (General.MainBlock.Right.blockGrid[y, gridSize - 1] != null &&
                                    General.MainBlock.Right.blockGrid[y, gridSize - 1].portDivision.ToString().Contains("BOT"))
                                {
                                    right = General.MainBlock.Right.blockGrid[y, gridSize - 1];
                                }
                                break;
                            }
                        default: break;
                    }
                }
                else
                {
                    if (blockGrid[x + 1, y] != null &&
                        blockGrid[x + 1, y].portDivision.ToString().Contains("LEFT"))
                    {
                        right = blockGrid[x + 1, y];
                    }
                }
            }

            if (blockGrid[x, y].portDivision.ToString().Contains("TOP"))
            {
                if (y <= 0)
                {
                    switch (gridFace)
                    {
                        case MainBlock.CubeFace.FRONT:
                            {
                                if (General.MainBlock.Top.blockGrid[x, gridSize - 1] != null &&
                                    General.MainBlock.Top.blockGrid[x, gridSize - 1].portDivision.ToString().Contains("BOT"))
                                {
                                    top = General.MainBlock.Top.blockGrid[x, gridSize - 1];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BACK:
                            {
                                if (General.MainBlock.Top.blockGrid[gridSize - 1 - x, 0] != null &&
                                    General.MainBlock.Top.blockGrid[gridSize - 1 - x, 0].portDivision.ToString().Contains("TOP"))
                                {
                                    top = General.MainBlock.Top.blockGrid[gridSize - 1 - x, 0];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.LEFT:
                            {
                                if (General.MainBlock.Top.blockGrid[0, x] != null &&
                                    General.MainBlock.Top.blockGrid[0, x].portDivision.ToString().Contains("LEFT"))
                                {
                                    top = General.MainBlock.Top.blockGrid[0, x];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.RIGHT:
                            {
                                if (General.MainBlock.Top.blockGrid[gridSize - 1, gridSize - 1 - x] != null &&
                                    General.MainBlock.Top.blockGrid[gridSize - 1, gridSize - 1 - x].portDivision.ToString().Contains("RIGHT"))
                                {
                                    top = General.MainBlock.Top.blockGrid[gridSize - 1, gridSize - 1 - x];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.TOP:
                            {
                                if (General.MainBlock.Back.blockGrid[gridSize - 1 - x, 0] != null &&
                                    General.MainBlock.Back.blockGrid[gridSize - 1 - x, 0].portDivision.ToString().Contains("TOP"))
                                {
                                    top = General.MainBlock.Back.blockGrid[gridSize - 1 - x, 0];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BOT:
                            {
                                if (General.MainBlock.Front.blockGrid[x, gridSize - 1] != null &&
                                    General.MainBlock.Front.blockGrid[x, gridSize - 1].portDivision.ToString().Contains("BOT"))
                                {
                                    top = General.MainBlock.Front.blockGrid[x, gridSize - 1];
                                }
                                break;
                            }
                        default: break;
                    }
                }
                else
                {
                    if (blockGrid[x, y - 1] != null &&
                        blockGrid[x, y - 1].portDivision.ToString().Contains("BOT"))
                    {
                       top = blockGrid[x, y - 1];
                    }
                }
            }
			
            if (blockGrid[x, y].portDivision.ToString().Contains("BOT"))
            {
                if (y >= gridSize - 1)
                {
                    switch (gridFace)
                    {
                        case MainBlock.CubeFace.FRONT:
                            {
                                if (General.MainBlock.Bot.blockGrid[x, 0] != null &&
                                    General.MainBlock.Bot.blockGrid[x, 0].portDivision.ToString().Contains("TOP")) 
                                {
                                    bot = General.MainBlock.Bot.blockGrid[x, 0];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BACK:
                            {
                                if (General.MainBlock.Bot.blockGrid[gridSize - 1 - x, y] != null &&
                                    General.MainBlock.Bot.blockGrid[gridSize - 1 - x, y].portDivision.ToString().Contains("BOT"))
                                {
                                    bot = General.MainBlock.Bot.blockGrid[gridSize - 1 - x, y];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.LEFT:
                            {
                                if (General.MainBlock.Bot.blockGrid[0, gridSize - 1 - x] != null &&
                                    General.MainBlock.Bot.blockGrid[0, gridSize - 1 - x].portDivision.ToString().Contains("LEFT"))
                                {
                                    bot = General.MainBlock.Bot.blockGrid[0, gridSize - 1 - x];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.RIGHT:
                            {
                                if (General.MainBlock.Bot.blockGrid[gridSize-1, x] != null &&
                                    General.MainBlock.Bot.blockGrid[gridSize-1, x].portDivision.ToString().Contains("RIGHT"))
                                {
                                    bot = General.MainBlock.Bot.blockGrid[gridSize-1, x];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.TOP:
                            {
                                if (General.MainBlock.Front.blockGrid[x, 0] != null &&
                                    General.MainBlock.Front.blockGrid[x, 0].portDivision.ToString().Contains("TOP"))
                                {
                                    bot = General.MainBlock.Front.blockGrid[x, 0];
                                }
                                break;
                            }
                        case MainBlock.CubeFace.BOT:
                            {
                                if (General.MainBlock.Back.blockGrid[gridSize - 1 - x, y] != null &&
                                    General.MainBlock.Back.blockGrid[gridSize - 1 - x, y].portDivision.ToString().Contains("BOT"))
                                {
                                    bot = General.MainBlock.Back.blockGrid[gridSize - 1 - x, y];
                                }
                                break;
                            }
                        default: break;
                    }
                }
                else
                {
                    if (blockGrid[x, y + 1] != null &&
                        blockGrid[x, y + 1].portDivision.ToString().Contains("TOP"))
                    {
                        bot = blockGrid[x, y + 1];
                    }
                }
            }
			
            blockGrid[x, y].SetNeighborhood(top, bot, left, right);
        }
    }

    public void SetNeighborhoodOfAll()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (blockGrid[i, j] != null)
                {
                    SetNeighborhood(i, j);
                }
            }
        }
    }
	
    public void SetSelectedBlock(Block block)
    { 
        if (selectedBlock != null) selectedBlock.isSelected = false;
        if (block == null)
        {
            selectedBlock = null;
        }
        else
        {
            selectedBlock = block;
            block.isSelected = true;
        }
    }
	
    IEnumerator ShuffleBlocks()
    {
        shuffling = true;

		while(General.Instance.time < 0f)
        {
            Block b = GetBlock((int)(Random.value * gridSize), (int)(Random.value * gridSize));
            
			if( b!=null && b.RandomMovement())
				yield return new WaitForSeconds(Constants.ShuffligDelay);
        }

        yield return new WaitForSeconds(1);
        shuffling = false;
		StartCoroutine(MainBlock.Instance.IVerifyEletricityFromAll());
    }
	
	bool HasNonStaticBlock()
	{
		for(int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++) 
			{
				if(blockGrid[i,j]!=null && !blockGrid[i,j].isStatic) return true;
			}
		}
		return false;
	}
	
    public Block GetBlock(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
        {
            return null;
        }
        else
        {
            return blockGrid[x, y];
        }
    }
    public Block GetBlock(Vector2 position)
    {
        if (position.x < 0f || position.x >= gridSize || position.y < 0f || position.y >= gridSize)
        {
            return null;
        }
        else
        {
            return blockGrid[(int)position.x, (int)position.y];
        }
    }
    public void SetBlock(Block block, int x, int y)
    {
        blockGrid[x, y] = block;
    }
    public void SetBlock(Block block, Vector2 position)
    {
        blockGrid[(int)position.x, (int)position.y] = block;
    }
	
	public string Orientation()
	{
		if(Mathf.Abs(transform.up.y) > Mathf.Abs(transform.right.y))
		{
			if(transform.up.y > 0f)
				return "UP";
			else
				return "DOWN";
		}
		else
		{
			if(transform.right.y > 0f)
				return "RIGHT";
			else
				return "LEFT";
		}
	}

    public void VerifyEletricityFromAll()
    {
        for(int i=0; i<gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (blockGrid[i, j] != null) blockGrid[i, j].VerifyElectricity();
            }
        }
    }

    void DebugGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            string s = string.Empty;
            for (int j = 0; j < gridSize; j++)
            {
                if (blockGrid[j, i] == null)
                {
                    s += "[0]";
                }
                else
                {
                    s += "[1]";
                }
            }
            Debug.Log(s);
            s = string.Empty;
        }
    }
}