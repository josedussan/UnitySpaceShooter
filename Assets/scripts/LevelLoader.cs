using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transicion;
    public string scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transicion.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
