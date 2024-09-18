using UnityEngine;
using System.Collections;


public class PlayerLook : MonoBehaviour
{
    public float lookDistance = 100f; // Distancia máxima a la que el jugador puede ver
    public LayerMask layerMask; // Capas que el raycast debe considerar

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookDistance, layerMask))
        {
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
        }
    }
}
