using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer : MonoBehaviour
{
    Gamemanager gamemanager;

    [Header("プレイヤーの残り歩数")]
    [SerializeField] int player_maxLife;
    [SerializeField] int player_Life;

    [Header("プレイヤーの位置")]
    [SerializeField] int p_vartical;
    [SerializeField] int p_horizontal;
    public int t_vartical;
    public int t_horizontal;

    [Header("回復薬での回復量")]
    [SerializeField] int caer;

    void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    void Start()
    {
        StageReset();
    }

    void Update()
    {
        
    }

    public bool Contactjudgment()//接触判定
    {
        bool judge = true;
        switch (gamemanager.objecttagsearch(p_vartical,p_horizontal))
        {
            case 3://氷
                //このマスに乗る前の進行方向を記録してその方向の1マス先が通行可能ならその方向に移動する
                judge = false;
                break;

            case 4://棘
                if (!Countcheak()) Debug.LogError("死亡しました");
                break;

            case 5://薬
                player_Life += 1;
                gamemanager.mapcange(p_vartical, p_horizontal, 1);
                break;

            case 8://テレポートA
                if (gamemanager.teleportsearch(9))
                {
                    p_vartical = t_vartical;
                    p_horizontal = t_horizontal;
                    Debug.LogWarning("テレポートに接触しました　現在の位置：" + p_vartical + "," + p_horizontal);
                }
                judge = false;
                break;

            case 9://テレポートB
                if (gamemanager.teleportsearch(8))
                {
                    p_vartical = t_vartical;
                    p_horizontal = t_horizontal;
                    Debug.LogWarning("テレポートに接触しました　現在の位置：" + p_vartical + "," + p_horizontal);
                }
                judge = false;
                break;

            case 10://階段
                Debug.Log("CLAER");
                judge = false;
                break;

            case 0://検証用
                Debug.Log("HIT");
                break;
        }
        return judge;
    }

    public bool Countcheak()//行動
    {
        if (player_Life > 0)
        {
            player_Life--;
            return (true);
        }
        else return (false);
    }

    public void StageReset()//盤面リセット
    {
        player_Life = player_maxLife;
    }

    public void Walk(int x, int y)
    {
        bool firstmove = true;
        int cangecount;
        int walkX = x - p_vartical;
        int walkY = y - p_horizontal;

        if (walkX == 0)
        {
            if (walkY > 0) cangecount = 1;//現在のプレイヤーの位置とタッチされた位置を比較して数値を割り当てる
            else cangecount = -1;

            while (walkY != 0)
            {
                if (gamemanager.objectTrafficsearch(p_vartical, p_horizontal + cangecount))//通行可能かの判定
                {
                    if (Countcheak())//プレイヤーの生存判定
                    {
                        firstmove = false;
                        p_horizontal += cangecount;//移動
                        if (!Contactjudgment())
                        {
                            gamemanager.gameturncange();
                            Debug.LogWarning("移動を終了しました：特別なオブジェクトに接触しました");
                            break;
                        }
                        gamemanager.gameturncange();

                        walkY -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_vartical + ":" + p_horizontal);
                    } else
                    {
                        Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                        break;
                    }
                } else
                {
                    if (gamemanager.objecttagsearch(p_vartical, p_horizontal + cangecount) == 2 && firstmove)//接触したオブジェクトが岩か＆これが一回目の移動か
                    {
                        if (Countcheak())//プレイヤーの生存判定
                        {
                            gamemanager.rockmovesearch(p_vartical, p_horizontal + cangecount, cangecount, "horizontal");//岩を移動する
                            gamemanager.gameturncange();
                        }
                        else
                        {
                            Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                            break;
                        }
                    }
                    Debug.LogError("移動に失敗しました：通行不可のオブジェクトに接触しました");
                    break;
                }
            }
        } else if (walkY == 0)//上の処理とほとんど同じなので上を参照してください
        {
            if (walkX > 0) cangecount = 1;
            else cangecount = -1;

            while (walkX != 0)
            {
                if (gamemanager.objectTrafficsearch(p_vartical + cangecount, p_horizontal))
                {
                    if (Countcheak())
                    {
                        firstmove = false;
                        p_vartical += cangecount;
                        if (!Contactjudgment())
                        {
                            gamemanager.gameturncange();
                            Debug.LogError("移動を終了しました：特別なオブジェクトに接触しました");
                            break;
                        }
                        gamemanager.gameturncange();

                        walkX -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_vartical + ":" + p_horizontal);
                    } else
                    {
                        Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                        break;
                    }
                } else
                {
                    if (gamemanager.objecttagsearch(p_vartical + cangecount, p_horizontal) == 2 && firstmove)
                    {
                        if (Countcheak())//プレイヤーの生存判定
                        {
                            gamemanager.rockmovesearch(p_vartical + cangecount, p_horizontal, cangecount, "vertical");
                            gamemanager.gameturncange();
                        }
                        else
                        {
                            Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                            break;
                        }
                    }
                    Debug.LogError("移動に失敗しました：通行不可のオブジェクトに接触しました");
                    break;
                }
            }
        } else
        {
            Debug.LogError("移動に失敗しました：プレイヤーの位置と直線上にある位置に向かってのみ移動できます");
            return;
        }
        Debug.Log("移動完了　現在のプレイヤーの位置:" + p_vartical + ":" + p_horizontal);
    }
}
