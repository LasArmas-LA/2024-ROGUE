using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Riri : MonoBehaviour
{
    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    FloorNoSys floorNoSys = null;

    [NonSerialized]
    public float maxhp = 0;
    [NonSerialized]
    public float maxmp = 0;

    public float hp = 0;
    [NonSerialized]
    public float mp = 0;
    [NonSerialized]
    public int power = 0;
    [NonSerialized]
    public int def = 0;

    [NonSerialized]
    public bool deathFlag = false;


    [Header("クラス参照")]
    [SerializeField]
    Dhia dhia = null;

    [Space(10)]

    //対象選択時のフラグ
    bool ririSelectFlag = false;
    bool dhiaSelectFlag = false;

    [SerializeField]
    GameObject recoveryWin = null;
    [SerializeField]
    GameObject commandWin = null;

    [SerializeField]
    TestEncount encountSys = null;

    [SerializeField]
    GameObject ririMain = null;

    public bool button = false;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        maxhp = ririStatus.MAXHP;
        maxmp = ririStatus.MAXMP;
        power = ririStatus.DEFATK;
        def = ririStatus.DEFDEF;
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        if (floorNoSys != null)
        {
            if (floorNoSys.floorNo == 0)
            {
                hp = maxhp;
                mp = maxmp;
                deathFlag = false;
            }
            else
            {
                hp = ririStatus.HP;
                mp = ririStatus.MP;
            }
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            deathFlag = true;
            ririMain.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        ririStatus.HP = hp;
        ririStatus.MP = mp;
    }

    public void Skil1()
    {
        if (!button)
        {
            recoveryWin.SetActive(true);
            commandWin.SetActive(false);
        }
    }
    public void Skil2()
    {
        if (maxhp > hp + 20 && dhia.maxhp > dhia.hp + 20)
        {
            hp += 20;
            dhia.hp += 20;
            encountSys.windowsMes.text = "リリーはオールヒールを唱えた！\nリリーとディアのHPを20ずつ回復した!";
        }
        else
        {
            if (maxhp < hp + 20)
            {
                hp = maxhp;
            }
            if (dhia.maxhp < dhia.hp + 20)
            {
                dhia.hp = dhia.maxhp;
            }
            encountSys.windowsMes.text = "リリーはオールヒールを唱えた！\nリリーのHPを" + (maxhp - hp) + "ディアのHPを" + (dhia.maxhp - dhia.hp) + "回復した!";
        }

    }
    public void Skil3()
    {
        Debug.Log("コマンド3リリー");
        encountSys.windowsMes.text = "リリーはバイキルトを唱えた！\nディアの攻撃力が上昇した!";
        dhia.powerUpFlag = true;
    }

    //リリーのヒール使用時にキャラクターを選択する関数。OnClickで呼ばれる
    public void RiriSlect()
    {
        if (!button)
        {
            button = true;
            ririSelectFlag = true;
            RecoveryWin();
        }
    }
    //リリーのヒール使用時にキャラクターを選択する関数。OnClickで呼ばれる
    public void DhiaSlect()
    {
        if (!button)
        {
            button = true;
            dhiaSelectFlag = true;
            RecoveryWin();
        }
    }

    public void RecoveryWin()
    {
        if (ririSelectFlag)
        {
            if (maxhp < hp + 50)
            {
                Debug.Log("コマンド1リリーHPマックス回復");
                encountSys.windowsMes.text = "リリーはヒールを唱えた！\n" + "リリー" + "のHPを" + (maxhp - hp) + "回復した!";
                hp = maxhp;
            }
            else
            {
                Debug.Log("コマンド1リリーHP差分回復");
                encountSys.windowsMes.text = "リリーはヒールを唱えた！\n" + "リリー" + "のHPを50回復した!";
                hp += 50;
            }
            ririSelectFlag = false;
            
        }
        if (dhiaSelectFlag)
        {
            if (dhia.maxhp < dhia.hp + 50)
            {
                Debug.Log("コマンド1リリーHPマックス回復");
                encountSys.windowsMes.text = "リリーはヒールを唱えた！\n" + "ディア" + "のHPを" + (dhia.maxhp - dhia.hp) + "回復した!";
                dhia.hp = dhia.maxhp;
            }
            else
            {
                Debug.Log("コマンド1リリーHP差分回復");
                encountSys.windowsMes.text = "リリーはヒールを唱えた！\n" + "ディア" + "のHPを50回復した!";
                dhia.hp += 50;
            }
            dhiaSelectFlag = false;
        }
        recoveryWin.SetActive(false);
        commandWin.SetActive(true);
    }
}
