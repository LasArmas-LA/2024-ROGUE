using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestEncount : MonoBehaviour
{
    public enum MainTurn
    {
        WAIT,

        RIRIMOVE,
        RIRIANIM,

        DHIAMOVE,
        DHIAANIM,

        ENEMY1MOVE,
        ENEMY1ANIM,
        ENEMY2MOVE,
        ENEMY2ANIM,
        
        GAMEOVER,

        END
    }
    public MainTurn mainTurn;

    //バトルコマンドのテキスト
    [Header("バトルコマンドのテキスト")]
    [SerializeField]
    public TextMeshProUGUI windowsMes = null;
    [SerializeField]
    public TextMeshProUGUI command1Text = null;
    [SerializeField]
    public TextMeshProUGUI command2Text = null;
    [SerializeField]
    public TextMeshProUGUI command3Text = null;

    //スクリプト参照
    [SerializeField]
    Riri ririScript = null;
    [SerializeField]
    Dhia dhiaScript = null;
    [SerializeField]
    public EnemyManager enemyScript = null;
    [SerializeField]
    EnemyFloorRunSys enemyFloorRunSysObj = null;
    FloorNoSys floorNoSys = null;

    //待機時間
    [SerializeField]
    public float waitTime = 0;
    public float timer = 0;

    GameObject floorNoSysObj = null;

    [Space(10)]

    [Header("体力ゲージ")]
    [SerializeField, Tooltip("リリーの体力ゲージ")]
    Slider ririSlider = null;
    [SerializeField, Tooltip("ディアの体力ゲージ")]
    Slider dhiaSlider = null;

    [Space(10)]
    [Header("各キャラクターの死亡フラグ")]
    bool ririDeath = false;
    bool dhiaDeath = false;
    bool enemyDeath = false;


    [Space(10)]

    [Header("各キャラクターのオブジェクト")]
    //リリー,ディア,エネミーのObj
    [SerializeField]
    GameObject ririObj;
    [SerializeField]
    GameObject dhiaObj;

    [Space(10)]

    [Header("各キャラクターのコマンドUI")]
    [SerializeField]
    GameObject ririCommand = null;
    [SerializeField]
    GameObject dhiaCommand = null;


    [Space(10)]

    [Header("コマンドの画像")]
    [SerializeField]
    Sprite ririCommandSp = null;
    [SerializeField]
    Sprite dhiaCommandSp = null;

    [Space(10)]

    [Header("コマンドのオブジェクト")]
    [SerializeField]
    Image[] commnadImage = null;

    [Space(10)]

    //休憩階のフラグ
    [NonSerialized]
    public bool restFlag = false;

    //ボス階のフラグ
    [NonSerialized]
    public bool bossFlag = false;

    [SerializeField]
    GameObject[] enemyObj = null;

    //敵の種類の抽選用
    public int[] typeRnd = null;

    //敵の数の抽選用
    public int numberRnd = 0;

    float hpMoveTimer = 0;
    bool hpMoveTimerFlag = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        //ステータスを待機状態に変更
        mainTurn = MainTurn.WAIT;

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        numberRnd = UnityEngine.Random.Range(0, 2);
        numberRnd = 1;

        //敵の数分データを格納
        if (numberRnd == 0)
        {
            //エネミーの種類抽選用
            typeRnd[0] = UnityEngine.Random.Range(0, enemyObj.Length);

            //ランダムで選ばれたエネミーオブジェクトの表示
            enemyObj[typeRnd[0]].transform.localScale = new Vector3(1, 1, 1);
        }
        if (numberRnd == 1)
        {
            //エネミーの種類抽選用
            typeRnd[0] = UnityEngine.Random.Range(0, enemyObj.Length - 2);

            //ランダムで選ばれたエネミーオブジェクトの表示
            enemyObj[typeRnd[0]].transform.localScale = new Vector3(1, 1, 1);

            //エネミーの種類抽選用
            typeRnd[1] = UnityEngine.Random.Range(3, enemyObj.Length);

            //ランダムで選ばれたエネミーオブジェクトの表示
            enemyObj[typeRnd[1]].transform.localScale = new Vector3(1, 1, 1);
        }

        //MaxHPの格納
        ririSlider.maxValue = ririScript.maxhp;
        dhiaSlider.maxValue = dhiaScript.maxhp;

        //MinHPの格納
        ririSlider.minValue = 0;
        dhiaSlider.minValue = 0;

        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();

        //フロアが1階の時
        if (floorNoSys.floorNo == 1)
        {
            //MaxのHPを現在のHPに格納
            ririSlider.value = ririSlider.maxValue;
            dhiaSlider.value = dhiaSlider.maxValue;
        }
        else
        {
            //Hpバーを残hpの割合で適用
            ririSlider.value *= (ririScript.hp / ririScript.maxhp);
            dhiaSlider.value *= (dhiaScript.hp / dhiaScript.maxhp);
        }
    }

    bool fast = true;
    float ririhpdf = 0;
    float dhiahpdf = 0;

    void Update()
    {

        switch (mainTurn)
        {
            case MainTurn.WAIT:
                break;
            case MainTurn.RIRIMOVE:
                //リリー死亡時ゲームオーバー
                if(ririScript.deathFlag)
                {
                    mainTurn = MainTurn.GAMEOVER;
                }
                if(fast)
                {
                    //コマンド部分の表示切り替え
                    dhiaCommand.SetActive(false);
                    ririCommand.SetActive(true);
                    

                    //ダメージを受けた時を判別できるように格納
                    ririhpdf = ririScript.hp;
                    fast = false;
                }
                break;
            case MainTurn.RIRIANIM:
                if (!fast)
                {
                    fast = true;
                }

                break;
            case MainTurn.DHIAMOVE:
                //ディア死亡時ターンをスキップ
                if (dhiaScript.deathFlag)
                {
                    mainTurn = MainTurn.ENEMY1MOVE;
                }
                if (fast)
                {
                    //コマンド部分の表示切り替え
                    ririCommand.SetActive(false);
                    dhiaCommand.SetActive(true);

                    //ダメージを受けた時を判別できるように格納
                    dhiahpdf = dhiaScript.hp;
                    fast = false;
                }
                break;
            case MainTurn.DHIAANIM:
                if (enemyScript.deathFlag)
                {
                    //タイマー開始
                    timer += Time.deltaTime;

                    if(timer >= 3)
                    {
                        mainTurn = MainTurn.END;
                        timer = 0;
                    }
                }
                if(!fast)
                {
                    fast = true;
                }
                break;
            case MainTurn.ENEMY1MOVE:
                enemyScript.enemyHpDef = enemyScript.hp;
                break;
            case MainTurn.ENEMY1ANIM:
                break;
            case MainTurn.ENEMY2MOVE:
                enemyScript.enemyHpDef = enemyScript.hp;
                break;
            case MainTurn.ENEMY2ANIM:
                break;
            case MainTurn.GAMEOVER:
                timer += Time.deltaTime;

                if (timer >= 2)
                {
                    SceneManager.LoadScene("GameOver");
                    timer = 0;
                }

                break;
            case MainTurn.END:
                break;
        }
        
        RiriMove();
        DhiaMove();
        EnemyMove();

        //リリーのHPが削られた時
        if (ririhpdf > ririScript.hp)
        {
            Debug.Log("リリーが攻撃を受けた");
            ririSlider.value -= ((ririSlider.maxValue * (ririScript.hp / ririScript.maxhp)) * Time.deltaTime);

            if(ririSlider.value <= ririScript.hp)
            {
                ririhpdf = ririScript.hp;
                ririSlider.value = ririScript.hp;
            }
        }

        //リリーのHPが回復された時
        if (ririhpdf < ririScript.hp && ririhpdf != 0)
        {
            Debug.Log("リリーを回復した");
            ririSlider.value += ((ririSlider.maxValue * (ririScript.hp / ririScript.maxhp)) * Time.deltaTime);

            if(ririSlider.value >= ririScript.hp)
            {
                ririhpdf = ririScript.hp;
                ririSlider.value = ririScript.hp;
            }
        }

        //ディアのHPが削られた時
        if (dhiahpdf > dhiaScript.hp)
        {
            Debug.Log("ディアが攻撃を受けた");
            dhiaSlider.value -= ((dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp)) * Time.deltaTime);

            if(dhiaSlider.value <= dhiaScript.hp)
            {
                dhiahpdf = dhiaScript.hp;
                dhiaSlider.value = dhiaScript.hp;
            }
        }
        //ディアのHPが回復された時
        if (dhiahpdf < dhiaScript.hp && dhiahpdf != 0)
        {
            Debug.Log("ディアを回復した");
            dhiaSlider.value += ((dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp)) * Time.deltaTime);

            if(dhiaSlider.value >= dhiaScript.hp)
            {
                dhiahpdf = dhiaScript.hp;
                dhiaSlider.value = dhiaScript.hp;
            }
        }

    }

    public void HpMoveWait(String charName)
    {
        hpMoveTimer += Time.deltaTime;

        if(hpMoveTimer >= 2f)
        {
            if(charName == "Riri")
            {
                ririhpdf = ririScript.hp;
            }
            if (charName == "Dhia")
            {
                dhiahpdf = dhiaScript.hp;
            }
            hpMoveTimer = 0;
        }
    }

    //コマンド処理
    public bool command1 = false;
    public bool command2 = false;
    public bool command3 = false;
    bool button = false;
    bool coLock = false;

    public void Command1()
    {
        //多段押し防止
        if(!button)
        {
            button = true;
            command1 = true;
        }
    }
    public void Command2()
    {
        //多段押し防止
        if (!button)
        {
            button = true;
            command2 = true;
        }
    }
    public void Command3()
    {
        //多段押し防止
        if (!button)
        {
            button = true;
            command3 = true;
        }
    }

    void RiriMove()
    {
        if (mainTurn == MainTurn.RIRIMOVE)
        {

            command1Text.text = "ヒール";
            command2Text.text = "オールヒール";
            command3Text.text = "バイキルト";

            commnadImage[0].sprite = ririCommandSp;
            commnadImage[1].sprite = ririCommandSp;
            commnadImage[2].sprite = ririCommandSp;

            //Debug.Log("リリーのターン");
            if (command1 || command2 || command3)
            {
                //タイマー開始
                if (!command1)
                {
                    timer += Time.deltaTime;
                }


                if (!coLock)
                {
                    if (command1)
                    {
                        ririScript.Skil1();
                    }
                    if (command2)
                    {
                        //ステータスを変更
                        mainTurn = MainTurn.RIRIANIM;

                        ririScript.Skil2();
                    }
                    if (command3)
                    {
                        //ステータスを変更
                        mainTurn = MainTurn.RIRIANIM;

                        ririScript.Skil3();
                    }
                    coLock = true;
                }
            }
            else
            {
                //コマンド表示の処理
                enemyFloorRunSysObj.commandMain.SetActive(true);
                enemyFloorRunSysObj.commandWin.SetActive(true);
            }
        }
        if (mainTurn == MainTurn.RIRIANIM)
        {
            //コマンド非表示の処理
            enemyFloorRunSysObj.commandMain.SetActive(false);
            enemyFloorRunSysObj.commandWin.SetActive(false);

            //タイマー開始
            timer += Time.deltaTime;

            //待機時間を超えて敵が生きている時
            if (timer >= waitTime && !enemyDeath)
            {
                //ステータスを変更
                mainTurn = MainTurn.DHIAMOVE;
                timer = 0;
                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;
            }

        }
    }
    void DhiaMove()
    {
        if (mainTurn == MainTurn.DHIAMOVE)
        {
            //コマンド表示の処理
            enemyFloorRunSysObj.commandMain.SetActive(true);
            enemyFloorRunSysObj.commandWin.SetActive(true);

            command1Text.text = "殴る";
            command2Text.text = "防御体制";
            command3Text.text = "守る";

            commnadImage[0].sprite = dhiaCommandSp;
            commnadImage[1].sprite = dhiaCommandSp;
            commnadImage[2].sprite = dhiaCommandSp;

            if (command1 || command2 || command3)
            {
                //ステータスを変更
                mainTurn = MainTurn.DHIAANIM;

                if (!coLock)
                {
                    if (command1)
                    {
                        dhiaScript.Skil1();
                    }
                    if (command2)
                    {
                        dhiaScript.Skil2();
                    }
                    if (command3)
                    {
                        dhiaScript.Skil3();
                    }
                    coLock = true;
                }
            }
            else
            {
                windowsMes.text = "ディアの行動をにゅうりょくしてください";
            }
        }
        //アニメーションの時間
        if (mainTurn == MainTurn.DHIAANIM)
        {
            //コマンド非表示の処理
            enemyFloorRunSysObj.commandMain.SetActive(false);
            enemyFloorRunSysObj.commandWin.SetActive(false);

            //タイマー開始
            timer += Time.deltaTime;

            //待機時間を超えて敵が生きている時
            if (timer >= waitTime && !enemyDeath)
            {
                //ステータスを変更
                mainTurn = MainTurn.ENEMY1MOVE;
                timer = 0;
                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;
            }
        }
    }
    void EnemyMove()
    {
        if (mainTurn == MainTurn.ENEMY1MOVE)
        {
            //タイマー開始
            timer += Time.deltaTime;

            //ステータスを変更
            //mainTurn = MainTurn.ENEMYANIM;

            if (!coLock)
            {
                //Init時に選択されたエネミーのスキル関数を呼び出す
                enemyScript.Move();
                coLock = true;
            }

            //待機時間を超えたら
            if (timer >= waitTime)
            {
                //ステータスを変更
                mainTurn = MainTurn.RIRIMOVE;
                coLock = false;
                timer = 0;
            }
        }
    }
}
