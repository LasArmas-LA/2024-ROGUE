using System;
using UnityEngine;

public class Riri : MonoBehaviour
{

    //技の管理用
    public enum RiriAtkSkill1
    {
        KeepItUp,
        BecomeWeak,
        Protect,
        DoNotMove
    }
    public enum RiriAtkSkill2
    {
        KeepItUp,
        BecomeWeak,
        Protect,
        DoNotMove
    }
    public enum RiriAtkSkill3
    {
        KeepItUp,
        BecomeWeak,
        Protect,
        DoNotMove
    }

    //技のenumの実体化
    public RiriAtkSkill1 ririAtkSkill1;
    public RiriAtkSkill2 ririAtkSkill2;
    public RiriAtkSkill3 ririAtkSkill3;

    [SerializeField]
    public String[] atkSkillName = null;

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

    [SerializeField]
    public Animator ririAnim = null;

    public bool button = false;

    //アニメーション制御用のタイマーとフラグ
    float timer = 0;
    bool timerFlag = false;


    void Awake()
    {
        //HPの初期化
        //InitStatus();
    }
    void Start()
    {
        Init();
    }

    public void Init()
    {
        //検索処理の初期化
        //InitFind();
        //スキルの名前を初期化
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
        maxhp = ririStatus.MAXHP;
        maxmp = ririStatus.MAXMP;
        power = ririStatus.DEFATK;
        def = ririStatus.DEFDEF;
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        //デバッグ用
        hp = maxhp;
        mp = maxmp;

        if (floorNoSys != null)
        {
            if (floorNoSys.floorCo == 1)
            {
                Debug.Log("0階です");
                ririStatus.HP = ririStatus.MAXHP;

                hp = maxhp;
                mp = maxmp;
                deathFlag = false;
            }
            else
            {
                //hp = ririStatus.HP;
                mp = ririStatus.MP;
            }
        }
    }
    void InitSkilName()
    {
        //攻撃スキル1の名前を変更
        switch (ririAtkSkill1)
        {
            case RiriAtkSkill1.KeepItUp:
                atkSkillName[0] = "頑張って！";
                break;
            case RiriAtkSkill1.BecomeWeak:
                atkSkillName[0] = "弱くなれ！";
                break;
            case RiriAtkSkill1.Protect:
                atkSkillName[0] = "守る";
                break;
            case RiriAtkSkill1.DoNotMove:
                atkSkillName[0] = "動かないで！";
                break;
        }

        //攻撃スキル2の名前を変更
        switch (ririAtkSkill2)
        {
            case RiriAtkSkill2.KeepItUp:
                atkSkillName[1] = "頑張って！";
                break;
            case RiriAtkSkill2.BecomeWeak:
                atkSkillName[1] = "弱くなれ！";
                break;
            case RiriAtkSkill2.Protect:
                atkSkillName[1] = "守る";
                break;
            case RiriAtkSkill2.DoNotMove:
                atkSkillName[1] = "動かないで！";
                break;
        }

        //攻撃スキル3の名前を変更
        switch (ririAtkSkill3)
        {
            case RiriAtkSkill3.KeepItUp:
                atkSkillName[2] = "頑張って！";
                break;
            case RiriAtkSkill3.BecomeWeak:
                atkSkillName[2] = "弱くなれ！";
                break;
            case RiriAtkSkill3.Protect:
                atkSkillName[2] = "守る";
                break;
            case RiriAtkSkill3.DoNotMove:
                atkSkillName[2] = "動かないで！";
                break;
        }
    }

    void Update()
    {
        //HP確認用
        HpCheck();
        //選択確認用
        SlectCheck();
        //再生中のアニメーション停止用
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

        ririStatus.HP = hp;
        ririStatus.MP = mp;
    }
    void SlectCheck()
    {
        if (ririSelectFlag || dhiaSelectFlag)
        {
            encountSys.timer += Time.deltaTime;

            if (encountSys.timer >= 3.5f)
            {
                ririAnim.SetBool("R_Skill", false);
            }
            if (encountSys.timer >= encountSys.waitTime)
            {
                ririSelectFlag = false;
                dhiaSelectFlag = false;
            }
        }
    }
    void AnimDelete()
    {
        if (timerFlag)
        {
            timer += Time.deltaTime;

            if (timer >= 3.5f)
            {
                ririAnim.SetBool("R_Skill", false);

                timer = 0;
                timerFlag = false;
            }
        }
    }

