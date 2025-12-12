using UnityEngine;

public class SeguirObjetivo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private float rotacion = 400f;
    private Transform objetivo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            objetivo.position = Vector3.zero;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo == null) return;

        // Dirección hacia el player
        Vector2 direccion = (objetivo.position - transform.position).normalized;

        // Rotar suavemente hacia el objetivo
        float rot = Vector3.Cross(direccion, transform.up).z;
        transform.Rotate(0, 0, -rot * rotacion * Time.deltaTime);

        // Avanzar hacia adelante
        transform.Translate(Vector3.up * velocidad * Time.deltaTime, Space.Self);
    }
}
