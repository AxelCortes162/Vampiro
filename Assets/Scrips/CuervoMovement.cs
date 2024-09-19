using UnityEngine;
using System.Collections; // Añadir esta línea para usar IEnumerator

public class CuervoMovement : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador desde el Inspector
    public float speed = 5f; // Velocidad de movimiento del cuervo
    private Animator animator;
    
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
    }

    public void StartFlying()
{
    if (audioSource != null)
    {
        audioSource.Play(); // Reproducir el audio
    }
    
    // Esperar un poco antes de iniciar la animación de vuelo
    Invoke(nameof(StartFlyingAnimation), 0.5f);
}

private void StartFlyingAnimation()
{
    animator.SetBool("Volar0", true); // Iniciar la animación de vuelo
    
    // Esperar un poco más antes de mover el cuervo hacia el jugador
    Invoke(nameof(StartMoving), 0.5f);
}

private void StartMoving()
{
    // Mover el cuervo hacia adelante
    Vector3 direction = transform.forward;
    transform.position += direction * speed * Time.deltaTime;

    StartCoroutine(DestroyAfterTime(4f));
}

private IEnumerator DestroyAfterTime(float time)
{
    yield return new WaitForSeconds(time);
    Destroy(gameObject);
}
}
