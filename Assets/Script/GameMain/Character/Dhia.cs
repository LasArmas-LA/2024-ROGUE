using System;
using System.Collections;
using TMPro;
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
        DefensiveAttack,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaAtkSkill2
    {
        HitSkill,
        KickSkill,
        DefensiveAttack,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly
    }
    public enum DhiaAtkSkill3
    {
        HitSkill,
        KickSkill,
        DefensiveAttack,
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
    public int attack = 0;
    //[NonSerialized]
    public int def = 0;
    [NonSerialized]
    public int power = 0;

    //防御の補正値用
    [NonSerialized]
    public int defCorrectionValue = 100;

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

    public Rabbit[] rabbitScript = null;
    public Bird[] birdScript = null;

    public GameObject enemySelectWin = null;

    [SerializeField]
    GameObject commandButton = null;

    //アニメーション管理用
    [SerializeField]
    public Animator dhiaAnim = null;
    float timer = 0;
    bool timerFlag = false;

    void Awake()
    {
        //ステータスの初期化
        //InitStatus();
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        //検索の初期化
        //InitFind();
        //スキルの名前の初期化
        InitSkilName();
    }
    public void InitFind()
    {
        //エラー回避
        try
        {
            floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        }
        catch { }
    }
    public void InitStatus()
    {
        //HPと攻撃力と防御力の初期化処理
        dhiaStatus.MAXHP = 150;

        //武器のHP補正値分下げる事で受けてる受けているダメージは保存する事ができる
        dhiaStatus.HP -= floorNoSys.dhiaHp;
        dhiaStatus.ATK = 100;
        dhiaStatus.DEF = 10;


        //ステータスのパラメーターを取り込む処理
        maxhp = dhiaStatus.MAXHP;
        maxmp = dhiaStatus.MAXMP;
        attack = dhiaStatus.ATK;
        def = dhiaStatus.DEF;

        //スケールの初期化処理
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        //デバッグ用
        hp = maxhp;
        mp = maxmp;
        deathFlag = false;


        if (floorNoSys != null)
        {
            if (floorNoSys.floorCo == 1)
            {
                dhiaStatus.HP = dhiaStatus.MAXHP;

                hp = maxhp;
                mp = maxmp;
                deathFlag = false;
            }
            else
            {
                //hp = dhiaStatus.HP;
                mp = dhiaStatus.MP;
            }
        }
        //ステータス補正値の初期化
        floorNoSys.dhiaHp = 0;
        floorNoSys.dhiaAtk = 0;
        floorNoSys.dhiaDef = 0;

        //セット効果処理用
        int rabbitSetCou = 0;
        int birdSetCou = 0;

        //パーツのメインステータスを格納処理
        //頭パーツの処理
        if (dhiaStatus.headPartsData != null)
        {
            floorNoSys.dhiaHp += dhiaStatus.headPartsData.HP;

            if (dhiaStatus.headPartsData.name == "Bird")
            {
                birdSetCou++;
            }
            if (dhiaStatus.headPartsData.name == "Rabbit")
            {
                rabbitSetCou++;
            }
        }
        //体パーツの処理
        if (dhiaStatus.bodyPartsData != null)
        {
            floorNoSys.dhiaHp += dhiaStatus.bodyPartsData.HP;

            if (dhiaStatus.bodyPartsData.name == "Bird")
            {
                birdSetCou++;
            }
            if (dhiaStatus.bodyPartsData.name == "Rabbit")
            {
                rabbitSetCou++;
            }
        }
        //足パーツの処理
        if (dhiaStatus.legPartsData != null)
        {
            floorNoSys.dhiaAtk += dhiaStatus.legPartsData.ATK;

            if (dhiaStatus.legPartsData.name == "Bird")
            {
                birdSetCou++;
            }
            if (dhiaStatus.legPartsData.name == "Rabbit")
            {
                rabbitSetCou++;
            }
        }
        //右手パーツの処理
        if (dhiaStatus.righthandPartsData != null)
        {
            floorNoSys.dhiaDef += dhiaStatus.righthandPartsData.DEF;

            if (dhiaStatus.righthandPartsData.name == "Bird")
            {
                birdSetCou++;
            }
            if (dhiaStatus.righthandPartsData.name == "Rabbit")
            {
                rabbitSetCou++;
            }
        }
        //左手パーツの処理
        if (dhiaStatus.lefthandPartsData != null)
        {
            floorNoSys.dhiaAtk += dhiaStatus.lefthandPartsData.ATK;

            if (dhiaStatus.lefthandPartsData.name == "Bird")
            {
                birdSetCou++;
            }
            if (dhiaStatus.lefthandPartsData.name == "Rabbit")
            {
                rabbitSetCou++;
            }
        }


        //うさぎのセット効果
        if (rabbitSetCou >= 2)
        {
            //4セット効果
            if (rabbitSetCou >= 4)
            {
                //リリーのHPを10%アップ
                riri.maxhp = (riri.maxhp * 1.1f);

                //自身のHPを10%アップ
                maxhp = (maxhp * 1.1f);
                Debug.Log("うさぎ4セット");
            }
            //2セット効果
            else
            {
                //自身のHPを10%アップ
                maxhp = (maxhp * 1.1f);
                Debug.Log("うさぎ2セット");
            }
        }
        //フクロウのセット効果
        if (birdSetCou >= 2)
        {
            //4セット効果
            if (birdSetCou >= 4)
            {
                //HPを10%減らす
                maxhp = (maxhp * 0.9f);
                hp = (hp * 0.9f);
                //キャスト変換して格納
                attack = (int)(attack * 1.2f);
                Debug.Log("フクロウ4セット");
            }
            //2セット効果
            else
            {
                //キャスト変換して格納
                attack = (int)(attack * 1.1f);
                Debug.Log("フクロウ2セット");
            }
        }


        //実際にパーツのステータスを反映
        dhiaStatus.MAXHP += floorNoSys.dhiaHp;

        dhiaStatus.ATK += floorNoSys.dhiaAtk;
        dhiaStatus.DEF = floorNoSys.dhiaDef;
}
    void InitSkilName()
    {
        //攻撃スキル1の名前を変更
        switch (dhiaAtkSkill1)
        {
            case DhiaAtkSkill1.HitSkill:
                atkSkillName[0] = "殴る";
                break;
            case DhiaAtkSkill1.KickSkill:
                atkSkillName[0] = "蹴る";
                break;
            case DhiaAtkSkill1.DefensiveAttack:
                atkSkillName[0] = "シールドバッシュ";
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

        //攻撃スキル2の名前を変更
        switch (dhiaAtkSkill2)
        {
            case DhiaAtkSkill2.HitSkill:
                atkSkillName[1] = "殴る";
                break;
            case DhiaAtkSkill2.KickSkill:
                atkSkillName[1] = "蹴る";
                break;
            case DhiaAtkSkill2.DefensiveAttack:
                atkSkillName[1] = "シールドバッシュ";
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

        //攻撃スキル3の名前を変更
        switch (dhiaAtkSkill3)
        {
            case DhiaAtkSkill3.HitSkill:
                atkSkillName[2] = "殴る";
                break;
            case DhiaAtkSkill3.KickSkill:
                atkSkillName[2] = "蹴る";
                break;
            case DhiaAtkSkill3.DefensiveAttack:
                atkSkillName[2] = "シールドバッシュ";
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

        //防御スキル1の名前を変更
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

        //防御スキル2の名前を変更
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

        //防御スキル3の名前を変更
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
        //HPの確認処理
        HpCheck();
        //アニメーションの停止処理
        AnimDelete();
    }

    void HpCheck()
    {
        if (hp <= 0)
        {
            deathFlag = true;
            if (this.transform.localScale.x >= 0)
            {
                this.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
            }
        }
        dhiaStatus.HP = hp;
        dhiaStatus.MP = mp;
    }

    void AnimDelete()
    {
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

    //多重押し防止
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
                    case DhiaAtkSkill1.DefensiveAttack:
                        DefensiveAttack();
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

    //ダメージテキスト表示用
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;

    public void Skill1Move(int enemyNumber)
    {
        //ダメージの計算
        enemyDamage = DamageCalculation(attack, encountSys.enemyScript.def[enemyNumber -1], power);

        timerFlag = true;
        dhiaAnim.SetBool("D_Attack", true);
        enemySelectWin.SetActive(false);
        encountSys.mainTurn = MainTurn.DHIAANIM;

        //テキストの表示処理
        damageTextObj[enemyNumber -1].SetActive(true);

        if (encountSys.enemyScript.hp[0] <= 0)
        {
            enemyNumber = 2;
        }
        if (encountSys.enemyScript.hp[1] <= 0)
        {
            enemyNumber = 1;
        }

        //アニメーション用
        //ウサギの時
        if (encountSys.typeRnd[enemyNumber - 1] == 0)
        {
            rabbitScript[enemyNumber - 1].rabbitAnim.SetBool("Damage2", true);
            rabbitScript[enemyNumber - 1].timerFlag = true;
        }
        //鳥の時
        if (encountSys.typeRnd[enemyNumber - 1] == 1)
        {
            birdScript[enemyNumber - 1].birdAnim.SetBool("Eb_Damage2", true);
            birdScript[enemyNumber - 1].timerFlag = true;
        }

        //攻撃の処理
        if (powerUpFlag)
        {
            encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage * 1.5f + "のダメージ!";
            encountSys.enemyScript.hp[enemyNumber - 1] -= (enemyDamage * 1.5f);

            damageText[enemyNumber - 1].text = (enemyDamage * 1.5f).ToString();

        powerUpFlag = false;
        }
        else
        {
            encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage + "のダメージ!";
            encountSys.enemyScript.hp[enemyNumber - 1] -= enemyDamage;

            damageText[enemyNumber - 1].text = enemyDamage.ToString();
        }

        StartCoroutine("DamageInit");
    }

    IEnumerator DamageInit()
    {
        yield return new WaitForSeconds(0.5f);

        damageTextObj[0].SetActive(false);
        damageTextObj[1].SetActive(false);

        damageText[0].text = "0";
        damageText[1].text = "0";
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
                    case DhiaAtkSkill2.DefensiveAttack:
                        DefensiveAttack();
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
                    case DhiaAtkSkill3.DefensiveAttack:
                        DefensiveAttack();
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

    int DamageCalculation(int attack, int defense, int power) //攻撃力、守備力、威力
    {
        //シード値の変更
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //素のダメージ計算
        int damage = ((attack + power) / 2) - (defense / 4);

        //ダメージ振幅の計算
        int width = damage / 16 + 1;

        //ダメージ振幅値を加味した計算
        damage = UnityEngine.Random.Range(damage - width, damage + width);

        //呼び出し側にダメージ数を返す
        return damage;
    }


    //スキル関数
    [Space (10)]
    [Header("技の威力")]
    [SerializeField]
    [CustomLabel("殴りの威力")]
    int hitPower = 0; 
    //殴る
    void HitSkill()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = hitPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("蹴りの威力")]
    int kickPower = 0;
    //蹴る
    void KickSkill()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = kickPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("シールドバッシュの威力")]
    int defatkPower = 0;
    //シールドバッシュ
    void DefensiveAttack()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = defatkPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(5)]

    [CustomLabel("お守りします！の防御補正(%)")]
    public int protectDef = 0;
    [CustomLabel("お守りします！の効果継続ターン数")]
    public int protectTurnInitial = 0;
    [NonSerialized]
    public int protectTurn = 0;
    //お守りします！の管理フラグ
    public bool protectFlag = false;
    //お守りします！
    void ProtectYou()
    {
        if(!button)
        {
            //ターン数の初期化
            protectTurn = protectTurnInitial;
            //フラグをtrueに
            protectFlag = true;
            //防御補正値を加算
            defCorrectionValue += protectDef;

            //多重押し防止
            button = true;
        }
    }

    [Space(5)]
    [CustomLabel("防御体制の防御補正(%)")]
    public int postureDef = 0;
    [CustomLabel("防御体制の効果継続ターン数")]
    public int postureTurnInitial = 0;
    //[NonSerialized]
    public int postureTurn = 0;
    //防御体制の管理フラグ
    public bool postureFlag = false;

    //防御体制
    void DefensivePosture()
    {
        if (!button)
        {
            //ターン数の初期化
            postureTurn = postureTurnInitial;
            //防御補正値を加算
            defCorrectionValue += postureDef;
            //防御アニメーションの再生
            dhiaAnim.SetBool("D_Shield", true);
            //防御体制フラグをtrueに
            postureFlag = true;
            //多重押し防止
            button = true;
        }
    }

    [Space(5)]
    //防御補正値の初期値
    [NonSerialized]
    public int ririProtectDef = 0;
    [CustomLabel("守るの効果継続ターン数")]
    public int ririProtectTurnInitial = 0;
    [NonSerialized]
    public int ririProtectTurn = 0;
    //守るの管理フラグ
    public bool ririProtectFlag = false;

    //守る
    void Protect()
    {
        if (!button)
        {
            //ターン数の初期化
            ririProtectTurn = ririProtectTurnInitial;
            //防御補正値の加算
            defCorrectionValue += ririProtectDef;
            //防御アニメーションの再生
            dhiaAnim.SetBool("D_Shield", true);
            //守るのフラグをtrueに
            ririDefenseFlag = true;
            //多重押し防止
            button = true;
        }
    }

    [Space(5)]
    [SerializeField]
    [CustomLabel("切るの威力")]
    int cutPower = 0;
    //切る
    void CutSkil()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = cutPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("撃つの威力")]
    int destroyPower = 0;
    //撃つ
    void Destroy()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = destroyPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("切り裂くの威力")]
    int cutUpPower = 0;
    //切り裂く
    void CutUp()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = cutUpPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("乱射の威力")]
    int blindlyPower = 0;
    //乱射
    void FiringBlindly()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = blindlyPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }
}
