using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class EditManager : MonoBehaviour
{
    public int[,] EdittingStage = new int[,] {

    {18,19,19,19,19,19,19,19,19,12},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {17,0,0,0,0,0,0,0,0,13},
    {16,15,15,15,15,15,15,15,15,14},
    
    };

    [Range(0, 36)] public int CurrentBlockID =0;

    public CSVLoad csvLoad;

    [SerializeField] Image CurrentImage;
    public Toggle eraser;
    [SerializeField] EditMapSetting editMapSetting;
    GameObject tmp;

    
    // Start is called before the first frame update
    void Start()
    {
        OnClickBlockChange();
        initBoard();
    }
    void WriteBoardLine()
    {
        Debug.Log("現在のボードの状況");
        for (int i = 0; i < EdittingStage.GetLength(0); i++)
        {
            string tmp = "";
            for (int h = 0; h < EdittingStage.GetLength(1); h++)
            {
                tmp += EdittingStage[i, h] + ",";
            }
            Debug.Log(tmp);
        }
    }

    void initBoard()
    {
        File.CreateText(Application.streamingAssetsPath + "/" + "test.ini");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickBlockChange()
    {
        if (CurrentBlockID == 36)
        {
            Debug.Log("ID:1に戻りました");
            CurrentBlockID =1;
        }
        else
        {
            CurrentBlockID += 1;
        }
        
        ChangeImage(CurrentBlockID);
    }

    void ChangeImage(int number)
    {
        Destroy(tmp);

        
        tmp = Instantiate<GameObject>(csvLoad.Blocks[number],new Vector2(0,0),Quaternion.identity,CurrentImage.transform);
        tmp.transform.localPosition = new Vector3(0, 0, 0);
        Debug.Log("ChangeBlock:" + number);
    }

   

    void GenerateCSVFile(string folderpath,string filename)
    {
        string filepath = folderpath+"/"+ filename + ".csv";

        bool isOverwrite = findFile(filepath);

        StreamWriter file = new StreamWriter(filepath, !isOverwrite,Encoding.UTF8);//falseの場合上書き

        for (int v = 0; v < EdittingStage.GetLength(0); v++)
        {
            string line = "";
            for(int h = 0; h < EdittingStage.GetLength(1); h++)
            {
                line += string.Format("{0},", EdittingStage[v, h]);
            }

            file.WriteLine(line);
        }

        file.Close();
    }

    

    void Generateini(string folderpath,string filename)
    {
        string filepath = folderpath + "/" + filename + ".ini";

        bool isOverwrite = findFile(filepath);

        INIParser inifile = new INIParser();

        inifile.Open(filepath);
        inifile.WriteValue("Player",":Life",editMapSetting.Life);
        inifile.Close();
    }

    bool findFile(string filepath)//ファイルがあるかを検出しboolを返す
    {
        if (File.Exists(filepath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GenerateFolder(string path)
    {
        Directory.CreateDirectory(path);
    }
    public void OnClickSave()
    {
        WriteBoardLine();
        string folderpath = Application.streamingAssetsPath +"/"+ editMapSetting.FileName;
        if (Directory.Exists(folderpath))
        {
            Debug.Log("フォルダを発見、上書きします");
            Debug.Log("CSVファイルを生成");
            GenerateCSVFile(folderpath,editMapSetting.FileName);
            Debug.Log("iniファイルを生成");
            Generateini(folderpath, editMapSetting.FileName);
        }
        else
        {
            Debug.Log("フォルダが見つかりませんでした、作成します");
            Debug.Log("フォルダ作成");
            GenerateFolder(folderpath);
            Debug.Log("iniファイルを生成");
            Generateini(folderpath,editMapSetting.FileName);
            Debug.Log("CSVファイルを生成");
            GenerateCSVFile(folderpath,editMapSetting.FileName);
        }
       
    }

  

}
