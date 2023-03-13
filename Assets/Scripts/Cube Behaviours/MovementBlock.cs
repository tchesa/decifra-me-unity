using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementBlock : Block
{
    public GameObject border;

    void start()
    {
        //InvokeRepeating("_VerifyEletricity", 0, 0.3f);
    }

    void Update()
    {
        if (isSelected) // Se ele está sendo selecionado (arrastado)
        {
            if (border) border.GetComponent<Renderer>().material.color = Color.Lerp(border.GetComponent<Renderer>().material.color, Color.white, 10f * Time.deltaTime);  // Cor da borda -> Branco
        }
        else
        {
            // Cor da borda -> cinza
            if (border) border.GetComponent<Renderer>().material.color = Color.Lerp(border.GetComponent<Renderer>().material.color, Color.gray, 10f * Time.deltaTime);
            // Ajusta a peça na posiçao do grid
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(position.x, position.y, transform.localPosition.z), 15f * Time.deltaTime);
            // Verifica se está sendo eletrizado
            //if(!General.Instance.end) VerifyElectricity();
			//if(!General.Instance.end) AS_VerifyElectricity();
        }
    }

    void _VerifyEletricity()
    {
        // Verifica se está sendo eletrizado
        VerifyElectricity();
    }
}