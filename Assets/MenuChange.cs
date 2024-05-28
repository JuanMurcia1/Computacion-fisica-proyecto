using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuChange : MonoBehaviour
{
     public GameObject panelInitial;
    public GameObject numberQuestion;
    
    // Start is called before the first frame update
    void Start()
    {
        numberQuestion.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Message()
    {
       
        panelInitial.SetActive(false);
        numberQuestion.SetActive(false);
      

    }
}
