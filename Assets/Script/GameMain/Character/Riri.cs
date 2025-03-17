using System;
using UnityEngine;

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
        //HP�̏�����
        //InitStatus();
    }
    void Start()
    {
        Init();
    }

    public void Init()
    {
        //���������̏�����
        //InitFind();
        //�X�L���̖��O��������
        InitSkilName();
    }

    public void InitFind()
    {
        //�G���[���
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

        //�f�o�b�O�p
        hp = maxhp;
        mp = maxmp;

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
                //hp = ririStatus.HP;
                mp = ririStatus.MP;
            }
        }
    }
    void InitSkilName()
    {
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
        //HP�m�F�p
        HpCheck();
        //�I���m�F�p
        SlectCheck();
        //�Đ����̃A�j���[�V������~�p
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

    //�G�l�~�[�̑Ώۗp�V�X�e��
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

    //�撣���āI
    void KeepItUp()
    {
        //�A�j���[�V�����̃J�E���g�_�E���ƃA�j���[�V�����X�^�[�g
        timerFlag = true;
        ririAnim.SetBool("R_Skill", true);

        encountSys.windowsMes.text = "�����[�̓o�C�L���g���������I\n�f�B�A�̍U���͂��㏸����!";
        dhia.powerUpFlag = true;

        //�X�e�[�^�X��ύX
        encountSys.mainTurn = TestEncount.MainTurn.RIRIANIM;
    }


    [SerializeField]
    GameObject ririEnemySlectWin = null;
    //�キ�Ȃ�I
    void BecomeWeak()
    {
        //�G�I���̃E�B���h�E��\��
        ririEnemySlectWin.SetActive(true);
    }


    //�����Ȃ�ŕK�v�ȕϐ�
    public bool becomeWeakFlag = false;
    //�G�̍U���͕␳�l
    public float powerValue = 0;
    //�G�̍U���͕␳�l�ۑ��p
    int[] powerValueKeep = new int[2];

    //�キ�Ȃ�I�̑ΏۑI��
    public void BecomeWeakSlect(int enemyNo)
    {
        //�G���S���ɑΏۓh��ւ�
        if (encountSys.enemyScript.enemyDeath[0]) { enemyNo = 1;}
        if (encountSys.enemyScript.enemyDeath[1]) { enemyNo = 0;}

        powerValue = 0.2f;
        //�G1�I����
        if (enemyNo == 0)
        {
             //���ʏI�����Ɍ��Z���邽�߂̕␳�l�̕ۑ�
             powerValueKeep[0] = 
                (int)(encountSys.enemyScript.power[0] * powerValue);

            encountSys.enemyScript.power[0] -= powerValueKeep[0];
        }
        //�G2�I����
        if (enemyNo == 1)
        {
            //���ʏI�����Ɍ��Z���邽�߂̕␳�l�̕ۑ�
            powerValueKeep[1] = 
                (int)(encountSys.enemyScript.power[1] * powerValue);

            encountSys.enemyScript.power[1] -= powerValueKeep[1];
        }
        //�p���[�������l�ɖ߂�����
        if(enemyNo == 100)
        {
            encountSys.enemyScript.power[0] += powerValueKeep[0];

            encountSys.enemyScript.power[1] += powerValueKeep[1];
            becomeWeakFlag = false;
        }
        else
        {
            //�X�e�[�^�X��ύX
            encountSys.mainTurn = TestEncount.MainTurn.RIRIANIM;
            //�G�I���̃E�B���h�E��\��
            ririEnemySlectWin.SetActive(false);
            becomeWeakFlag = true;
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

        //�X�e�[�^�X��ύX
        encountSys.mainTurn = TestEncount.MainTurn.RIRIANIM;
    }

    //�����Ȃ��ŁI
    void DoNotMove()
    {
        //�G�I���̃E�B���h�E��\��
        ririEnemySlectWin.SetActive(true);

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

