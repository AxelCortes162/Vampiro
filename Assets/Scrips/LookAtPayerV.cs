using UnityEngine;
using UnityEngine.AI; // Necesario para usar NavMeshAgent
using System.Collections;

public class LookAtPlayerV : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float delay = 55.0f; // Tiempo de espera en segundos
    public NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent que está desactivado

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
        }
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
    }
}
