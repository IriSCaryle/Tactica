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
    GameObject[] SBHVartical = new GameObject[10];
    GameObject[,] stageblocks = new GameObject[10, 10];
    GameObject[,] stage = new GameObject[10, 10];

    RectTransform[,] stagerect = new RectTransform[10, 10];

    Stagemanager[,] stagemanager = new Stagemanager[10, 10];

    void Start()
    {
        cSVLoad = GetComponent<CSVLoad>();
        genereatebject();
    }

    void genereatebject()
    {
        for(int i = 0; i < 10; i++)
        {
            SBHVartical[i] = stageblockholder.transform.GetChild(i).gameObject;
            for (int j = 0; j < 10; j++)
            {
                stageblocks[i, j] = SBHVartical[i].transform.GetChild(j).gameObject;
                stagerect[i, j] = stageblocks[i, j].GetComponent<RectTransform>();

                stage[i, j] = Instantiate(
                    testimage,
                    stagerect[i,j].position,
                    Quaternion.identity,parent.transform);

                stagemanager[i, j] = stage[i, j].GetComponent<Stagemanager>();
                stagemanager[i, j].vertical = i;
                stagemanager[i, j].horizontal = j;
            }
        }
    }

    public bool objectTrafficsearch(int x,int y)
    {
        return stagemanager[x,y].objectTraffic();
    }

    public string objecttagsearch(int x,int y)
    {
        Debug.Log("これは" + stage[x, y].gameObject.tag + "です");
        return stage[x, y].gameObject.tag;//タグ→ID
    }

    public void rockmovesearch(int x,int y,int z,string Coordinate)
    {
        stagemanager[x, y].rockmove(z, Coordinate);
    }

    public bool teleportsearch(string x)
    {
        for(int i = 0;i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                if(stage[i,j].gameObject.tag == "teleport_" + x)//タグ→ID
                {
                    return stagemanager[i,j].teleporttraffic(i,j);
                }
            }
        }
        return false;
    }
}
