using UnityEngine;
using System.Collections;

public class LevelStatistics : MonoBehaviour {
	
	#region SINGLETON DESIGN PATTERN
	private static LevelStatistics instance = null;
    public static LevelStatistics Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (LevelStatistics)FindObjectOfType(typeof(LevelStatistics));
                if (instance == null)
                    instance = (new GameObject("_LevelStatistics")).AddComponent<LevelStatistics>();
            }
            return instance;
        }
    }
	#endregion
		
	public float timeCeil, timeFloor;
	public float movesCeil, movesFloor;
	
	// Gera um exemplo com base nas estatísticas numéricas alcançadas pelo jogador ao longo da fase
	// Converte as estatísticas numéricas em nominais para serem classificadas pela árvore de decisão
	public Hashtable PlayerPerformance()
	{
        float time = General.Instance.time;
        int moves = General.Instance.moves;

		Hashtable hash = new Hashtable();
		
		// Classificando o tempo
		if(time < timeCeil + ((timeFloor - timeCeil) / 3))
		{
			hash.Add("tempo", 0);
		}
		else if(time < timeCeil + ((timeFloor - timeCeil) / 3) * 2)
		{
			hash.Add("tempo", 1);
		}
		else
		{
			hash.Add ("tempo", 2);
		}
		
		// Classificando o numero de movimentos
		if(moves < movesCeil + ((movesFloor - movesCeil) / 3))
		{
			hash.Add("movimentos", 0);
		}
		else if(moves < movesCeil + ((movesFloor - movesCeil) / 3) * 2)
		{
			hash.Add("movimentos", 1);
		}
		else
		{
			hash.Add("movimentos", 2);
		}

        print(string.Format("Tempo: {0}, Movimentos: {1}", time, moves));

		return hash;
	}

    void Start()
    {
        //ID3Classification.Instance.Initialize();
    }
}

/*
atributos
	tempo { curto, medio, longo }
	movimentos { poucos, medios, muitos }

   |   |   |   |   -> 2,7
0 1 2 3 4 5 6 7 8 9

(7-2)/3 -> 1,7

2+1,7 = 3,7
3,7+1,7 = 5,4
5,4+1,7 = 7,1

2 < slow < 3,7
3,7 < average < 5,4
5,4 < fast < 7,1
*/