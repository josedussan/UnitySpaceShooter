using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefab;
    [SerializeField] private TMP_Text textoOleadas;
    [SerializeField] private List<GameObject> PowerUps;
    [SerializeField] private GameObject PanelWin;
    private float probA = 0.4f, probB = 0.3f;
    private int puntoAleatorioAnterior = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnearEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnearEnemy() {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                textoOleadas.text = "Nivel " + (i + 1) + " - Oleada " + (j + 1);
                yield return new WaitForSeconds(2f);
                textoOleadas.text = "";
                for (int k = 0; k < 8; k++)
                {
                    
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, NumeroAletario(), 0);
                    Instantiate(EscogerEnemigo(j), puntoAleatorio, Quaternion.identity,transform);
                    if (k==3)
                    {
                        Vector3 puntoAleatorioP = new Vector3(transform.position.x, NumeroAletario(), 0);
                        Instantiate(SpawnPowerUp(), puntoAleatorioP, Quaternion.identity, transform);
                    }
                    yield return new WaitForSeconds(1f);

                }
                yield return new WaitForSeconds(3f);
            }
            yield return new WaitForSeconds(5f);
        }
        PanelWin.SetActive(true);
    }

    private int NumeroAletario() {
        int posAleatoriaY;

        do
        {
            posAleatoriaY = Random.Range(-9, 9);
        }
        while (posAleatoriaY == puntoAleatorioAnterior);
        puntoAleatorioAnterior = posAleatoriaY;
        return posAleatoriaY;
    }

    GameObject EscogerEnemigo(int oleada) {
        int valor = Random.Range(0, oleada);
        return enemyPrefab[valor];
    }

    public GameObject SpawnPowerUp()
    {
        float r = Random.value; // 0.0 – 1.0
        GameObject resultado;
        if (r <= probA)
        {
            resultado= PowerUps[0];
        }
        else if (r <= probA + probB)
        {
            resultado= PowerUps[1];
        }
        else
        {
            resultado= PowerUps[2];
        }

        return resultado;
    }
}
