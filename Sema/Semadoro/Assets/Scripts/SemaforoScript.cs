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
    public TMPro.TextMeshPro txtCont;
    public float fltTimer = 1f;
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
        blnPrendidos = true;
        //semaforoespejo.estadoActual = estadoActual;
        //Debug.Log("Iniciando");
        while (blnPrendidos)
        {

            yield return new WaitForSeconds(0.5f);

            //Debug.Log("Entrando al bucle");
            if (estadoActual.Equals(estadoApagado))
            {
                //Debug.Log("Apagado");
                esteMaterial = estadoApagado.material;
                esterender.material = esteMaterial;
            }
            else if (estadoActual.Equals(estadoVerde))
            {
                estadoRojo.blnBanderaCambio = true;
                siguienteSemaforo.estadoActual = siguienteSemaforo.estadoRojo;

                if (fltTimer < 1)
                {
                    fltTimer = 1;
                    esterender.material = estadoVerde.material;
                    
                        txtCont.text = ((int)fltTimer).ToString();
                        txtCont.color = Color.green;
                    

                }
                else if (fltTimer < 15 && fltTimer >= 1)
                {
                    esterender.material = estadoVerde.material;
                   
                        txtCont.text = ((int)fltTimer).ToString();
                        txtCont.color = Color.green;
                    
                }
                else if (fltTimer == 15)
                {
                    esterender.material = estadoVerde.material;
                   
                        txtCont.text = ((int)fltTimer).ToString();
                        txtCont.color = Color.green;
                    
                }
                else if (fltTimer >= 15.5f)
                {
                    fltTimer = 0.5f;
                    esterender.material = estadoApagado.material;
                    
                    
                        txtCont.color = Color.gray;
                        txtCont.text = "15";
                    
                    estadoActual = estadoVerdePar;
                }
            }
            else if (estadoActual.Equals(estadoVerdePar))
            {
                if (fltTimer % 1 == 0.5f)
                {
                    esterender.material = estadoApagado.material;
                    
                        txtCont.color = Color.gray;
                    
                }
                else if (fltTimer % 1 == 0)
                {
                    esterender.material = estadoVerdePar.material;
                    
                        txtCont.color = Color.green;
                    
                }


                if (fltTimer < 3 && fltTimer >= 1)
                {
                    
                        txtCont.text = ((int)fltTimer).ToString();
                    
                }
                else if (fltTimer < 1)
                {
                    
                        txtCont.text = "1"; 
                    
                }
                else if (fltTimer == 3)
                {
                    
                        txtCont.text = ((int)fltTimer).ToString(); 
                    
                }
                else if (fltTimer >= 3.5f)
                {
                    fltTimer = 0.5f;
                    
                        txtCont.text = "3";
                        txtCont.color = Color.gray; 
                    
                    esterender.material = estadoApagado.material;
                    estadoActual = estadoAmarillo;
                }


            }
            else if (estadoActual.Equals(estadoAmarillo))
            {

                if (fltTimer < 3 && fltTimer >= 1)
                {
                    esterender.material = estadoAmarillo.material;
                    txtCont.text = ((int)fltTimer).ToString();
                    txtCont.color = Color.yellow;
                }
                else if (fltTimer < 1)
                {
                    txtCont.text = "1";
                    esterender.material = estadoAmarillo.material;
                }
                else if (fltTimer == 3)
                {
                    esterender.material = estadoAmarillo.material;
                    txtCont.text = ((int)fltTimer).ToString();
                    txtCont.color = Color.yellow;
                }
                else if (fltTimer >= 3.5f)
                {
                    fltTimer = 0.5f;
                    esterender.material = estadoApagado.material;
                    txtCont.color = Color.gray;
                    txtCont.text = "3";
                    estadoActual = estadoRojo;
                }
            }
            else if (estadoActual.Equals(estadoRojo) && estadoRojo.blnBanderaCambio)
            {

                if (fltTimer < 2 && fltTimer >= 1)
                {
                    esterender.material = estadoRojo.material;
                    txtCont.text = ((int)fltTimer).ToString();
                    txtCont.color = Color.red;
                }
                else if (fltTimer < 1)
                {
                   
                    txtCont.text = "1";
                    esterender.material = estadoRojo.material;
                }
                else if (fltTimer == 2)
                {
                    esterender.material = estadoRojo.material;
                    txtCont.text = ((int)fltTimer).ToString();
                    txtCont.color = Color.red;
                }
                else if (fltTimer > 2.5f)
                {
                    esterender.material = estadoRojo.material;
                    txtCont.text = "2";
                    txtCont.color = Color.red;
                    siguienteSemaforo.fltTimer = 0.5f;
                    fltTimer = 0.5f;
                    siguienteSemaforo.estadoActual = siguienteSemaforo.estadoVerde;
                    estadoRojo.blnBanderaCambio = false;
                }
                
            }
            else if (estadoActual.Equals(estadoRojo) && !estadoRojo.blnBanderaCambio)
            {
                esterender.material = estadoRojo.material;
                fltTimer = 0.5f;
            }
            else
            {

            }
            fltTimer += 0.5f;
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator Preventivas()
    {
        blnPrendidos = false;
        blnPrendidos = true;
        //Debug.Log("Preventivas");
        while (blnPrendidos)
        {


            yield return new WaitForSeconds(0.5F);
            esteMaterial = estadoApagado.material;
            esterender.material = esteMaterial;
            txtCont.color = Color.gray;
            yield return new WaitForSeconds(0.5F);
            esteMaterial = estadoAmarillo.material;
            esterender.material = esteMaterial;
            txtCont.color = Color.yellow;
        }

        yield return new WaitForSeconds(1);
    }

    public void ComenzarPreventivas()
    {
        StartCoroutine(Preventivas());
    }
}
