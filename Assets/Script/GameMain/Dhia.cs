using System;
using UnityEngine;
using static TestEncount;
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

    [SerializeField]
    Rabbit[] rabbitScript = null;
    [SerializeField]
    Bird[] birdScript = null;

    [SerializeField]
    GameObject enemySelectWin = null;

    [SerializeField]
    GameObject commandButton = null;

    //アニメーション管理用
    [SerializeField]
    public Animator dhiaAnim = null;
    float timer = 0;
    bool timerFlag = false;



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

        if (floorNoSys != null)
        {
            if (floorNoSys.floorNo == 1)
            {
                dhiaStatus.HP = dhiaStatus.MAXHP;

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

        //パーツのステータスを反映する処理
        if (dhiaStatus.headPartsData != null)
        {
            power += dhiaStatus.headPartsData.ATK;
            def += dhiaStatus.headPartsData.DEF;
        }
        if (dhiaStatus.bodyPartsData != null)
        {
            power += dhiaStatus.bodyPartsData.ATK;
            def += dhiaStatus.bodyPartsData.DEF;
        }
        if (dhiaStatus.legPartsData != null)
        {
            power += dhiaStatus.legPartsData.ATK;
            def += dhiaStatus.legPartsData.DEF;
        }
        if (dhiaStatus.righthandPartsData != null)
        {
            power += dhiaStatus.righthandPartsData.ATK;
            def += dhiaStatus.righthandPartsData.DEF;
        }
        if (dhiaStatus.lefthandPartsData != null)
        {
            power += dhiaStatus.lefthandPartsData.ATK;
            def += dhiaStatus.lefthandPartsData.DEF;
        }

        enemyDamage = DamageCalculation(power, encountSys.enemyScript.def[0]);
    }

    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
            if (this.transform.localScale.x >= 0)
            {
                this.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
            }
        }
        dhiaStatus.HP = hp;
        dhiaStatus.MP = mp;

        if (timerFlag)
        {
            timer += Time.deltaTime;

            if (timer >= 3.5f)
            {
                dhiaAnim.SetBool("D_Attack", false);

                timer = 0;
                timerFlag = false;
            }
        }
    }

    public bool button = false;
    public void Skil1()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    public void Skill1Move(int enemyNumber)
    {
        timerFlag = true;
        dhiaAnim.SetBool("D_Attack", true);
        enemySelectWin.SetActive(false);
        encountSys.mainTurn = MainTurn.DHIAANIM;

        //敵1を選ばれた時
        if (enemyNumber == 1)
        {
            //ウサギの時
            if (encountSys.typeRnd[0] == 0)
            {
                Debug.Log("うさぎが選ばれました");
                rabbitScript[0].rabbitAnim.SetBool("Damage2", true);
                rabbitScript[0].timerFlag = true;
            }
            //鳥の時
            if (encountSys.typeRnd[0] == 1)
            {
                Debug.Log("とりが選ばれました");
                birdScript[0].birdAnim.SetBool("Eb_Damage2", true);
                birdScript[0].timerFlag = true;
            }
            if (powerUpFlag)
            {
                encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage * 1.5f + "のダメージ!";
                encountSys.enemyScript.hp[0] -= (enemyDamage * 1.5f);
                powerUpFlag = false;
            }
            else
            {
                Debug.Log("コマンド1ディア通常攻撃");
                encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage + "のダメージ!";
                encountSys.enemyScript.hp[0] -= enemyDamage;
            }
        }
        //敵2を選択された時
        if (enemyNumber == 2)
        {
            //ウサギの時
            if (encountSys.typeRnd[1] == 3)
            {
                rabbitScript[1].rabbitAnim.SetBool("Damage2", true);
                rabbitScript[1].timerFlag = true;
            }
            //鳥の時
            if (encountSys.typeRnd[1] == 4)
            {
                birdScript[1].birdAnim.SetBool("Eb_Damage2", true);
                birdScript[1].timerFlag = true;
            }
            if (powerUpFlag)
            {
                encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage * 1.5f + "のダメージ!";
                encountSys.enemyScript.hp[1] -= (enemyDamage * 1.5f);
                powerUpFlag = false;
            }
            else
            {
                Debug.Log("コマンド1ディア通常攻撃");
                encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage + "のダメージ!";
                encountSys.enemyScript.hp[1] -= enemyDamage;
            }
        }
    }


    public void Skil2()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            encountSys.windowsMes.text = "ディアは身を守っている。";
            defenseFlag = true;
            button = true;
        }
    }
    public void Skil3()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            Debug.Log("コマンド3ディア");
            encountSys.windowsMes.text = "ディアはリリーを守っている。";
            ririDefenseFlag = true;
            button = true;
        }
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
