using UnityEngine;

public class Escudo : MonoBehaviour
{
    [SerializeField] private GameObject danioPrefab;
    [SerializeField] private GameObject explosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoEnemigo")||elOtro.gameObject.CompareTag("Enemigo")) {
            GameObject efecto;
            if (elOtro.gameObject.CompareTag("Enemigo"))
            {
                efecto = Instantiate(explosionPrefab, elOtro.transform.position, Quaternion.identity);
            }
            else {
                efecto= Instantiate(danioPrefab, elOtro.transform.position, Quaternion.identity);
            }

            Destroy(elOtro.gameObject);
            Destroy(efecto, 0.16f);
        }
    }


}
