using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditorInternal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class EncountSys : MonoBehaviour
{
    //バトルコマンドのテキスト
    [SerializeField]
    TextMeshProUGUI windowsMes = null;
    [SerializeField]
    TextMeshProUGUI command1Text = null;
    [SerializeField]
    TextMeshProUGUI command2Text = null;
    [SerializeField]
    TextMeshProUGUI command3Text = null;

    //Moveフラグ
    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;

    //休憩階のフラグ
    public bool restFlag = false;

    //ボス階のフラグ
    public bool bossFlag = false;

    bool fastMove = false;

    //ボタン連続入力抑制用
    bool button = false;

    //バイキルト状態の判別
    bool powerUpFlag = false;

    //ディアの守り状態判別
    bool defenseFlag = false;

    //ディアのリリー守り状態判別
    bool ririDefenseFlag = false;


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

    //体力ゲージのObj
    [SerializeField]
    Slider ririSlider = null;
    [SerializeField]
    Slider dhiaSlider = null;
    [SerializeField]
    Slider enemySlider = null;

    //リリー,ディア,エネミーのObj
    [SerializeField]
    GameObject ririObj;
    [SerializeField]
    GameObject dhiaObj;
    [SerializeField]
    GameObject enemyObj;


    void Awake()
    {
        Init();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    #region Init処理
    void Init()
    {
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

        fastMove = true;
        //windowsMes.text = "リリーの行動をにゅうりょくしてください";
        //RiriMove();
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
        command1Text.text = "ヒール";
        command2Text.text = "オールヒール";
        command3Text.text = "バイキルト";

        if (enemy.deathFlag)
        {
            windowsMes.text = "敵を倒した！";
            enemyObj.SetActive(false);
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
        }
    }

    void DhiaMove()
    {
        command1Text.text = "殴る";
        command2Text.text = "防御体制";
        command3Text.text = "守る";
        ririDefenseFlag = false;
        defenseFlag = false;

        if (enemy.deathFlag)
        {
            windowsMes.text = "敵を倒した！";
            Invoke("EnemyDeat", 1.0f);
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("ディア");
            dhiaMoveFlag = true;
            ririMoveFlag = false;
            windowsMes.text = "ディアの行動をにゅうりょくしてください";
            button = false;
            StartCoroutine(DhiaEnterWait());
        }
    }

    void EnemyMove()
    {
        if (enemy.deathFlag)
        {
            windowsMes.text = "敵を倒した！";
            Invoke("EnemyDeat", 1.0f);
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("エネミー");
            enemyMoveFlag = true;
            if(defenseFlag)
            {
                windowsMes.text = "てきのこうげき！" + enemy.power * 0.5f + "のダメージ!";
                dhia.hp -= (enemy.power * 0.5f);
            }
            else
            {
                windowsMes.text = "てきのこうげき！" + enemy.power + "のダメージ!";
                dhia.hp -= enemy.power;
            }
            ririSlider.value *= (riri.hp / riri.maxhp);
            dhiaSlider.value *= (dhia.hp / dhia.maxhp);
            button = true;
            StartCoroutine(EnemyEnterWait());
        }
    }
    #endregion


    #region 死亡処理

    void EnemyDeat()
    {
        enemyFloorRunSysObj.battleEndFlag = true;
        bool enemyDeat = false;
        if(!enemyDeat)
        {
            //floorNoSys.floorNo += 1;
            enemyDeat = true;
        }
    }

    void RiriDeath()
    {

    }

    void DhiaDeath()
    {

    }

    #endregion

    #region ボタン決定時処理
    public void Command1Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if(dhia.maxhp < dhia.hp + 50)
            {
                windowsMes.text = "リリーはヒールを唱えた！\n" + "ディア" + "のHPを"+  (dhia.maxhp - dhia.hp)  +"回復した!";
                dhia.hp = dhia.maxhp;
                dhiaSlider.value = dhiaSlider.maxValue;
            }
            else
            {
                windowsMes.text = "リリーはヒールを唱えた！\n" + "〇〇" + "のHPを50回復した!";
                dhia.hp += 50;
                dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
            }
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if(powerUpFlag)
            {
                windowsMes.text = "ディアのこうげき！" + dhia.power * 1.5f + "のダメージ!";
                enemy.hp -= (dhia.power * 1.5f);
                enemySlider.value *= (enemy.hp / enemy.maxhp);
                powerUpFlag = false;
            }
            else
            {
                windowsMes.text = "ディアのこうげき！" + dhia.power + "のダメージ!";
                enemy.hp -= dhia.power;
                enemySlider.value *= (enemy.hp / enemy.maxhp);
            }
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    public void Command2Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "リリーはオールヒールを唱えた！\n2人のHPを20ずつ回復した!";
            riri.hp += 20;
            dhia.hp += 20;
            ririSlider.value = (ririSlider.maxValue * (riri.hp / riri.maxhp));
            dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "ディアは身を守っている。";
            defenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    public void Command3Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "リリーはバイキルトを唱えた！\nディアの攻撃力が上昇した!";
            powerUpFlag = true;
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "ディアはリリーを守っている。";
            ririDefenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    #endregion


    #region 行動後の待機処理
    IEnumerator RiriEnterWait()
    {
        yield return new WaitForSeconds(2.0f);
        if(fastMove)
        {
            Debug.Log("リリーエンターウェイト");
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
        yield return new WaitForSeconds(2.0f);

        if (button && dhiaMoveFlag)
        {
            dhiaMoveFlag = false;
            button = false;
            EnemyMove();
        }
    }
    IEnumerator EnemyEnterWait()
    {
        yield return new WaitForSeconds(2.0f);

        windowsMes.text = "リリーの行動をにゅうりょくしてください";
        button = false;
        enemyMoveFlag = false;
        RiriMove();
    }
    #endregion
}
