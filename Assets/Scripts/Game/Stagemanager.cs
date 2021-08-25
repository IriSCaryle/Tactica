using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stagemanager : MonoBehaviour
{
    Player player;
    Gamemanager gamemanager;

    [Header("オブジェクトの座標")]
    public int horizontal;
    public int vertical;

    [Header("このオブジェクトは通過可能か")]
    public bool Traffic;

    public int ID = 0;

    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    public void oncrick()
    {
        Debug.Log("縦:" + vertical + " 横:" + horizontal + " がクリックされました");
        StartCoroutine(player.Walk(horizontal, vertical));
    }

    public bool objectTraffic()
    {
        return Traffic;
    }

    public void rockmove(int i, string Coordinate)
    {
        Debug.Log("!");
        if (Coordinate == "horizontal")
        {
            Debug.Log("横");
            if (gamemanager.objecttagsearch(horizontal + i, vertical) == 1)
            {
                gamemanager.mapcange(horizontal + i, vertical, 2);
                gamemanager.mapcange(horizontal, vertical, 1);
                gamemanager.SEoneshot(1);
                Debug.Log("岩の位置:" + horizontal + i + ":" + vertical);
            } else
            {
                if (gamemanager.objecttagsearch(horizontal + i, vertical) == 6)
                {
                    gamemanager.mapcange(horizontal + i, vertical, 7);
                    gamemanager.mapcange(horizontal, vertical, 1);
                    gamemanager.SEoneshot(1);
                    Debug.LogWarning("岩によって穴が塞がりました");
                }
                else Debug.LogError("岩の移動に失敗しました:通行不可のオブジェクトに接触しました");
            }
        } else if (Coordinate == "vertical")
        {
            Debug.Log("縦");
            if (gamemanager.objecttagsearch(horizontal, vertical + i) == 1)
            {
                gamemanager.mapcange(horizontal, vertical + i, 2);
                gamemanager.mapcange(horizontal, vertical, 1);
                gamemanager.SEoneshot(1);
                Debug.Log("岩の位置:" + horizontal + ":" + vertical + i);
            } else
            {
                if (gamemanager.objecttagsearch(horizontal, vertical + i) == 6)
                {
                    gamemanager.mapcange(horizontal, vertical + i, 7);
                    gamemanager.mapcange(horizontal, vertical, 1);
                    gamemanager.SEoneshot(1);
                    Debug.LogWarning("岩によって穴が塞がりました");
                }else Debug.LogError("岩の移動に失敗しました:通行不可のオブジェクトに接触しました");
            }
        }
    }

    public bool teleporttraffic(int x,int y)
    {
        player.p_horizontal = x;
        player.p_vartical = y;
        return true;
    }
}
