using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectorBlock : Block {

	public enum State { NONE, RECEIVING, SENDING };
	public State state = State.NONE;
	public ConnectorBlock mirror;
	
	void Update(){
		grid.SetNeighborhood((int)position.x, (int)position.y);
		
		switch(state){
		case State.NONE : //Verifica se esta recebendo energia
			electrified = false;
			VerifyElectricity();
			if(electrified){
				state = State.RECEIVING;
				mirror.state = State.SENDING;
			}
			break;
		case State.RECEIVING :
			VerifyElectricity();
			if(!electrified){
				state = State.NONE;
				mirror.state = State.NONE;
			}
			break;
		case State.SENDING :
			electrified = true;
			break;
			default : break;
		}
	}
}
