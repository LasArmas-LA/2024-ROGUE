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
            windowsMes.text = "てきのこうげき！" + enemy.power + "のダメージ!";
            dhia.hp -= enemy.power;
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
    public void AttackButton()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "リリーのこうげき！" + riri.power + "のダメージ!";
            enemy.hp -= riri.power;
            enemySlider.value *= (enemy.hp / enemy.maxhp);
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "ディアのこうげき！" + dhia.power + "のダメージ!";
            enemy.hp -= dhia.power;
            enemySlider.value *= (enemy.hp / enemy.maxhp);
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    public void DefenseButton()
    {
        button = true;
        if(ririMoveFlag)
        {
            windowsMes.text = "リリーはぼうぎょした!";
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag)
        {
            windowsMes.text = "ディアはぼうぎょした!";
            StartCoroutine(DhiaEnterWait());
        }
    }

    #endregion


    #region 行動後の待機処理
    IEnumerator RiriEnterWait()
    {
        yield return new WaitForSeconds(1.0f);
        if(fastMove)
        {
            windowsMes.text = "リリーの行動をにゅうりょくしてください";
            fastMove = false;
        }
        if (button)
        {
            ririMoveFlag = false;
            DhiaMove();
            button = false;
        }
    }
    IEnumerator DhiaEnterWait()
    {
        if (button)
        {
            yield return new WaitForSeconds(1.0f);
            dhiaMoveFlag = false;
            button = false;
            EnemyMove();
        }
    }
    IEnumerator EnemyEnterWait()
    {
        if (button)
        {
            yield return new WaitForSeconds(1.0f);
            windowsMes.text = "リリーの行動をにゅうりょくしてください";
            RiriMove();
            button = false;
            enemyMoveFlag = false;
        }
    }
    #endregion
}
