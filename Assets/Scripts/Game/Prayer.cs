using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer : MonoBehaviour
{
    [Header("プレイヤーの残り歩数")]
    [SerializeField] int player_maxcount;
    [SerializeField] int player_count;

    [SerializeField] int p_vartical;
    [SerializeField] int p_horizontal;

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
                player_count -= 1;
                break;

            case "medcine"://薬
                player_count += 1;
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
        if (player_count > 0)
        {
            player_count--;
            return (true);
        }
        else return (false);
    }

    public void StageReset()//盤面リセット
    {
        player_count = player_maxcount;
    }

    public void Walk(int x, int y)
    {
        int cangecount;
        int walkX = x - p_vartical;
        int walkY = y - p_horizontal;

        if (walkX == 0)
        {
            if (walkY > 0) cangecount = 1;//現在のプレイヤーの位置とタッチされた位置を比較して数値を割り当てる
            else cangecount = -1;

            while (walkY != 0)
            {
                if (Countcheak())
                {
                    p_horizontal += cangecount;
                    walkY -= cangecount;
                    Debug.Log("プレイヤーの位置:" + p_vartical + ":" + p_horizontal);
                }
                else
                {
                    Debug.LogError("移動に失敗しました：プレイヤーは死亡しています");
                    break;
                }
            }
        } else if (walkY == 0)
        {
            if (walkX > 0) cangecount = 1;//現在のプレイヤーの位置とタッチされた位置を比較して数値を割り当てる
            else cangecount = -1;

            while (walkX != 0)
            {
                if (Countcheak())
                {
                    p_vartical += cangecount;
                    walkX -= cangecount;
                    Debug.Log("プレイヤーの位置:" + p_vartical + ":" + p_horizontal);
                }
                else
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
