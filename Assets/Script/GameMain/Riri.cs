using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
using static TestEncount;
using static Dhia;
using System.Data;

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
        Init();
    }
    void Start()
    {
    }

    void Init()
    {
        maxhp = ririStatus.MAXHP;
        maxmp = ririStatus.MAXMP;
        power = ririStatus.DEFATK;
        def = ririStatus.DEFDEF;
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        //エラー回避
        try
        {
            floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        }
        catch { }



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
                hp = ririStatus.HP;
                mp = ririStatus.MP;
            }
        }

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
        if (hp <= 0)
        {
            deathFlag = true;
            if (this.transform.localScale.x >= 0)
            {
                this.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
            }
        }

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
        ririStatus.HP = hp;
        ririStatus.MP = mp;
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
        if(encountSys.command1)
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
    }

    //弱くなれ！
    void BecomeWeak()
    {

    }
    //強くなれで必要な変数
    public bool becomeWeakFlag = false;
    //敵の攻撃力補正値
    public float powerValue = 0;

    //弱くなれ！の対象選択
    public void BecomeWeakSlect(int enemyNo)
    {
        powerValue = 0.2f;
        //敵1選択時
        if (enemyNo == 0)
        {
            encountSys.enemyScript.power[0] = encountSys.enemyScript.power[0] + (int)(encountSys.enemyScript.power[0] * powerValue);
        }
        //敵2選択時
        if(enemyNo == 1)
        {
            encountSys.enemyScript.power[1] = encountSys.enemyScript.power[1] + (int)(encountSys.enemyScript.power[1] * powerValue);
        }
        //パワーを初期値に戻す処理
        if(enemyNo == 100)
        {
            encountSys.enemyScript.power[0] = encountSys.enemyScript.power[0] - (int)(encountSys.enemyScript.power[0] * powerValue);
            encountSys.enemyScript.power[1] = encountSys.enemyScript.power[1] - (int)(encountSys.enemyScript.power[1] * powerValue);
            becomeWeakFlag = false;
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
    }

    //動かないで！
    void DoNotMove()
    {
        
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




/*        //アニメーションのカウントダウンとアニメーションスタート
timerFlag = true;
ririAnim.SetBool("R_Skill", true);

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
*/


