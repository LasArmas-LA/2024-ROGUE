using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static Dhia;
using static TestEncount;
using static UnityEngine.EventSystems.EventTrigger;

public class Dhia : MonoBehaviour
{
    //�Z�̊Ǘ��p
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

    //�Z��enum�̎��̉�
    public DhiaAtkSkill1 dhiaAtkSkill1;    
    public DhiaAtkSkill2 dhiaAtkSkill2;    
    public DhiaAtkSkill3 dhiaAtkSkill3;
    public DhiaDefSkill1 dhiaDefSkill1;    
    public DhiaDefSkill2 dhiaDefSkill2;    
    public DhiaDefSkill3 dhiaDefSkill3;

    //�U���Ɩh��enum�̎��̉�
    public AtkDefSlect atkDefSlect;

    [SerializeField]
    public String[] atkSkillName = null;
    [SerializeField]
    public String[] defSkillName = null;

    //�f�B�A�̃X�e�[�^�X
    [SerializeField]
    Status dhiaStatus = null;
    //�K�w�f�[�^�Ǘ��V�X�e��
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

    //�h��̕␳�l�p
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

    [Header("�N���X�Q��")]
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

    //�A�j���[�V�����Ǘ��p
    [SerializeField]
    public Animator dhiaAnim = null;
    float timer = 0;
    bool timerFlag = false;

