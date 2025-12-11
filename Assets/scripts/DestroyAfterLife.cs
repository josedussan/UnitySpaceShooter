using System;
using UnityEngine;

public class DestroyAfterLife : MonoBehaviour
{
    private GameObject objeto;
    [SerializeField] private float tiempo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objeto = GetComponent<GameObject>();
        Destruir();
    }

    private void Destruir()
    {
        Destroy(objeto,tiempo);
    }
}
