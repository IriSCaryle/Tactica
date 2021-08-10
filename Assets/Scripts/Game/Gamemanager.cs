﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] RectTransform player_rectTransform;
    CSVLoad cSVLoad;
    [SerializeField] Animator animator;

    [Header("オーディオ")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sE;

    [Header("生成した画像の親")]
    [SerializeField] GameObject parent;
    [Header("生成する画像")]
    [SerializeField] GameObject testimage;

    [Header("参照するオブジェクト")]
    [SerializeField] GameObject stageblockholder;
    GameObject[] SBHHrizontal = new GameObject[10];
    GameObject[,] stageblocks = new GameObject[10, 10];
    GameObject[,] stage = new GameObject[10, 10];
    int[,] startpass = new int[10, 10];
    int[,] stagepass = new int[10, 10];

    RectTransform[,] stagerect = new RectTransform[10, 10];

    Stagemanager[,] stagemanager = new Stagemanager[10, 10];

    [Header("ID更新ボタン")]//検証用
    [SerializeField] bool iDupdate;

    void Start()
    {
        //Clearanim();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player_rectTransform = player.GetComponent<RectTransform>();
        cSVLoad = GetComponent<CSVLoad>();
        genereatebject();
        gamereset();
    }

    private void Update()
    {
        if (iDupdate)
        {
            gameturncange();
            iDupdate = false;
        }
    }

    void genereatebject()//ブロック生成＆ID取り込み
    {
        for(int i = 0; i < 10; i++)
        {
            SBHHrizontal[i] = stageblockholder.transform.GetChild(i).gameObject;
            for (int j = 0; j < 10; j++)
            {
                stageblocks[i, j] = SBHHrizontal[i].transform.GetChild(j).gameObject;
                stagerect[i, j] = stageblocks[i, j].GetComponent<RectTransform>();

                stage[i, j] = Instantiate(
                    testimage,
                    //cSVLoad.Blocks[1],
                    stagerect[i,j].position,
                    Quaternion.identity,parent.transform);

                stagemanager[i, j] = stage[i, j].GetComponent<Stagemanager>();

                startpass[i, j] = stagemanager[i, j].ID;

                stagemanager[i, j].horizontal = i;
                stagemanager[i, j].vertical = j;
            }
        }
    }

    public void gamereset()//ステージの状態を初期化する
    {
        player.player_Life = player.player_maxLife;//検証用
        player.p_horizontal = 1;//検証用
        player.p_vartical = 1;//検証用
        gameturncange();

        for (int i = 0; i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                stagepass[i, j] = startpass[i, j];
            }
        }
        Debug.Log("リセットしました");
    }

    public void gameturncange()//マップを更新する
    {
        player_rectTransform.anchoredPosition = new Vector2(player.p_horizontal * 125, player.p_vartical * -125 + 20);
        for (int i = 0; i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                stagepass[i, j] = stagemanager[i, j].ID;
                //オブジェクトの画像を差し替える処理をここに書く
            }
        }
    }

    public bool objectTrafficsearch(int x,int y)//侵入可能かの判定
    {
        return stagemanager[x,y].objectTraffic();
    }

    public int objecttagsearch(int x,int y)//objectのIDをみる
    {
        Debug.Log("これは" + stagepass[x,y] + "です");
        return stagepass[x,y];
    }

    public void rockmovesearch(int x,int y,int z,string Coordinate)//岩を動かす処理
    {
        stagemanager[x, y].rockmove(z, Coordinate);
    }

    public void mapcange(int x, int y, int z, bool traffic)//オブジェクトを変更する処理
    {
        stagemanager[x, y].ID = z;
        stagemanager[x, y].Traffic = traffic;
    }

    public bool teleportsearch(int x)//もう一方のテレポートを探す処理
    {
        for(int i = 0;i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                if(stagepass[i,j] == x)
                {
                    return stagemanager[i,j].teleporttraffic(i,j);
                }
            }
        }
        return false;
    }

    public void Clearanim()
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger("CLEARanim");
    }

    public void SEoneshot(int i)
    {
        audioSource.PlayOneShot(sE[i]);
    }
}
