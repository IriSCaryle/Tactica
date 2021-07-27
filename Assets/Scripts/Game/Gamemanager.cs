using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] Prayer prayer;

    [Header("生成した画像の親")]
    [SerializeField] GameObject parent;
    [Header("生成する画像")]
    [SerializeField] GameObject testimage;

    [Header("参照するオブジェクト")]
    [SerializeField] GameObject stageblockholder;
    GameObject[] SBHHorizontal = new GameObject[10];
    GameObject[,] stageblocks = new GameObject[10, 10];
    GameObject[,] stage = new GameObject[10, 10];

    RectTransform[,] stagerect = new RectTransform[10, 10];

    Stagemanager[,] stagemanager = new Stagemanager[10, 10];

    int p_vartical;
    int p_horizontal;

    void Start()
    {
        genereatebject();

        //確認用
        p_vartical = 1;
        p_horizontal = 1;
        //
    }

    void genereatebject()
    {
        for(int i = 0; i < 10; i++)
        {
            SBHHorizontal[i] = stageblockholder.transform.GetChild(i).gameObject;
            for (int j = 0; j < 10; j++)
            {
                stageblocks[i, j] = SBHHorizontal[i].transform.GetChild(j).gameObject;
                stagerect[i, j] = stageblocks[i, j].GetComponent<RectTransform>();

                stage[i, j] = Instantiate(
                    testimage,
                    stagerect[i,j].position,
                    Quaternion.identity,parent.transform);

                stagemanager[i, j] = stage[i, j].GetComponent<Stagemanager>();
                stagemanager[i, j].vertical = j;
                stagemanager[i, j].horizontal = i;
            }
        }
    }

   /* public void StagePointCheck(int x, int y)
    {
        if (x == p_vartical) prayer.Walk(0, y - p_horizontal);
        else if (y == p_horizontal) prayer.Walk(x - p_vartical, 0);
    }*/
}
