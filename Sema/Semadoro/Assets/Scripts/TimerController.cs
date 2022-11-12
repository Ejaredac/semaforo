using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    public SemaforoScript[] semaforos1;
    public SemaforoScript[] semaforos2;
    public TMPro.TextMeshPro m_TextMeshPro;
    public SemaforoScript semaforoActual;
    public float m_Time = 0f;
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
            semaforo.StopAllCoroutines();
        }
        foreach (SemaforoScript semaforo in semaforos2)
        {
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
            semaforo.StopAllCoroutines();
        }
        foreach (SemaforoScript semaforo in semaforos2)
        {
            semaforo.StopAllCoroutines();
        }
        StartCoroutine(Prevenntivos());
    }

    public void ApagarTodo()
    {
        StopAllCoroutines();
        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.StopAllCoroutines();
        }
        foreach (SemaforoScript semaforo in semaforos2)
        {
            semaforo.StopAllCoroutines();
        }
        m_Time = 0;
        m_TextMeshPro.text = "0";

        m_TextMeshPro.color = Color.gray;
        StartCoroutine(Apagar());
    }

    public IEnumerator ComenzarSemaforos()
    {
        yield return new WaitForSeconds(1);
        m_Running = true;
        m_Time = 0f;
        foreach (SemaforoScript semaforo in semaforos1)
        {
            semaforo.blnPrendidos = true;
            semaforo.estadoActual = semaforo.estadoVerde;
        }
        //siguiente rojo

        foreach (SemaforoScript semaforo in semaforos2)
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
        foreach (SemaforoScript item in semaforos1)
        {
            item.Comenzar();
        }
        foreach (SemaforoScript item in semaforos2)
        {
            item.Comenzar();
        }
        while (m_Running)
        {
            foreach (SemaforoScript item in semaforos1)
            {
                if (item.estadoActual.Equals(item.estadoVerde))
                {
                    semaforoActual = item;
                }
            }
            foreach (SemaforoScript item in semaforos2)
            {
                if (item.estadoActual.Equals(item.estadoVerde))
                {
                    semaforoActual = item;
                }
            }
            int temp = (int)m_Time;
            m_TextMeshPro.text = temp.ToString();
            //Debug.Log(m_Time.ToString());
            if (semaforoActual.estadoActual.Equals(semaforoActual.estadoVerde))
            {
                m_Duration = 16.0f;
                m_TextMeshPro.color = Color.green;
            }
            else if (semaforoActual.estadoActual.Equals(semaforoActual.estadoVerdePar))
            {
                m_Duration = 4.0f;
            }
            else if (semaforoActual.estadoActual.Equals(semaforoActual.estadoAmarillo)) {
                m_Duration = 3.0f;
                m_TextMeshPro.color = Color.yellow;
            }
            else if (semaforoActual.estadoActual.Equals(semaforoActual.estadoRojo))
            {
                m_Duration = 2.0f;
                m_TextMeshPro.color = Color.red;
            }

            m_Time += 0.5f;

            if (m_Time%1==0.5f && semaforoActual.estadoActual.Equals(semaforoActual.estadoVerdePar))
            {
                m_TextMeshPro.color = Color.gray;
            }
            else if (m_Time % 1 == 0f && semaforoActual.estadoActual.Equals(semaforoActual.estadoVerdePar))
            {
                m_TextMeshPro.color = Color.green;
            }
            yield return new WaitForSeconds(0.5f);
            if (m_Duration <= m_Time)
            {
                m_Time = 0;
            }
        }
        //Debug.Log("Salio");

    }
    IEnumerator Prevenntivos()
    {

        yield return new WaitForSeconds(1);

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
    }

    IEnumerator Apagar()
    {
        yield return new WaitForSeconds(1);

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
    }
}
