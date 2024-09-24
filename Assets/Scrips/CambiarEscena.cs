using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarEscena : MonoBehaviour
{
    public Collider triggerCollider;
    public GameObject escena1;
    public GameObject escena2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     
        {
            StartCoroutine(CambioEscena());
        }
    }

    private IEnumerator CambioEscena()
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }

        if (escena2 != null)
        {
            escena2.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        if (escena1 != null)
        {
            escena1.SetActive(false);
        }  

        Destroy(gameObject);
    }
}