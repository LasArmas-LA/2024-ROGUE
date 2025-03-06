using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading;

public class EncountSys : MonoBehaviour
{
   /* //バトルコマンドのテキスト
    [Header("バトルコマンドのテキスト")]
    [SerializeField]
    TextMeshProUGUI windowsMes = null;
    [SerializeField]
    TextMeshProUGUI command1Text = null;
    [SerializeField]
    TextMeshProUGUI command2Text = null;
    [SerializeField]
    TextMeshProUGUI command3Text = null;

    [SerializeField]
    GameObject recoveryWin = null;

    [Space(10)]

    //Moveフラグ
    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;

    //対象選択時のフラグ
    bool ririSelectFlag = false;
    bool dhiaSelectFlag = false;

    //休憩階のフラグ
    [NonSerialized]
    public bool restFlag = false;

    //ボス階のフラグ
    [NonSerialized]
    public bool bossFlag = false;

    //初回ターンフラグ
    bool fastMove = false;

    //ボタン連続入力抑制用
    bool button = false;

    //バイキルト状態の判別
    bool powerUpFlag = false;

    //ディアの守り状態判別
    bool defenseFlag = false;

    //ディアのリリー守り状態判別
    bool ririDefenseFlag = false;

    //ターン切り替えの待機時間
    [Header("ターン切り替え待機時間")]
    [SerializeField, Tooltip("リリーのターン切り替え待機時間")]
    float ririWaitTime = 0f;
    [SerializeField, Tooltip("ディアのターン切り替え待機時間")]
    float DhiaWaitTime = 0f;
    [SerializeField, Tooltip("エネミーのターン切り替え待機時間")]
    float enemyWaitTime = 0f;
    
    [Space(10)]

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

    //体力ゲージのObj
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

    private enum GameState
    { 
        WAIT,

        RIRI_TRUN,
        RIRI_ANIMATION,

        DHIA_TRUN,
        DHIA_ANIMATION,

        ENEMY_TRUN,
        ENEMY_ANIMATION,
        
        RESULT,
        GAME_END
    }
    GameState gameState;

    private enum Command
    {
        Command1,
        Command2,
        Command3,
    }
    Command playerCommand;

    [SerializeField]
    [Tooltip("コマンド選択時の表示テキスト")]
    String[] texts1 = null;

    void Awake()
    {

    }
    void Start()
    {
        Init();
    }

    private float waitT;

    void Update()
    {
        switch (gameState)
        {
            case GameState.WAIT:

                break;
            case GameState.RIRI_TRUN:
                // キー入力待ち

                break;
            case GameState.RIRI_ANIMATION:
                // 待機処理
                waitT += Time.deltaTime;
                if(waitT > 100){
                    gameState = GameState.ENEMY_TRUN;
                }
                break;
            case GameState.DHIA_TRUN:
                break;
            case GameState.DHIA_ANIMATION:
                break;
            case GameState.ENEMY_TRUN:
                break;
            case GameState.ENEMY_ANIMATION:
                break;
            case GameState.RESULT:
                gameState = GameState.GAME_END;
                // プレイヤーのHPが0だったら
                // ゲームオーバー
                Debug.Log(texts1[(int)Command.Command1]);

                break;
        }
    }

    #region Init処理
    void Init()
    {
        // VSyncCount を Dont Sync に変更
        QualitySettings.vSyncCount = 0;
        // 60fpsを目標に設定
        Application.targetFrameRate = 60;

        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();

        //休憩フロアフラグオン
        if (floorNoSys.floorNo % 5 == 0 && floorNoSys.floorNo != 0)
        {
            restFlag = true;
        }
        else
        {
            restFlag = false;
        }
        //ボスフロアフラグオン
        if (floorNoSys.floorNo % 10 == 0 && floorNoSys.floorNo != 0)
        {
            bossFlag = true;
        }
        else
        {
            bossFlag = false;
        }

        //ムーブフラグの初期化
        ririMoveFlag = false;
        dhiaMoveFlag = false;
        enemyMoveFlag = false;

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

        fastMove = true;
    }
    #endregion

    #region ループInit処理
    void RiriInit()
    {

    }

    void DhiaInit()
    {

    }
    #endregion

    #region ムーブ処理
    public void RiriMove()
    {
        gameState = GameState.RIRI_TRUN;

        if (riri.hp <= 0)
        {
           RiriDeath();
        }
        if(dhia.hp <= 0)
        {
           DhiaDeath();
        }

        command1Text.text = "ヒール";
        command2Text.text = "オールヒール";
        command3Text.text = "バイキルト";
        if(riri.deathFlag)
        {
            //リリーが死亡している場合ターンをスキップ
            DhiaMove();
        }
        else
        {
            if(!ririMoveFlag && fastMove)
            {
                if(bossFlag)
                {
                    windowsMes.text = "ボスが現れた！";
                }
                else
                {
                    windowsMes.text = "敵が現れた！";
                }
            }
            Debug.Log("リリー");
            ririMoveFlag = true;
            StartCoroutine(RiriEnterWait());
            return;
        }
    }

    void DhiaMove()
    {
        gameState = GameState.DHIA_TRUN;
        if (dhia.deathFlag)
        {
            //ディアが死亡している場合ディアのターンをスキップ
            EnemyMove();
        }
        else
        {
            Debug.Log("ディア");
            //コマンドのテキスト処理
            command1Text.text = "殴る";
            command2Text.text = "防御体制";
            command3Text.text = "守る";

            //守りのフラグを初期化
            ririDefenseFlag = false;
            defenseFlag = false;

            dhiaMoveFlag = true;
            ririMoveFlag = false;
            windowsMes.text = "ディアの行動をにゅうりょくしてください";
            button = false;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }

    void EnemyMove()
    {
        gameState = GameState.ENEMY_TRUN;
        if (enemyMoveFlag) 
        {
            windowsMes.text = "敵を倒した！";
            Invoke("EnemyDeath", 1.0f);
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("エネミー");
            enemyMoveFlag = true;

            int rnd = 0;
            for (int i = 0; i < 5;i++)
            {
                rnd = UnityEngine.Random.Range(0, 2);
            }
            if(dhiaDeath)
            {
                rnd = 0;
            }

            //攻撃対象リリー
            if(rnd == 0)
            {
                //70%軽減
                if (ririDefenseFlag)
                {
                    windowsMes.text = "てきのこうげき！ディアがリリーを守った！ディアに" + enemy.power * 0.3f + "のダメージ!";
                    dhia.hp -= (enemy.power * 0.3f);
                }
                else
                {
                    windowsMes.text = "てきのこうげき！リリーに" + enemy.power + "のダメージ!";
                    riri.hp -= enemy.power;
                }

            }
            //攻撃対象ディア
            else if (rnd == 1)
            {
                if (defenseFlag)
                {
                    windowsMes.text = "てきのこうげき！ディアに" + enemy.power * 0.5f + "のダメージ!";
                    dhia.hp -= (enemy.power * 0.5f);
                }
                else
                {
                    windowsMes.text = "てきのこうげき！ディアに" + enemy.power + "のダメージ!";
                    dhia.hp -= enemy.power;
                }
            }
            ririSlider.value *= (riri.hp / riri.maxhp);
            dhiaSlider.value *= (dhia.hp / dhia.maxhp);
            button = true;
            StartCoroutine(EnemyEnterWait());
            return;
        }
    }
    #endregion


    #region 死亡処理

    void EnemyDeath()
    {
        enemyFloorRunSysObj.battleEndFlag = true;
    }

    void RiriDeath()
    {
        enemyFloorRunSysObj.gameOverFlag = true;
    }

    void DhiaDeath()
    {
        dhiaDeath = true;
        dhiaObj.SetActive(false);
    }

    #endregion

    #region ボタン決定時処理
    public void Command1Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.RIRI_TRUN) { return; }

            windowsMes.text = "回復対象を選んでください";

            if (ririSelectFlag || dhiaSelectFlag)
            {
                if (ririSelectFlag)
                {
                    if (riri.maxhp < riri.hp + 50)
                    {
                        Debug.Log("コマンド1リリーHPマックス回復");
                        windowsMes.text = "リリーはヒールを唱えた！\n" + "リリー" + "のHPを" + (riri.maxhp - riri.hp) + "回復した!";
                        riri.hp = riri.maxhp;
                        ririSlider.value = ririSlider.maxValue;
                    }
                    else
                    {
                        Debug.Log("コマンド1リリーHP差分回復");
                        windowsMes.text = "リリーはヒールを唱えた！\n" + "リリー" + "のHPを50回復した!";
                        riri.hp += 50;
                        ririSlider.value = (ririSlider.maxValue * (riri.hp / riri.maxhp));
                    }
                }
                if (dhiaSelectFlag)
                {
                    if (dhia.maxhp < dhia.hp + 50)
                    {
                        Debug.Log("コマンド1リリーHPマックス回復");
                        windowsMes.text = "リリーはヒールを唱えた！\n" + "ディア" + "のHPを" + (dhia.maxhp - dhia.hp) + "回復した!";
                        dhia.hp = dhia.maxhp;
                        dhiaSlider.value = dhiaSlider.maxValue;
                    }
                    else
                    {
                        Debug.Log("コマンド1リリーHP差分回復");
                        windowsMes.text = "リリーはヒールを唱えた！\n" + "ディア" + "のHPを50回復した!";
                        dhia.hp += 50;
                        dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
                    }
                    dhiaSelectFlag = false;
                }
                button = true;
                StartCoroutine(RiriEnterWait());
                return;
            }
            else
            {
                recoveryWin.SetActive(true);
            }
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.DHIA_TRUN) { return; }

            if (powerUpFlag)
            {
                Debug.Log("コマンド1ディアパワーアップ攻撃");

                windowsMes.text = "ディアのこうげき！" + dhia.power * 1.5f + "のダメージ!";
                enemy.hp -= (dhia.power * 1.5f);
                enemySlider.value *= (enemy.hp / enemy.maxhp);
                powerUpFlag = false;
            }
            else
            {
                Debug.Log("コマンド1ディア通常攻撃");
                windowsMes.text = "ディアのこうげき！" + dhia.power + "のダメージ!";
                enemy.hp -= dhia.power;
                enemySlider.value *= (enemy.hp / enemy.maxhp);
            }
            button = true;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }
    public void Command2Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.RIRI_TRUN) { return; }

            Debug.Log("コマンド2リリー");
            
            if(riri.maxhp > riri.hp + 20 && dhia.maxhp > dhia.hp + 20)
            {
                riri.hp += 20;
                dhia.hp += 20;
                windowsMes.text = "リリーはオールヒールを唱えた！\nリリーとディアのHPを20ずつ回復した!";
            }
            else
            {
                if(riri.maxhp < riri.hp + 20)
                {
                    riri.hp = riri.maxhp;
                }
                if(dhia.maxhp < dhia.hp + 20)
                {
                    dhia.hp = dhia.maxhp;
                }
                windowsMes.text = "リリーはオールヒールを唱えた！\nリリーのHPを"+ (riri.maxhp - riri.hp) + "ディアのHPを"+ (dhia.maxhp - dhia.hp) + "回復した!";
            }
            ririSlider.value = (ririSlider.maxValue * (riri.hp / riri.maxhp));
            dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
            button = true;
            StartCoroutine(RiriEnterWait());
            return;
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.DHIA_TRUN) { return; }

            Debug.Log("コマンド2ディア");
            windowsMes.text = "ディアは身を守っている。";
            defenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }
    public void Command3Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.RIRI_TRUN) { return; }

            Debug.Log("コマンド3リリー");
            windowsMes.text = "リリーはバイキルトを唱えた！\nディアの攻撃力が上昇した!";
            powerUpFlag = true;
            button = true;
            StartCoroutine(RiriEnterWait());
            return;
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.DHIA_TRUN) { return; }

            Debug.Log("コマンド3ディア");
            windowsMes.text = "ディアはリリーを守っている。";
            ririDefenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }

    public void RiriSlect()
    {
        if(gameState != GameState.RIRI_TRUN) { return; }

        ririSelectFlag = true;
        recoveryWin.SetActive(false);
        Command1Button();
    }
    public void DhiaSlect()
    {
        if (gameState != GameState.RIRI_TRUN) { return; }

        dhiaSelectFlag = true;
        recoveryWin.SetActive(false);
        Command1Button();
    }
    #endregion

    public void Result(int no)
    {
        switch (playerCommand)
        { 
            case Command.Command1:
                // 攻撃
                gameState = GameState.RIRI_ANIMATION;
                // コルーチン開始
                

                break;
            case Command.Command2:
                // 防御

                break;
        }
    }


    #region 行動後の待機処理
    IEnumerator RiriEnterWait()
    {
        if (gameState != GameState.RIRI_TRUN) { yield return null; }

        yield return new WaitUntil(() => ririMoveFlag);
        yield return new WaitForSeconds(ririWaitTime);
        if (fastMove)
        {
            windowsMes.text = "リリーの行動をにゅうりょくしてください";
            fastMove = false;
        }
        if (button)
        {
            ririMoveFlag = false;
            button = false;
            DhiaMove();
        }
    }
    IEnumerator DhiaEnterWait()
    {
        if (gameState != GameState.DHIA_TRUN) { yield return null; }

        yield return new WaitUntil(() => dhiaMoveFlag);
        yield return new WaitForSeconds(DhiaWaitTime);

        if (button && dhiaMoveFlag)
        {
            dhiaMoveFlag = false;
            EnemyMove();
            button = false;
        }
    }
    IEnumerator EnemyEnterWait()
    {
        if (gameState != GameState.ENEMY_TRUN) { yield return null; }

        yield return new WaitForSeconds(enemyWaitTime);

        windowsMes.text = "リリーの行動をにゅうりょくしてください";
        enemyMoveFlag = false;
        RiriMove();
        button = false;
    }
    #endregion
   */
}
