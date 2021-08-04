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
    public bool Traffic;

    public int ID = 0;

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
            if (gamemanager.objecttagsearch(vertical + i, horizontal) == 1)
            {
                vertical += i;
                Debug.Log("岩の位置:" + vertical + ":" + horizontal);
            } else
            {
                if (gamemanager.objecttagsearch(vertical + i, horizontal) == 6)
                {
                    gamemanager.mapcange(vertical + i, horizontal, 7);
                    ID = 1;
                    Debug.Log("岩によって穴が塞がりました");
                }
                Debug.LogError("岩の移動に失敗しました:通行不可のオブジェクトに接触しました");
            }
        } else if (Coordinate == "horizontal")
        {
            if (gamemanager.objecttagsearch(vertical, horizontal + i) == 1)
            {
                horizontal += i;
                Debug.Log("岩の位置:" + vertical + ":" + horizontal);
            } else
            {
                if (gamemanager.objecttagsearch(vertical, horizontal + i) == 6)
                {
                    gamemanager.mapcange(vertical, horizontal + i, 7);
                    ID = 1;
                    Debug.Log("岩によって穴が塞がりました");
                }
                Debug.LogError("岩の移動に失敗しました:通行不可のオブジェクトに接触しました");
            }
        }
    }

    public bool teleporttraffic(int x,int y)
    {
        prayer.t_vartical = x;
        prayer.t_horizontal = y;
        return true;
    }
}
