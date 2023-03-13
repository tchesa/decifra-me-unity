using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockObserver : Block 
{
    List<IObserver> observadores;

    int count;

    void Awake()
    {
        observadores = new List<IObserver>();
    }

    public void CadastrarObservador(IObserver observador)
    {
        observadores.Add(observador);
    }

    public void RemoverObservador(IObserver observador)
    {
        observadores.Remove(observador);
    }

    public void Notificar()
    {
        foreach (IObserver observador in observadores)
        {
            observador.Atualizar();
        }
    }
}

public interface IObserver
{
    void Atualizar();
}
