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

    private void Awake()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Contactjudgment(other.gameObject);
    }

    public void Contactjudgment(GameObject other)//接触判定
    {
        switch (other .tag)
        {
            case "ice"://氷
                //このマスに乗る前の進行方向を記録してその方向の1マス先が通行可能ならその方向に移動する
                break;

            case "thorn"://棘
                player_Life -= 1;
                break;

            case "medcine"://薬
                player_Life += 1;
                break;

            case "teleport_A"://テレポートA
                //もう一方のテレポートの座標まで移動する(この効果で移動した先のテレポートは判定しない)
                break;

            case "teleport_B"://テレポートB
                break;

            case "stairs"://階段
                break;

            case "Player"://検証用
                Debug.Log("hit");
                break;
        }
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
                if (Countcheak())//プレイヤーの生存判定
                {
                    if (gamemanager.objectsearch(p_vartical, p_horizontal + cangecount))//通行可能かの判定
                    {
                        firstmove = false;
                        p_horizontal += cangecount;//移動
                        walkY -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_vartical + ":" + p_horizontal);
                    } else
                    {
                        if (gamemanager.objecttagsearch(p_vartical, p_horizontal + cangecount) == "rock" && firstmove)
                        {
                            gamemanager.rockmovesearch(p_vartical, p_horizontal + cangecount,cangecount,"horizontal");
                        }
                        Debug.LogError("移動に失敗しました：通行不可の箇所に差し掛かりました");
                        break;
                    }
                } else
                {
                    Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                    break;
                }
            }
        } else if (walkY == 0)//上の処理とほとんど同じなので上を参照してください
        {
            if (walkX > 0) cangecount = 1;
            else cangecount = -1;

            while (walkX != 0)
            {
                if (Countcheak())
                {
                    if (gamemanager.objectsearch(p_vartical + cangecount, p_horizontal))
                    {
                        firstmove = false;
                        p_vartical += cangecount;
                        walkX -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_vartical + ":" + p_horizontal);
                    } else
                    {
                        if (gamemanager.objecttagsearch(p_vartical + cangecount, p_horizontal) == "rock" && firstmove)
                        {
                            gamemanager.rockmovesearch(p_vartical + cangecount, p_horizontal, cangecount, "vertical");
                        }
                        Debug.LogError("移動に失敗しました：通行不可の箇所に差し掛かりました");
                        break;
                    }
                } else
                {
                    Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
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
