using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerP : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject danoEnemigoPrefab;
    [SerializeField] private List<GameObject> SpawnPoints;
    [SerializeField] private Slider salud;
    [SerializeField] private TMP_Text score;
    [SerializeField] private List<AudioClip> sonidos;
    private AudioSource aSource;
    private float temporizador = 0.5f;
    private int vidas = 100;
    private int scoreValor = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        ActualizarVida();
    }

    // Update is called once per frame
    void Update()
    {

        Movimiento();
        DelimitarMov();
        Disparar();
        
    }

    void ActualizarVida() {
        salud.value = vidas;
    }

    void Movimiento() {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }

    void DelimitarMov() {
        float xClamped = Mathf.Clamp(transform.position.x, -17.79f, 17.79f);
        float yClamped = Mathf.Clamp(transform.position.y, -9.5f, 9.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    void Disparar() {
        temporizador += 1*Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)&& temporizador>ratioDisparo)
        {
            Instantiate(disparoPrefab,SpawnPoints[0].transform.position,Quaternion.identity);
            temporizador = 0;
        }
    }

    public void ActualizarScore(int valor) {
        scoreValor += valor;
        score.text = scoreValor.ToString();
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoEnemigo") || elOtro.gameObject.CompareTag("Enemigo"))
        {
            vidas -= 10;
            ActualizarVida();
            GameObject dano = Instantiate(danoEnemigoPrefab,elOtro.transform.position,Quaternion.identity);
            Destroy(dano,0.12f);
            Destroy(elOtro.gameObject);
            aSource.PlayOneShot(sonidos[0]);
            if (vidas<=0)
            {
                aSource.PlayOneShot(sonidos[1]);
                Destroy(this.gameObject,1f);
            }
        }
    }
}
