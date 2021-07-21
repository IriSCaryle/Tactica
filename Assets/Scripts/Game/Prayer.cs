using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer : MonoBehaviour
{
    int vartical;
    int horizontal;

    [Header("プレイヤーの残り歩数")]
    [SerializeField] int player_maxcount;
    int player_count;
    bool player_alive;


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

    public void Countcheak()//行動
    {
        if (player_maxcount > 0) player_maxcount--;
        else player_alive = false;
    }

    public void StageReset()//盤面リセット
    {
        player_alive = true;
        player_count = player_maxcount;
    }
}
