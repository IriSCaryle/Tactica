﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stagemanager : MonoBehaviour
{
    Prayer prayer;
    Gamemanager gamemanager;

    [Header("オブジェクトの座標")]
    public int horizontal;
    public int vertical;

    [Header("このオブジェクトは通過可能か")]
    public bool Traffic;

    public int ID = 0;

    void Awake()
    {
        prayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Prayer>();
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    public void oncrick()
    {
        Debug.Log("縦:" + vertical + " 横:" + horizontal + " がクリックされました");
        StartCoroutine(prayer.Walk(horizontal, vertical));
    }

    public bool objectTraffic()
    {
        return Traffic;
    }

    public void rockmove(int i, string Coordinate)
    {
        if (Coordinate == "horizontal")
        {
            if (gamemanager.objecttagsearch(horizontal + i, vertical) == 1)
            {
                horizontal += i;
                Debug.Log("岩の位置:" + horizontal + ":" + vertical);
            } else
            {
                if (gamemanager.objecttagsearch(horizontal + i, vertical) == 6)
                {
                    gamemanager.mapcange(horizontal + i, vertical, 7, true);
                    ID = 1;
                    Traffic = true;
                    Debug.LogWarning("岩によって穴が塞がりました");
                }else Debug.LogError("岩の移動に失敗しました:通行不可のオブジェクトに接触しました");
            }
        } else if (Coordinate == "vertical")
        {
            if (gamemanager.objecttagsearch(horizontal, vertical + i) == 1)
            {
                horizontal += i;
                Debug.Log("岩の位置:" + horizontal + ":" + vertical);
            } else
            {
                if (gamemanager.objecttagsearch(horizontal, vertical + i) == 6)
                {
                    gamemanager.mapcange(horizontal, vertical + i, 7, true);
                    ID = 1;
                    Traffic = true;
                    Debug.LogWarning("岩によって穴が塞がりました");
                }else Debug.LogError("岩の移動に失敗しました:通行不可のオブジェクトに接触しました");
            }
        }
    }

    public bool teleporttraffic(int x,int y)
    {
        prayer.p_horizontal = x;
        prayer.p_vartical = y;
        return true;
    }
}
