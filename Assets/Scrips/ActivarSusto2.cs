using UnityEngine;
using System.Collections;

public class ActivarSusto2 : MonoBehaviour
{
    public GameObject arañaPrefab; // Prefab de la araña a instanciar
    public int cantidadArañas = 25; // Número de arañas a instanciar
    public Vector3 terrenoMin; // Coordenadas mínimas del terreno para la instanciación
    public Vector3 terrenoMax; // Coordenadas máximas del terreno para la instanciación
    public float tiempoParaInstanciar = 2.0f; // Tiempo total en el que se instanciarán todas las arañas
    public SustosManager sustosManager; // Controlador de los sustos

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia el proceso de susto
            StartCoroutine(SustoSequence());
        }
    }

    // Coroutine para manejar la secuencia del susto
    private IEnumerator SustoSequence()
    {
        // Desactiva el collider para evitar que se repita el susto
        GetComponent<Collider>().enabled = false;

        // Llama la coroutine para instanciar arañas
        StartCoroutine(InstanciarArañas());

        // Aquí podrías agregar otros efectos de susto si lo deseas...

        // Notificar al SustosManager para activar el siguiente susto cuando termine
        yield return new WaitForSeconds(tiempoParaInstanciar);
        sustosManager.ActivarSiguienteSusto();

        // Destruir el susto después de completarse
        Destroy(gameObject);
    }

    // Coroutine para instanciar arañas aleatoriamente en un lapso de 2 segundos
    private IEnumerator InstanciarArañas()
    {
        float delayEntreArañas = tiempoParaInstanciar / cantidadArañas;

        for (int i = 0; i < cantidadArañas; i++)
        {
            // Generar una posición aleatoria dentro del terreno definido
            float randomX = Random.Range(terrenoMin.x, terrenoMax.x);
            float randomZ = 4; // Coordenada z fija a -3
            float randomY = Random.Range(terrenoMin.y, terrenoMax.y); // Si necesitas altura variable

            Vector3 posicionAleatoria = new Vector3(randomX, randomY, randomZ);

            // Instanciar la araña en la posición aleatoria
            Instantiate(arañaPrefab, posicionAleatoria, Quaternion.Euler(0, 180, 0));

            // Esperar un tiempo antes de instanciar la siguiente araña
            yield return new WaitForSeconds(delayEntreArañas);
        }
    }
}
