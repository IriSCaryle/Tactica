using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PalletManager : MonoBehaviour
{

    [SerializeField] EditManager editManager;
    [SerializeField] CSVLoad cSVLoad;

    [SerializeField] ToggleGroup toggleGroup;

    [SerializeField] List<Toggle> PalletBlocks;
    [SerializeField] List<GameObject> PalletObjects;
    public int[] PalletBlocksID = new int[26];


   
    // Start is called before the first frame update
    void Start()
    {
        CheckToggle();
    }

    public void LoadSprites()
    {
        LoadWall();
        LoadIce();
        LoadHole();
        LoadTeleport();
        LoadStair();
        LoadMedicine();
        LoadThorn();
        LoadRock();
        LoadPlayer();
    }

    public void CheckToggle()
    {
        Toggle Activetoggle = toggleGroup.ActiveToggles().FirstOrDefault();
        for (int i = 0; i < PalletBlocks.Count; i++)
        {
            if(PalletBlocks[i].gameObject.name == Activetoggle.gameObject.name)
            {
                editManager.CurrentBlockID = PalletBlocksID[i];
                Debug.Log("ID:" + editManager.CurrentBlockID +",Name:" +Activetoggle.gameObject.name);
                break;
            }
        }
     
    }
    void LoadWall()
    {
        int index = 0;
        GameObject Wall;
        for(int id = 12; id <= 36; id++)// 12 => 壁オブジェクトの開始ID 36 =>壁オブジェクト終了ID
        {
            Wall = Instantiate<GameObject>(cSVLoad.Blocks[id], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[index].transform.GetChild(0).gameObject.transform);
            Wall.transform.SetAsFirstSibling();
            Wall.transform.localPosition = new Vector3(0, 0, 0);
            Wall.transform.localScale = new Vector3(0.64f, 0.64f, 1);
            PalletObjects.Add(Wall);
            PalletBlocksID[index] = id;
            index += 1;
        }

    } 

    void LoadIce()
    {//3 =>氷ブロック
        GameObject Ice;
        Ice = Instantiate<GameObject>(cSVLoad.Blocks[3], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[25].transform.GetChild(0).gameObject.transform);
        Ice.transform.SetAsFirstSibling();
        Ice.transform.localPosition = new Vector3(0, 0, 0);
        Ice.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        PalletObjects.Add(Ice);
        PalletBlocksID[25] = 3;
    }

    void LoadHole()
    {//6,7 =>穴ブロック
        GameObject Hole1;
        GameObject Hole2;
        Hole1 = Instantiate<GameObject>(cSVLoad.Blocks[6], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[26].transform.GetChild(0).gameObject.transform);
        Hole2 = Instantiate<GameObject>(cSVLoad.Blocks[7], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[27].transform.GetChild(0).gameObject.transform);

        Hole1.transform.SetAsFirstSibling();
        Hole2.transform.SetAsFirstSibling();

        Hole1.transform.localPosition = new Vector3(0, 0, 0);
        Hole2.transform.localPosition = new Vector3(0, 0, 0);

        Hole1.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        Hole2.transform.localScale = new Vector3(0.64f, 0.64f, 1);

        PalletObjects.Add(Hole1);
        PalletObjects.Add(Hole2);

        PalletBlocksID[26] = 6;
        PalletBlocksID[27] = 7;
    }

    void LoadTeleport()
    {//8,9 =>テレポートブロック
        GameObject Teleport1;
        GameObject Teleport2;
        Teleport1 = Instantiate<GameObject>(cSVLoad.Blocks[8], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[28].transform.GetChild(0).gameObject.transform);
        Teleport2 = Instantiate<GameObject>(cSVLoad.Blocks[9], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[29].transform.GetChild(0).gameObject.transform);

        Teleport1.transform.SetAsFirstSibling();
        Teleport2.transform.SetAsFirstSibling();

        Teleport1.transform.localPosition = new Vector3(0, 0, 0);
        Teleport2.transform.localPosition = new Vector3(0, 0, 0);

        Teleport1.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        Teleport2.transform.localScale = new Vector3(0.64f, 0.64f, 1);

        PalletObjects.Add(Teleport1);
        PalletObjects.Add(Teleport2);

        PalletBlocksID[28] = 8;
        PalletBlocksID[29] = 9;
    } 
    void LoadStair()
    {//10 =>階段ブロック
        GameObject Stair;
        Stair = Instantiate<GameObject>(cSVLoad.Blocks[10], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[30].transform.GetChild(0).gameObject.transform);
        Stair.transform.SetAsFirstSibling();
        Stair.transform.localPosition = new Vector3(0, 0, 0);
        Stair.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        PalletObjects.Add(Stair);
        PalletBlocksID[30] = 10;
    }

    void LoadMedicine()
    {//5 =>回復ブロック
        GameObject Medicine;
        Medicine = Instantiate<GameObject>(cSVLoad.Blocks[5], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[31].transform.GetChild(0).gameObject.transform);
        Medicine.transform.SetAsFirstSibling();
        Medicine.transform.localPosition = new Vector3(0, 0, 0);
        Medicine.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        PalletObjects.Add(Medicine);
        PalletBlocksID[31] = 5;
    }

    void LoadThorn()
    {//4 =>棘ブロック
        GameObject Thorn;
        Thorn = Instantiate<GameObject>(cSVLoad.Blocks[4], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[32].transform.GetChild(0).gameObject.transform);
        Thorn.transform.localPosition = new Vector3(0, 0, 0);
        Thorn.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        PalletObjects.Add(Thorn);
        PalletBlocksID[32] = 4;
    }

    void LoadRock()
    {//2 =>岩ブロック
        GameObject Rock;
        Rock = Instantiate<GameObject>(cSVLoad.Blocks[2], new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[33].transform.GetChild(0).gameObject.transform);
        Rock.transform.SetAsFirstSibling();
        Rock.transform.localPosition = new Vector3(0, 0, 0);
        Rock.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        PalletObjects.Add(Rock);
        PalletBlocksID[33] = 2;
    }

    void LoadPlayer()
    {
        GameObject Player;
        Player = Instantiate<GameObject>(Resources.Load<GameObject>("prefab/PlayerImage"), new Vector3(0, 0, 0), Quaternion.identity, PalletBlocks[34].transform.GetChild(0).gameObject.transform);
        Player.transform.SetAsFirstSibling();
        Player.transform.localPosition = new Vector3(0, 0, 0);
        Player.transform.localScale = new Vector3(0.64f, 0.64f, 1);
        PalletObjects.Add(Player);
        PalletBlocksID[34] = 37;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
