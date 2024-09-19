using UnityEngine;
using System.Collections;

public class ActivarSusto : MonoBehaviour
{
    public GameObject esfera; // Referencia a la esfera que se activará y desactivará
    public Collider triggerCollider; // El collider que se desactivará al final
    public GameObject estatua; // Asigna la estatua desactivada desde el Inspector
    public GameObject vampiroComiendo;
    public SustosManager sustosManager; // Referencia al script controlador de los sustos
    public GameObject vampiro; // Asigna el vampiro desde el inspector

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es el Player
        if (other.CompareTag("Player"))
        {
            // Inicia el proceso de susto
            StartCoroutine(SustoSequence());
        }
    }

    // Coroutine para manejar toda la secuencia del susto
    private IEnumerator SustoSequence()
    {
        // Desactiva el collider para que no se vuelva a activar
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }

        // 1. Activar el sonido del vampiro
        if (vampiro != null)
        {
            AudioSource vampiroAudio = vampiro.GetComponent<AudioSource>();
            if (vampiroAudio != null)
            {
                vampiroAudio.enabled = true; // Activa el AudioSource si estaba desactivado
                vampiroAudio.Play(); // Reproduce el sonido
            }

            // 2. Activar la animación del vampiro
            Animator vampiroAnimator = vampiro.GetComponent<Animator>();
            if (vampiroAnimator != null)
            {
                vampiroAnimator.SetTrigger("Susto"); // Activar animación del susto
            }
        }

        // 3. Esperar 1.5 segundos antes de activar la esfera
        yield return new WaitForSeconds(1.5f);
        esfera.SetActive(true);

        // 4. Esperar 2 segundos con la esfera activa
        yield return new WaitForSeconds(2.0f);

        // 5. Desactivar el vampiro y activar la estatua
        if (vampiro != null)
        {
            vampiro.SetActive(false);
        }
        if (estatua != null)
        {
            estatua.SetActive(true);
        }
        if (vampiroComiendo != null)
        {
            vampiroComiendo.SetActive(true);
        }

        // 6. Desactivar la esfera
        esfera.SetActive(false);

        // 7. Notificar al SustosManager para activar el siguiente susto
        if (sustosManager != null)
        {
            sustosManager.ActivarSiguienteSusto();
        }

        // Finalmente, destruir este susto después de que se complete
        Destroy(gameObject);
    }
}
