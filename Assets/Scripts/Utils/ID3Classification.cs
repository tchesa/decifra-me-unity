using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ID3Classification : MonoBehaviour {
	
	#region Singleton Design Pattern
	private static ID3Classification instance = null;
    public static ID3Classification Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ID3Classification)FindObjectOfType(typeof(ID3Classification));
                if (instance == null)
                    instance = (new GameObject("_ID3Classification")).AddComponent<ID3Classification>();
            }
            return instance;
        }
    }
	#endregion
	
	// Classes e atributos
    static string[] tempo = { "curto", "medio", "longo" };
    static string[] movimentos = { "poucos", "medios", "muitos" };
    static string[] desempenho = { "ruim", "regular", "bom" };

    // Qual classe será o objetivo
    static string objetivo = "desempenho";
	
	// Lista de exemplos, utilizada para o aprendizado de máquina
	List<Hashtable> Exemplos = new List<Hashtable>();
	
	// Tabela auxiliar para referenciar os atributos das classes
    Hashtable Atributos = new Hashtable();
	
	// Arvore de decisão que será gerada pelo método ID3
	Arvore arvoreDecisao;
	
	// Executa assim que o objeto é inicializado
	void Awake()
	{
        DontDestroyOnLoad(gameObject);

        #region Adicionando as classes e artibutos à tabela 'Atributos'
        Atributos.Add("tempo", tempo);
        Atributos.Add("movimentos", movimentos);
        Atributos.Add("desempenho", desempenho);
        #endregion

        #region Adicionando os exemplos à tabela de exemplos
        /*
		    TEMPO	MOVIMENTOS	CLASSIFICACAO
		    curto	poucos		bom
		    curto	medios		bom
		    curto	muitos		medio
		    medio	poucos		bom
		    medio	medios		medio
		    medio	muitos		ruim
		    longo	poucos		medio
		    longo	medios		medio
		    longo	muitos		ruim
		    */
        #region Exemplo 1
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 0);
            exemplo.Add("movimentos", 0);
            exemplo.Add("desempenho", 2);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 2
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 0);
            exemplo.Add("movimentos", 1);
            exemplo.Add("desempenho", 2);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 3
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 0);
            exemplo.Add("movimentos", 2);
            exemplo.Add("desempenho", 1);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 4
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 1);
            exemplo.Add("movimentos", 0);
            exemplo.Add("desempenho", 2);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 5
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 1);
            exemplo.Add("movimentos", 1);
            exemplo.Add("desempenho", 1);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 6
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 1);
            exemplo.Add("movimentos", 2);
            exemplo.Add("desempenho", 0);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 7
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 2);
            exemplo.Add("movimentos", 0);
            exemplo.Add("desempenho", 1);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 8
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 2);
            exemplo.Add("movimentos", 1);
            exemplo.Add("desempenho", 1);
            Exemplos.Add(exemplo);
        }
        #endregion
        #region Exemplo 9
        {
            Hashtable exemplo = new Hashtable();
            exemplo.Add("tempo", 2);
            exemplo.Add("movimentos", 2);
            exemplo.Add("desempenho", 0);
            Exemplos.Add(exemplo);
        }
        #endregion
        #endregion

		// Atributos que serão passados para a função de aprendizagem
		string[] atributos = { "tempo", "movimentos" };
		// Gera uma árvore de decisão a partir do método ID3 e armazena-a na variável 'arvoreDecisao'
		arvoreDecisao = AprendizadoArvoreDecisao(Exemplos, atributos, "ruim");

        print("Arvore de decisao inicializada com sucesso!");
	}

    // Função auxiliar para que o objeto da classe seja inicializado
    public void Initialize()
    {
        
    }
	
	Arvore AprendizadoArvoreDecisao(List<Hashtable> exemplos, string[] atributos, string padrao)
    {
        // Se exemplos for vazio, Então retornar padrão
        if (exemplos.Count == 0) return new Arvore(padrao);
        // Senão Se todos os exemplos têm a mesma classificação, Então retornar esta classificação
        else if (TemMesmadesempenho(exemplos)) return new Arvore(Mesmadesempenho(exemplos));
        // Senão Se atributos é vazio, Então retornar VALOR_DA_MAIORIA(exemplos)
        else if (atributos.Length == 0) return new Arvore(ValorDaMaioria(exemplos));
        else
        {
            string melhor = MelhorAtributo(atributos, exemplos); // Escolhe o melhor atributo de atributos
            string[] _melhor = (string[])Atributos[melhor];
            // Cria uma nova arvore com teste de raiz 'melhor', com o numero de ramificações = o numero de atributos de melhor
            Arvore arvore = new Arvore(melhor, _melhor.Length); 
            string m = ValorDaMaioria(exemplos); // ValorDaMaioria(exemplos_i) ??
            
            for(int i=0; i<_melhor.Length; i++) // Para cada valor 'i' de 'melhor', faça
            {
                // _exemplos com elementos com 'melhor' = 'i'
                List<Hashtable> _exemplos = new List<Hashtable>();
                foreach (Hashtable exemplo in exemplos) { if ((int)exemplo[melhor] == i) _exemplos.Add(exemplo); }
                // _atributos = atributos - melhor
                string[] _atributos = new string[atributos.Length - 1];
                if(_atributos.Length > 0)
                {
                    int k = 0;
                    for (int j = 0; j < atributos.Length; j++)
                    {
                        if (atributos[j] != melhor)
                        {
                            _atributos[k] = atributos[j];
                            k++;
                        }
                    }
                }
                //subarvore <- APRENDIZADO_ARVORE_DECISAO(exemplos_i, atributos - melhor, m)
                arvore.subArvore[i] = AprendizadoArvoreDecisao(_exemplos, _atributos, m);
            }

            return arvore;
        }
    }
	
	float Entropia(string chave, List<Hashtable> exemplos)
    {
        string[] atributoChave = (string[])Atributos[chave];
        string[] atributoObjetivo = (string[])Atributos[objetivo];

        float[] resultados = new float[atributoChave.Length];

        for (int i = 0; i < resultados.Length; i++)
        {
            int nExemplos = 0;
            float[] nObjetivos = new float[atributoObjetivo.Length]; { for (int j = 0; j < nObjetivos.Length; j++) nObjetivos[j] = 0; }

            foreach (Hashtable exemplo in exemplos)
            {
                if ((int)exemplo[chave] == i)
                {
                    nExemplos++;
                    nObjetivos[(int)exemplo[objetivo]]++;
                }
            }

            //Console.Write(String.Format("{0}/{1} * I({2}/{0}, {3}/{0}) = ", nExemplos, exemplos.Count, nObjetivos[0], nObjetivos[1]));

            for (int j = 0; j < nObjetivos.Length; j++) nObjetivos[j] /= nExemplos;

            resultados[i] = (nExemplos / (float)exemplos.Count) * I(nObjetivos);

            //Console.WriteLine(resultados[i]);
        }

        float resultado = 0;

        for (int i = 0; i < resultados.Length; i++)
        {
            resultado += resultados[i];
        }
    
        return resultado;
    }

    float Ganho(string chave, List<Hashtable> exemplos)
    {
        string[] atributoObjetivo = (string[])Atributos[objetivo];

        float[] nObjetivos = new float[atributoObjetivo.Length]; { for (int j = 0; j < nObjetivos.Length; j++) nObjetivos[j] = 0; }

        foreach (Hashtable exemplo in exemplos)
        {
            nObjetivos[(int)exemplo[objetivo]]++;
        }

        for (int i = 0; i < nObjetivos.Length; i++) nObjetivos[i] /= (float)exemplos.Count;

        float entropiaConjunto = I(nObjetivos);

        return entropiaConjunto - Entropia(chave, exemplos);
    }

    float I(float[] valores)
    {
        float[] resultados = new float[valores.Length];

        for(int i = 0; i<valores.Length; i++)
        {
            if (valores[i] == 0) resultados[i] = 0;
            else resultados[i] = (-valores[i]) * Mathf.Log(valores[i], valores.Length);
        }

        float resultado = 0;

        for (int i = 0; i < resultados.Length; i++)
        {
            resultado += resultados[i];
        }

        return resultado;
    }

    bool TemMesmadesempenho(List<Hashtable> exemplos)
    {
        bool mesmadesempenho = true;
        int desempenho = (int)exemplos[0][objetivo];

        foreach (Hashtable exemplo in exemplos)
        {
            if ((int)exemplo[objetivo] != desempenho)
            {
                mesmadesempenho = false;
            }
        }

        return mesmadesempenho;
    }

    string Mesmadesempenho(List<Hashtable> exemplos)
    {
        if (!TemMesmadesempenho(exemplos)) return null;

        string[] _objetivo = (string[])Atributos[objetivo];
        return _objetivo[(int)exemplos[0][objetivo]];
    }

    string ValorDaMaioria(List<Hashtable> exemplos)
    {
        string[] _objetivo = (string[])Atributos[objetivo];
        int[] desempenho = new int[_objetivo.Length];

        foreach (Hashtable exemplo in exemplos)
        {
            desempenho[(int)exemplo[objetivo]]++;
        }

        int indice = 0;

        for (int i = 0; i < desempenho.Length; i++)
        {
            if (desempenho[i] > desempenho[indice]) indice = i;
        }

        return _objetivo[indice];
    }

    string MelhorAtributo(string[] atributos, List<Hashtable> exemplos)
    {
        float[] ganhoAtributos = new float[atributos.Length]; // Armazena o ganho de cada atributo

        for (int i = 0; i < atributos.Length; i++)
        {
            ganhoAtributos[i] = Ganho(atributos[i], exemplos);
        }

        int indice = 0;

        for (int i = 0; i < ganhoAtributos.Length; i++)
        {
            if (ganhoAtributos[i] > ganhoAtributos[indice]) indice = i;
        }

        return atributos[indice];
    }
    
    public string ClassificarElemento(Hashtable elemento)
    {
        Arvore comparador = arvoreDecisao; // 'comparador' é o elemento que percorerá a árvore. Inicia posicionado na raiz da árvore

        while (comparador.subArvore != null) // desce na árvore até chegar em uma folha
        {
            comparador = comparador.subArvore[(int)elemento[comparador.rotulo]];
        }

        return comparador.rotulo; // retorna o rótulo da folha
    }
}

class Arvore
{
    public string rotulo;
    public Arvore[] subArvore;

    public Arvore(string rotulo)
    {
        this.rotulo = rotulo;
    }

    public Arvore(string rotulo, int ramificacoes)
    {
        this.rotulo = rotulo;
        subArvore = new Arvore[ramificacoes];
    }
}
