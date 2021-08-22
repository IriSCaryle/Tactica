using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MapCSVLoad : MonoBehaviour
{
    // Start is called before the first frame update
    List<string[]> CSVList = new List<string[]>();

    public Dictionary<int,string> Maps = new Dictionary<int, string>();
    public List<string> MapNameList = new List<string>();
    void Awake()
    {
        Load();
    }
    

    void Load()
    {
        TextAsset BlockCSV = Resources.Load<TextAsset>("DefaultMapList");
        StringReader render = new StringReader(BlockCSV.text);
        while (render.Peek() > -1)
        {
            CSVList.Add(render.ReadLine().Split(','));

        }
        for (int i = 1; i < CSVList.Count; i++)
        {
            Debug.Log("ID:" + CSVList[i][0]);
            int ID = 0;
            int.TryParse(CSVList[i][0], out ID);
            MapNameList.Add(CSVList[i][1]);
            Maps.Add(ID, Application.streamingAssetsPath + CSVList[i][2]);
            Debug.Log("マップを追加:" );
        }
    }
}
