using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    RectTransform rectTransform;
    Image image;
    [SerializeField] Sprite sprite;
    Gamemanager gamemanager;
    public Text life_text;

    [Header("プレイヤーの残り歩数")]
    public int player_maxLife;
    public int player_Life;

    [Header("プレイヤーの位置")]
    public int p_horizontal;
    public int p_vartical;

    [Header("1歩歩くたびに待つ時間(歩/秒)")]
    [SerializeField] float stand;

    [Header("プレイヤーのアニメーション")]
    [SerializeField] Animator player_anim;
    [SerializeField] float speed;

    float stop = 0.1f;
    float st = 0;
    bool bto = false;

    int cg;
    string dirc = "";

    bool Inaction = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    void Start()
    {
        life_text.text ="のこり" + player_maxLife +"ほ" ;
        gamemanager.gameturncange();
    }

    void Update()
    {
        if (bto)
        {
            st += Time.deltaTime;
            if (st >= stop)
            {
                bto = false;
                st = 0;
                Contactjudgment(cg, dirc);
            }
        }

        float spd = speed * Time.deltaTime;

        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(p_horizontal * 125, p_vartical * -125 + 20), spd);
        if(Inaction && rectTransform.anchoredPosition ==new Vector2(p_horizontal *125,p_vartical *-125 + 20))
        {
            Debug.Log("移動完了　現在のプレイヤーの位置:" + p_horizontal + ":" + p_vartical);
            Inaction = false;
            player_anim.SetTrigger("idle");
        }

    }

    public void resetbutton()
    {
        player_anim.SetTrigger("idle");
        image.sprite = sprite;
        life_text.text = "のこり" + player_maxLife + "ほ";
        gamemanager.gamereset();
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
                        Debug.LogWarning("滑る！");
                        //Contactjudgment(cange, direction);//氷以外に到達するまで繰り返すことになる
                        bto = true;
                        cg = cange;
                        dirc = direction;
                    }
                } 
                else if(direction == "p_horizontal")
                {
                    if (gamemanager.objectTrafficsearch(p_horizontal + cange, p_vartical)) 
                    {
                        p_horizontal += cange;
                        Debug.LogWarning("滑る！");
                        //Contactjudgment(cange, direction);
                        bto = true;
                        cg = cange;
                        dirc = direction;
                    }
                }
                judge = false;
                break;

            case 4://棘
                gamemanager.SEoneshot(3);
                if (!Countcheak()) Debug.LogError("死亡しました");
                break;

            case 5://薬
                player_Life = player_maxLife;
                life_text.text = "のこり" + player_Life + "ほ";
                gamemanager.SEoneshot(4);
                gamemanager.mapcange(p_horizontal, p_vartical, 1);
                break;

            case 8://テレポートA
                if (gamemanager.teleportsearch(9))
                {
                    gamemanager.SEoneshot(7);
                    Debug.LogWarning("テレポートに接触しました　現在の位置：" + p_horizontal + "," + p_vartical);
                    gamemanager.gameturncange();
                }
                judge = false;
                break;

            case 9://テレポートB
                if (gamemanager.teleportsearch(8))
                {
                    gamemanager.SEoneshot(7);
                    Debug.LogWarning("テレポートに接触しました　現在の位置：" + p_horizontal + "," + p_vartical);
                    gamemanager.gameturncange();
                }
                judge = false;
                break;

            case 10://階段
                gamemanager.Clearanim();
                gamemanager.SEoneshot(9);
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
            life_text.text = "のこり" + player_Life + "ほ";
            return (true);
        }
        else 
        {
            gamemanager.SEoneshot(2);
            player_anim.SetTrigger("idle");
            player_anim.SetTrigger("dead");
            return false;
        };
    }

    public IEnumerator Walk(int x, int y)
    {
        if (Inaction) yield break;
        Inaction = true;
        bool firstmove = true;
        int cangecount;
        int walkX = x - p_horizontal;
        int walkY = y - p_vartical;

        if (walkX == 0)
        {
            if (walkY > 0) 
            {
                cangecount = 1;
                player_anim.SetTrigger("down");
            }//現在のプレイヤーの位置とタッチされた位置を比較して数値を割り当てる
            else 
            { 
                cangecount = -1;
                player_anim.SetTrigger("up");
            }

            while (walkY != 0)
            {
                if (gamemanager.objectTrafficsearch(p_horizontal, p_vartical + cangecount))//通行可能かの判定
                {
                    if (Countcheak())//プレイヤーの生存判定
                    {
                        firstmove = false;

                        gamemanager.SEoneshot(0);
                        p_vartical += cangecount;//移動
                        yield return new WaitForSeconds(stand);
                        if (!Contactjudgment(cangecount,"p_vartical"))
                        {
                            Debug.LogWarning("移動を終了しました：特別なオブジェクトに接触しました");
                            break;
                        }
                        walkY -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_horizontal + ":" + p_vartical);
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
                            Debug.Log("動かせます");
                            gamemanager.rockmovesearch(p_horizontal, p_vartical + cangecount, cangecount, "vertical");//岩を移動する
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
            if (walkX > 0)
            {
                cangecount = 1;
                player_anim.SetTrigger("right");
            }
            else
            {
                cangecount = -1;
                player_anim.SetTrigger("left");
            }

            while (walkX != 0)
            {
                if (gamemanager.objectTrafficsearch(p_horizontal + cangecount, p_vartical))
                {
                    if (Countcheak())
                    {
                        firstmove = false;

                        gamemanager.SEoneshot(0);
                        p_horizontal += cangecount;
                        yield return new WaitForSeconds(stand);
                        if (!Contactjudgment(cangecount,"p_horizontal"))
                        {
                            Debug.LogError("移動を終了しました：特別なオブジェクトに接触しました");
                            break;
                        }
                        walkX -= cangecount;
                        Debug.Log("プレイヤーの位置:" + p_horizontal + ":" + p_vartical);
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
                            Debug.Log("動かせます");
                            gamemanager.rockmovesearch(p_horizontal + cangecount, p_vartical, cangecount, "horizontal");
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
            gamemanager.SEoneshot(8);
            Debug.LogError("移動に失敗しました：プレイヤーの位置と直線上にある位置に向かってのみ移動できます");
            yield break;
        }
    }
}
