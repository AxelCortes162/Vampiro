using UnityEngine;

public class ArañaAutoDestructiva : MonoBehaviour
{
    private void Start()
    {
        // Destruir la araña después de 25 segundos
        Destroy(gameObject, 25f);
    }
}