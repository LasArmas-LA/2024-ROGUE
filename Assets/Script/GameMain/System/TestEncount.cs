using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseEquipment;
using static Dhia;
using static DhiaSkillList;

public class TestEncount : MonoBehaviour
{
    //バトル用
    public enum MainTurn
    {
        WAIT,

        STRATRUN,


        RIRILOOPINIT,
        RIRIMOVE,
        RIRIANIM,
        RIRIEFFECT,


        DHIALOOPINIT,
        DHIAATKDEFSLECT,
        DHIAMOVE,
        DHIAANIM,
        DHIAEFFECT,

        ENEMY1LOOPINIT,
        ENEMY1MOVE,
        ENEMY1ANIM,
        ENEMY1EFFECT,

        ENEMY2LOOPINIT,
        ENEMY2MOVE,
        ENEMY2ANIM,
        ENEMY2EFFECT,

        GAMEOVER,

        ENDRUN
    }
    //実体化
    public MainTurn mainTurn;

    //バトル後のパーツ表示用
    enum PartsMode
    {
        //パーツの表示処理
        DISP,
        //パーツの選択待機中
        WAIT,
        //パーツ選択後
        END
    }
    //実体化
    PartsMode partsMode = PartsMode.DISP;

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
    FloorNoSys floorNoSysScript = null;

    //待機時間
    [SerializeField]
    public float waitTime = 0;
    //エフェクトの待機時間設定用
    public float effectWaitTime = 0;
    //エフェクトの待機時間カウント用
    public float effectWaitTimer = 0;


    [SerializeField]
    GameObject floorNoSysObj = null;

    [Space(10)]

    //体力ゲージ用
    [Header("体力ゲージ")]
    [SerializeField, Tooltip("リリーの体力ゲージ")]
    Slider ririSlider = null;
    [SerializeField, Tooltip("ディアの体力ゲージ")]
    Slider dhiaSlider = null;

    //各キャラクターの死亡フラグ
    [Space(10)]
    [Header("各キャラクターの死亡フラグ")]
    bool ririDeath = false;
    bool dhiaDeath = false;
    bool enemyDeath = false;


    [Space(10)]

    //リリー,ディアのObj
    [Header("各キャラクターのオブジェクト")]
    [SerializeField]
    GameObject ririObj;
    [SerializeField]
    GameObject dhiaObj;

    [Header("各キャラクターのステータス")]
    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    Status dhiaStatus = null;


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
    //[NonSerialized]
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

    //敵の停止フラグ
    public bool[] enemyStopFlag = new bool[2];

    float hpMoveTimer = 0;
    bool hpMoveTimerFlag = false;

    [Space(5)]
    [Header("パラメーター調整")]
    [SerializeField]
    float hpLowSpeed = 1;

    [Space(5)]
    [Header("パーツ管理用")]
    [SerializeField]
    bool[] partsSlect;
    //パーツ選択時の表示切り替え用
    [SerializeField]
    GameObject[] partsObj = null;
    [SerializeField]
    Image[] partsImage = null;
    [SerializeField]
    Sprite slectOnSp = null;
    [SerializeField]
    Sprite slectOffSp = null;
    [SerializeField]
    GameObject[] arrowObj = null;


    //パーツの名前
    string[] partsName = { "RightHand", "LeftHand", "Head", "Body", "Feet" };

    //ドロップしたパーツの情報表示用
    [SerializeField]
    TextMeshProUGUI[] slectText;

    //ドロップしたパーツの画像表示用
    [SerializeField]
    Image[] dropPartsSp = null;

    //現在装備しているパーツの情報表示用
    [SerializeField]
    TextMeshProUGUI[] slectNowText;

    //パーツを選択後確定させた時の判断
    bool allPartsSlect;

    //パーツを選択するウィンドウ
    [SerializeField]
    GameObject partsSlectWin = null;

    [Space(5)]

    //装備をランダムで入手するロジック組みのシステム
    [SerializeField, Header("ドロップ装備ランダム化システム")]
    EquipmentManager equipmentManager = null;

    //アニメーション
    [SerializeField, Header("アニメーション管理用")]
    Animator ririAnim = null;
    [SerializeField]
    Animator dhiaAnim = null;

    //コマンドを選択するウィンドウ
    [SerializeField, Header("コマンド管理用")]
    public GameObject commandWin = null;
    [SerializeField]
    public GameObject commandMain = null;

