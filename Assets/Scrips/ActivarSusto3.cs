using UnityEngine;
using System.Collections;

public class ActivarSusto3 : MonoBehaviour
{
    public Animator sustoAnimator; // Referencia al Animator que controlará la animación
    public Collider triggerCollider; // Referencia al Collider del susto
    public Transform player; // Referencia al jugador para que el personaje lo mire
    public Transform personaje; // Referencia al personaje que te mirará
    public float rotationSpeed = 2.0f; // Velocidad con la que el personaje te mira
    public float tiempoParaDestruir = 22.0f; // Tiempo que el personaje espera antes de destruirse

    public AudioSource audio1; // AudioSource para el audio inicial (antes del susto)
    public AudioSource audio2; // AudioSource para el audio que se reproduce 4 segundos después del susto
    public AudioSource audio3; // AudioSource para el audio que se reproduce 3 segundos después del audio2
    public AudioSource audio4; // AudioSource para el primer audio que se reproduce junto con audio5
    public AudioSource audio5; // AudioSource para el segundo audio que se reproduce junto con audio4

    private bool shouldLookAtPlayer = false;

    void Start()
    {
        // Inicia el audio1 al inicio
        if (audio1 != null)
        {
            audio1.Play();
        }
    }

    // Método que se llama cuando otro objeto entra en el trigger del collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el collider es el Player (Character Controller) con el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Activar el trigger "Susto3" en el Animator
            if (sustoAnimator != null)
            {
                sustoAnimator.SetTrigger("Susto3");
            }

            // Iniciar la secuencia de reproducción de sonidos
            StartCoroutine(ReproducirAudiosSecuencia());

            // Iniciar el proceso para que el personaje te empiece a mirar
            shouldLookAtPlayer = true;

            // Desactivar el collider para evitar que el susto se repita
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
            }

            // Iniciar la corrutina para destruir el personaje después de un tiempo
            StartCoroutine(DestruirPersonajeDespuesDeTiempo());
        }
    }

    void Update()
    {
        // Si debe mirar al jugador, ajusta la rotación gradualmente
        if (shouldLookAtPlayer && personaje != null && player != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direction = player.position - personaje.position;
            direction.y = 0; // Mantener la rotación solo en el plano horizontal (Y no cambia)

            // Calcular la rotación necesaria
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Suavizar la rotación del personaje hacia el jugador
            personaje.rotation = Quaternion.Slerp(personaje.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Corrutina que espera y luego destruye el personaje
    private IEnumerator DestruirPersonajeDespuesDeTiempo()
    {
        // Esperar los segundos especificados antes de destruir
        yield return new WaitForSeconds(tiempoParaDestruir);

        // Destruir el personaje
        if (personaje != null)
        {
            Destroy(personaje.gameObject);
        }
    }

    // Corrutina para manejar la secuencia de los audios
    private IEnumerator ReproducirAudiosSecuencia()
    {
        // Esperar 4 segundos después de activar el susto para reproducir el audio2
        yield return new WaitForSeconds(3.5f);
        if (audio2 != null)
        {
            audio2.Play();
        }

        // Esperar 3 segundos después de reproducir el audio2 para reproducir el audio3
        yield return new WaitForSeconds(4.5f);
        if (audio3 != null)
        {
            audio3.Play();
        }

        // Esperar 2 segundos después de audio3 para reproducir audio4 y audio5 juntos
        yield return new WaitForSeconds(9.5f);
        if (audio4 != null && audio5 != null)
        {
            audio4.Play();
            audio5.Play();
        }
    }
}
