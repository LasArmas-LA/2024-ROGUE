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

        DHIAATKDEFSLECT,

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
    [SerializeField]
    GameObject atkDefSlectWin = null;

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
            typeRnd[0] = UnityEngine.Random.Range(0, enemyObj.Length - 2);

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
        if (floorNoSys.floorCo == 1)
        {
            //MaxのHPを現在のHPに格納
            ririSlider.value = ririSlider.maxValue;
            dhiaSlider.value = dhiaSlider.maxValue;
        }
        else
        {
            //Hpバーを残hpの割合で適用
            ririSlider.value = ririSlider.maxValue * (ririScript.hp / ririScript.maxhp);
            dhiaSlider.value = dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp);
        }
        if(floorNoSys.floorCo % 5 == 0)
        {
            restFlag = true;
        }
        if(floorNoSys.floorCo % 10 == 0)
        {
            bossFlag = true;
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

                    dhiaScript.button = false;

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
            case MainTurn.DHIAATKDEFSLECT:

                //コマンド部分の表示切り替え
                dhiaCommand.SetActive(true);
                ririCommand.SetActive(false);

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

                    enemyScript.enemyHpDef[0] = enemyScript.hp[0];
                    enemyScript.enemyHpDef[1] = enemyScript.hp[1];

                    //ダメージを受けた時を判別できるように格納
                    dhiahpdf = dhiaScript.hp;
                    fast = false;
                    ririScript.button = false;
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
                break;
            case MainTurn.ENEMY1ANIM:
                break;
            case MainTurn.ENEMY2MOVE:
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
        Enemy1Move();
        Enemy2Move();

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

    int dhiaSlectNomber = 0;
    public void DhiaAtkDefSlect(int number)
    {
        if (!button)
        {
            button = true;
            dhiaSlectNomber = number;
        }
    }

    void RiriMove()
    {
        if (mainTurn == MainTurn.RIRIMOVE)
        {

            command1Text.text = "ヒール";
            command2Text.text = "押さないで";
            command3Text.text = "攻撃アップ";

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
                timer = 0;

                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;

                //ステータスを変更
                mainTurn = MainTurn.DHIAATKDEFSLECT;
            }

        }
    }
    void DhiaMove()
    {
        if (mainTurn == MainTurn.DHIAATKDEFSLECT)
        {
            atkDefSlectWin.SetActive(true);
            enemyFloorRunSysObj.commandWin.SetActive(true);

            if (button)
            {
                //アタックスキルを選択
                if (dhiaSlectNomber == 0)
                {
                    command1Text.text = dhiaScript.atkSkillName[0];
                    command2Text.text = dhiaScript.atkSkillName[1];
                    command3Text.text = dhiaScript.atkSkillName[2];

                    dhiaScript.atkDefSlect = Dhia.AtkDefSlect.ATK;
                }
                //ディフェンススキルを選択
                if (dhiaSlectNomber == 1)
                {
                    command1Text.text = dhiaScript.defSkillName[0];
                    command2Text.text = dhiaScript.defSkillName[1];
                    command3Text.text = dhiaScript.defSkillName[2];
                    dhiaScript.atkDefSlect = Dhia.AtkDefSlect.DEF;
                }

                button = false;
                atkDefSlectWin.SetActive(false);
                mainTurn = MainTurn.DHIAMOVE;   
            }
        }

        if (mainTurn == MainTurn.DHIAMOVE)
        {

            commnadImage[0].sprite = dhiaCommandSp;
            commnadImage[1].sprite = dhiaCommandSp;
            commnadImage[2].sprite = dhiaCommandSp;

            if (command1 || command2 || command3)
            {
                if (!coLock)
                {
                    if (command1)
                    {
                        dhiaScript.Skil1();
                        //攻撃選択時は対象を選ばせる
                        if (dhiaSlectNomber == 0)
                        {
                            if (numberRnd != 1)
                            {
                                //ステータスを変更
                                mainTurn = MainTurn.DHIAANIM;
                            }
                        }
                        //防御時は対象を選ばせない
                        if(dhiaSlectNomber == 1)
                        {
                            //ステータスを変更
                            mainTurn = MainTurn.DHIAANIM;   
                        }

                    }
                    if (command2)
                    {
                        dhiaScript.Skil2();
                        //攻撃選択時は対象を選ばせる
                        if (dhiaSlectNomber == 0)
                        {
                            if (numberRnd != 1)
                            {
                                //ステータスを変更
                                mainTurn = MainTurn.DHIAANIM;
                            }
                        }
                        //防御時は対象を選ばせない
                        if (dhiaSlectNomber == 1)
                        {
                            //ステータスを変更
                            mainTurn = MainTurn.DHIAANIM;
                        }
                    }
                    if (command3)
                    {
                        dhiaScript.Skil3();
                        //攻撃選択時は対象を選ばせる
                        if (dhiaSlectNomber == 0)
                        {
                            if (numberRnd != 1)
                            {
                                //ステータスを変更
                                mainTurn = MainTurn.DHIAANIM;
                            }
                        }
                        //防御時は対象を選ばせない
                        if (dhiaSlectNomber == 1)
                        {
                            //ステータスを変更
                            mainTurn = MainTurn.DHIAANIM;
                        }
                    }
                    coLock = true;
                }
            }
            else
            {
                //コマンド表示の処理
                enemyFloorRunSysObj.commandMain.SetActive(true);
                enemyFloorRunSysObj.commandWin.SetActive(true);

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
                timer = 0;
                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;

                //ステータスを変更
                mainTurn = MainTurn.ENEMY1MOVE;
            }
        }
    }
    void Enemy1Move()
    {
        if (mainTurn == MainTurn.ENEMY1MOVE)
        {
            if (enemyScript.deathLook[0])
            {
                mainTurn = MainTurn.ENEMY2MOVE;
                return;
            }

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
                coLock = false;
                timer = 0;

                //ステータスを変更
                //敵が2体の時
                if (numberRnd == 1)
                {
                    mainTurn = MainTurn.ENEMY2MOVE;
                }
                else
                {
                    mainTurn = MainTurn.RIRIMOVE;
                }
            }
        }
    }
    void Enemy2Move()
    {
        if (mainTurn == MainTurn.ENEMY2MOVE)
        {

            if (numberRnd == 0 || enemyScript.deathLook[1])
            {
                mainTurn = MainTurn.RIRIMOVE;
                return;
            }

            //タイマー開始
            timer += Time.deltaTime;

            if (!coLock)
            {
                //Init時に選択されたエネミーのスキル関数を呼び出す
                enemyScript.Move();
                coLock = true;
            }

            //待機時間を超えたら
            if (timer >= waitTime)
            {
                coLock = false;
                timer = 0;

                //ステータスを変更
                mainTurn = MainTurn.RIRIMOVE;
            }
        }
    }
}