    //敵の場所まで歩くフラグ
    bool runStratFlag = false;
    //扉に着いてその階が終了する時のフラグ
    bool floorEndFlag = false;
    //最初に1回だけ呼び出したい処理
    bool partFastMove = true;


    //キャラクターの親オブジェクト
    [SerializeField]
    GameObject characterMainObj = null;

    [Space(10)]
    //カメラの動く速度
    [SerializeField, Header("カメラ管理用")]
    Vector3 characterMoveSpeed = Vector3.zero;

    [Space(10)]

    [SerializeField, Header("オブジェクト管理用")]
    GameObject restObj = null;
    [SerializeField]
    GameObject doorObj = null;

    void Awake()
    {
        //Find処理の初期化
        InitFind();

        //リリーのFindの初期化
        ririScript.InitFind();
        //ディアのFindの初期化
        dhiaScript.InitFind();

        //リリーのHPの初期化
        ririScript.InitStatus();
        //ディアのHPの初期化
        dhiaScript.InitStatus();
    }
    void Start()
    {
        Init();
    }


    void Init()
    {
        //ステータスを待機状態に変更
        mainTurn = MainTurn.WAIT;

        //敵の抽選
        InitEnemy();
        //HPの初期化
        InitHp();
        //アクティブ状態の初期化
        InitActive();
        //アニメーションの初期化
        InitAnim();

        //武器の抽選
        equipmentManager.LoopInit();

        //Time.timeScale = 100.0f;
    }

    void InitEnemy()
    {
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
            typeRnd[1] = UnityEngine.Random.Range(3, enemyObj.Length + 1);

            //ランダムで選ばれたエネミーオブジェクトの表示
            enemyObj[typeRnd[1 -1]].transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }

