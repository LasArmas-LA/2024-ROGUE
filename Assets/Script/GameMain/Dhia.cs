using System;
using UnityEngine;
using static Dhia;
using static TestEncount;
using static UnityEngine.EventSystems.EventTrigger;

public class Dhia : MonoBehaviour
{
    //技の管理用
    public enum DhiaSkill1
    {
        HitSkill,
        KickSkill,
        ProtectYou,
        DefensivePosture,
        Protect,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaSkill2
    {
        HitSkill,
        KickSkill,
        ProtectYou,
        DefensivePosture,
        Protect,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaSkill3
    {
        HitSkill,
        KickSkill,
        ProtectYou,
        DefensivePosture,
        Protect,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }

    //技のenumの実体化
    public DhiaSkill1 dhiaSkill1;    
    public DhiaSkill2 dhiaSkill2;    
    public DhiaSkill3 dhiaSkill3;    
    
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
        //エラー回避
        try
        {
            floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        }
        catch { }

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
        switch (dhiaSkill1)
        {
            case DhiaSkill1.HitSkill:
                HitSkill();
                break;
            case DhiaSkill1.KickSkill:
                KickSkill();
                break;
            case DhiaSkill1.ProtectYou:
                ProtectYou();
                break;
            case DhiaSkill1.DefensivePosture:
                DefensivePosture();
                break;
            case DhiaSkill1.Protect:
                Protect();
                break;
            case DhiaSkill1.CutSkil:
                CutSkil();
                break;
            case DhiaSkill1.Destroy:
                Destroy();
                break;
            case DhiaSkill1.CutUp:
                CutUp();
                break;
            case DhiaSkill1.FiringBlindly:
                FiringBlindly();
                break;
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

        switch (dhiaSkill2)
        {
            case DhiaSkill2.HitSkill:
                HitSkill();
                break;
            case DhiaSkill2.KickSkill:
                KickSkill();
                break;
            case DhiaSkill2.ProtectYou:
                ProtectYou();
                break;
            case DhiaSkill2.DefensivePosture:
                DefensivePosture();
                break;
            case DhiaSkill2.Protect:
                Protect();
                break;
            case DhiaSkill2.CutSkil:
                CutSkil();
                break;
            case DhiaSkill2.Destroy:
                Destroy();
                break;
            case DhiaSkill2.CutUp:
                CutUp();
                break;
            case DhiaSkill2.FiringBlindly:
                FiringBlindly();
                break;
        }
    }
    public void Skil3()
    {
        switch (dhiaSkill3)
        {
            case DhiaSkill3.HitSkill:
                HitSkill();
                break;
            case DhiaSkill3.KickSkill:
                KickSkill();
                break;
            case DhiaSkill3.ProtectYou:
                ProtectYou();
                break;
            case DhiaSkill3.DefensivePosture:
                DefensivePosture();
                break;
            case DhiaSkill3.Protect:
                Protect();
                break;
            case DhiaSkill3.CutSkil:
                CutSkil();
                break;
            case DhiaSkill3.Destroy:
                Destroy();
                break;
            case DhiaSkill3.CutUp:
                CutUp();
                break;
            case DhiaSkill3.FiringBlindly:
                FiringBlindly();
                break;
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


    //スキル関数


    //殴る
    void HitSkill()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    //蹴る
    void KickSkill()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    //お守りします！
    void ProtectYou()
    {
        if(!button)
        {

        }
    }

    //防御体制
    void DefensivePosture()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            encountSys.windowsMes.text = "ディアは身を守っている。";
            defenseFlag = true;
            button = true;
        }
    }

    //守る
    void Protect()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            encountSys.windowsMes.text = "ディアはリリーを守っている。";
            ririDefenseFlag = true;
            button = true;
        }
    }

    //切る
    void CutSkil()
    {

    }

    //撃つ
    void Destroy()
    {
        
    }

    //切り裂く
    void CutUp()
    {

    }

    //乱射
    void FiringBlindly()
    {

    }
}
