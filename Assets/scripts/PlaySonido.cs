using UnityEngine;

public class PlaySonido : MonoBehaviour
{
    [SerializeField] private AudioClip sonido;
    private AudioSource aSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        Sonido();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Sonido() {
        if (aSource != null) aSource.PlayOneShot(sonido);
    }
}
