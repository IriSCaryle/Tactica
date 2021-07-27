using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stagemanager : MonoBehaviour
{
    Prayer prayer;
    Gamemanager gamemanager;

    public int vertical;
    public int horizontal;

    void Awake()
    {
        prayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Prayer>();
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    void Update()
    {

    }

    public void oncrick()
    {
        Debug.Log("縦:" + vertical + " 横:" + horizontal + " がクリックされました");

        // gamemanager.StagePointCheck(vertical, horizontal);
        prayer.Walk(vertical, horizontal);
    }
}
