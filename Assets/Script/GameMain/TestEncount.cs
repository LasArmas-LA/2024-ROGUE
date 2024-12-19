using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestEncount : MonoBehaviour
{
    enum MainTurn
    {
        WAIT,

        RIRIMOVE,
        RIRIANIM,

        DHIAMOVE,
        DHIAANIM,

        ENEMYMOVE,
        ENEMYANIM,

        END
    }
    MainTurn mainTurn;

    //バトルコマンドのテキスト
    [Header("バトルコマンドのテキスト")]
    [SerializeField]
    TextMeshProUGUI windowsMes = null;
    [SerializeField]
    TextMeshProUGUI command1Text = null;
    [SerializeField]
    TextMeshProUGUI command2Text = null;
    [SerializeField]
    TextMeshProUGUI command3Text = null;

    //スクリプト参照
    [SerializeField]
    Riri ririScript = null;
    [SerializeField]
    Dhia dhiaScript = null;
    [SerializeField]
    Enemy enemyScript = null;

    //待機時間
    [SerializeField]
    float waitTime = 0;
    float timer = 0;

    [Header("クラス参照")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;
    [SerializeField]
    Enemy enemy = null;
    FloorNoSys floorNoSys = null;
    GameObject floorNoSysObj = null;
    [SerializeField]
    EnemyFloorRunSys enemyFloorRunSysObj = null;

    [Space(10)]

    [Header("体力ゲージ")]
    [SerializeField, Tooltip("リリーの体力ゲージ")]
    Slider ririSlider = null;
    [SerializeField, Tooltip("ディアの体力ゲージ")]
    Slider dhiaSlider = null;
    [SerializeField, Tooltip("敵の体力ゲージ")]
    Slider enemySlider = null;

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
    [SerializeField]
    GameObject enemyObj;


    void Start()
    {
        Init();
    }

    void Init()
    {
        mainTurn = MainTurn.RIRIMOVE;

        //MaxHPの格納
        ririSlider.maxValue = riri.maxhp;
        dhiaSlider.maxValue = dhia.maxhp;
        enemySlider.maxValue = enemy.maxhp;

        //MinHPの格納
        ririSlider.minValue = 0;
        dhiaSlider.minValue = 0;
        enemySlider.minValue = 0;

        //MaxのHPを現在のHPに格納
        ririSlider.value = ririSlider.maxValue;
        dhiaSlider.value = dhiaSlider.maxValue;
        enemySlider.value = enemySlider.maxValue;

        //MaxのHPを現在のHPに格納
        ririSlider.value *= (riri.hp / riri.maxhp);
        dhiaSlider.value *= (dhia.hp / dhia.maxhp);
        enemySlider.value *= (enemy.hp / enemy.maxhp);

    }

    void FixedUpdate()
    {
        switch (mainTurn)
        {
            case MainTurn.WAIT:
                break;
            case MainTurn.RIRIMOVE:
                break;
            case MainTurn.RIRIANIM:
                break;
            case MainTurn.DHIAMOVE:
                break;
            case MainTurn.DHIAANIM:
                break;
            case MainTurn.ENEMYMOVE:
                break;
            case MainTurn.ENEMYANIM:
                break;
            case MainTurn.END:
                break;
        }
        
        RiriMove();
        DhiaMove();
        EnemyMove();
    }

    //コマンド処理
    bool command1 = false;
    bool command2 = false;
    bool command3 = false;
    bool button = false;

    public void Command1()
    {
        if(!button)
        {
            button = true;
            command1 = true;
        }
    }
    public void Command2()
    {
        if(!button)
        {
            button = true;
            command2 = true;
        }
    }
    public void Command3()
    {
        if (!button)
        {
            button = true;
            command3 = true;
        }
    }

    void RiriMove()
    {
        if (mainTurn == MainTurn.RIRIMOVE || mainTurn == MainTurn.RIRIANIM)
        {
            windowsMes.text = "リリーの行動をにゅうりょくしてください";
            command1Text.text = "ヒール";
            command2Text.text = "オールヒール";
            command3Text.text = "バイキルト";

            Debug.Log("リリーのターン");
            if (command1 || command2 || command3)
            {
                //タイマー開始
                timer += Time.deltaTime;

                //ステータスを変更
                mainTurn = MainTurn.RIRIANIM;

                if (command1)
                {
                    ririScript.Skil1();
                }
                if (command2)
                {
                    ririScript.Skil2();
                }
                if (command3)
                {
                    ririScript.Skil3();
                }
            }
            //待機時間を超えたら
            if (timer >= waitTime)
            {
                //ステータスを変更
                mainTurn = MainTurn.DHIAMOVE;
                timer = 0;
                button = false;
                command1 = false;
                command2 = false;
                command3 = false;
            }
        }
    }
    void DhiaMove()
    {
        if (mainTurn == MainTurn.DHIAMOVE || mainTurn == MainTurn.DHIAANIM)
        {
            Debug.Log("ディアのターン");
            windowsMes.text = "ディアの行動をにゅうりょくしてください";
            command1Text.text = "殴る";
            command2Text.text = "防御体制";
            command3Text.text = "守る";

            if (command1 || command2 || command3)
            {
                //タイマー開始
                timer += Time.deltaTime;

                //ステータスを変更
                mainTurn = MainTurn.DHIAANIM;

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

                //待機時間を超えたら
                if (timer >= waitTime)
                {
                    //ステータスを変更
                    mainTurn = MainTurn.ENEMYMOVE;
                    timer = 0;
                    button = false;
                    command1 = false;
                    command2 = false;
                    command3 = false;
                }
            }
        }
    }
    void EnemyMove()
    {
        if (mainTurn == MainTurn.ENEMYMOVE || mainTurn == MainTurn.ENEMYANIM)
        {
            windowsMes.text = "エネミーの攻撃";

            //タイマー開始
            timer += Time.deltaTime;

            //ステータスを変更
            mainTurn = MainTurn.ENEMYANIM;

            Debug.Log("エネミーのターン");
            enemyScript.Skil();

            //待機時間を超えたら
            if (timer >= waitTime)
            {
                //ステータスを変更
                mainTurn = MainTurn.RIRIMOVE;
                timer = 0;
            }
        }
    }
}
