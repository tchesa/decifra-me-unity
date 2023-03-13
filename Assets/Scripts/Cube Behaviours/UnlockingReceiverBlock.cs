using UnityEngine;
using System.Collections;

public class UnlockingReceiverBlock : BlockObserver 
{
	public UnlockableMovementBlock[] childBlocks; // Vetor de blocos desbloqueaveis
	
    void Start()
    {
        foreach (UnlockableMovementBlock block in childBlocks)
        {
            block.subjectBlock = this;
            CadastrarObservador(block); // Cadastra o bloco como observador
        }
    }

	void Update()
    {
        bool _electrified = electrified; // Armazena o estado antes de verificar o estado atual
		if(!General.Instance.end) VerifyElectricity(); // Verifica a eletricidade

        if(electrified != _electrified) // O estado foi alterado
        {
            Notificar();
        }

		//foreach(UnlockableMovementBlock block in childBlocks)
        //    block.unblocked = electrified; // Se o bloco estiver eletrizado, desbloqueia o bloco filho
	}
}
