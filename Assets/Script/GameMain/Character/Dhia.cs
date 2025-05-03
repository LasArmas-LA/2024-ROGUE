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
        FiringBlindly,
        FocusedShot,
        ChoppingIntoChunks,
        IaiCutting,
        ArmourCrushing,
        ReverseClipping,
        RevolvingSlash,
        MagicArrows,
        MagicMissile,
        LightningArrow,
        LightningBolt
    }
    public enum DhiaAtkSkill2
    {
        HitSkill,
        KickSkill,
        DefensiveAttack,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly,
        FocusedShot,
        ChoppingIntoChunks,
        IaiCutting,
        ArmourCrushing,
        ReverseClipping,
        RevolvingSlash,
        MagicArrows,
        MagicMissile,
        LightningArrow,
        LightningBolt
    }
    public enum DhiaAtkSkill3
    {
        HitSkill,
        KickSkill,
        DefensiveAttack,
        CutSkil,
        Destroy,
        CutUp,
        FiringBlindly,
        FocusedShot,
        ChoppingIntoChunks,
        IaiCutting,
        ArmourCrushing,
        ReverseClipping,
        RevolvingSlash,
        MagicArrows,
        MagicMissile,
        LightningArrow,
        LightningBolt
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

    //パラメーター
    [Space(10),Header("パラメーター")]
    [NonSerialized]
    public float maxhp = 0;
    [NonSerialized]
    public float maxmp = 0;

    public float hp = 0;
    [NonSerialized]
    public float mp = 0;
    //攻撃力
    //[NonSerialized]
    public int attack = 0;
    [NonSerialized]
    public int attackDefault = 0;
    //攻撃の補正値用
    public int atkCorrectionValue = 100;

    //防御
    //[NonSerialized]
    public int def = 0;
    //防御の初期値
    public int defDefault = 0;

    //威力
    public int power = 0;

    //防御の補正値用
    //[NonSerialized]
    public int defCorrectionValue = 100;

    //命中率
    public int hitRate = 100;


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
    Riri ririScript = null;

    [SerializeField]
    TestEncount encountSys = null;

    [SerializeField]
    GameObject dhiaMain = null;

    public Rabbit[] rabbitScript = null;
    public Bird[] birdScript = null;

    //敵を選択するウィンドウ
    public GameObject enemySelectWin = null;

    public GameObject commandButton = null;

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
                ririScript.maxhp = (ririScript.maxhp * 1.1f);

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

        defDefault = dhiaStatus.DEF;
        attackDefault = dhiaStatus.ATK;
    }

    void InitSkilName()
    {
        //攻撃名の適用
        for (int i = 0; i < 3; i++)
        {
            atkSkillName[i] = dhiaSkillList.atkSkillList[floorNoSys.skillNoDhiaAtk[i]].name;
        }
        //防御名の適用
        for (int i = 0; i < 3; i++)
        {
            defSkillName[i] = dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[i]].name;
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


    enum eSkill
    {
        Skill1Dhia,
        Skill2Dhia,
        Skill3Dhia,
    }
    public void AtkDef()
    {
        switch(atkDefSlect)
        {
            case AtkDefSlect.ATK:
                Skill(AtkDefSlect.ATK);
                break;
            case AtkDefSlect.DEF:
                Skill(AtkDefSlect.DEF);
                break;

        }
    }

    [SerializeField]
    DhiaSkillList dhiaSkillList;

    public void Skill(AtkDefSlect atkDef)
    {
        if(atkDef == AtkDefSlect.ATK)
        {
        }
        if(atkDef == AtkDefSlect.DEF)
        {

        }
    }

    //多重押し防止
    public bool button = false;

    //ダメージテキスト表示用
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;

    //攻撃回数
    [SerializeField]
    int attackFrequency = 1;
    //ランダム攻撃のフラグ
    public bool rndAtk = false;

    public void Skill1Move(int enemyNumber)
    {
        //攻撃の時
        if(atkDefSlect == AtkDefSlect.ATK)
        {
            SkillAtk(enemyNumber);
        }
        //防御の時
        if(atkDefSlect == AtkDefSlect.DEF)
        {
            SkillDef(enemyNumber);
        }
    }

    void SkillAtk(int enemyNumber)
    {
        for (int i = 1; i <= attackFrequency; i++)
        {
            UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
            
            //ランダム攻撃の時
            if (rndAtk)
            {
                int rndAttack = UnityEngine.Random.Range(1, 2);
                enemyNumber = rndAttack;
            }

            //0から100の乱数を取得
            int rnd = UnityEngine.Random.Range(0, 100);

            Debug.Log(rnd);

            //技命中
            if (rnd <= hitRate)
            {
                //ダメージの計算
                enemyDamage = DamageCalculation(attack, encountSys.enemyScript.def[enemyNumber - 1], power);

                //逆刃斬り攻撃の時
                if (reverseClippingFlag)
                {
                    //HPを減らす
                    hp -= enemyDamage / reverseClippingDamageRate;
                    //フラグをオフ
                    reverseClippingFlag = false;
                }
                //ライトニングアロー攻撃の時
                if (lightningArrowFlag)
                {
                    int lightningArrowRnd = UnityEngine.Random.Range(0, 100);
                    if (lightningArrowRate <= lightningArrowRnd)
                    {
                        encountSys.enemyStopFlag[enemyNumber] = true;
                    }

                    lightningArrowFlag = false;

                }
                //ライトニングボルト攻撃の時
                if (lightningBoltFlag)
                {
                    int lightningBoltRnd = UnityEngine.Random.Range(0, 100);
                    if (lightningBoltRate <= lightningBoltRnd)
                    {
                        encountSys.enemyStopFlag[enemyNumber] = true;
                    }

                    lightningBoltFlag = false;
                }

                timerFlag = true;
                dhiaAnim.SetBool("D_Attack", true);
                enemySelectWin.SetActive(false);

                //テキストの表示処理
                damageTextObj[enemyNumber - 1].SetActive(true);

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
                encountSys.windowsMes.text = "ディアのこうげき！" + enemyDamage + "のダメージ!";
                encountSys.enemyScript.hp[enemyNumber - 1] -= enemyDamage;

                damageText[enemyNumber - 1].text = enemyDamage.ToString();

                Debug.Log(enemyDamage);


                StartCoroutine("DamageInit");
            }
            //技外し
            else
            {
                Debug.Log("残念でした笑");

                enemySelectWin.SetActive(false);
            }
        }
        encountSys.mainTurn = MainTurn.DHIAANIM;

        attackFrequency = 1;
    }

    void SkillDef(int enemyNumber)
    {
        //補正値無し
        if(dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._NO)
        {
            return;
        }

        //攻撃
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._ATK)
        {
            atkCorrectionValue += dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionValue;
        }

        //防御
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
        {
            defCorrectionValue += dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionValue;
        }

        //HP
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._HP)
        {

        }

        //命中率
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._HITRATE)
        {

        }


        encountSys.mainTurn = MainTurn.DHIAANIM;
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
                    case DhiaAtkSkill2.FocusedShot:
                        FocusedShot();
                        break;
                    case DhiaAtkSkill2.ChoppingIntoChunks:
                        ChoppingIntoChunks();
                        break;
                    case DhiaAtkSkill2.IaiCutting:
                        IaiCutting();
                        break;
                    case DhiaAtkSkill2.ArmourCrushing:
                        ArmourCrushing();
                        break;
                    case DhiaAtkSkill2.ReverseClipping:
                        ReverseClipping();
                        break;
                    case DhiaAtkSkill2.RevolvingSlash:
                        RevolvingSlash();
                        break;
                    case DhiaAtkSkill2.MagicArrows:
                        MagicArrows();
                        break;
                    case DhiaAtkSkill2.MagicMissile:
                        MagicMissile();
                        break;
                    case DhiaAtkSkill2.LightningArrow:
                        LightningArrow();
                        break;
                    case DhiaAtkSkill2.LightningBolt:
                        LightningBolt();
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
                    case DhiaAtkSkill3.FocusedShot:
                        FocusedShot();
                        break;
                    case DhiaAtkSkill3.ChoppingIntoChunks:
                        ChoppingIntoChunks();
                        break;
                    case DhiaAtkSkill3.IaiCutting:
                        IaiCutting();
                        break;
                    case DhiaAtkSkill3.ArmourCrushing:
                        ArmourCrushing();
                        break;
                    case DhiaAtkSkill3.ReverseClipping:
                        ReverseClipping();
                        break;
                    case DhiaAtkSkill3.RevolvingSlash:
                        RevolvingSlash();
                        break;
                    case DhiaAtkSkill3.MagicArrows:
                        MagicArrows();
                        break;
                    case DhiaAtkSkill3.MagicMissile:
                        MagicMissile();
                        break;
                    case DhiaAtkSkill3.LightningArrow:
                        LightningArrow();
                        break;
                    case DhiaAtkSkill3.LightningBolt:
                        LightningBolt();
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
    [SerializeField]
    [CustomLabel("殴りの命中率")]
    int hitHitRate = 0;

    //殴る
    void HitSkill()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = hitPower;

            //命中率
            hitRate = hitHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("蹴りの威力")]
    int kickPower = 0;
    [SerializeField]
    [CustomLabel("蹴りの命中率")]
    int kickHitRate = 0;
    //蹴る
    void KickSkill()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = kickPower;
            //命中率
            hitRate = kickHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("シールドバッシュの威力")]
    int defatkPower = 0;
    [SerializeField]
    [CustomLabel("シールドバッシュの命中率")]
    int defatkHitRate = 0;

    //シールドバッシュ
    void DefensiveAttack()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = def;
            //命中率
            hitRate = defatkHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]

    [CustomLabel("お守りします！の防御補正(%)")]
    public int protectDef = 0;
    [CustomLabel("お守りします！の効果継続ターン数")]
    public int protectTurnInitial = 0;
    [NonSerialized]
    public int protectTurn = 0;
    [NonSerialized]
    int protectdefTurn = 0;
    //お守りします！の管理フラグ
    [NonSerialized]
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

    [Space(10)]
    [CustomLabel("防御体制の防御補正(%)")]
    public int postureDef = 0;
    [CustomLabel("防御体制の効果継続ターン数")]
    public int postureTurnInitial = 0;
    [NonSerialized]
    public int postureTurn = 0;
    //防御体制の管理フラグ
    [NonSerialized]
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

    [Space(10)]
    //防御補正値の初期値
    //[NonSerialized]
    public int ririProtectDef = 0;
    [CustomLabel("守るの効果継続ターン数")]
    public int ririProtectTurnInitial = 0;
    [NonSerialized]
    public int ririProtectTurn = 0;
    //守るの管理フラグ
    [NonSerialized]
    public bool ririProtectFlag = false;

    //守る
    void Protect()
    {
        if (!button)
        {
            //ターン数の初期化
            ririProtectTurn = ririProtectTurnInitial;

            //防御補正値の加算
            ririScript.defCorrectionValue += ririProtectDef;

            //防御アニメーションの再生
            dhiaAnim.SetBool("D_Shield", true);
            //守るのフラグをtrueに
            ririDefenseFlag = true;
            //多重押し防止
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("斬撃の威力")]
    int cutPower = 0;
    [SerializeField]
    [CustomLabel("斬撃の命中率")]
    int cutHitRate = 0;
    //切る
    void CutSkil()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = cutPower;

            //命中率
            hitRate = cutHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("射撃の威力")]
    int destroyPower = 0;
    [SerializeField]
    [CustomLabel("射撃の命中率")]
    int destroyHitRate = 0;

    //撃つ
    void Destroy()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = destroyPower;
            //命中率
            hitRate = destroyHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("切り裂くの威力")]
    int cutUpPower = 0;
    [SerializeField]
    [CustomLabel("切り裂くの命中率")]
    int cutUpPowerHitRate = 0;

    //切り裂く
    void CutUp()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = cutUpPower;
            //命中率
            hitRate = cutUpPowerHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("乱射の威力")]
    int blindlyPower = 0;
    [SerializeField]
    [CustomLabel("乱射の命中率")]
    int blindlyHitRate = 0;
    [SerializeField]
    [CustomLabel("乱射の攻撃回数")]
    int blindlyAtkFrequency = 0;

    //乱射
    void FiringBlindly()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //攻撃回数
            attackFrequency = blindlyAtkFrequency;
            //ランダム攻撃フラグをオン
            rndAtk = true;

            //威力
            power = blindlyPower;
            //命中率
            hitRate = blindlyHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("集中射撃の威力")]
    int focusedShotPower = 0;
    [SerializeField]
    [CustomLabel("集中射撃の命中率")]
    int focusedShotHitRate = 0;

    //集中射撃
    void FocusedShot()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = focusedShotPower;
            //命中率
            hitRate = focusedShotHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("乱切りの威力")]
    int choppingIntoChunksPower = 0;
    [SerializeField]
    [CustomLabel("乱切りの命中率")]
    int choppingIntoChunksHitRate = 0;
    [SerializeField]
    [CustomLabel("乱切りの攻撃回数")]
    int choppingIntoChunksFrequency = 0;

    //乱切り
    void ChoppingIntoChunks()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //攻撃回数
            attackFrequency = choppingIntoChunksFrequency;
            //ランダム攻撃フラグをオン
            rndAtk = true;

            //威力
            power = choppingIntoChunksPower;
            //命中率
            hitRate = choppingIntoChunksHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("居合切りの威力")]
    int iaiCuttingPower = 0;
    [SerializeField]
    [CustomLabel("居合切りの命中率")]
    int iaiCuttingHitRate = 0;

    //居合切り
    void IaiCutting()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = iaiCuttingPower;
            //命中率
            hitRate = iaiCuttingHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("鎧砕きの威力")]
    int armourCrushingPower = 0;
    [SerializeField]
    [CustomLabel("鎧砕きの命中率")]
    int armourCrushingHitRate = 0;

    //鎧砕き
    void ArmourCrushing()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = armourCrushingPower;
            //命中率
            hitRate = armourCrushingHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("逆刃斬りの威力")]
    int reverseClippingPower = 0;
    [SerializeField]
    [CustomLabel("逆刃斬りの命中率")]
    int reverseClippingHitRate = 0;
    [SerializeField]
    [CustomLabel("逆刃斬りの反動ダメージ(％)")]
    int reverseClippingDamageRate = 0;


    //逆刃斬りのフラグ
    bool reverseClippingFlag = false;

    //逆刃斬り　
    void ReverseClipping()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = reverseClippingPower;
            //命中率
            hitRate = reverseClippingHitRate;

            //フラグをオンに
            reverseClippingFlag = true;


            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("回転斬りの威力")]
    int revolvingSlashPower = 0;
    [SerializeField]
    [CustomLabel("回転斬りの命中率")]
    int revolvingSlashHitRate = 0;

    //回転斬り
    void RevolvingSlash()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = revolvingSlashPower;
            //命中率
            hitRate = revolvingSlashHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("マジックアローの威力")]
    int magicArrowsPower = 0;
    [SerializeField]
    [CustomLabel("マジックアローの命中率")]
    int magicArrowsHitRate = 0;

    //マジックアロー
    void MagicArrows()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = magicArrowsPower;
            //命中率
            hitRate = magicArrowsHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("マジックミサイルの威力")]
    int magicMissilePower = 0;
    [SerializeField]
    [CustomLabel("マジックミサイルの命中率")]
    int magicMissileHitRate = 0;

    //マジックミサイル
    void MagicMissile()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = magicMissilePower;
            //命中率
            hitRate = magicMissileHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("ライトニングアローの威力")]
    int lightningArrowPower = 0;
    [SerializeField]
    [CustomLabel("ライトニングアローの命中率")]
    int lightningArrowHitRate = 0;
    [SerializeField]
    [CustomLabel("ライトニングアローの行動不能確率(％)")]
    int lightningArrowRate = 0;
    //ライトニングアローのフラグ
    bool lightningArrowFlag = false;

    //ライトニングアロー
    void LightningArrow()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = lightningArrowPower;
            //命中率
            hitRate = lightningArrowHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("ライトニングボルトの威力")]
    int lightningBoltPower = 0;
    [SerializeField]
    [CustomLabel("ライトニングボルトの命中率")]
    int lightningBoltHitRate = 0;
    [SerializeField]
    [CustomLabel("ライトニングアローの行動不能確率(％)")]
    int lightningBoltRate = 0;
    //ライトニングボルトのフラグ
    bool lightningBoltFlag = false;



    //ライトニングボルト
    void LightningBolt()
    {
        //敵の選択ウィンドウ表示
        if (!button)
        {
            //威力
            power = lightningBoltPower;
            //命中率
            hitRate = lightningBoltHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }
}
