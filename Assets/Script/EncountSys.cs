using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditorInternal;

public class EncountSys : MonoBehaviour
{
    //バトルコマンドのテキスト
    [SerializeField]
    TextMeshProUGUI windowsMes = null;

    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;
    bool button = false;

    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;
    [SerializeField]
    Enemy enemy = null;

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    void Init()
    {
        ririMoveFlag = false;
        dhiaMoveFlag = false;
        enemyMoveFlag = false;
        windowsMes.text = "リリーの行動をにゅうりょくしてください";
        RiriMove();
    }

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
        Debug.Log("リリー");
        ririMoveFlag = true;
        StartCoroutine(RiriEnterWait());
    }

    void DhiaMove()
    {
        Debug.Log("ディア");
        dhiaMoveFlag = true;
        ririMoveFlag = false;
        windowsMes.text = "ディアの行動をにゅうりょくしてください";
        StartCoroutine(DhiaEnterWait());
    }

    void EnemyMove()
    {
        Debug.Log("エネミー");
        enemyMoveFlag = true;
        windowsMes.text = "てきのこうげき！" + enemy.power + "のダメージ!";
        button = true;
        StartCoroutine(EnemyEnterWait());
    }
    #endregion


    #region ボタン決定時処理
    public void AttackButton()
    {
        button = true;
        if(ririMoveFlag)
        {
            windowsMes.text = "リリーのこうげき！" + riri.power + "のダメージ!";
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag)
        {
            windowsMes.text = "ディアのこうげき！" + riri.power + "のダメージ!";
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
