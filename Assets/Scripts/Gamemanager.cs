using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject testimage;

    [SerializeField] GameObject[] stageblockholder = new GameObject[10];
    GameObject[,] stageblocks = new GameObject[10, 10];
    GameObject[,] stage = new GameObject[10, 10];

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                stageblocks[i, j] = stageblockholder[i].transform.GetChild(j).gameObject;
                Debug.Log(stageblocks[i, j].name +"parent:"+stageblocks[i,j].transform.parent.name+ "i="+i +"j="+j);
            }
        }
        genereatebject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void genereatebject()
    {
        for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                stage[i, j] = Instantiate(
                    testimage,
                    stageblocks[i, j].transform.localPosition,
                    Quaternion.identity);

                stage[i, j].transform.parent = parent.transform;
            }
        }
    }
}
