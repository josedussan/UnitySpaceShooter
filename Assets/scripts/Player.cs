using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerP : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject PanelGameOver,PanelPausa,PanelWin,PanelDamage;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject disparoPrefab,escudo;
    [SerializeField] private GameObject danoEnemigoPrefab;
    [SerializeField] private List<GameObject> SpawnPoints;
    [SerializeField] private Slider salud;
    [SerializeField] private TMP_Text score,ScoreWin;
    [SerializeField] private List<AudioClip> sonidos;
    private AudioSource aSource;
    private float temporizador = 0.5f;
    private int vidas = 100;
    private int scoreValor = 0;
    private int numPoints=1;
    private bool invulnerabilidad=false;
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
            for (int i = 0; i < numPoints; i++)
            {
                Instantiate(disparoPrefab, SpawnPoints[i].transform.position, Quaternion.identity);
            }
            
            temporizador = 0;
        }
    }

    public void ActualizarScore(int valor) {
        scoreValor += valor;
        score.text = scoreValor.ToString();
        ScoreWin.text= "SCORE: "+scoreValor.ToString();
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoEnemigo") || elOtro.gameObject.CompareTag("Enemigo"))
        {
            
            GameObject dano = Instantiate(danoEnemigoPrefab,elOtro.transform.position,Quaternion.identity);

            Destroy(dano,0.12f);
            Destroy(elOtro.gameObject);
            if (!invulnerabilidad)
            {
                vidas -= 5;
                ActualizarVida();
                aSource.PlayOneShot(sonidos[0]);
                StartCoroutine(recibirDanio());
                
                if (vidas <= 0)
                {
                    aSource.PlayOneShot(sonidos[1]);
                    Destroy(this.gameObject);
                    PanelGameOver.SetActive(true);
                }
            }
            
        }
        Debug.Log(elOtro.gameObject.tag);
        if (ActivarPowerUps(elOtro.gameObject.tag))
        {
            aSource.PlayOneShot(sonidos[2]);
            Destroy(elOtro.gameObject);
        }
    }

    IEnumerator recibirDanio() {
        PanelDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        PanelDamage.SetActive(false);
    }

    private bool ActivarPowerUps(string tag) {
        bool aplica = false;
        switch (tag)
        {
            case "PowerUp":
                numPoints = 3;
                StartCoroutine(Espera(8f));
                aplica = true;
                break;
            case "PowerUp1":
                escudo.gameObject.SetActive(true);
                invulnerabilidad = true;
                StartCoroutine(Espera(8f));
                aplica = true;
                break;
            case "PowerUp2":
                vidas += (100-vidas);
                ActualizarVida();
                aplica = true;
                break;
        }
        return aplica;
    }

    IEnumerator Espera(float tiempo) {
        yield return new WaitForSeconds(tiempo);
        numPoints = 1;
        escudo.gameObject.SetActive(false);
        invulnerabilidad = false;
    }




    public void PausarJuego()
    {
        Time.timeScale = 0f;
        PanelPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1f;
        PanelPausa.SetActive(false);
        PanelWin.SetActive(false);
    }


}