        //階層データ保持クラスの検索と情報を格納
        floorNoSysScript = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
    }

    void InitHp()
    {
        //MaxHPの格納
        ririSlider.maxValue = ririScript.maxhp;
        dhiaSlider.maxValue = dhiaScript.maxhp;

        //MinHPの格納
        ririSlider.minValue = 0;
        dhiaSlider.minValue = 0;

        //デバッグ用
        //MaxのHPを現在のHPに格納
        ririSlider.value = ririSlider.maxValue;
        dhiaSlider.value = dhiaSlider.maxValue;

        //フロアが1階の時
        if (floorNoSysScript.floorCo == 1)
        {
            //MaxのHPを現在のHPに格納
            ririSlider.value = ririSlider.maxValue;
            dhiaSlider.value = dhiaSlider.maxValue;
        }
        //それ以外
        else
        {
            //Hpバーを残hpの割合で適用
            ririSlider.value = ririSlider.maxValue * (ririScript.hp / ririScript.maxhp);
            dhiaSlider.value = dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp);
        }
        if (floorNoSysScript.floorCo % 5 == 0 && floorNoSysScript.floorCo != 0)
        {
            restFlag = true;
        }
        if (floorNoSysScript.floorCo % 10 == 0)
        {
            bossFlag = true;
        }
    }

    void InitActive()
    {
        commandWin.SetActive(false);
        commandMain.SetActive(false);

        //パーツ選択時の初期化処理
        arrowObj[0].SetActive(false);
        arrowObj[1].SetActive(false);
        arrowObj[2].SetActive(false);
    }

    void InitAnim()
    {
        //歩きアニメーションを開始
        ririAnim.SetBool("R_Walk", true);
        dhiaAnim.SetBool("D_Walk", true);
    }


    bool fast = true;
    float ririhpdf = 0;
    float dhiahpdf = 0;

    void Update()
    {
        switch (mainTurn)
        {
            case MainTurn.WAIT:
                Wait();
                break;
            case MainTurn.STRATRUN:
                StartRun();
                break;

                //<<<リリー>>>
            //リリーのムーブ
            case MainTurn.RIRILOOPINIT:
                RiriLoopInit();
                break;
            //リリーのムーブ
            case MainTurn.RIRIMOVE:
                RiriMove();
                break;
            //リリーのアニメーション
            case MainTurn.RIRIANIM:
                RiriAnimMove();
                break;
                //リリーのエフェクト
            case MainTurn.RIRIEFFECT:
                RiriEffect();
                break;

　　　　　　     //<<<ディア>>>
            case MainTurn.DHIALOOPINIT:
                DhiaLoopInit();
                break;
            //ディアの攻防選択
            case MainTurn.DHIAATKDEFSLECT:
                DhiaFastSlect();
                break;
            //ディアのムーブ
            case MainTurn.DHIAMOVE:
                DhiaMove();
                break;
            //ディアのアニメーション
            case MainTurn.DHIAANIM:
                DhiaAnimMove();
                break;
            //ディアのエフェクト
            case MainTurn.DHIAEFFECT:
                DhiaEffect();
                break;

                 //<<<敵1>>>
            case MainTurn.ENEMY1LOOPINIT:
                Enemy1LoopInit();
                break;
            case MainTurn.ENEMY1MOVE:
                Enemy1Move();
                break;
            case MainTurn.ENEMY1ANIM:
                Enemy1AnimMove();
                //敵1のエフェクト
                break;
            case MainTurn.ENEMY1EFFECT:
                Enmey1Effect();
                break;

            //<<<敵2>>>
            case MainTurn.ENEMY2LOOPINIT:
                Enemy2LoopInit();
                break;
            case MainTurn.ENEMY2MOVE:
                Enemy2Move();
                break;
            case MainTurn.ENEMY2ANIM:
                Enemy2AnimMove();
                break;
            //敵2のエフェクト
            case MainTurn.ENEMY2EFFECT:
                Enemy2Effect();
                break;

                //<<<ゲームオーバー>>>
            case MainTurn.GAMEOVER:
                GameOver();
                break;

                //<<<敵倒した後の移動>>>
            case MainTurn.ENDRUN:
                EndRun();
                break;
        }

        HpCheck();
    }

    void HpCheck()
    {
        //リリーのHPが削られた時
        if (ririhpdf > ririScript.hp)
        {
            /*
            //HPが0以下になってる時
            if(ririScript.hp <= 0)
            {
                ririScript.hp = 0;
                ririSlider.value -= (ririScript.maxhp * Time.deltaTime);
            }
            */

            ririSlider.value -= ((ririSlider.maxValue * (ririScript.hp / ririScript.maxhp)) * Time.deltaTime) * hpLowSpeed;

            if (ririSlider.value <= ririScript.hp)
            {
                ririhpdf = ririScript.hp;
                ririSlider.value = ririScript.hp;
            }
        }

        //ディアのHPが削られた時
        if (dhiahpdf > dhiaScript.hp)
        {
            /*
            //HPが0以下になってる時
            if (dhiaScript.hp <= 0)
            {
                dhiaScript.hp = 0;
                dhiaSlider.value -= (dhiaScript.maxhp * Time.deltaTime);
            }
            */

            dhiaSlider.value -= ((dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp)) * Time.deltaTime) * hpLowSpeed;

            if (dhiaSlider.value <= dhiaScript.hp)
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

    int dhiaSlectNumber = 0;
    public void DhiaAtkDefSlect(int number)
    {
        if (!button)
        {
            button = true;
            dhiaSlectNumber = number;
        }
    }

    bool floorFast = false;
    void Wait()
    {
        //1回だけ呼び出す
        if (!floorFast)
        {
            //ステータスの変更
            mainTurn = MainTurn.RIRILOOPINIT;

            //歩きアニメーションを停止
            ririAnim.SetBool("R_Walk", false);
            dhiaAnim.SetBool("D_Walk", false);

            //コマンドを表示
            commandWin.SetActive(true);
            commandMain.SetActive(true);
            fast = true;
            runStratFlag = false;
        }

    }

    void StartRun()
    {

    }


    //リリーの初期化
    void RiriLoopInit()
    {
        //スキルの名前表示を書き換え
        command1Text.text = ririScript.atkSkillName[0];
        command2Text.text = ririScript.atkSkillName[1];
        command3Text.text = ririScript.atkSkillName[2];

        //コマンドのテクスチャを切り替え
        commnadImage[0].sprite = ririCommandSp;
        commnadImage[1].sprite = ririCommandSp;
        commnadImage[2].sprite = ririCommandSp;

        //コマンド部分の表示アクティブを切り替え
        dhiaCommand.SetActive(false);
        ririCommand.SetActive(true);

        dhiaScript.button = false;

        
        if (ririScript.becomeWeakFlag)
        {
            ririScript.BecomeWeakSlect(100);
        }

        //頑張って！のターン経過処理
        if (ririScript.keepItUpFlag)
        {
            ririScript.keepItUpTurn--;
            if (ririScript.keepItUpTurn == 0)
            {
                ririScript.keepItUpFlag = false;
                dhiaScript.atkCorrectionValue -= ririScript.keepItUpValue;
            }
        }
        //守ってあげる！のターン経過処理
        if (ririScript.protectFlag)
        {
            ririScript.protectTurn--;
            if (ririScript.protectTurn == 0)
            {
                ririScript.protectFlag = false;
                //リリーの減算
                ririScript.defCorrectionValue -= ririScript.protectValue;
                //ディアの減算
                dhiaScript.defCorrectionValue -= ririScript.protectValue;
            }
        }

        //ダメージを受けた時を判別できるように格納
        ririhpdf = ririScript.hp;

        //ステータスの切り替え
        mainTurn = MainTurn.RIRIMOVE;
    }

    float rirMoveiTimer = 0;

    void RiriMove()
    {
        //リリー死亡時ゲームオーバー
        if (ririScript.deathFlag)
        {
            mainTurn = MainTurn.GAMEOVER;
        }

        //ボタンが押されるで待機
        if (command1 || command2 || command3)
        {
            rirMoveiTimer += Time.deltaTime;

            //ボタンの多段押し防止
            if (!coLock)
            {
                if (command1)
                {
                    //ステータスを変更
                    //mainTurn = MainTurn.RIRIANIM;
                    //コマンド非表示の処理
                    enemyFloorRunSysObj.commandMain.SetActive(false);

                    ririScript.Skil1();
                }
                if (command2)
                {
                    //ステータスを変更
                    //mainTurn = MainTurn.RIRIANIM;
                    //コマンド非表示の処理
                    enemyFloorRunSysObj.commandMain.SetActive(false);

                    ririScript.Skil2();
                }
                if (command3)
                {
                    //ステータスを変更
                    //mainTurn = MainTurn.RIRIANIM;
                    //コマンド非表示の処理
                    enemyFloorRunSysObj.commandMain.SetActive(false);

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

    public float ririAnimTimer = 0f;
    void RiriAnimMove()
    {
        if (!fast)
        {
            fast = true;
        }

        //コマンド非表示の処理
        enemyFloorRunSysObj.commandMain.SetActive(false);
        enemyFloorRunSysObj.commandWin.SetActive(false);

        //タイマー開始
        ririAnimTimer += Time.deltaTime;

        //待機時間を超えて敵が生きている時
        if (ririAnimTimer >= waitTime && !enemyDeath)
        {
            ririAnimTimer = 0;

            button = false;
            coLock = false;
            command1 = false;
            command2 = false;
            command3 = false;

            //ステータスを変更
            mainTurn = MainTurn.RIRIEFFECT;
        }
    }

    void RiriEffect()
    {
        effectWaitTimer += Time.deltaTime;

        if(effectWaitTimer >= effectWaitTime)
        {
            //ステータスを変更
            mainTurn = MainTurn.DHIALOOPINIT;
            effectWaitTimer = 0f;
            effectWaitTime = 0f;
        }
    }


    void DhiaLoopInit()
    {
        //コマンド部分の表示切り替え
        ririCommand.SetActive(false);
        dhiaCommand.SetActive(true);

        //コマンドのテクスチャの切り替え
        commnadImage[0].sprite = dhiaCommandSp;
        commnadImage[1].sprite = dhiaCommandSp;
        commnadImage[2].sprite = dhiaCommandSp;


        atkDefSlectWin.SetActive(true);
        enemyFloorRunSysObj.commandWin.SetActive(true);

        enemyScript.enemyHpDef[0] = enemyScript.hp[0];
        enemyScript.enemyHpDef[1] = enemyScript.hp[1];



        //防御スキルの初期化処理
        //お守りします！
        if (dhiaScript.protectFlag)
        {
            //ディアの補正値の代入
            dhiaScript.def -= (dhiaScript.def * (dhiaScript.defCorrectionValue / 100));

            dhiaScript.protectTurn--;
            if (dhiaScript.protectTurn <= 0)
            {
                dhiaScript.protectFlag = false;
                //防御補正値を減算
                dhiaScript.defCorrectionValue -= dhiaScript.postureDef;
            }
        }
        //防御体制
        if (dhiaScript.postureFlag)
        {
            dhiaScript.postureTurn--;
            if (dhiaScript.postureTurn <= 0)
            {
                dhiaScript.postureFlag = false;
                //防御補正値を減算
                dhiaScript.defCorrectionValue -= dhiaScript.postureDef;
            }
        }

        //守る
        if (dhiaScript.ririDefenseFlag)
        {
            dhiaScript.ririProtectTurn--;
            if (dhiaScript.ririProtectTurn <= 0)
            {
                //リリーの補正値の代入
                //ririScript.def -= (ririScript.def * (ririScript.defCorrectionValue / 100));

                dhiaScript.ririDefenseFlag = false;
                //防御補正値を減算
                ririScript.defCorrectionValue -= dhiaScript.ririProtectDef;
            }
        }

        if (dhiaScript.defCorrectionValue <= 100)
        {
            dhiaScript.defCorrectionValue = 100;
        }

        dhiaScript.powerUpFlag = false;

        //ダメージを受けた時を判別できるように格納
        dhiahpdf = dhiaScript.hp;

        ririScript.button = false;

        //リリーのステータスをデフォルト値に初期化
        ririScript.def = ririScript.defaultDef;

        ririScript.def = (ririScript.def * ririScript.defCorrectionValue) / 100;


        //ディアのステータスをデフォルト値に初期化
        dhiaScript.attack = dhiaScript.attackDefault;

        dhiaScript.attack = (dhiaScript.attack * dhiaScript.atkCorrectionValue) / 100;

        dhiaScript.def = dhiaScript.defDefault;

        dhiaScript.def = (dhiaScript.def * dhiaScript.defCorrectionValue) / 100;


        mainTurn = MainTurn.DHIAATKDEFSLECT;
    }

    void DhiaFastSlect()
    {
        if (button)
        {
            //アタックスキルを選択
            if (dhiaSlectNumber == 0)
            {
                command1Text.text = dhiaScript.atkSkillName[0];
                command2Text.text = dhiaScript.atkSkillName[1];
                command3Text.text = dhiaScript.atkSkillName[2];

                dhiaScript.atkDefSlect = Dhia.AtkDefSlect.ATK;
            }
            //ディフェンススキルを選択
            if (dhiaSlectNumber == 1)
            {
                command1Text.text = dhiaScript.defSkillName[0];
                command2Text.text = dhiaScript.defSkillName[1];
                command3Text.text = dhiaScript.defSkillName[2];
                dhiaScript.atkDefSlect = Dhia.AtkDefSlect.DEF;
            }

            button = false;
            atkDefSlectWin.SetActive(false);

            //コマンド表示の処理
            enemyFloorRunSysObj.commandMain.SetActive(true);
            enemyFloorRunSysObj.commandWin.SetActive(true);

            mainTurn = MainTurn.DHIAMOVE;
        }
    }

    [SerializeField]
    DhiaSkillList dhiaSkillList;
    [NonSerialized]
    public int commandNo = 0;
    void DhiaMove()
    {
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

            fast = false;
        }

        if (command1 && !coLock)
        {
            //攻撃スキルの時
            if (dhiaScript.atkDefSlect == AtkDefSlect.ATK)
            {
                //計算に必要な数値の代入
                dhiaScript.power = dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[0]].power;
                dhiaScript.hitRate = dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[0]].hitRate;


                //対象を選ばせる(単体)
                //味方
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == AtackSkillStatus.eCharSlectType._PICKCHARA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    ririScript.ririEnemySlectWin.SetActive(true);
                }
                //敵
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == AtackSkillStatus.eCharSlectType._PICKENEMY)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    dhiaScript.enemySelectWin.SetActive(true);
                }

                //対象を選ばせない(全体)
                //味方
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == AtackSkillStatus.eCharSlectType._ALLCHARA)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //敵
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == AtackSkillStatus.eCharSlectType._ALLENEMY)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
            }
            //防御スキルの時
            if (dhiaScript.atkDefSlect == AtkDefSlect.DEF)
            {
                //対象を選ばせる(単体)
                //味方
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == DefenseSkillStatus.eCharSlectType._PICKCHARA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                }
                //敵
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == DefenseSkillStatus.eCharSlectType._PICKENEMY)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    dhiaScript.enemySelectWin.SetActive(true);
                }

                //対象を選ばせない(全体)
                //味方
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == DefenseSkillStatus.eCharSlectType._ALLCHARA)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //リリー単体
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == DefenseSkillStatus.eCharSlectType._RIRI)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    //防御バフ
                    if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
                    {
                        ririScript.defCorrectionValue += dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[0]].correctionValue;
                    }

                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //ディア単体
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == DefenseSkillStatus.eCharSlectType._DHIA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    //防御バフ
                    if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
                    {
                        dhiaScript.defCorrectionValue += dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[0]].correctionValue;
                    }

                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //敵
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[0]].charSlectType == DefenseSkillStatus.eCharSlectType._ALLENEMY)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
            }

            //多重押し制限
            coLock = true;

            commandNo = 1;
        }
        if (command2 && !coLock)
        {
            //攻撃スキルの時
            if (dhiaScript.atkDefSlect == AtkDefSlect.ATK)
            {

                //計算に必要な数値の代入
                dhiaScript.power = dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[1]].power;
                dhiaScript.hitRate = dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[1]].hitRate;


                //対象を選ばせる(単体)
                //味方
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == AtackSkillStatus.eCharSlectType._PICKCHARA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    ririScript.ririEnemySlectWin.SetActive(true);
                }
                //敵
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == AtackSkillStatus.eCharSlectType._PICKENEMY)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    dhiaScript.enemySelectWin.SetActive(true);
                }

                //対象を選ばせない(全体)
                //味方
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == AtackSkillStatus.eCharSlectType._ALLCHARA)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //敵
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == AtackSkillStatus.eCharSlectType._ALLENEMY)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }

                //多重押し制限
                coLock = true;

                commandNo = 2;
            }
            //防御スキルの時
            if (dhiaScript.atkDefSlect == AtkDefSlect.DEF)
            {
                //対象を選ばせる(単体)
                //味方
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == DefenseSkillStatus.eCharSlectType._PICKCHARA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                }
                //敵
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == DefenseSkillStatus.eCharSlectType._PICKENEMY)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    dhiaScript.enemySelectWin.SetActive(true);
                }

                //対象を選ばせない(全体)
                //味方
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == DefenseSkillStatus.eCharSlectType._ALLCHARA)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //リリー単体
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == DefenseSkillStatus.eCharSlectType._RIRI)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    //防御バフ
                    if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[1]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
                    {
                        ririScript.defCorrectionValue += dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[1]].correctionValue;
                    }

                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //ディア単体
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == DefenseSkillStatus.eCharSlectType._DHIA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    //防御バフ
                    if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[1]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
                    {
                        dhiaScript.defCorrectionValue += dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[1]].correctionValue;
                    }

                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //敵
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[1]].charSlectType == DefenseSkillStatus.eCharSlectType._ALLENEMY)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
            }

        }
        if (command3 && !coLock)
        {

            //攻撃スキルの時
            if (dhiaScript.atkDefSlect == AtkDefSlect.ATK)
            {
                //計算に必要な数値の代入
                dhiaScript.power = dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[2]].power;
                dhiaScript.hitRate = dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[2]].hitRate;


                //対象を選ばせる(単体)
                //味方
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == AtackSkillStatus.eCharSlectType._PICKCHARA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    ririScript.ririEnemySlectWin.SetActive(true);
                }
                //敵
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == AtackSkillStatus.eCharSlectType._PICKENEMY)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    dhiaScript.enemySelectWin.SetActive(true);
                }

                //対象を選ばせない(全体)
                //味方
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == AtackSkillStatus.eCharSlectType._ALLCHARA)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //敵
                if (dhiaSkillList.atkSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == AtackSkillStatus.eCharSlectType._ALLENEMY)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
            }
            //防御スキルの時
            if (dhiaScript.atkDefSlect == AtkDefSlect.DEF)
            {
                //対象を選ばせる(単体)
                //味方
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == DefenseSkillStatus.eCharSlectType._PICKCHARA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                }
                //敵
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == DefenseSkillStatus.eCharSlectType._PICKENEMY)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    dhiaScript.enemySelectWin.SetActive(true);
                }

                //対象を選ばせない(全体)
                //味方
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == DefenseSkillStatus.eCharSlectType._ALLCHARA)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //リリー単体
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == DefenseSkillStatus.eCharSlectType._RIRI)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    //防御バフ
                    if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[2]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
                    {
                        ririScript.defCorrectionValue += dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[2]].correctionValue;
                    }

                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //ディア単体
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == DefenseSkillStatus.eCharSlectType._DHIA)
                {
                    //3つのコマンドボタンのアクティブ消し
                    dhiaScript.commandButton.SetActive(false);

                    //防御バフ
                    if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[2]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
                    {
                        dhiaScript.defCorrectionValue += dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaDef[2]].correctionValue;
                    }

                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
                //敵
                if (dhiaSkillList.defSkillList[floorNoSysScript.skillNoDhiaAtk[2]].charSlectType == DefenseSkillStatus.eCharSlectType._ALLENEMY)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.DHIAANIM;
                }
            }

            //多重押し制限
            coLock = true;

            commandNo = 3;
        }
    }

        float endWaitTimer = 0f;
    float dhiaAnimTimer = 0f;
    void DhiaAnimMove()
    {
    //アニメーションの時間
    //敵を倒したか確認
    if (enemyScript.deathFlag)
        {
            //タイマー開始
            endWaitTimer += Time.deltaTime;

            if (endWaitTimer >= 3)
            {
                mainTurn = MainTurn.ENDRUN;
                endWaitTimer = 0;
            }
        }
        else
        {
            //コマンド非表示の処理
            enemyFloorRunSysObj.commandMain.SetActive(false);
            enemyFloorRunSysObj.commandWin.SetActive(false);

            //タイマー開始
            dhiaAnimTimer += Time.deltaTime;

            //待機時間を超えて敵が生きている時
            if (dhiaAnimTimer >= waitTime && !enemyDeath)
            {
                dhiaAnimTimer = 0;
                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;

                //リリーのステータスをデフォルト値に初期化
                ririScript.def = ririScript.defaultDef;

                ririScript.def = (ririScript.def * ririScript.defCorrectionValue) / 100;


                //ディアのステータスをデフォルト値に初期化
                dhiaScript.def = dhiaScript.defDefault;

                dhiaScript.def = (dhiaScript.def  * dhiaScript.defCorrectionValue) / 100;

                //ステータスを変更
                mainTurn = MainTurn.DHIAEFFECT;
            }
        }
    }

    void DhiaEffect()
    {
        effectWaitTimer += Time.deltaTime;
        if(effectWaitTimer >= effectWaitTime)
        {
            //ステータスを変更
            mainTurn = MainTurn.ENEMY1LOOPINIT;
            effectWaitTimer = 0;
            effectWaitTime = 0;
        }
    }


    void Enemy1LoopInit()
    {

        mainTurn = MainTurn.ENEMY1MOVE;
    }

    void Enemy1Move()
    {
        //死んでる時はターンをスキップして戻る
        if (enemyScript.enemyDeath[0] || enemyStopFlag[0])
        {
            mainTurn = MainTurn.ENEMY2MOVE;
            return;
        }


        if (!coLock)
        {
            //Init時に選択されたエネミーのスキル関数を呼び出す
            enemyScript.Move();
            coLock = true;
        }

        mainTurn = MainTurn.ENEMY1ANIM;
    }

    float enemy1Timer = 0f;
    void Enemy1AnimMove()
    {
        //タイマー開始
        enemy1Timer += Time.deltaTime;

        //待機時間を超えたら
        if (enemy1Timer >= waitTime)
        {
            coLock = false;
            enemy1Timer = 0;

            //ステータスを変更
            mainTurn = MainTurn.ENEMY1EFFECT;
        }
    }

    void Enmey1Effect()
    {
        effectWaitTimer += Time.deltaTime;

        if (effectWaitTimer >= effectWaitTime)
        {
            //ステータスを変更
            //敵が2体の時
            if (numberRnd == 1)
            {
                mainTurn = MainTurn.ENEMY2LOOPINIT;
            }
            else
            {
                mainTurn = MainTurn.RIRILOOPINIT;
            }
            effectWaitTimer = 0;
            effectWaitTime = 0;
        }
    }


    void Enemy2LoopInit()
    {



        mainTurn = MainTurn.ENEMY2MOVE;
    }

    void Enemy2Move()
    {

        if (numberRnd == 0 || enemyScript.enemyDeath[1] || enemyStopFlag[1])
        {
            mainTurn = MainTurn.RIRILOOPINIT;
            return;
        }


        if (!coLock)
        {
            //Init時に選択されたエネミーのスキル関数を呼び出す
            enemyScript.Move();
            coLock = true;
        }

        //ステータスを変更
        mainTurn = MainTurn.ENEMY2ANIM;
    }

    float enemy2Timer = 0f;
    void Enemy2AnimMove()
    {
        //タイマー開始
        enemy2Timer += Time.deltaTime;

        //待機時間を超えたら
        if (enemy2Timer >= waitTime)
        {
            coLock = false;
            enemy2Timer = 0;

            //ステータスを変更
            mainTurn = MainTurn.ENEMY2EFFECT;
        }
    }

    void Enemy2Effect()
    {
        effectWaitTimer += Time.deltaTime;

        if (effectWaitTimer >= effectWaitTime)
        {
            //ステータスを変更
            mainTurn = MainTurn.RIRILOOPINIT;

            effectWaitTimer = 0;
            effectWaitTime = 0;
        }
    }

    //ゲームオーバーの時間計測用
    float gameOvertimer = 0f;
    //ゲームオーバーの遷移タイム設定用
    [SerializeField]
    float gameOverTime = 0f;
    void GameOver()
    {
        gameOvertimer += Time.deltaTime;

        if (gameOvertimer >= gameOverTime)
        {
            SceneManager.LoadScene("GameOver");
            gameOvertimer = 0;
        }
    }

    [SerializeField]
    int partsDispTime = 0;
    float partsTimer = 0;

    void EndRun()
    {
        switch(partsMode)
        {
            case PartsMode.DISP:
                partsTimer += Time.deltaTime;
                if(partsTimer >= partsDispTime)
                {
                    partsMode = PartsMode.WAIT;
                    partsTimer = 0;
                }
                break;
            case PartsMode.WAIT:
                if (partFastMove)
                {
                    //ドロップ装備の表示処理
                    slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName;
                    slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName;
                    slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName;

                    //ドロップ装備の画像表示処理
                    dropPartsSp[0].sprite = equipmentManager.randomEquip[equipmentManager.rnd[0]].sprite;
                    dropPartsSp[1].sprite = equipmentManager.randomEquip[equipmentManager.rnd[1]].sprite;
                    dropPartsSp[2].sprite = equipmentManager.randomEquip[equipmentManager.rnd[2]].sprite;

                    partFastMove = false;
                }
                partsSlectWin.SetActive(true);

                break;
            case PartsMode.END:
                if (characterMainObj.transform.position.x <= doorObj.transform.position.x + 70)
                {
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);
                    characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
                    //歩きアニメーションを開始
                    dhiaAnim.SetBool("D_Shield", false);
                    ririAnim.SetBool("R_Walk", true);
                    dhiaAnim.SetBool("D_Walk", true);
                }
                else
                {
                    //歩きアニメーションを停止
                    ririAnim.SetBool("R_Walk", false);
                    dhiaAnim.SetBool("D_Walk", false);
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);

                    commandMain.SetActive(false);
                    StartCoroutine(FloorEnd());
                }

                break;
        }
        commandWin.SetActive(false);
        commandMain.SetActive(false);

        if (!allPartsSlect)
        {
        }
        //装備が選ばれたら画面外まで移動する処理
        else
        {
        }
    }

    bool[] partsButton = new bool[3];
    public void PartsSlect1()
    {
        if (!partsButton[0])
        {
            partsImage[0].sprite = slectOnSp;
            partsImage[1].sprite = slectOffSp;
            partsImage[2].sprite = slectOffSp;
            partsObj[0].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);
            arrowObj[0].SetActive(true);
            arrowObj[1].SetActive(false);
            arrowObj[2].SetActive(false);

            partsSlect[0] = true;
            partsSlect[1] = false;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect2()
    {
        if (!partsButton[1])
        {
            partsImage[0].sprite = slectOffSp;
            partsImage[1].sprite = slectOnSp;
            partsImage[2].sprite = slectOffSp;
            partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[1].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);
            arrowObj[0].SetActive(false);
            arrowObj[1].SetActive(true);
            arrowObj[2].SetActive(false);

            partsSlect[0] = false;
            partsSlect[1] = true;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect3()
    {
        if (!partsButton[2])
        {
            partsImage[0].sprite = slectOffSp;
            partsImage[1].sprite = slectOffSp;
            partsImage[2].sprite = slectOnSp;
            partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[2].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            arrowObj[0].SetActive(false);
            arrowObj[1].SetActive(false);
            arrowObj[2].SetActive(true);

            partsSlect[0] = false;
            partsSlect[1] = false;
            partsSlect[2] = true;
        }
    }

    public void PartsSlecteEnd()
    {
        button = true;
        partsMode = PartsMode.END;
        partsSlectWin.SetActive(false);

        //該当する部位にパーツデータを格納する処理
        if (partsSlect[0])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
        }
        if (partsSlect[1])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
        }
        if (partsSlect[2])
        {
            //右手
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //左手
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //足
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //体
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //頭
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
        }
    }

    [SerializeField]
    GameObject mapWindow;
    void LoadScene()
    {
        //mapWindow.SetActive(true);
        SceneManager.LoadScene("LoadScene");
    }

    //フェード処理用
    [SerializeField]
    Animator fadeAnim = null;
    //ドアまで到着した時の処理
    IEnumerator FloorEnd()
    {
        //fadeAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1.0f);
        LoadScene();
        floorEndFlag = true;
    }

}
