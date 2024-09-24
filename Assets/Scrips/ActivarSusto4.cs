using UnityEngine;
using System.Collections;

public class ActivarSusto4 : MonoBehaviour
{
    public Collider triggerCollider; // Collider que activará el susto al ser atravesado
    public GameObject personaje; // El personaje que empezará desactivado
    public float tiempoParaDestruir = 20.0f; // Tiempo que el personaje esperará antes de destruirse
    public SustosManager sustosManager; // Referencia al SustosManager para activar el siguiente susto

    private bool personajeActivado = false;

    // Método que se llama cuando otro objeto entra en el trigger del collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el collider es el Player (Character Controller) con el tag "Player"
        if (other.CompareTag("Player") && !personajeActivado)
        {

            // Activar el personaje
            if (personaje != null)
            {
                personaje.SetActive(true); // Activa el personaje que estaba desactivado
                personajeActivado = true; // Marca como activado para evitar múltiples activaciones

                // Iniciar la corrutina para destruir el personaje después de un tiempo
                StartCoroutine(DestruirPersonajeDespuesDeTiempo());
            }
        }
    }

    // Corrutina que espera y luego destruye el personaje
    private IEnumerator DestruirPersonajeDespuesDeTiempo()
    {
        // Espera el tiempo especificado antes de destruir el personaje
        yield return new WaitForSeconds(tiempoParaDestruir);

        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
        // Destruir el personaje
        if (personaje != null)
        {
            Destroy(personaje.gameObject);
        }

        // Notificar al SustosManager que este susto ha terminado
        if (sustosManager != null)
        {
            sustosManager.ActivarSiguienteSusto();
        }

        // Destruir el objeto del susto (este script)
        Destroy(gameObject);
    }
}
