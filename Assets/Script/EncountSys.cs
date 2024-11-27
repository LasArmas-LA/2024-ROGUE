using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditorInternal;
using UnityEngine.UI;

public class EncountSys : MonoBehaviour
{
    //バトルコマンドのテキスト
    [SerializeField]
    TextMeshProUGUI windowsMes = null;

    //Moveフラグ
    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;

    //ボタン連続入力抑制用
    bool button = false;

    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;
    [SerializeField]
    Enemy enemy = null;

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



        windowsMes.text = "リリーの行動をにゅうりょくしてください";
        RiriMove();
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
    void RiriMove()
    {
        if (enemy.deathFlag)
        {
            windowsMes.text = "敵を倒した！";
            enemyObj.SetActive(false);
        }
        else
        {
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
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("ディア");
            dhiaMoveFlag = true;
            ririMoveFlag = false;
            windowsMes.text = "ディアの行動をにゅうりょくしてください";
            StartCoroutine(DhiaEnterWait());
        }
    }

    void EnemyMove()
    {
        if (enemy.deathFlag)
        {
            windowsMes.text = "敵を倒した！";
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


    #region ボタン決定時処理
    public void AttackButton()
    {
        button = true;
        if(ririMoveFlag)
        {
            windowsMes.text = "リリーのこうげき！" + riri.power + "のダメージ!";
            enemy.hp -= riri.power;
            enemySlider.value *= (enemy.hp / enemy.maxhp);
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag)
        {
            windowsMes.text = "ディアのこうげき！" + dhia.power + "のダメージ!";
            enemy.hp -= dhia.power;
            enemySlider.value *= (enemy.hp / enemy.maxhp);
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
        if (button)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            ririMoveFlag = false;
            button = false;
            DhiaMove();
        }
    }
    IEnumerator DhiaEnterWait()
    {
        if (button)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            dhiaMoveFlag = false;
            button = false;
            EnemyMove();
        }
    }
    IEnumerator EnemyEnterWait()
    {
        if (button)
        {
            yield return new WaitForSeconds(1.5f);
            windowsMes.text = "リリーの行動をにゅうりょくしてください";
            button = false;
            enemyMoveFlag = false;
            RiriMove();
        }
    }
    #endregion
}
