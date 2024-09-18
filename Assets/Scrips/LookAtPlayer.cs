using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador desde el Inspector

    void Update()
    {
        // Asegúrate de que el prefab siempre esté mirando hacia el jugador
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }
}
