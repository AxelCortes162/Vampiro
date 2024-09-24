using UnityEngine;
using UnityEngine.AI; // Necesario para usar NavMeshAgent
using System.Collections;

public class LookAtPlayerV : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float delay = 55.0f; // Tiempo de espera en segundos
    public NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent que está desactivado
    public AudioSource audioSource; // Referencia al componente de audio
    public AudioClip audioClip; // Referencia al clip de audio

    private bool shouldRotate = false;

    void Start()
    {
        // Inicia la corrutina para esperar 55 segundos
        StartCoroutine(StartRotationAfterDelay());

        // Asegurarse de que el NavMeshAgent esté desactivado inicialmente
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        // Desactivar el audio source al principio
        if (audioSource != null)
        {
            audioSource.enabled = false; // Desactivar el audio source
        }
    }

    void Update()
    {
        if (shouldRotate)
        {
            // Hace que el objeto mire al jugador
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.0f);

            // Moverse hacia el jugador
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(player.position); // Fija el destino del NavMeshAgent hacia el jugador
            }

            // Verificar si está cerca del jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 2.0f) // ajusta el valor de distancia según sea necesario
            {
                // Activar el bool del animator
                GetComponent<Animator>().SetBool("SustoTumba", true);

                // Reproducir el sonido
                audioSource.PlayOneShot(audioClip); // Reproduce el clip de audio

                // Destruir el objeto cuando termine la animación
                StartCoroutine(DestroyAfterAnimation());
            }
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private IEnumerator StartRotationAfterDelay()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Activa la rotación hacia el jugador
        shouldRotate = true;

        // Activa el NavMeshAgent si no está ya activado
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }

        // Activar el audio source después del tiempo de espera
        if (audioSource != null)
        {
            audioSource.enabled = true; // Activar el audio source
            audioSource.Play(); // Reproducir el audio
        }
    }
}
