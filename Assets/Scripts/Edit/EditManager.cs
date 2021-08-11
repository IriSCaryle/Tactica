using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SFB;
public class EditManager : MonoBehaviour
{

    public int[,] EdittingStage = new int[,] {//現在のブロック配列

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

    int[,] initStage = new int[,]//初期状態のブロック配列
    {
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
    [Header("現在選択しているブロックのID")]
    [Range(0, 36)] public int CurrentBlockID = 0;
    [Header("選択しているイメージ画像")]
    [SerializeField] Image CurrentImage;
    [Header("消しゴムトグル")]
    public Toggle eraser;
    [Header("各種スクリプト")]
    public CSVLoad csvLoad;
    [SerializeField] EditMapSetting editMapSetting;
    [SerializeField] PalletManager palletManager;
    GameObject tmp;//選択しているブロックのイメージオブジェクト
    [Header("ブロックのイメージの親オブジェクト")]
    [SerializeField] GameObject[] BlocksParentObject = new GameObject[8];
    GameObject[,] BlockObjects = new GameObject[8, 8];
    [Header("エラー用イメージオブジェクト")]
    [SerializeField] GameObject Error1;
    [SerializeField] GameObject Error2;
    [SerializeField] GameObject Error3;

    List<int[]> ReadMap = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        OnClickBlockChange();
        initBoard();
    }
    void WriteBoardLine(int[,] board)
    {
        Debug.Log("現在のボードの状況");
        for (int i = 0; i < board.GetLength(0); i++)
        {
            string tmp = "";
            for (int h = 0; h < board.GetLength(1); h++)
            {
                tmp += board[i, h] + ",";
            }
            Debug.Log(tmp);
        }
    }

    void initBoard()
    {
        EdittingStage = initStage;
        GetObjects();
        palletManager.LoadSprites();
    }


    void GetObjects()
    {
        for (int v = 1; v <= BlockObjects.GetLength(0); v++)
        {
            
            for(int h = 1; h <= BlockObjects.GetLength(1); h++)
            {
                BlockObjects[v - 1, h - 1] = BlocksParentObject[v - 1].transform.GetChild(h).gameObject;
            }
        }

        Debug.Log("BlockObjectの取得が完了");
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
                if (h == EdittingStage.GetLength(1) - 1)
                {
                    line += string.Format("{0}", EdittingStage[v, h]);
                }
                else
                {
                    line += string.Format("{0},", EdittingStage[v, h]);
                }
                
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
        inifile.WriteValue("Player", ":X", editMapSetting.PlayerHorizontal);
        inifile.WriteValue("Player", ":Y", editMapSetting.PlayerVertical);
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
        if (CheckNull())
        {
            Debug.Log("セーブを開始");
            Save();
        }
        else
        {
            Debug.LogError("エラーが発生していますせーぷを中止します");
        }

    }

    public void OnClickOpen()
    {
        Open();
    }

    void Open()
    {
        string path = StandaloneFileBrowser.OpenFilePanel("CSVファイルを開いてください", Application.streamingAssetsPath,"CSV",false)[0];
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        StreamReader reader = new StreamReader(path);

        while (reader.Peek() > -1)
        {
            int[] line = new int[0];

            string[] stline = reader.ReadLine().Split(',');

            Array.Resize(ref line, stline.Length);

            for (int i = 0; i < stline.Length; i++)
            {
                int.TryParse(stline[i], out line[i]);
            }

            ReadMap.Add(line);

        }

        for (int v = 0; v < ReadMap.Count; v++)
        {
            string line="";
            for(int h = 0; h < ReadMap[v].Length; ++h)
            {
                line += ReadMap[v][h] + ","; 
                Debug.Log("v:" + v + ",h:" + h + ",value:" + ReadMap[v][h]);
            }
            Debug.Log("Line" + v + ":" + line);
        }
    }
    bool CheckNull()
    {
        string FolderName = editMapSetting.FileName;
        string Step = editMapSetting.Life;
        string PlayerXpos = editMapSetting.PlayerHorizontal;
        string PlayerYpos = editMapSetting.PlayerVertical;

        if (string.IsNullOrEmpty(FolderName))
        {
            Debug.LogError("フォルダ名が設定されていません");
            Error3.SetActive(true);
            return false;
        }
        if (string.IsNullOrEmpty(Step))
        {
            Debug.LogError("歩数が設定されていません");
            Error2.SetActive(true);
            return false;
        }
        if(string.IsNullOrEmpty(PlayerXpos) || string.IsNullOrEmpty(PlayerYpos))
        {
            Debug.LogError("プレイヤーの初期位置が設定されていません");
            Error1.SetActive(true);
            return false;
        }


        return true;

    }
  

 

    void Save()
    {
        WriteBoardLine(EdittingStage);
        string folderpath = Application.streamingAssetsPath + "/" + editMapSetting.FileName;
        if (Directory.Exists(folderpath))
        {
            Debug.Log("フォルダを発見、上書きします");
            Debug.Log("CSVファイルを生成");
            GenerateCSVFile(folderpath, editMapSetting.FileName);
            Debug.Log("iniファイルを生成");
            Generateini(folderpath, editMapSetting.FileName);
        }
        else
        {
            Debug.Log("フォルダが見つかりませんでした、作成します");
            Debug.Log("フォルダ作成");
            GenerateFolder(folderpath);
            Debug.Log("iniファイルを生成");
            Generateini(folderpath, editMapSetting.FileName);
            Debug.Log("CSVファイルを生成");
            GenerateCSVFile(folderpath, editMapSetting.FileName);
        }
    }

    public void SettingPlayer(int vertical,int horizontal)
    {
        editMapSetting.PlayerHorizontal = horizontal.ToString();
        editMapSetting.PlayerVertical = vertical.ToString();
        editMapSetting.X.text = horizontal.ToString();
        editMapSetting.Y.text = vertical.ToString();
    }
}
