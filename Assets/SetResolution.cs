using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    void Start()
    {
        // Configura la resolución deseada (ancho, alto, pantalla completa)
        Screen.SetResolution(1920, 1080, true);
    }
}

