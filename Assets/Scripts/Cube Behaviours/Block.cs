using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    public Grid grid;
    protected Vector2 position;
    public bool isSelected = false; 
    public bool isStatic = false; 
    public bool electrified = false; 
    public Neighborhood neighborhood = new Neighborhood();
    Vector3 mouseClick; 
    public int limitLeft, limitRight, limitTop, limitBot;
	Vector2 oldPosition;
	
	public int h = 99; // Variável que armazena a estimativa heuristica do bloco, utilizada no metodo A*.
	
    public enum PortDivision
    {
        NONE, TOP_BOT, LEFT_RIGHT, TOP_RIGHT, RIGHT_BOT, BOT_LEFT, LEFT_TOP,
        BOT_LEFT_TOP, LEFT_TOP_RIGHT, TOP_RIGHT_BOT, RIGHT_BOT_LEFT, TOP_BOT_LEFT_RIGHT
    }
    public PortDivision portDivision = PortDivision.TOP_BOT;
    
    public Neighborhood GetNeighborhood()
    {
        return neighborhood;
    }
    public void SetNeighborhood(Block top, Block bot, Block left, Block right)
    {
        neighborhood.top = top;
        neighborhood.bot = bot;
        neighborhood.left = left;
        neighborhood.right = right;
    }
    public void SetNeighborhood(Neighborhood neighborhood)
    {
        this.neighborhood.top = neighborhood.top;
        this.neighborhood.bot = neighborhood.bot;
        this.neighborhood.left = neighborhood.left;
        this.neighborhood.right = neighborhood.right;
    }

    void OnMouseDown()
    {
        if (!isStatic)
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, 100))
            {
                mouseClick = hit.point - transform.position; 
				mouseClick = new Vector3(mouseClick.x*grid.transform.up.y + mouseClick.y*grid.transform.right.y, 
										 mouseClick.y*grid.transform.right.x + mouseClick.x*grid.transform.up.x, 0.1f);
                Select();
            }
        }
    }
    void OnMouseDrag()
    {
        if (isSelected)
        {
            if (!isStatic)
            {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                RaycastHit hit; 
                if (Physics.Raycast(ray, out hit, 100))
                {
					transform.localPosition = (new Vector3(
						(grid.transform.position.x - hit.point.x)*grid.transform.up.y + (grid.transform.position.y - hit.point.y)*grid.transform.right.y,
						(grid.transform.position.y - hit.point.y)*grid.transform.right.x + (grid.transform.position.x - hit.point.x)*grid.transform.up.x, 0.1f));
					transform.localPosition -= new Vector3(1f, 1f, 0f) - mouseClick; 
                }
				// Limitando a movimentaçao em um unico eixo
				if (Mathf.Abs((transform.localPosition - new Vector3(position.x, position.y, transform.localPosition.z)).x) >
                   Mathf.Abs((transform.localPosition - new Vector3(position.x, position.y, transform.localPosition.z)).y))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, position.y, transform.localPosition.z); 
                }
                else if (Mathf.Abs((transform.localPosition - new Vector3(position.x, position.y, transform.localPosition.z)).x) <
                        Mathf.Abs((transform.localPosition - new Vector3(position.x, position.y, transform.localPosition.z)).y))
                {
                    transform.localPosition = new Vector3(position.x, transform.localPosition.y, transform.localPosition.z);
                }
                // Atualiza a posiçao da peça constantemente. Usado no teste de movimentaçao da peça sem limitaçao de um dos eixos
				//position = new Vector2(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
				
				SetLimits();
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, position.x - limitLeft, position.x + limitRight),
                                                      Mathf.Clamp(transform.localPosition.y, position.y - limitTop, position.y + limitBot),
                                                      0.1f);
            }
        }
    }
    void OnMouseUp()
    {
        if (isSelected)
        {
            if (!isStatic)
            {
                position = new Vector2(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y));
                Unselect();
            }
        }
    }

    public void Select()
    {
        isSelected = true;
        electrified = false;
        grid.selectedBlock = this;
        grid.SetBlock(null, position);
		oldPosition = new Vector2((int)position.x, (int)position.y);
		
		StartCoroutine(MainBlock.Instance.IVerifyEletricityFromAll());
    }
    public void Unselect()
    {
        isSelected = false;
        grid.selectedBlock = null;
        grid.SetBlock(this, position);
        grid.SetNeighborhood((int)position.x, (int)position.y);
        General.ConnectionIconsGen.Invoke("UpdateIcons", General.invokeFrequence + 0.1f);
		if(oldPosition != new Vector2((int)position.x, (int)position.y)) 
		{
			MainBlock.Instance.AddMovement(oldPosition, new Vector2((int)position.x, (int)position.y), this);
			General.Instance.moves++;
		}
		
		StartCoroutine(MainBlock.Instance.IVerifyEletricityFromAll());
    }
	
	public Vector2 GetPosition()
    {
        return position;
    }
    public void SetPosition(Vector2 newPosition)
    {
        position = newPosition;
    }
	
	public void ForceMovement(Vector2 newPosition)
	{
		grid.SetBlock(null, position);
		position = newPosition;
		grid.SetBlock(this, position);
		StartCoroutine(MainBlock.Instance.IVerifyEletricityFromAll());
	}
	
	public bool RandomMovement()	
	{
        if (!isStatic &&  ((position.y > 0 && !grid.GetBlock(new Vector2(position.x, position.y - 1))) || 
                           (position.y < grid.gridSize - 1 && !grid.GetBlock(new Vector2(position.x, position.y + 1))) || 
                           (position.x > 0 && !grid.GetBlock(new Vector2(position.x - 1, position.y))) || 
                           (position.x < grid.gridSize - 1 && !grid.GetBlock(new Vector2(position.x + 1, position.y)))))
        {			
			while(true)
			{
	            int r = (int)(Random.value * 4);
	
	            if (r == 0 && position.y > 0 && grid.GetBlock(new Vector2(position.x, position.y - 1)) == null)
	            {
	                grid.SetBlock(null, position);
	                position = new Vector2(position.x, position.y - 1);
	                grid.SetBlock(this, position);
	                return true;
	            }
	            else if (r == 1 && position.y < grid.gridSize - 1 && grid.GetBlock(new Vector2(position.x, position.y + 1)) == null)
	            {
	                grid.SetBlock(null, position);
	                position = new Vector2(position.x, position.y + 1);
	                grid.SetBlock(this, position);
	                return true;
	            }
	            else if (r == 2 && position.x > 0 && grid.GetBlock(new Vector2(position.x - 1, position.y)) == null)
	            {
	                grid.SetBlock(null, position);
	                position = new Vector2(position.x - 1, position.y);
	                grid.SetBlock(this, position);
	                return true;
	            }
	            else if (r == 3 && position.x < grid.gridSize - 1 && grid.GetBlock(new Vector2(position.x + 1, position.y)) == null)
	            {
	                grid.SetBlock(null, position);
	                position = new Vector2(position.x + 1, position.y);
	                grid.SetBlock(this, position);
	                return true;
	            }
			}    
        }
        return false;
	}

    public void SetLimits()
    {
        limitLeft = 0;
        limitRight = 0;
        limitTop = 0;
        limitBot = 0;

        while ((int)position.x - limitLeft >= 0 && grid.GetBlock((int)position.x - limitLeft, (int)position.y) == null && limitLeft < 100)
        {
            limitLeft++;
        }
        while ((int)position.x + limitRight < grid.gridSize && grid.GetBlock((int)position.x + limitRight, (int)position.y) == null && limitRight < 100)
        {
            limitRight++;
        }
        while ((int)position.y - limitTop >= 0 && grid.GetBlock((int)position.x, (int)position.y - limitTop) == null && limitTop < 100)
        {
            limitTop++;
        }
        while ((int)position.y + limitBot < grid.gridSize && grid.GetBlock((int)position.x, (int)position.y + limitBot) == null && limitBot < 100)
        {
            limitBot++;
        }

        limitLeft--;
        limitRight--;
        limitTop--;
        limitBot--;
    }

    public void VerifyElectricity()
    {
        bool oldState = electrified;

        electrified = false;

        List<Block> blocks = new List<Block>();
        List<Block> travelBlocks = new List<Block>();

        blocks.Add(this);
        travelBlocks.Add(this);

        while (blocks.Count > 0)
        {
            if (blocks[0].GetType() == typeof(EmitterBlock) ||
               (blocks[0].GetType() == typeof(ConnectorBlock) && blocks[0].gameObject.GetComponent<ConnectorBlock>().state == ConnectorBlock.State.SENDING))
                electrified = true;
            Expand(blocks, travelBlocks);
        }

        if (oldState != electrified)
        {
            if (electrified) OnElectricity();
            else OffEletricity();
        }
    }
	
	public void AS_VerifyElectricity()
    {
        bool oldState = electrified;

        electrified = false;

        List<Block> blocks = new List<Block>();
        List<Block> travelBlocks = new List<Block>();

        blocks.Add(this);
        travelBlocks.Add(this);

		#region Método A*
		
		LinkedList<Caminho> percorrer = new LinkedList<Caminho>();
		LinkedList<Caminho> expandidos = new LinkedList<Caminho>();
		
		percorrer.AddAtEnd(new Caminho(null, this, 1, h));
		
		while(!percorrer.Empty() && !ExisteEmissorEmExpandidos(expandidos))
		{
			Caminho e = percorrer.RemoveFromBegin();
			expandidos.AddAtEnd(e);			
			
			if(e.para.neighborhood.top != null && !ExisteEmExpandidos(e.para.neighborhood.top, expandidos)) 
				percorrer.AddSorted(new Caminho(e.para, e.para.neighborhood.top, e.g + 1, e.para.neighborhood.top.h));
			if(e.para.neighborhood.bot != null && !ExisteEmExpandidos(e.para.neighborhood.bot, expandidos)) 
				percorrer.AddSorted(new Caminho(e.para, e.para.neighborhood.bot, e.g + 1, e.para.neighborhood.bot.h));
			if(e.para.neighborhood.left != null && !ExisteEmExpandidos(e.para.neighborhood.left, expandidos)) 
				percorrer.AddSorted(new Caminho(e.para, e.para.neighborhood.left, e.g + 1, e.para.neighborhood.left.h));
			if(e.para.neighborhood.right != null && !ExisteEmExpandidos(e.para.neighborhood.right, expandidos)) 
				percorrer.AddSorted(new Caminho(e.para, e.para.neighborhood.right, e.g + 1, e.para.neighborhood.right.h));
		}
		
		if(ExisteEmissorEmExpandidos(expandidos)) // O bloco emissor foi encontrado
		{
			LinkedList<Block> resultado = new LinkedList<Block>();
            Caminho temp = expandidos.RemoveFromEnd();

            resultado.AddAtEnd(temp.para);
            Block de = temp.de;

            while (!resultado.Exists(this))
            {
                while (temp.para != de) temp = expandidos.RemoveFromEnd();

                resultado.AddAtBegin(temp.para);
                de = temp.de;
            }

            h = resultado.count;
			electrified = true;
		}	
		#endregion

        if (oldState != electrified)
        {
            if (electrified) OnElectricity();
            else OffEletricity();
        }
    }
	
	bool ExisteEmExpandidos(Block bloco, LinkedList<Caminho> expandidos)
    {
        if (expandidos.Empty()) return false;

        foreach (Caminho c in expandidos)
        {
            if (c.para.Equals(bloco)) return true;
        }

        return false;
    }
	
	bool ExisteEmissorEmExpandidos(LinkedList<Caminho> expandidos) // Verifica se existe um bloco do tipo emissor na lista
    {
        if (expandidos.Empty()) return false;

        foreach (Caminho c in expandidos)
        {
            if (c.para.GetType() == typeof(EmitterBlock)) return true;
        }

        return false;
    }

    void OnElectricity() 
    {
        if (!grid.shuffling)
        {
			AudioClip clip = SoundArchive.Instance.GetRandomEffect();
			if(clip && General.Instance.eletrifiedSoundBreak < 0f)
			{
				AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
				General.Instance.eletrifiedSoundBreak = Constants.EletrifiedSoundBreak;
			}			
			else
				Debug.LogWarning("SoundArchive.Instance.GetRandomEffect() returned a null AudioClip");
        }
    }

    void OffEletricity() 
    {
        
    }

    void Expand(List<Block> blocks, List<Block> travelBlocks)
    {
        Block expandedBlock = blocks[0];
        
        if (expandedBlock.neighborhood.top != null && !travelBlocks.Contains(expandedBlock.neighborhood.top))
        {
            blocks.Add(expandedBlock.neighborhood.top);
            travelBlocks.Add(expandedBlock.neighborhood.top);
        }
        if (expandedBlock.neighborhood.bot != null && !travelBlocks.Contains(expandedBlock.neighborhood.bot))
        {
            blocks.Add(expandedBlock.neighborhood.bot);
            travelBlocks.Add(expandedBlock.neighborhood.bot);
        }
        if (expandedBlock.neighborhood.left != null && !travelBlocks.Contains(expandedBlock.neighborhood.left))
        {
            blocks.Add(expandedBlock.neighborhood.left);
            travelBlocks.Add(expandedBlock.neighborhood.left);
        }
        if (expandedBlock.neighborhood.right != null && !travelBlocks.Contains(expandedBlock.neighborhood.right))
        {
            blocks.Add(expandedBlock.neighborhood.right);
            travelBlocks.Add(expandedBlock.neighborhood.right);
        }

        blocks.RemoveAt(0);
    }

    void SetNeighborhoodOfNeighborhood() 
    {
        if (neighborhood.left != null) neighborhood.left.grid.SetNeighborhood((int)neighborhood.left.position.x, (int)neighborhood.left.position.y);
        if (neighborhood.right != null) neighborhood.right.grid.SetNeighborhood((int)neighborhood.right.position.x, (int)neighborhood.right.position.y);
        if (neighborhood.top != null) neighborhood.top.grid.SetNeighborhood((int)neighborhood.top.position.x, (int)neighborhood.top.position.y);
        if (neighborhood.bot != null) neighborhood.bot.grid.SetNeighborhood((int)neighborhood.bot.position.x, (int)neighborhood.bot.position.y);
    }
}

