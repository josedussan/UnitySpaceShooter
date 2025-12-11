using System.Collections;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TMP_Text textoOleadas;
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
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                textoOleadas.text = "Nivel " + (i + 1) + "- Oleada" + (j + 1);
                yield return new WaitForSeconds(2f);
                textoOleadas.text = "";
                for (int k = 0; k < 10; k++)
                {
                    int posAleatoriaY;

                    do
                    {
                        posAleatoriaY = Random.Range(-9, 9);
                    }
                    while (posAleatoriaY == puntoAleatorioAnterior);
                    Debug.Log(posAleatoriaY);
                    puntoAleatorioAnterior = posAleatoriaY;
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, posAleatoriaY, 0);
                    Instantiate(enemyPrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);

                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(3f);
        }
        
    }
}
