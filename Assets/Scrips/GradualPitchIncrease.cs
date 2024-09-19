using UnityEngine;

public class GradualPitchIncrease : MonoBehaviour
{
    public AudioSource audioSource;
    public float initialDuration = 55.0f; // Duración en segundos del pitch normal (1 minuto)
    public float increaseDuration = 60.0f; // Duración en segundos del aumento de pitch (1 minuto)
    public float targetPitch = 3.0f; // Pitch objetivo

    private float initialPitch;
    private float elapsedTime = 0.0f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        initialPitch = audioSource.pitch;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > initialDuration && elapsedTime <= initialDuration + increaseDuration)
        {
            float t = (elapsedTime - initialDuration) / increaseDuration;
            audioSource.pitch = Mathf.Lerp(initialPitch, targetPitch, t);
        }
    }
}
