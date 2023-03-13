using UnityEngine;
using System.Collections;

public class UnlockableMovementBlock : Block, IObserver {

	public bool unblocked = false;
    public bool staticOnBlocked = false;
	Block.PortDivision realPortDivision;
	public GameObject border;

    public UnlockingReceiverBlock subjectBlock; // Referência ao bloco sujeito (pai)

	void Start()
    {
		realPortDivision = portDivision;
        //InvokeRepeating("_VerifyEletricity", 0f, 0.3f);
	}
	
	void Update()
    {
		if(!unblocked)
        {
			portDivision = Block.PortDivision.NONE;
            if (staticOnBlocked) isStatic = true;
		}
        else
        {
			portDivision = realPortDivision;
            if (staticOnBlocked) isStatic = false;
		}
		
        if (isSelected)
		{
			border.renderer.material.color = Color.Lerp(border.renderer.material.color, Color.white, 10f * Time.deltaTime);  // Cor da borda -> Branco
        }
		else
		{
            // Cor da borda -> cinza
            if (border) border.renderer.material.color = Color.Lerp(border.renderer.material.color, Color.gray, 10f * Time.deltaTime);
            // Ajusta a peça na posiçao do grid
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(position.x, position.y, transform.localPosition.z), 15f * Time.deltaTime); 
            // Verifica se está sendo eletrizado
            //if(!General.Instance.end) VerifyElectricity();
		}
    }

    void _VerifyEletricity()
    {
        // Verifica se está sendo eletrizado
        VerifyElectricity();
    }

    public void Atualizar()
    {
        unblocked = subjectBlock.electrified;
    }
}
