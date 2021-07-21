using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stagemanager : MonoBehaviour
{
    Gamemanager gamemanager;

    public int vertical;
    public int horizontal;

    void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    void Update()
    {

    }

    public void oncrick()
    {
        Debug.Log("!");


    }
}
