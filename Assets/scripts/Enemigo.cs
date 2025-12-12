using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip sonido;
    private AudioSource aSource;
    private PlayerP jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = FindAnyObjectByType<PlayerP>();
        aSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnDisparos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad * Time.deltaTime);
    }

    IEnumerator SpawnDisparos() {
        while(true)
        {
            Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoPlayer")|| elOtro.gameObject.CompareTag("Escudo"))
        {
            //aSource.PlayOneShot(sonido);
            jugador.ActualizarScore(10);
            GameObject explosion= Instantiate(explosionPrefab,this.transform.position,Quaternion.identity);
            if (!elOtro.gameObject.CompareTag("Escudo")) {
                Destroy(elOtro.gameObject);
            }
            Destroy(this.gameObject);
            Destroy(explosion,0.16f);
            
        }
    }
}
