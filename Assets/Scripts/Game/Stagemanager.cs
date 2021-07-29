using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stagemanager : MonoBehaviour
{
    Prayer prayer;
    Gamemanager gamemanager;

    [Header("オブジェクトの座標")]
    public int vertical;
    public int horizontal;

    [Header("このオブジェクトは通過可能か")]
    [SerializeField] bool Traffic;

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
       // gamemanager.objecttagsearch(vertical, horizontal);
        prayer.Walk(vertical, horizontal);
    }

    public bool objectTraffic()
    {
        return Traffic;
    }

    public void rockmove(int i, string Coordinate)
    {
        if (Coordinate == "vertical")
        {
            if (gamemanager.objectsearch(vertical + i, horizontal))
            {
                vertical += i;
                Debug.Log("岩の位置:" + vertical + ":" + horizontal);
            }
            else
            {
                Debug.LogError("岩の移動に失敗しました:通行不可の場所に差し掛かりました");
            }
        }else if(Coordinate == "horizontal")
        {
            if (gamemanager.objectsearch(vertical, horizontal + i))
            {
                horizontal += i;
                Debug.Log("岩の位置:" + vertical + ":" + horizontal);
            }
            else
            {
                Debug.LogError("岩の移動に失敗しました:通行不可の場所に差し掛かりました");
            }
        }
    }
}