    void Awake()
    {
        //�X�e�[�^�X�̏�����
        //InitStatus();
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        //�����̏�����
        //InitFind();
        //�X�L���̖��O�̏�����
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
        //HP�ƍU���͂Ɩh��͂̏���������
        dhiaStatus.MAXHP = 150;

        //�����HP�␳�l�������鎖�Ŏ󂯂Ă�󂯂Ă���_���[�W�͕ۑ����鎖���ł���
        dhiaStatus.HP -= floorNoSys.dhiaHp;
        dhiaStatus.ATK = 100;
        dhiaStatus.DEF = 10;


        //�X�e�[�^�X�̃p�����[�^�[����荞�ޏ���
        maxhp = dhiaStatus.MAXHP;
        maxmp = dhiaStatus.MAXMP;
        attack = dhiaStatus.ATK;
        def = dhiaStatus.DEF;

        //�X�P�[���̏���������
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        //�f�o�b�O�p
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
        //�X�e�[�^�X�␳�l�̏�����
        floorNoSys.dhiaHp = 0;
        floorNoSys.dhiaAtk = 0;
        floorNoSys.dhiaDef = 0;

        //�Z�b�g���ʏ����p
        int rabbitSetCou = 0;
        int birdSetCou = 0;

        //�p�[�c�̃��C���X�e�[�^�X���i�[����
        //���p�[�c�̏���
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
        //�̃p�[�c�̏���
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
        //���p�[�c�̏���
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
        //�E��p�[�c�̏���
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
        //����p�[�c�̏���
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


        //�������̃Z�b�g����
        if (rabbitSetCou >= 2)
        {
            //4�Z�b�g����
            if (rabbitSetCou >= 4)
            {
                //�����[��HP��10%�A�b�v
                riri.maxhp = (riri.maxhp * 1.1f);

                //���g��HP��10%�A�b�v
                maxhp = (maxhp * 1.1f);
                Debug.Log("������4�Z�b�g");
            }
            //2�Z�b�g����
            else
            {
                //���g��HP��10%�A�b�v
                maxhp = (maxhp * 1.1f);
                Debug.Log("������2�Z�b�g");
            }
        }
        //�t�N���E�̃Z�b�g����
        if (birdSetCou >= 2)
        {
            //4�Z�b�g����
            if (birdSetCou >= 4)
            {
                //HP��10%���炷
                maxhp = (maxhp * 0.9f);
                hp = (hp * 0.9f);
                //�L���X�g�ϊ����Ċi�[
                attack = (int)(attack * 1.2f);
                Debug.Log("�t�N���E4�Z�b�g");
            }
            //2�Z�b�g����
            else
            {
                //�L���X�g�ϊ����Ċi�[
                attack = (int)(attack * 1.1f);
                Debug.Log("�t�N���E2�Z�b�g");
            }
        }


        //���ۂɃp�[�c�̃X�e�[�^�X�𔽉f
        dhiaStatus.MAXHP += floorNoSys.dhiaHp;

        dhiaStatus.ATK += floorNoSys.dhiaAtk;
        dhiaStatus.DEF = floorNoSys.dhiaDef;
}
    void InitSkilName()
    {
        //�U���X�L��1�̖��O��ύX
        switch (dhiaAtkSkill1)
        {
            case DhiaAtkSkill1.HitSkill:
                atkSkillName[0] = "����";
                break;
            case DhiaAtkSkill1.KickSkill:
                atkSkillName[0] = "�R��";
                break;
            case DhiaAtkSkill1.DefensiveAttack:
                atkSkillName[0] = "�V�[���h�o�b�V��";
                break;
            case DhiaAtkSkill1.CutSkil:
                atkSkillName[0] = "�؂�";
                break;
            case DhiaAtkSkill1.Destroy:
                atkSkillName[0] = "����";
                break;
            case DhiaAtkSkill1.CutUp:
                atkSkillName[0] = "�؂��";
                break;
            case DhiaAtkSkill1.FiringBlindly:
                atkSkillName[0] = "����";
                break;
        }

        //�U���X�L��2�̖��O��ύX
        switch (dhiaAtkSkill2)
        {
            case DhiaAtkSkill2.HitSkill:
                atkSkillName[1] = "����";
                break;
            case DhiaAtkSkill2.KickSkill:
                atkSkillName[1] = "�R��";
                break;
            case DhiaAtkSkill2.DefensiveAttack:
                atkSkillName[1] = "�V�[���h�o�b�V��";
                break;
            case DhiaAtkSkill2.CutSkil:
                atkSkillName[1] = "�؂�";
                break;
            case DhiaAtkSkill2.Destroy:
                atkSkillName[1] = "����";
                break;
            case DhiaAtkSkill2.CutUp:
                atkSkillName[1] = "�؂��";
                break;
            case DhiaAtkSkill2.FiringBlindly:
                atkSkillName[1] = "����";
                break;
        }

        //�U���X�L��3�̖��O��ύX
        switch (dhiaAtkSkill3)
        {
            case DhiaAtkSkill3.HitSkill:
                atkSkillName[2] = "����";
                break;
            case DhiaAtkSkill3.KickSkill:
                atkSkillName[2] = "�R��";
                break;
            case DhiaAtkSkill3.DefensiveAttack:
                atkSkillName[2] = "�V�[���h�o�b�V��";
                break;
            case DhiaAtkSkill3.CutSkil:
                atkSkillName[2] = "�؂�";
                break;
            case DhiaAtkSkill3.Destroy:
                atkSkillName[2] = "����";
                break;
            case DhiaAtkSkill3.CutUp:
                atkSkillName[2] = "�؂��";
                break;
            case DhiaAtkSkill3.FiringBlindly:
                atkSkillName[2] = "����";
                break;
        }

        //�h��X�L��1�̖��O��ύX
        switch (dhiaDefSkill1)
        {
            case DhiaDefSkill1.ProtectYou:
                defSkillName[0] = "����肵�܂��I";
                break;
            case DhiaDefSkill1.DefensivePosture:
                defSkillName[0] = "�h��Ԑ�";
                break;
            case DhiaDefSkill1.Protect:
                defSkillName[0] = "���";
                break;
        }

        //�h��X�L��2�̖��O��ύX
        switch (dhiaDefSkill2)
        {
            case DhiaDefSkill2.ProtectYou:
                defSkillName[1] = "����肵�܂��I";
                break;
            case DhiaDefSkill2.DefensivePosture:
                defSkillName[1] = "�h��Ԑ�";
                break;
            case DhiaDefSkill2.Protect:
                defSkillName[1] = "���";
                break;
        }

        //�h��X�L��3�̖��O��ύX
        switch (dhiaDefSkill3)
        {
            case DhiaDefSkill3.ProtectYou:
                defSkillName[2] = "����肵�܂��I";
                break;
            case DhiaDefSkill3.DefensivePosture:
                defSkillName[2] = "�h��Ԑ�";
                break;
            case DhiaDefSkill3.Protect:
                defSkillName[2] = "���";
                break;
        }
    }




