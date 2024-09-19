using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audio1; // Audio que siempre estará sonando
    public AudioSource audio2; // Audio que empieza sonando y se detiene a los 30 segundos
    public AudioSource audio3; // Audio que empieza a sonar a los 30 segundos
    public AudioSource audio4; // Audio que empieza a sonar a los 30 segundos

    private float timer = 0f; // Temporizador para controlar el tiempo

    void Start()
    {
        // Comienza reproduciendo el audio1 y audio2
        audio1.Play();
        audio2.Play();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Detener audio2 después de 30 segundos
        if (timer >= 30f && audio2.isPlaying)
        {
            audio2.Stop();
        }

        // Comenzar a reproducir audio3 y audio4 después de 30 segundos
        if (timer >= 29f && !audio3.isPlaying && !audio4.isPlaying)
        {
            audio3.Play();
            audio4.Play();
        }
    }
}
