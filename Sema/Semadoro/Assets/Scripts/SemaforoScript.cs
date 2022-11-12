using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SemaforoScript : MonoBehaviour
{
    public StateSemaforo estadoRojo;
    public StateSemaforo estadoAmarillo;
    public StateSemaforo estadoVerde;
    public StateSemaforo estadoVerdePar;
    public StateSemaforo estadoApagado;
    public SemaforoScript semaforoespejo;
    public SemaforoScript siguienteSemaforo;
    public Material esteMaterial;
    public Renderer esterender;
    public StateSemaforo estadoActual;
    public StateSemaforo estadoSiguiente;
    public bool blnPrendidos = true;
    private void Update()
    {

    }

    public void Comenzar()
    {
        StartCoroutine(CambioEstado());
    }
    private void Start()
    {
        esterender = GetComponent<Renderer>();
        esteMaterial = GetComponent<Renderer>().material;

    }
    IEnumerator CambioEstado()
    {
        blnPrendidos = false;
        yield return new WaitForSeconds(1);
        blnPrendidos = true;
        //semaforoespejo.estadoActual = estadoActual;
        //Debug.Log("Iniciando");
        while (blnPrendidos)
        {
            //Debug.Log("Entrando al bucle");
            if (estadoActual.Equals(estadoApagado))
            {
                //Debug.Log("Apagado");
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
            }
            else if (estadoActual.Equals(estadoVerde))
            {
                //siguienteSemaforo.estadoActual = siguienteSemaforo.estadoRojo;
                estadoRojo.blnBanderaCambio = true;
                //Debug.Log("Estado verde");
                estadoSiguiente = estadoVerdePar;
                esteMaterial = estadoVerde.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(15);
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                estadoActual = estadoSiguiente;
            }
            else if (estadoActual.Equals(estadoVerdePar))
            {
               // Debug.Log("Estado verde parpadeando");
                estadoSiguiente = estadoAmarillo;
                yield return new WaitForSeconds(0.5f);
                esteMaterial = estadoVerdePar.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                esteMaterial = estadoVerdePar.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                esteMaterial = estadoVerdePar.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);
                estadoActual = estadoSiguiente;
            }
            else if (estadoActual.Equals(estadoAmarillo))
            {
                //Debug.Log("Estado amarillo");
                estadoSiguiente = estadoRojo;
                esteMaterial = estadoAmarillo.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(2);
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
                yield return new WaitForSeconds(0.5f);

                estadoActual = estadoSiguiente;
            }
            else if (estadoActual.Equals(estadoRojo) && estadoRojo.blnBanderaCambio)
            {


                //Debug.Log("Estado Rojo");
                esteMaterial = estadoRojo.material;
                esterender.material = esteMaterial;
                estadoSiguiente = estadoVerde;
                yield return new WaitForSeconds(2);
                siguienteSemaforo.estadoActual = siguienteSemaforo.estadoVerde;
                estadoRojo.blnBanderaCambio = false;



            }
            else if (estadoActual.Equals(estadoRojo) && !estadoRojo.blnBanderaCambio) { /*yield return new WaitForSeconds(5f);*/ }
            else
            {
                
            }
                yield return new WaitForSeconds(0.00000001f);
              
        }
    }

    IEnumerator Preventivas()
    {
        blnPrendidos = false;
        yield return new WaitForSeconds(1);
        blnPrendidos = true;
        //Debug.Log("Preventivas");
        while (blnPrendidos)
        {
            
            
            yield return new WaitForSeconds(0.5F);
            esteMaterial = estadoApagado.material;
            esterender.material = esteMaterial;
            yield return new WaitForSeconds(0.5F);
            esteMaterial = estadoAmarillo.material;
            esterender.material = esteMaterial;
        }

    }

    public void ComenzarPreventivas()
    {
        StartCoroutine(Preventivas());
    }
}
