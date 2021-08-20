using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] RectTransform player_rectTransform;
    CSVLoad cSVLoad;
    [SerializeField] Animator animator;
    [SerializeField] Animator p_anim;
    [SerializeField] Text log_text;

    INIParser ini = new INIParser();

    [Header("オーディオ")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sE;

    [Header("生成した画像の親")]
    [SerializeField] GameObject parent;
    [Header("生成する画像")]
    [SerializeField] GameObject testimage;

    [Header("参照するオブジェクト")]
    [SerializeField] GameObject stageblockholder;
    GameObject[] SBHHrizontal = new GameObject[10];
    GameObject[,] stageblocks = new GameObject[10, 10];
    GameObject[,] stage = new GameObject[10, 10];
    Image[,] stageimage = new Image[10, 10];
    int[,] startpass = new int[10, 10];
    int[,] stagepass = new int[10, 10];

    RectTransform[,] stagerect = new RectTransform[10, 10];

    Stagemanager[,] stagemanager = new Stagemanager[10, 10];

    [Header("読み込むステージ名")]
    [SerializeField] string stagename;

    string testpath;

    [Header("ID更新ボタン")]//検証用
    [SerializeField] bool iDupdate;

    void Awake()
    {
        Application.targetFrameRate = 60;
        testpath = Application.streamingAssetsPath + "/defaultstages/" + stagename;
    }

    void Start()
    {
        //Clearanim();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player_rectTransform = player.GetComponent<RectTransform>();
        cSVLoad = GetComponent<CSVLoad>();
        gamereset();
    }

    private void Update()
    {
        if (iDupdate)
        {
            gameturncange();
            iDupdate = false;
        }
    }

    void iniload()
    {
        ini.Open(testpath + "/" + stagename + ".ini");
        int.TryParse(ini.ReadValue("Player", ":Life", "0"), out player.player_maxLife);
        int.TryParse(ini.ReadValue("Player", ":X", "1"), out player.p_horizontal);
        int.TryParse(ini.ReadValue("Player", ":Y", "1"), out player.p_vartical);

        log_text.text = "*そうさせつめい*\nプレイヤーとちょくせんじょうのマスをクリックかタップするとすすむ\n" +
            "いわ：となりならうごかせる\nとげ：さわるといたい\nくすり：のむとげんきになる\nこおり：すべる\n" +
            "ワープ：ワープする\nかいだん：ゴール、ここをめざそう";

        player.player_Life = player.player_maxLife;

        Debug.LogWarning(player.p_horizontal + "," + player.p_vartical);
    }

    void mapload()
    {
        string path = testpath + "/" + stagename + ".csv";
        StreamReader streamReader = new StreamReader(path);

        int count = 0;
        while (streamReader.Peek() > -1)
        {
            string teststr = "";
            int[] intline = new int[10];
            string[] line = streamReader.ReadLine().Split(',');
            for(int i = 0;i < line.Length; i++)
            {
                int.TryParse(line[i], out intline[i]);
                startpass[i,count] = intline[i];
                teststr += intline[i] + ",";
            }
            count++;
            //Debug.Log(teststr);
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

                //Debug.Log("stagepass:" + i + "," + j + "=" + stagepass[i, j]);
                objectinst(i,j,stagepass[i,j]);
            }
        }
    }

    void objectinst(int x,int y ,int z)
    {
        stage[x, y] = Instantiate(
            cSVLoad.Blocks[z],
            stagerect[x, y].position,
            Quaternion.identity,
            parent.transform
            );

        stageimage[x, y] = stage[x, y].GetComponent<Image>();

        if (z == 2 || z == 6 || z == 7 || z == 4 || z == 5)
        {
            //Debug.LogWarning("!");
            stageimage[x, y].color = Color.white;
        }
        stagemanager[x, y] = stage[x, y].GetComponent<Stagemanager>();

        stagemanager[x, y].ID = z;

        stagemanager[x, y].horizontal = x;
        stagemanager[x, y].vertical = y;
    }

    public void gamereset()//ステージの状態を初期化する
    {
        iniload();
        mapload();
        gameturncange();
        //p_anim.SetTrigger("idle");

        for (int i = 0; i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                stagepass[i, j] = startpass[i, j];
            }
        }
        Debug.Log("リセットしました");

        genereatebject();
    }

    public void gameturncange()//マップを更新する
    {
        player_rectTransform.anchoredPosition = new Vector2(player.p_horizontal * 125, player.p_vartical * -125 + 20);
    }

    public bool objectTrafficsearch(int x,int y)//侵入可能かの判定
    {
        return stagemanager[x,y].objectTraffic();
    }

    public int objecttagsearch(int x,int y)//objectのIDをみる
    {
        Debug.Log("これは" + stagemanager[x,y].ID + "です");
        return stagemanager[x, y].ID;
    }

    public void rockmovesearch(int x,int y,int z,string Coordinate)//岩を動かす処理
    {
        stagemanager[x, y].rockmove(z, Coordinate);
        Debug.Log(z + Coordinate);
    }

    public void mapcange(int x, int y, int z)//オブジェクトを変更する処理
    {
        Destroy(stage[x, y]);
        objectinst(x, y, z);
    }

    public bool teleportsearch(int x)//もう一方のテレポートを探す処理
    {
        for(int i = 0;i < 10; i++)
        {
            for(int j = 0;j < 10; j++)
            {
                if(stagepass[i,j] == x)
                {
                    return stagemanager[i,j].teleporttraffic(i,j);
                }
            }
        }
        return false;
    }

    public void Clearanim()
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger("CLEARanim");
    }

    public void SEoneshot(int i)
    {
        audioSource.PlayOneShot(sE[i]);
    }
}
