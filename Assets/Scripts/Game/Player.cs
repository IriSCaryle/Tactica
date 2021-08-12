using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    RectTransform rectTransform;
    Gamemanager gamemanager;

    [Header("プレイヤーの残り歩数")]
    public int player_maxLife;//本来はゲームデータに保存される数値なので必要性がない(検証用)
    public int player_Life;

    [Header("プレイヤーの位置")]
    public int p_horizontal;
    public int p_vartical;

    [Header("1歩歩くたびに待つ時間(歩/秒)")]
    [SerializeField] float stand;

    [Header("回復薬での回復量")]
    [SerializeField] int care;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    void Start()
    {
        gamemanager.gameturncange();
    }

    public bool Contactjudgment(int cange,string direction)//接触判定
    {
        bool judge = true;
        switch (gamemanager.objecttagsearch(p_horizontal,p_vartical))
        {
            case 3://氷
                //このマスに乗る前の進行方向を記録してその方向の1マス先が通行可能ならその方向に移動する
                if(direction == "p_vartical")
                {
                    if(gamemanager.objectTrafficsearch(p_horizontal, p_vartical + cange))
                    {
                        p_vartical += cange;
                        gamemanager.gameturncange();
                        Debug.LogWarning("滑る！");
                        gamemanager.SEoneshot(3);
                        Contactjudgment(cange, direction);//氷以外に到達するまで繰り返すことになる
                    }
                } 
                else if(direction == "p_horizontal")
                {
                    if (gamemanager.objectTrafficsearch(p_horizontal + cange, p_vartical)) 
                    {
                        p_horizontal += cange;
                        gamemanager.gameturncange();
                        Debug.LogWarning("滑る！");
                        gamemanager.SEoneshot(3);
                        Contactjudgment(cange, direction);
                    }
                }
                judge = false;
                break;

            case 4://棘
                gamemanager.SEoneshot(4);
                if (!Countcheak()) Debug.LogError("死亡しました");
                break;

            case 5://薬
                player_Life += care;
                gamemanager.SEoneshot(5);
                gamemanager.mapcange(p_horizontal, p_vartical, 1, true);
                break;

            case 8://テレポートA
                if (gamemanager.teleportsearch(9))
                {
                    gamemanager.SEoneshot(8);
                    Debug.LogWarning("テレポートに接触しました　現在の位置：" + p_horizontal + "," + p_vartical);
                }
                judge = false;
                break;

            case 9://テレポートB
                if (gamemanager.teleportsearch(8))
                {
                    gamemanager.SEoneshot(8);
                    Debug.LogWarning("テレポートに接触しました　現在の位置：" + p_horizontal + "," + p_vartical);
                }
                judge = false;
                break;

            case 10://階段
                gamemanager.Clearanim();
                gamemanager.SEoneshot(10);
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

    public IEnumerator Walk(int x, int y)
    {
        bool firstmove = true;
        int cangecount;
        int walkX = x - p_horizontal;
        int walkY = y - p_vartical;

        if (walkX == 0)
        {
            if (walkY > 0) cangecount = 1;//現在のプレイヤーの位置とタッチされた位置を比較して数値を割り当てる
            else cangecount = -1;

            while (walkY != 0)
            {
                if (gamemanager.objectTrafficsearch(p_horizontal, p_vartical + cangecount))//通行可能かの判定
                {
                    if (Countcheak())//プレイヤーの生存判定
                    {
                        firstmove = false;

                        p_vartical += cangecount;//移動
                        if (!Contactjudgment(cangecount,"p_vartical"))
                        {
                            gamemanager.gameturncange();
                            Debug.LogWarning("移動を終了しました：特別なオブジェクトに接触しました");
                            break;
                        }
                        gamemanager.gameturncange();

                        walkY -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_horizontal + ":" + p_vartical);
                        yield return new WaitForSeconds(stand);
                    } else
                    {
                        Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                        break;
                    }
                } else
                {
                    Debug.Log("岩かどうか");
                    if (gamemanager.objecttagsearch(p_horizontal, p_vartical + cangecount) == 2 && firstmove)//接触したオブジェクトが岩か＆これが一回目の移動か
                    {
                        if (Countcheak())//プレイヤーの生存判定
                        {
                            gamemanager.rockmovesearch(p_horizontal, p_vartical + cangecount, cangecount, "vartical");//岩を移動する
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
                if (gamemanager.objectTrafficsearch(p_horizontal + cangecount, p_vartical))
                {
                    if (Countcheak())
                    {
                        firstmove = false;

                        p_horizontal += cangecount;
                        if (!Contactjudgment(cangecount,"p_horizontal"))
                        {
                            gamemanager.gameturncange();
                            Debug.LogError("移動を終了しました：特別なオブジェクトに接触しました");
                            break;
                        }
                        gamemanager.gameturncange();

                        walkX -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_horizontal + ":" + p_vartical);
                        yield return new WaitForSeconds(stand);
                    } else
                    {
                        Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                        break;
                    }
                } else
                {
                    Debug.Log("岩かどうか");
                    if (gamemanager.objecttagsearch(p_horizontal + cangecount, p_vartical) == 2 && firstmove)
                    {
                        if (Countcheak())//プレイヤーの生存判定
                        {
                            gamemanager.rockmovesearch(p_horizontal + cangecount, p_vartical, cangecount, "horizontal");
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
            yield break;
        }
        Debug.Log("移動完了　現在のプレイヤーの位置:" + p_horizontal + ":" + p_vartical);
    }
}
