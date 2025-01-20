using System;
using UnityEngine;
using static Dhia;
using static TestEncount;
using static UnityEngine.EventSystems.EventTrigger;

public class Dhia : MonoBehaviour
{
    //技の管理用
    public enum DhiaAtkSkill1
    {
        HitSkill,
        KickSkill,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaAtkSkill2
    {
        HitSkill,
        KickSkill,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaAtkSkill3
    {
        HitSkill,
        KickSkill,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaDefSkill1
    {
        ProtectYou,
        DefensivePosture,
        Protect,
    }
    public enum DhiaDefSkill2
    {
        ProtectYou,
        DefensivePosture,
        Protect,
    }
    public enum DhiaDefSkill3
    {
        ProtectYou,
        DefensivePosture,
        Protect,
    }

    public enum AtkDefSlect
    {
        ATK,
        DEF
    }

    //技のenumの実体化
    public DhiaAtkSkill1 dhiaAtkSkill1;    
    public DhiaAtkSkill2 dhiaAtkSkill2;    
    public DhiaAtkSkill3 dhiaAtkSkill3;
    public DhiaDefSkill1 dhiaDefSkill1;    
    public DhiaDefSkill2 dhiaDefSkill2;    
    public DhiaDefSkill3 dhiaDefSkill3;

    //攻撃と防御enumの実体化
    public AtkDefSlect atkDefSlect;

    [SerializeField]
    public String[] atkSkillName = null;
    [SerializeField]
    public String[] defSkillName = null;

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

        if (encountSys != null)
        {
            enemyDamage = DamageCalculation(power, encountSys.enemyScript.def[0]);
        }

        
        switch (dhiaAtkSkill1)
        {
            case DhiaAtkSkill1.HitSkill:
                atkSkillName[0] = "殴る";
                break;
            case DhiaAtkSkill1.KickSkill:
                atkSkillName[0] = "蹴る";
                break;
            case DhiaAtkSkill1.CutSkil:
                atkSkillName[0] = "切る";
                break;
            case DhiaAtkSkill1.Destroy:
                atkSkillName[0] = "撃つ";
                break;
            case DhiaAtkSkill1.CutUp:
                atkSkillName[0] = "切り裂く";
                break;
            case DhiaAtkSkill1.FiringBlindly:
                atkSkillName[0] = "乱射";
                break;
        }

        switch (dhiaAtkSkill2)
        {
            case DhiaAtkSkill2.HitSkill:
                atkSkillName[1] = "殴る";
                break;
            case DhiaAtkSkill2.KickSkill:
                atkSkillName[1] = "蹴る";
                break;
            case DhiaAtkSkill2.CutSkil:
                atkSkillName[1] = "切る";
                break;
            case DhiaAtkSkill2.Destroy:
                atkSkillName[1] = "撃つ";
                break;
            case DhiaAtkSkill2.CutUp:
                atkSkillName[1] = "切り裂く";
                break;
            case DhiaAtkSkill2.FiringBlindly:
                atkSkillName[1] = "乱射";
                break;
        }

        switch (dhiaAtkSkill3)
        {
            case DhiaAtkSkill3.HitSkill:
                atkSkillName[2] = "殴る";
                break;
            case DhiaAtkSkill3.KickSkill:
                atkSkillName[2] = "蹴る";
                break;
            case DhiaAtkSkill3.CutSkil:
                atkSkillName[2] = "切る";
                break;
            case DhiaAtkSkill3.Destroy:
                atkSkillName[2] = "撃つ";
                break;
            case DhiaAtkSkill3.CutUp:
                atkSkillName[2] = "切り裂く";
                break;
            case DhiaAtkSkill3.FiringBlindly:
                atkSkillName[2] = "乱射";
                break;
        }

        switch (dhiaDefSkill1)
        {
            case DhiaDefSkill1.ProtectYou:
                defSkillName[0] = "お守りします！";
                break;
            case DhiaDefSkill1.DefensivePosture:
                defSkillName[0] = "防御態勢";
                break;
            case DhiaDefSkill1.Protect:
                defSkillName[0] = "守る";
                break;
        }

        switch (dhiaDefSkill2)
        {
            case DhiaDefSkill2.ProtectYou:
                defSkillName[1] = "お守りします！";
                break;
            case DhiaDefSkill2.DefensivePosture:
                defSkillName[1] = "防御態勢";
                break;
            case DhiaDefSkill2.Protect:
                defSkillName[1] = "守る";
                break;
        }

        switch (dhiaDefSkill3)
        {
            case DhiaDefSkill3.ProtectYou:
                defSkillName[2] = "お守りします！";
                break;
            case DhiaDefSkill3.DefensivePosture:
                defSkillName[2] = "防御態勢";
                break;
            case DhiaDefSkill3.Protect:
                defSkillName[2] = "守る";
                break;
        }

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
        switch (atkDefSlect)
        {
            //攻撃
            case AtkDefSlect.ATK:
                switch (dhiaAtkSkill1)
                {
                    case DhiaAtkSkill1.HitSkill:
                        HitSkill();
                        break;
                    case DhiaAtkSkill1.KickSkill:
                        KickSkill();
                        break;
                    case DhiaAtkSkill1.CutSkil:
                        CutSkil();
                        break;
                    case DhiaAtkSkill1.Destroy:
                        Destroy();
                        break;
                    case DhiaAtkSkill1.CutUp:
                        CutUp();
                        break;
                    case DhiaAtkSkill1.FiringBlindly:
                        FiringBlindly();
                        break;
                }
                break;

                //防御
            case AtkDefSlect.DEF:
                switch (dhiaDefSkill1)
                {
                    case DhiaDefSkill1.ProtectYou:
                        ProtectYou();
                        break;
                    case DhiaDefSkill1.DefensivePosture:
                        DefensivePosture();
                        break;
                    case DhiaDefSkill1.Protect:
                        Protect();
                        break;
                }
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
        switch (atkDefSlect)
        {
            //攻撃
            case AtkDefSlect.ATK:

                switch (dhiaAtkSkill2)
                {
                    case DhiaAtkSkill2.HitSkill:
                        HitSkill();
                        break;
                    case DhiaAtkSkill2.KickSkill:
                        KickSkill();
                        break;
                    case DhiaAtkSkill2.CutSkil:
                        CutSkil();
                        break;
                    case DhiaAtkSkill2.Destroy:
                        Destroy();
                        break;
                    case DhiaAtkSkill2.CutUp:
                        CutUp();
                        break;
                    case DhiaAtkSkill2.FiringBlindly:
                        FiringBlindly();
                        break;
                }

                break;

            case AtkDefSlect.DEF:
                switch (dhiaDefSkill2)
                {
                    case DhiaDefSkill2.ProtectYou:
                        ProtectYou();
                        break;
                    case DhiaDefSkill2.DefensivePosture:
                        DefensivePosture();
                        break;
                    case DhiaDefSkill2.Protect:
                        Protect();
                        break;
                }
                break;
        }

    }
    public void Skil3()
    {
        switch (atkDefSlect)
        {
            //攻撃
            case AtkDefSlect.ATK:

                switch (dhiaAtkSkill3)
                {
                    case DhiaAtkSkill3.HitSkill:
                        HitSkill();
                        break;
                    case DhiaAtkSkill3.KickSkill:
                        KickSkill();
                        break;
                    case DhiaAtkSkill3.CutSkil:
                        CutSkil();
                        break;
                    case DhiaAtkSkill3.Destroy:
                        Destroy();
                        break;
                    case DhiaAtkSkill3.CutUp:
                        CutUp();
                        break;
                    case DhiaAtkSkill3.FiringBlindly:
                        FiringBlindly();
                        break;
                }

                break;
            case AtkDefSlect.DEF:

                switch (dhiaDefSkill3)
                {
                    case DhiaDefSkill3.ProtectYou:
                        ProtectYou();
                        break;
                    case DhiaDefSkill3.DefensivePosture:
                        DefensivePosture();
                        break;
                    case DhiaDefSkill3.Protect:
                        Protect();
                        break;
                }
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
            Debug.Log("お守りします");
            button = true;
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
