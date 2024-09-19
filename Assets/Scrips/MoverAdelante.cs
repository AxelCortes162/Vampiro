using UnityEngine;

public class MoverAdelante : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        // Mueve el objeto hacia adelante en el eje Z
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
