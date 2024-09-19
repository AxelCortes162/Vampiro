using UnityEngine;

public class SustosManager : MonoBehaviour
{
    public GameObject[] sustos; // Array para contener los GameObjects de los sustos
    private int indiceSustoActual = 0; // Para llevar el seguimiento del susto actual

    // Método para activar el primer susto al inicio
    private void Start()
    {
        ActivarSusto(0); // Activar el primer susto
    }

    // Método para activar el siguiente susto
    public void ActivarSiguienteSusto()
    {
        indiceSustoActual++; // Incrementar el índice para el siguiente susto

        if (indiceSustoActual < sustos.Length)
        {
            ActivarSusto(indiceSustoActual);
        }
    }

    // Método para activar un susto por índice
    private void ActivarSusto(int index)
    {
        if (index >= 0 && index < sustos.Length)
        {
            sustos[index].SetActive(true); // Activar el susto correspondiente
        }
    }
}
