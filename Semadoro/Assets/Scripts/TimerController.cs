using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    public SemaforoScript[] semaforos1;
    public SemaforoScript[] semaforos2;
    public TMPro.TextMeshPro m_TextMeshPro;
    public SemaforoScript semaforoActual;
    public float m_Time = 1f;
    public float m_Duration = 1f;
    public bool m_Running = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            ComenzarConteoSemaforo();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            EncenderPreventivas();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ApagarTodo();
        }
    }

    public void ComenzarConteoSemaforo()
    {
        StopAllCoroutines();
        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.fltTimer = 1;
            semaforo.StopAllCoroutines();
        }
        foreach (SemaforoScript semaforo in semaforos2)
        {
            semaforo.fltTimer = 1;
            semaforo.StopAllCoroutines();
        }
        m_Time = 0;
        StartCoroutine(ComenzarSemaforos());
    }

    public void EncenderPreventivas()
    {
        StopAllCoroutines();
        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.fltTimer = 1;
            semaforo.StopAllCoroutines();
        }
        foreach (SemaforoScript semaforo in semaforos2)
        {
            semaforo.fltTimer = 1;
            semaforo.StopAllCoroutines();
        }
        StartCoroutine(Prevenntivos());
    }

    public void ApagarTodo()
    {
        StopAllCoroutines();
        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.fltTimer = 1;
            semaforo.StopAllCoroutines();
        }
        foreach (SemaforoScript semaforo in semaforos2)
        {
            semaforo.fltTimer = 1;
            semaforo.StopAllCoroutines();
        }
        m_Time = 1;
        m_TextMeshPro.text = "0";

        m_TextMeshPro.color = Color.gray;
        StartCoroutine(Apagar());
    }

    public IEnumerator ComenzarSemaforos()
    {
        m_Running = true;
        m_Time = 0f;
        foreach (SemaforoScript semaforo in semaforos2)
        {
            semaforo.blnPrendidos = true;
            semaforo.estadoActual = semaforo.estadoVerde;
        }
        //siguiente rojo

        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.blnPrendidos = true;
            semaforo.estadoActual = semaforo.estadoRojo;
            semaforo.estadoRojo.blnBanderaCambio = false;
            semaforo.esteMaterial = semaforo.estadoRojo.material;
            semaforo.esterender.material = semaforo.esteMaterial;
        }
        //espejo rojo

        //espejo principal verde


        //Secuenciadores
        //semaforos1[0].Comenzar();
        semaforos2[0].Comenzar();
        
        //Debug.Log("Salio");
        yield return new WaitForSeconds(1);

    }
    IEnumerator Prevenntivos()
    {


        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.blnPrendidos = true;
            semaforo.ComenzarPreventivas();
        }
        //siguiente rojo

        foreach (SemaforoScript semaforo in semaforos2)
        {

            semaforo.blnPrendidos = true;
            semaforo.ComenzarPreventivas();
        }
        while (m_Running)
        {
            if(m_Time % 1 == 0.5f)
            {
                m_TextMeshPro.color = Color.gray;
                m_TextMeshPro.text = "0";
            }
            else if (m_Time % 1 == 0f)
            {
                m_TextMeshPro.color = Color.yellow;
                m_TextMeshPro.text = "0";
            }
            yield return new WaitForSeconds(0.5f);
            m_Time += 0.5f;
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator Apagar()
    {

        foreach (SemaforoScript item in semaforos1)
        {
            item.blnPrendidos = false;

            item.esterender.material = item.estadoApagado.material;
        }

        foreach (SemaforoScript item in semaforos2)
        {
            item.blnPrendidos = false;
            item.esterender.material = item.estadoApagado.material;
        }
        m_Running = false;
        yield return new WaitForSeconds(1);
    }
}
