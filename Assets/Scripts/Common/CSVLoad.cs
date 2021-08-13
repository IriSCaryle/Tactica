using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CSVLoad : MonoBehaviour
{

    List<string[]> CSVList = new List<string[]>();

    public Dictionary<int, GameObject> Blocks = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Awake()
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

        for(int i = 1; i < CSVList.Count; i++)
        {
            Debug.Log("ID:" + CSVList[i][0]);
            int ID=0;
            int.TryParse(CSVList[i][0], out ID);
           /* if (ID == 37)//ID37はプレイヤーのオブジェクトなので今のところ無視させてます  時が来たら消してください
            {
                continue;
            }*/
            Blocks.Add(ID, Resources.Load<GameObject>(CSVList[i][2]));
            Debug.Log("ブロックを追加:"+Blocks[ID].name);

        }
        Debug.Log("ブロックを登録完了、いつでも使えます");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
