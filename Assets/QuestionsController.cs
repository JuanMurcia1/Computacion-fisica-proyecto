using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO.Ports;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;
}

[System.Serializable]
public class Retroalimenta
{
    public string retroAliemtaText;
    
}

public class QuestionsController : MonoBehaviour
{
    string datoRecibido;
    public SerialPort puerto= new SerialPort("COM3",9600);
    public List<Question> questions;
    private int currentQuestionIndex = 0;
    public TMP_Text questionText; 
    public List<Button> answerButtons;

    public List<Retroalimenta> retroalimentación;
    public TMP_Text retroalimentaText;
    public TMP_Text bienOmal;
    public GameObject panelRetro;

    public AudioSource audioSource;
     public AudioSource audioSource2;
  

    void Start()
    {
        panelRetro.SetActive(false);
        DisplayQuestion();
        puerto.ReadTimeout= 30;
        try {
    puerto.Open();
} catch (System.UnauthorizedAccessException e) {
    Debug.LogError("Access to the serial port is denied: " + e.Message);
} catch (System.IO.IOException e) {
    Debug.LogError("The specified port could not be found or could not be opened: " + e.Message);
} catch (Exception e) {
    Debug.LogError("Error opening serial port: " + e.Message);
}

    }

    void Update()
    {
        try{
        if (puerto.IsOpen)
        {
            datoRecibido= puerto.ReadLine();
            Debug.Log(datoRecibido);
            HandleArduinoInput(datoRecibido.Trim());
        }
        }catch(System.Exception ex1)
        {
            ex1= new System.Exception();
        }
    }

    void HandleArduinoInput(string input)
{
    if (input == "Boton1_p")
    {
        Answer(0);
    }
    else if (input == "Boton2_p")
    {
        Answer(1);
    }
    else if (input == "Boton3_p")
    {
        Answer(2);
    }
    else if (input == "Boton4_p")
    {
        Answer(3);
    }
}

    public void DisplayQuestion()
    {
        if (questionText == null || questions == null || currentQuestionIndex >= questions.Count)
        {
            Debug.LogError("Check if the questionText, questions list are assigned or index is out of range!");
            return;
        }

        if (answerButtons == null || answerButtons.Count == 0)
        {
            Debug.LogError("Answer Buttons are not assigned in the Inspector or list is empty!");
            return;
        }

        questionText.text = questions[currentQuestionIndex].questionText;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (answerButtons[i] == null)
            {
                Debug.LogError("One of the answer buttons is not assigned in the Inspector!");
                continue;
            }

            Text answerText = answerButtons[i].GetComponentInChildren<Text>();
            if (answerText == null)
            {
                Debug.LogError("Text component missing in one of the answer buttons!");
                continue;
            }

            if (i < questions[currentQuestionIndex].answers.Length)
            {
                answerText.text = questions[currentQuestionIndex].answers[i];
                answerButtons[i].onClick.RemoveAllListeners();
                int index = i;
                answerButtons[i].onClick.AddListener(() => Answer(index));
            }
            else
            {
                Debug.LogError("Not enough answers provided for the question!");
            }
        }
    }

    public void Answer(int index)
{
    Debug.Log("Button pressed: " + index);  // Registra qué botón fue presionado.
    // Mostrar retroalimentación
    if (retroalimentación != null && currentQuestionIndex < retroalimentación.Count)
    {
        retroalimentaText.text = retroalimentación[currentQuestionIndex].retroAliemtaText;
        panelRetro.SetActive(true);
    }

    if (index == questions[currentQuestionIndex].correctAnswerIndex)
    {
        Debug.Log("Correct Answer!");
        bienOmal.text = "Correct Answer!";
        audioSource.Play();
    }
    else
    {
        Debug.Log("Wrong Answer!");
        bienOmal.text = "Wrong Answer!";
        audioSource2.Play();
        
        
    }

    // Mover a la siguiente pregunta o terminar el cuestionario
    if (currentQuestionIndex + 1 < questions.Count)
    {
        currentQuestionIndex++;
        DisplayQuestion();
    }
    else
    {
        Debug.Log("Quiz Finished!");
        currentQuestionIndex = 0;  // Restablecer para comenzar de nuevo o manejar el fin del cuestionario
        SceneManager.LoadScene("Menu");
    }
}
    

public void siguientePregunta()
{
    panelRetro.SetActive(false);
}

    
}

