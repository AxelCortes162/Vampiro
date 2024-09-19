using UnityEngine;
using System.Collections; 

public class LookAtPlayerV : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float delay = 55.0f; // Tiempo de espera en segundos

    private bool shouldRotate = false;

    void Start()
    {
        // Inicia la corrutina para esperar 45 segundos
        StartCoroutine(StartRotationAfterDelay());
    }

    void Update()
    {
        if (shouldRotate)
        {
            // Hace que el prefab mire al jugador
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.0f);
        }
    }

    private IEnumerator StartRotationAfterDelay()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(delay);
        shouldRotate = true;
    }
}