    void Update()
    {
        //HP�̊m�F����
        HpCheck();
        //�A�j���[�V�����̒�~����
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

    //���d�����h�~
    public bool button = false;
    public void Skil1()
    {
        switch (atkDefSlect)
        {
            //�U��
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

                //�h��
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

    //�_���[�W�e�L�X�g�\���p
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;

    public void Skill1Move(int enemyNumber)
    {
        //�_���[�W�̌v�Z
        enemyDamage = DamageCalculation(attack, encountSys.enemyScript.def[enemyNumber -1], power);

        timerFlag = true;
        dhiaAnim.SetBool("D_Attack", true);
        enemySelectWin.SetActive(false);
        encountSys.mainTurn = MainTurn.DHIAANIM;

        //�e�L�X�g�̕\������
        damageTextObj[enemyNumber -1].SetActive(true);

        if (encountSys.enemyScript.hp[0] <= 0)
        {
            enemyNumber = 2;
        }
        if (encountSys.enemyScript.hp[1] <= 0)
        {
            enemyNumber = 1;
        }

        //�A�j���[�V�����p
        //�E�T�M�̎�
        if (encountSys.typeRnd[enemyNumber - 1] == 0)
        {
            rabbitScript[enemyNumber - 1].rabbitAnim.SetBool("Damage2", true);
            rabbitScript[enemyNumber - 1].timerFlag = true;
        }
        //���̎�
        if (encountSys.typeRnd[enemyNumber - 1] == 1)
        {
            birdScript[enemyNumber - 1].birdAnim.SetBool("Eb_Damage2", true);
            birdScript[enemyNumber - 1].timerFlag = true;
        }

        //�U���̏���
        if (powerUpFlag)
        {
            encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage * 1.5f + "�̃_���[�W!";
            encountSys.enemyScript.hp[enemyNumber - 1] -= (enemyDamage * 1.5f);

            damageText[enemyNumber - 1].text = (enemyDamage * 1.5f).ToString();

        powerUpFlag = false;
        }
        else
        {
            encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage + "�̃_���[�W!";
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
            //�U��
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
            //�U��
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

    int DamageCalculation(int attack, int defense, int power) //�U���́A����́A�З�
    {
        //�V�[�h�l�̕ύX
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //�f�̃_���[�W�v�Z
        int damage = ((attack + power) / 2) - (defense / 4);

        //�_���[�W�U���̌v�Z
        int width = damage / 16 + 1;

        //�_���[�W�U���l�����������v�Z
        damage = UnityEngine.Random.Range(damage - width, damage + width);

        //�Ăяo�����Ƀ_���[�W����Ԃ�
        return damage;
    }


    //�X�L���֐�
    [Space (10)]
    [Header("�Z�̈З�")]
    [SerializeField]
    [CustomLabel("����̈З�")]
    int hitPower = 0; 
    //����
    void HitSkill()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = hitPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("�R��̈З�")]
    int kickPower = 0;
    //�R��
    void KickSkill()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = kickPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("�V�[���h�o�b�V���̈З�")]
    int defatkPower = 0;
    //�V�[���h�o�b�V��
    void DefensiveAttack()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = defatkPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(5)]

    [CustomLabel("����肵�܂��I�̖h��␳(%)")]
    public int protectDef = 0;
    [CustomLabel("����肵�܂��I�̌��ʌp���^�[����")]
    public int protectTurnInitial = 0;
    [NonSerialized]
    public int protectTurn = 0;
    //����肵�܂��I�̊Ǘ��t���O
    public bool protectFlag = false;
    //����肵�܂��I
    void ProtectYou()
    {
        if(!button)
        {
            //�^�[�����̏�����
            protectTurn = protectTurnInitial;
            //�t���O��true��
            protectFlag = true;
            //�h��␳�l�����Z
            defCorrectionValue += protectDef;

            //���d�����h�~
            button = true;
        }
    }

    [Space(5)]
    [CustomLabel("�h��̐��̖h��␳(%)")]
    public int postureDef = 0;
    [CustomLabel("�h��̐��̌��ʌp���^�[����")]
    public int postureTurnInitial = 0;
    //[NonSerialized]
    public int postureTurn = 0;
    //�h��̐��̊Ǘ��t���O
    public bool postureFlag = false;

    //�h��̐�
    void DefensivePosture()
    {
        if (!button)
        {
            //�^�[�����̏�����
            postureTurn = postureTurnInitial;
            //�h��␳�l�����Z
            defCorrectionValue += postureDef;
            //�h��A�j���[�V�����̍Đ�
            dhiaAnim.SetBool("D_Shield", true);
            //�h��̐��t���O��true��
            postureFlag = true;
            //���d�����h�~
            button = true;
        }
    }

    [Space(5)]
    //�h��␳�l�̏����l
    [NonSerialized]
    public int ririProtectDef = 0;
    [CustomLabel("���̌��ʌp���^�[����")]
    public int ririProtectTurnInitial = 0;
    [NonSerialized]
    public int ririProtectTurn = 0;
    //���̊Ǘ��t���O
    public bool ririProtectFlag = false;

    //���
    void Protect()
    {
        if (!button)
        {
            //�^�[�����̏�����
            ririProtectTurn = ririProtectTurnInitial;
            //�h��␳�l�̉��Z
            defCorrectionValue += ririProtectDef;
            //�h��A�j���[�V�����̍Đ�
            dhiaAnim.SetBool("D_Shield", true);
            //���̃t���O��true��
            ririDefenseFlag = true;
            //���d�����h�~
            button = true;
        }
    }

    [Space(5)]
    [SerializeField]
    [CustomLabel("�؂�̈З�")]
    int cutPower = 0;
    //�؂�
    void CutSkil()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = cutPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("���̈З�")]
    int destroyPower = 0;
    //����
    void Destroy()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = destroyPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("�؂�􂭂̈З�")]
    int cutUpPower = 0;
    //�؂��
    void CutUp()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = cutUpPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [SerializeField]
    [CustomLabel("���˂̈З�")]
    int blindlyPower = 0;
    //����
    void FiringBlindly()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = blindlyPower;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }
}
