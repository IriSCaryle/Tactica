using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CSVLoad : MonoBehaviour
{

    List<string[]> CSVList = new List<string[]>(); 
    // Start is called before the first frame update
    void Start()
    {
        LoadCSV();
    }


    void LoadCSV()
    {
        TextAsset BlockCSV = Resources.Load<TextAsset>("BlockList");
        StringReader render = new StringReader(BlockCSV.text);
        while (render.Peek() > -1)
        {
            CSVList.Add(render.ReadLine().Split(','));
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