    public void Skil1()
    {
        switch (ririAtkSkill1)
        {
            case RiriAtkSkill1.KeepItUp:
                KeepItUp();
                break;
            case RiriAtkSkill1.BecomeWeak:
                BecomeWeak();
                break;
            case RiriAtkSkill1.Protect:
                Protect();
                break;
            case RiriAtkSkill1.DoNotMove:
                DoNotMove();
                break;
        }

    }
    public void Skil2()
    {
        switch (ririAtkSkill2)
        {
            case RiriAtkSkill2.KeepItUp:
                KeepItUp();
                break;
            case RiriAtkSkill2.BecomeWeak:
                BecomeWeak();
                break;
            case RiriAtkSkill2.Protect:
                Protect();
                break;
            case RiriAtkSkill2.DoNotMove:
                DoNotMove();
                break;
        }
    }
    public void Skil3()
    {
        switch (ririAtkSkill3)
        {
            case RiriAtkSkill3.KeepItUp:
                KeepItUp();
                break;
            case RiriAtkSkill3.BecomeWeak:
                BecomeWeak();
                break;
            case RiriAtkSkill3.Protect:
                Protect();
                break;
            case RiriAtkSkill3.DoNotMove:
                DoNotMove();
                break;
        }
    }

    //エネミーの対象用システム
    public void EnemySlectSys(int enemyNo)
    {
        if (encountSys.command1)
        {
            switch (ririAtkSkill1)
            {
                case RiriAtkSkill1.BecomeWeak:
                    BecomeWeakSlect(enemyNo);
                    break;
                case RiriAtkSkill1.DoNotMove:
                    DoNotMoveSlect(enemyNo);
                    break;
            }
        }
        if (encountSys.command2)
        {
            switch (ririAtkSkill2)
            {
                case RiriAtkSkill2.BecomeWeak:
                    BecomeWeakSlect(enemyNo);
                    break;
                case RiriAtkSkill2.DoNotMove:
                    DoNotMoveSlect(enemyNo);
                    break;
            }
        }
        if (encountSys.command3)
        {
            switch (ririAtkSkill3)
            {
                case RiriAtkSkill3.BecomeWeak:
                    BecomeWeakSlect(enemyNo);
                    break;
                case RiriAtkSkill3.DoNotMove:
                    DoNotMoveSlect(enemyNo);
                    break;
            }
        }
    }

    //頑張って！
    void KeepItUp()
    {
        //アニメーションのカウントダウンとアニメーションスタート
        timerFlag = true;
        ririAnim.SetBool("R_Skill", true);

        encountSys.windowsMes.text = "リリーはバイキルトを唱えた！\nディアの攻撃力が上昇した!";
        dhia.powerUpFlag = true;

        //ステータスを変更
        encountSys.mainTurn = TestEncount.MainTurn.RIRIANIM;
    }


    [SerializeField]
    GameObject ririEnemySlectWin = null;
    //弱くなれ！
    void BecomeWeak()
    {
        //敵選択のウィンドウを表示
        ririEnemySlectWin.SetActive(true);
    }


    //強くなれで必要な変数
    public bool becomeWeakFlag = false;
    //敵の攻撃力補正値
    public float powerValue = 0;
    //敵の攻撃力補正値保存用
    int[] powerValueKeep = new int[2];

    //弱くなれ！の対象選択
    public void BecomeWeakSlect(int enemyNo)
    {
        //敵死亡時に対象塗り替え
        if (encountSys.enemyScript.enemyDeath[0]) { enemyNo = 1;}
        if (encountSys.enemyScript.enemyDeath[1]) { enemyNo = 0;}

        powerValue = 0.2f;
        //敵1選択時
        if (enemyNo == 0)
        {
             //効果終了時に減算するための補正値の保存
             powerValueKeep[0] = 
                (int)(encountSys.enemyScript.power[0] * powerValue);

            encountSys.enemyScript.power[0] -= powerValueKeep[0];
        }
        //敵2選択時
        if (enemyNo == 1)
        {
            //効果終了時に減算するための補正値の保存
            powerValueKeep[1] = 
                (int)(encountSys.enemyScript.power[1] * powerValue);

            encountSys.enemyScript.power[1] -= powerValueKeep[1];
        }
        //パワーを初期値に戻す処理
        if(enemyNo == 100)
        {
            encountSys.enemyScript.power[0] += powerValueKeep[0];

            encountSys.enemyScript.power[1] += powerValueKeep[1];
            becomeWeakFlag = false;
        }
        else
        {
            //ステータスを変更
            encountSys.mainTurn = TestEncount.MainTurn.RIRIANIM;
            //敵選択のウィンドウを表示
            ririEnemySlectWin.SetActive(false);
            becomeWeakFlag = true;
        }
    }

    int prtectTurnDef = 2;
    public int prtectTurn = 0;
    public bool prtectFlag = false;

    //守ってあげる！
    void Protect()
    {
        //ターンの代入
        prtectTurn = prtectTurnDef;
        prtectFlag = true;
        dhia.defCorrectionValue = (int)(dhia.defCorrectionValue + (dhia.defCorrectionValue * 0.1f));

        //ステータスを変更
        encountSys.mainTurn = TestEncount.MainTurn.RIRIANIM;
    }

    //動かないで！
    void DoNotMove()
    {
        //敵選択のウィンドウを表示
        ririEnemySlectWin.SetActive(true);

    }
    //動かないで！の対処選択
    void DoNotMoveSlect(int enemyNo)
    {
        if (enemyNo == 0)
        {

        }
        if (enemyNo == 1)
        {

        }
    }

}

