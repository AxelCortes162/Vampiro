using UnityEngine;

public class ActivarSusto : MonoBehaviour
{
    // Método que se llama cuando otro objeto entra en el trigger del collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el collider es el Player (Character Controller) con el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Busca el prefab con el tag "Vampiro"
            GameObject vampiro = GameObject.FindGameObjectWithTag("Vampiro");

            if (vampiro != null)
            {
                // Activar el Animator
                Animator vampiroAnimator = vampiro.GetComponent<Animator>();
                if (vampiroAnimator != null)
                {
                    vampiroAnimator.enabled = true; // Activa el Animator
                }

                // Activar el AudioSource
                AudioSource vampiroAudio = vampiro.GetComponent<AudioSource>();
                if (vampiroAudio != null)
                {
                    vampiroAudio.enabled = true; // Activa el AudioSource si estaba desactivado

                    // Reproduce el sonido
                    vampiroAudio.Play();
                }
            }
        }
    }
}