internal class Caminho
{
    public Block de;
    public Block para;
    public int g, h;

    public Caminho(Block _de, Block _para, int _g, int _h)
    {
        de = _de;
        para = _para;
        g = _g;
        h = _h;
    }

    public override string ToString()
    {
        if (g + h < 10) return "0" + (g + h);
        else return (g + h).ToString();
    }
}

#region Minha Lista Encadeada Genérica
internal class Node<Type>
{
    public Node<Type> previous;
    public Node<Type> next;
    public Type item;

    /// <summary>Construtor da classe</summary>
    /// <param name="previous">Nodo anterior</param>
    /// <param name="next">Nodo seguinte</param>
    /// <param name="item">Elemento do nodo</param>
    public Node(Node<Type> previous, Node<Type> next, Type item)
    {
        this.previous = previous;
        this.next = next;
        this.item = item;
    }

    public Node()
    {
        this.previous = null;
        this.next = null;
        this.item = default(Type);
    }
}

internal class LinkedList<Type> : IEnumerable
{
    public int count // Armazena o numero de elementos da lista
    {
        get { return Count(); }
    }

    public Type first // Armazena o primeiro elemento da lista
    {
        get { return FirstNode().item; }
    }

    public Type last // Armazena o ultimo elemento da lista
    {
        get { return LastNode().item; }
    }

