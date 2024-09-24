using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour
{
    public float lookDistance = 100f; // Distancia máxima a la que el jugador puede ver
    public LayerMask layerMask; // Capas que el raycast debe considerar
    public float moveSpeed = 2f; // Velocidad de movimiento hacia el jugador

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookDistance, layerMask))
        {
            // Lógica para los cuervos
            if (hit.collider.CompareTag("Cuervo"))
            {
                Debug.Log("Cuervo a la vista!!!!!!");
                Animator cuervoAnimator = hit.collider.GetComponent<Animator>();
                if (cuervoAnimator != null)
                {
                    cuervoAnimator.SetTrigger("Volar");
                    CuervoMovement cuervoMovement = hit.collider.GetComponent<CuervoMovement>();
                    if (cuervoMovement != null)
                    {
                        cuervoMovement.StartFlying();
                    }
                }
            }

            // Lógica para los murciélagos
            if (hit.collider.CompareTag("Murcielagos"))
            {
                Debug.Log("Murciélagos a la vista!!!!!!");
                StartCoroutine(ActivateMurcielagos(hit.collider.transform));
            }
        }
    }

    // Coroutine para activar los murciélagos con un retraso de 2 segundos
    IEnumerator ActivateMurcielagos(Transform murcielagosParent)
    {
        yield return new WaitForSeconds(2f);

        // Recorrer todos los hijos (murciélagos) del objeto padre
        for (int i = 0; i < murcielagosParent.childCount; i++)
        {
            Transform murcielago = murcielagosParent.GetChild(i);
            Animator murcielagoAnimator = murcielago.GetComponent<Animator>();
            AudioSource murcielagoAudio = murcielago.GetComponent<AudioSource>();
            
            if (murcielagoAnimator != null)
            {
                murcielagoAnimator.SetBool("volar", true); // Activar animación "volar"
            }

            if (murcielagoAudio != null)
            {
                // Activar el AudioSource si está deshabilitado
                if (!murcielagoAudio.enabled)
                {
                    murcielagoAudio.enabled = true;
                }

                // Reproducir el audio
                murcielagoAudio.Play();
            }

            // Mover cada murciélago hacia el jugador
            StartCoroutine(MoveTowardsPlayer(murcielago));

            // Iniciar la destrucción de cada murciélago 2 segundos después de activar el vuelo
            StartCoroutine(DestroyMurcielagoAfterTime(murcielago, 2f));
        }
    }

    // Coroutine para mover los murciélagos hacia el jugador
    IEnumerator MoveTowardsPlayer(Transform murcielago)
    {
        while (murcielago != null && Vector3.Distance(murcielago.position, transform.position) > 0.5f)
        {
            murcielago.position = Vector3.MoveTowards(murcielago.position, transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Coroutine para destruir el murciélago después de un tiempo
    IEnumerator DestroyMurcielagoAfterTime(Transform murcielago, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (murcielago != null)
        {
            Destroy(murcielago.gameObject); // Destruye el murciélago
        }
    }
}
