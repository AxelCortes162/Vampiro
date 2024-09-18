using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Necesario para acceder a los componentes UI


public class ActivarSusto : MonoBehaviour
{
    public Image fadeImage; // El componente Image que controlará el desvanecimiento
    public Collider triggerCollider; // El collider que se desactivará al final
    public GameObject estatua; // Asigna la estatua desactivada desde el Inspector

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

                // Inicia el proceso de susto
                StartCoroutine(SustoSequence(vampiro));
            }
        }
    }

    // Coroutine para manejar toda la secuencia
    private IEnumerator SustoSequence(GameObject vampiro)
    {
        // Esperar 1 segundo antes de activar el panel negro
        yield return new WaitForSeconds(1.5f);

        // Activar el panel negro y hacer que se desvanezca a negro
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeToBlack());

        // Esperar 1 segundo con la pantalla en negro
        yield return new WaitForSeconds(2.0f);

        // Desactivar el vampiro y activar la estatua
        vampiro.SetActive(false);
        if (estatua != null)
        {
            estatua.SetActive(true); // Activar la estatua
        }

        // Empezar a desvanecer el panel negro de regreso a transparente
        StartCoroutine(FadeToClear());
    }

    // Coroutine para oscurecer la pantalla cambiando la transparencia del color del Image
    private IEnumerator FadeToBlack()
    {
        float duration = 0.5f; // Duración del efecto de desvanecimiento
        float currentTime = 0f;

        Color originalColor = fadeImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f); // Alpha a 1 (negro)

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(originalColor, targetColor, currentTime / duration); // Oscurece gradualmente
            yield return null;
        }
    }

    // Coroutine para desvanecer el panel negro a transparente
    private IEnumerator FadeToClear()
    {
        float duration = 3.0f; // Duración del efecto de desvanecimiento
        float currentTime = 0f;

        Color originalColor = fadeImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // Alpha a 0 (transparente)

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(originalColor, targetColor, currentTime / duration); // Aclarar gradualmente
            yield return null;
        }

        // Cuando el fade termina, desactiva el collider para evitar que el susto se repita
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }
}
