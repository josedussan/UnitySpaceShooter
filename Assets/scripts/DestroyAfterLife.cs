using System;
using UnityEngine;

public class DestroyAfterLife : MonoBehaviour
{
    [SerializeField] private float tiempo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, tiempo);
    }
}
