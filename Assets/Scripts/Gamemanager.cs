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

    RectTransform[,] stagerect = new RectTransform[10, 10];

    void Start()
    {
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
                stageblocks[i, j] = stageblockholder[i].transform.GetChild(j).gameObject;
                stagerect[i, j] = stageblocks[i, j].GetComponent<RectTransform>();
                stage[i, j] = Instantiate(
                    testimage,
                    stagerect[i,j].position,
                    Quaternion.identity,parent.transform);
            }
        }
    }
}