    public Node<Type> sentinel; // Celula central da lista

    IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator)GetEnumerator(); }

    public LinkedListEnum<Type> GetEnumerator()
    {
        return new LinkedListEnum<Type>(GetArray());
    }

    public LinkedList() // Construtor da classe
    {
        sentinel = new Node<Type>();
        sentinel.previous = sentinel;
        sentinel.next = sentinel;
        sentinel.item = default(Type);
    }

    public LinkedList(params Type[] itens)
    {
        sentinel = new Node<Type>();
        sentinel.previous = sentinel;
        sentinel.next = sentinel;
        sentinel.item = default(Type);

        foreach (Type item in itens) AddAtEnd(item);
    }

    public void AddAtBegin(Type item) // Adiciona o elemento no inicio da lista
    {
        Node<Type> newNode = new Node<Type>(sentinel, sentinel.next, item);
        sentinel.next = newNode;
        newNode.next.previous = newNode;
    }

    public void AddAtEnd(Type item) // Adiciona o elemento no fim da lista
    {
        Node<Type> newNode = new Node<Type>(sentinel.previous, sentinel, item);
        sentinel.previous = newNode;
        newNode.previous.next = newNode;
    }

    public void AddBefore(Type item, int index) // Adiciona o elemento antes do elemento do indice passado
    {
        if (index < count)
        {
            Node<Type> temp = FirstNode();

            while (index > 0)
            {
                temp = temp.next;
                index--;
            }

            Node<Type> n = new Node<Type>(temp, temp.next, item);
            n.previous.next = n;
            n.next.previous = n;
        }
    }

    public void AddAfter(Type item, int index) // Adiciona o elemento depois do elemento do indice passado
    {
        if (index < count)
        {
            Node<Type> temp = FirstNode();

            while (index > 0)
            {
                temp = temp.next;
                index--;
            }

            Node<Type> n = new Node<Type>(temp, temp.next, item);
            n.next.previous = n;
            n.previous.next = n;
        }
    }

    public void AddSorted(Type item) // Insere o elemento ordenando por ToString()
    {
        if (Empty()) AddAtEnd(item);
        else
        {
            Node<Type> temp = FirstNode();

            while (temp != sentinel && string.Compare(item.ToString(), temp.item.ToString()) > 0)
            {
                temp = temp.next;
            }

            Node<Type> n = new Node<Type>(temp.previous, temp, item);
            n.previous.next = n;
            n.next.previous = n;
        }
    }

    public Type RemoveFromBegin() // Remove o elemento do inicio da lista e o retorna
    {
        if (Empty()) return default(Type);

        Node<Type> removedNode = FirstNode();
        sentinel.next = removedNode.next;
        sentinel.next.previous = sentinel;
        return removedNode.item;
    }

    public Type RemoveFromEnd() // Remove o elemento do fim da lista e o retorna
    {
        if (Empty()) return default(Type);

        Node<Type> removedNode = LastNode();
        sentinel.previous = removedNode.previous;
        sentinel.previous.next = sentinel;
        return removedNode.item;
    }

    public bool Empty() // Retorna verdadeiro se a lista estiver vazia
    {
        return (sentinel.next == sentinel);
    }

    private Node<Type> FirstNode()
    {
        return sentinel.next;
    }

    private Node<Type> LastNode() // Retorna o ultimo nodo da lista
    {
        return sentinel.previous;
    }

    private int Count() // Retorna o numero de elementos da lista
    {
        Node<Type> aux = FirstNode();
        int count = 0;
        while (aux != sentinel)
        {
            count++;
            aux = aux.next;
        }
        return count;
	}

    public Type ElementAtIndex(int index) // Retorna o elemento que está no indice passado por parâmetro
    {
        if (index > Count() || index < 0) return default(Type); // Indice inválido

        Node<Type> n = FirstNode(); // cria um nodo auxiliar

        while (index > 0) // o nodo auxiliar avança na lista no numero de passos igual ao indice passado
        {
            n = n.next; // avança o nodo um passo pra frente
            index--; // decrementa o indice
        }
        return n.item; // retorna o elemento do nodo auxiliar
    }

    public Type[] GetArray() // Retorna um vetor com todos os elementos da lista
    {
        if (Empty()) return null;

        Type[] array = new Type[count];

        for (int i = 0; i < count; i++)
            array[i] = ElementAtIndex(i);

        return array;
    }

    public void Erase() // Esvazia a lista
    {
        while (!Empty()) RemoveFromBegin();
    }

    public bool Exists(Type item) // returna verdadeiro se ele encontrar o elemento na lista
    {
        for (Node<Type> temp = FirstNode(); temp != sentinel; temp = temp.next)
        {
            if (temp.item.Equals(item)) return true;
        }

        return false;
    }

    public int Find(Type item) // Retorna o indice que o elemento está na lista. Retorna -1 se não existir
    {
        if (!Exists(item)) return -1; // O elemento não existe na lista
        int i = 0;

        for (Node<Type> n = FirstNode(); n != sentinel; n = n.next)
        {
            if (n.item.Equals(item)) break;
            i++;
        }

        return i;
    }

    public void Sort()
    {

    }
}

internal class LinkedListEnum<Type> : IEnumerator
{
    Type[] array;

    int position;

    object IEnumerator.Current
    {
        get { return Current; }
    }

    public Type Current
    {
        get
        {
            try
            {
                return array[position];
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new System.InvalidOperationException();
            }
        }
    }

    public LinkedListEnum(Type[] _array)
    {
        array = _array;
        position = -1;
    }

    public bool MoveNext()
    {
        position++;
        return (position < array.Length);
    }

    public void Reset()
    {
        position = -1;
    }
}
#endregion

[System.Serializable]
public class Neighborhood
{
    public Block top;
    public Block bot;
    public Block left;
    public Block right;

    public Neighborhood(Neighborhood neighborhood)
    {
        this.top = neighborhood.top;
        this.bot = neighborhood.bot;
        this.left = neighborhood.left;
        this.right = neighborhood.right;
    }

    public Neighborhood(Block top, Block bot, Block left, Block right)
    {
        this.top = top;
        this.bot = bot;
        this.left = left;
        this.right = right;
    }

    public Neighborhood()
    {
        this.top = null;
        this.bot = null;
        this.left = null;
        this.right = null;
    }
}