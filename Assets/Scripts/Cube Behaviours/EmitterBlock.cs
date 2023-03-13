using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmitterBlock : Block {

    public bool test;

	void Start(){
		electrified = true;
	}
	
	// Funçao para atualizar todos os blocos conectados diretamente e indiretamente a este emissor como eletrizado
	void Electrify(){
		List<Block> blocks = new List<Block>(); // Lista de blocos em espera para serem expandidos
		List<Block> travelBlocks = new List<Block>(); // Lista de blocos percorridos
		
		blocks.Add(this);
		travelBlocks.Add(this);
		
		while(blocks.Count>0){ // Enquanto existir blocos na lista de espera, expandir
			Debug.Log(blocks[0].GetPosition());
			blocks[0].electrified = true;
			Expand(blocks, travelBlocks);
		}
	}
	
	// Funçao modularizada da expansao dos blocos usado pela funçao Electrify
	void Expand(List<Block> blocks, List<Block> travelBlocks){
		Block expandedBlock = blocks[0]; // Armazena o bloco a ser expandido
		// Verificando se o vizinho do bloco a ser expandido nao e nulo e se ele ja foi percorrido
		if(expandedBlock.neighborhood.top != null && !travelBlocks.Contains(expandedBlock.neighborhood.top)){
			blocks.Add(expandedBlock.neighborhood.top); // Adiciona o vizinho na lista de espera
			travelBlocks.Add(expandedBlock.neighborhood.top); // Adiciona o vizinho na lista de percorridos
		}
		if(expandedBlock.neighborhood.bot != null && !travelBlocks.Contains(expandedBlock.neighborhood.bot)){
			blocks.Add(expandedBlock.neighborhood.bot);
			travelBlocks.Add(expandedBlock.neighborhood.bot);
		}
		if(expandedBlock.neighborhood.left != null && !travelBlocks.Contains(expandedBlock.neighborhood.left)){
			blocks.Add(expandedBlock.neighborhood.left);
			travelBlocks.Add(expandedBlock.neighborhood.left);
		}
		if(expandedBlock.neighborhood.right != null && !travelBlocks.Contains(expandedBlock.neighborhood.right)){
			blocks.Add(expandedBlock.neighborhood.right);
			travelBlocks.Add(expandedBlock.neighborhood.right);
		}
		
		blocks.RemoveAt(0);
	}
	
	void Update(){
		grid.SetNeighborhood((int)position.x, (int)position.y);

        if (test)
        {
            RandomMovement();
            test = true;
        }
	}
}