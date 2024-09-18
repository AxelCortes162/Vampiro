using UnityEngine;
using System.Collections; // Añadir esta línea para usar IEnumerator

public class CuervoMovement : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador desde el Inspector
    public float speed = 5f; // Velocidad de movimiento del cuervo
    private Animator animator;
    private bool isFlying = false;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
    }

    void Update()
    {
        if (isFlying)
        {
            // Mover el cuervo hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void StartFlying()
    {
        isFlying = true;
        if (audioSource != null)
        {
            audioSource.Play(); // Reproducir el audio
        }
        StartCoroutine(DestroyAfterTime(4f)); // Iniciar la corrutina para destruir el cuervo después de 5 segundos
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject); // Destruir el cuervo
    }
}
