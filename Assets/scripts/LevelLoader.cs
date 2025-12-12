using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transicion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string scene) {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene) {
        transicion.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
