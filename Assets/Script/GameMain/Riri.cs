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

    //�Z�̊Ǘ��p
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

    //�Z��enum�̎��̉�
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


    [Header("�N���X�Q��")]
    [SerializeField]
    Dhia dhia = null;

    [Space(10)]

    //�ΏۑI�����̃t���O
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

    //�A�j���[�V��������p�̃^�C�}�[�ƃt���O
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

        //�G���[���
        try
        {
            floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        }
        catch { }



        if (floorNoSys != null)
        {
            if (floorNoSys.floorCo == 1)
            {
                Debug.Log("0�K�ł�");
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

        //�U���X�L��1�̖��O��ύX
        switch (ririAtkSkill1)
        {
            case RiriAtkSkill1.KeepItUp:
                atkSkillName[0] = "�撣���āI";
                break;
            case RiriAtkSkill1.BecomeWeak:
                atkSkillName[0] = "�キ�Ȃ�I";
                break;
            case RiriAtkSkill1.Protect:
                atkSkillName[0] = "���";
                break;
            case RiriAtkSkill1.DoNotMove:
                atkSkillName[0] = "�����Ȃ��ŁI";
                break;
        }

        //�U���X�L��2�̖��O��ύX
        switch (ririAtkSkill2)
        {
            case RiriAtkSkill2.KeepItUp:
                atkSkillName[1] = "�撣���āI";
                break;
            case RiriAtkSkill2.BecomeWeak:
                atkSkillName[1] = "�キ�Ȃ�I";
                break;
            case RiriAtkSkill2.Protect:
                atkSkillName[1] = "���";
                break;
            case RiriAtkSkill2.DoNotMove:
                atkSkillName[1] = "�����Ȃ��ŁI";
                break;
        }

        //�U���X�L��3�̖��O��ύX
        switch (ririAtkSkill3)
        {
            case RiriAtkSkill3.KeepItUp:
                atkSkillName[2] = "�撣���āI";
                break;
            case RiriAtkSkill3.BecomeWeak:
                atkSkillName[2] = "�キ�Ȃ�I";
                break;
            case RiriAtkSkill3.Protect:
                atkSkillName[2] = "���";
                break;
            case RiriAtkSkill3.DoNotMove:
                atkSkillName[2] = "�����Ȃ��ŁI";
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

    //�G�l�~�[�̑Ώۗp�V�X�e��
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

    //�撣���āI
    void KeepItUp()
    {
        //�A�j���[�V�����̃J�E���g�_�E���ƃA�j���[�V�����X�^�[�g
        timerFlag = true;
        ririAnim.SetBool("R_Skill", true);

        encountSys.windowsMes.text = "�����[�̓o�C�L���g���������I\n�f�B�A�̍U���͂��㏸����!";
        dhia.powerUpFlag = true;
    }

    //�キ�Ȃ�I
    void BecomeWeak()
    {

    }
    //�����Ȃ�ŕK�v�ȕϐ�
    public bool becomeWeakFlag = false;
    //�G�̍U���͕␳�l
    public float powerValue = 0;

    //�キ�Ȃ�I�̑ΏۑI��
    public void BecomeWeakSlect(int enemyNo)
    {
        powerValue = 0.2f;
        //�G1�I����
        if (enemyNo == 0)
        {
            encountSys.enemyScript.power[0] = encountSys.enemyScript.power[0] + (int)(encountSys.enemyScript.power[0] * powerValue);
        }
        //�G2�I����
        if(enemyNo == 1)
        {
            encountSys.enemyScript.power[1] = encountSys.enemyScript.power[1] + (int)(encountSys.enemyScript.power[1] * powerValue);
        }
        //�p���[�������l�ɖ߂�����
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

    //����Ă�����I
    void Protect()
    {
        //�^�[���̑��
        prtectTurn = prtectTurnDef;
        prtectFlag = true;
        dhia.defCorrectionValue = (int)(dhia.defCorrectionValue + (dhia.defCorrectionValue * 0.1f));
    }

    //�����Ȃ��ŁI
    void DoNotMove()
    {
        
    }
    //�����Ȃ��ŁI�̑Ώ��I��
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




/*        //�A�j���[�V�����̃J�E���g�_�E���ƃA�j���[�V�����X�^�[�g
timerFlag = true;
ririAnim.SetBool("R_Skill", true);

if (maxhp > hp + 20 && dhia.maxhp > dhia.hp + 20)
{
    hp += 20;
    dhia.hp += 20;
    encountSys.windowsMes.text = "�����[�̓I�[���q�[�����������I\n�����[�ƃf�B�A��HP��20���񕜂���!";
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
    encountSys.windowsMes.text = "�����[�̓I�[���q�[�����������I\n�����[��HP��" + (maxhp - hp) + "�f�B�A��HP��" + (dhia.maxhp - dhia.hp) + "�񕜂���!";
}
*/


