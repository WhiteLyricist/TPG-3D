﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Operate() // Метод с таким же именем как в сценарии для двери.
    {
        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));  // RGB - значения в диапозоне от 0 до 1.
        GetComponent<Renderer>().material.color = random; // Цвет задаётся в назначенном объекту материалу.
    }
}
