using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Gamemanager : MonoBehaviour
{
    // [SerializeField] Prayer prayer;
    CSVLoad cSVLoad;

    [Header("生成した画像の親")]
    [SerializeField] GameObject parent;
    [Header("生成する画像")]
    [SerializeField] GameObject testimage;

    [Header("参照するオブジェクト")]
    [SerializeField] GameObject stageblockholder;
    GameObject[] SBHHrizontal = new GameObject[10];
    GameObject[,] stageblocks = new GameObject[10, 10];
    GameObject[,] stage = new GameObject[10, 10];
    int[,] stagepass = new int[10, 10];

    RectTransform[,] stagerect = new RectTransform[10, 10];

    Stagemanager[,] stagemanager = new Stagemanager[10, 10];

    [Header("ID更新ボタン")]//検証用
    [SerializeField] bool iDupdate;

    void Start()
    {
        cSVLoad = GetComponent<CSVLoad>();
        genereatebject();
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
                stagepass[i, j] = stagemanager[i, j].ID;
                stagemanager[i, j].vertical = i;
                stagemanager[i, j].horizontal = j;
            }
        }
    }

    public void gameturncange()//ブロックが動いた際にオブジェクトのマップを更新する
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                stagepass[i, j] = stagemanager[i, j].ID;
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
        return stagepass[x,y];//タグ→ID
    }

    public void rockmovesearch(int x,int y,int z,string Coordinate)//岩を動かす処理
    {
        stagemanager[x, y].rockmove(z, Coordinate);
    }

    public void mapcange(int x, int y, int z)//穴が塞がる処理
    {
        stagemanager[x, y].ID = z;
    }

    public void Trafficcange(int x, int y)
    {
        stagemanager[x, y].Traffic = !stagemanager[x, y].Traffic;
    }

    public bool teleportsearch(int x)//もう一方のテレポートを探す処理
    {
        for(int i = 0;i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                if(stagepass[i,j] == x)//タグ→ID
                {
                    return stagemanager[i,j].teleporttraffic(i,j);
                }
            }
        }
        return false;
    }
}
