using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange(string Millonario)
    {
        SceneManager.LoadScene("Millonario");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
