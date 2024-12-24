using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Dhia : MonoBehaviour
{
    //ディアのステータス
    [SerializeField]
    Status dhiaStatus = null;
    //階層データ管理システム
    [SerializeField]
    FloorNoSys floorNoSys = null;

    [NonSerialized]
    public float maxhp = 0;
    [NonSerialized]
    public float maxmp = 0;

    [NonSerialized]
    public float hp = 0;
    [NonSerialized]
    public float mp = 0;
    [NonSerialized]
    public int power = 0;
    [NonSerialized]
    public int def = 0;

    float enemyDamage = 0;

    [NonSerialized]
    public bool deathFlag = false;

    [NonSerialized]
    public bool powerUpFlag = false;

    [NonSerialized]
    public bool ririDefenseFlag = false;

    [NonSerialized]
    public bool defenseFlag = false;

    [Header("クラス参照")]
    [SerializeField]
    Riri riri = null;

    [SerializeField]
    TestEncount encountSys = null;

    [SerializeField]
    GameObject dhiaMain = null;
    

    void Awake()
    {
        Init();
    }

    void Init()
    {
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        maxhp = dhiaStatus.MAXHP;
        maxmp = dhiaStatus.MAXMP;
        power = dhiaStatus.ATK;
        def = dhiaStatus.DEF;

        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        enemyDamage = DamageCalculation(power, encountSys.rndEnemy.def);

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
                hp = dhiaStatus.HP;
                mp = dhiaStatus.MP;
            }
        }
    }

    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
            dhiaMain.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        dhiaStatus.HP = hp;
        dhiaStatus.MP = mp;
    }

    public void Skil1()
    {
        if (powerUpFlag)
        {
            Debug.Log("コマンド1ディアパワーアップ攻撃");

            encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage * 1.5f + "のダメージ!";
            encountSys.rndEnemy.hp -= (enemyDamage * 1.5f);
            powerUpFlag = false;
        }
        else
        {
            Debug.Log("コマンド1ディア通常攻撃");
            encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage + "のダメージ!";
            encountSys.rndEnemy.hp -= enemyDamage;
        }
    }
    public void Skil2()
    {
        encountSys.windowsMes.text = "ディアは身を守っている。";
        defenseFlag = true;
    }
    public void Skil3()
    {
        Debug.Log("コマンド3ディア");
        encountSys.windowsMes.text = "ディアはリリーを守っている。";
        ririDefenseFlag = true;
    }

    int DamageCalculation(int attack, int defense)
    {
        //シード値の変更
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //素のダメージ計算
        int damage = (attack / 2) - (defense / 4);

        //ダメージ振幅の計算
        int width = damage / 16 + 1;

        //ダメージ振幅値を加味した計算
        damage = UnityEngine.Random.Range(damage - width, damage + width);
        //呼び出し側にダメージ数を返す
        return damage;
    }

}
