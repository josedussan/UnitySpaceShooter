using UnityEngine;

public class DisparoGuiado : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Vector3 direccion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direccion = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
}
