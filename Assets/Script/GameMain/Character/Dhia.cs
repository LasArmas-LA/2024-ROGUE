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

    //�p�����[�^�[
    [Space(10),Header("�p�����[�^�[")]
    [NonSerialized]
    public float maxhp = 0;
    [NonSerialized]
    public float maxmp = 0;

    public float hp = 0;
    [NonSerialized]
    public float mp = 0;
    //�U����
    //[NonSerialized]
    public int attack = 0;
    [NonSerialized]
    public int attackDefault = 0;
    //�U���̕␳�l�p
    public int atkCorrectionValue = 100;

    //�h��
    //[NonSerialized]
    public int def = 0;
    //�h��̏����l
    public int defDefault = 0;

    //�З�
    public int power = 0;

    //�h��̕␳�l�p
    //[NonSerialized]
    public int defCorrectionValue = 100;

    //������
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

    [Header("�N���X�Q��")]
    [SerializeField]
    Riri ririScript = null;

    [SerializeField]
    TestEncount encountSys = null;

    [SerializeField]
    GameObject dhiaMain = null;

    public Rabbit[] rabbitScript = null;
    public Bird[] birdScript = null;

    //�G��I������E�B���h�E
    public GameObject enemySelectWin = null;

    public GameObject commandButton = null;

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
                ririScript.maxhp = (ririScript.maxhp * 1.1f);

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

        defDefault = dhiaStatus.DEF;
        attackDefault = dhiaStatus.ATK;
    }

    void InitSkilName()
    {
        //�U�����̓K�p
        for (int i = 0; i < 3; i++)
        {
            atkSkillName[i] = dhiaSkillList.atkSkillList[floorNoSys.skillNoDhiaAtk[i]].name;
        }
        //�h�䖼�̓K�p
        for (int i = 0; i < 3; i++)
        {
            defSkillName[i] = dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[i]].name;
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

    //���d�����h�~
    public bool button = false;

    //�_���[�W�e�L�X�g�\���p
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;

    //�U����
    [SerializeField]
    int attackFrequency = 1;
    //�����_���U���̃t���O
    public bool rndAtk = false;

    public void Skill1Move(int enemyNumber)
    {
        //�U���̎�
        if(atkDefSlect == AtkDefSlect.ATK)
        {
            SkillAtk(enemyNumber);
        }
        //�h��̎�
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
            
            //�����_���U���̎�
            if (rndAtk)
            {
                int rndAttack = UnityEngine.Random.Range(1, 2);
                enemyNumber = rndAttack;
            }

            //0����100�̗������擾
            int rnd = UnityEngine.Random.Range(0, 100);

            Debug.Log(rnd);

            //�Z����
            if (rnd <= hitRate)
            {
                //�_���[�W�̌v�Z
                enemyDamage = DamageCalculation(attack, encountSys.enemyScript.def[enemyNumber - 1], power);

                //�t�n�a��U���̎�
                if (reverseClippingFlag)
                {
                    //HP�����炷
                    hp -= enemyDamage / reverseClippingDamageRate;
                    //�t���O���I�t
                    reverseClippingFlag = false;
                }
                //���C�g�j���O�A���[�U���̎�
                if (lightningArrowFlag)
                {
                    int lightningArrowRnd = UnityEngine.Random.Range(0, 100);
                    if (lightningArrowRate <= lightningArrowRnd)
                    {
                        encountSys.enemyStopFlag[enemyNumber] = true;
                    }

                    lightningArrowFlag = false;

                }
                //���C�g�j���O�{���g�U���̎�
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

                //�e�L�X�g�̕\������
                damageTextObj[enemyNumber - 1].SetActive(true);

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
                encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage + "�̃_���[�W!";
                encountSys.enemyScript.hp[enemyNumber - 1] -= enemyDamage;

                damageText[enemyNumber - 1].text = enemyDamage.ToString();

                Debug.Log(enemyDamage);


                StartCoroutine("DamageInit");
            }
            //�Z�O��
            else
            {
                Debug.Log("�c�O�ł�����");

                enemySelectWin.SetActive(false);
            }
        }
        encountSys.mainTurn = MainTurn.DHIAANIM;

        attackFrequency = 1;
    }

    void SkillDef(int enemyNumber)
    {
        //�␳�l����
        if(dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._NO)
        {
            return;
        }

        //�U��
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._ATK)
        {
            atkCorrectionValue += dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionValue;
        }

        //�h��
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._DEF)
        {
            defCorrectionValue += dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionValue;
        }

        //HP
        if (dhiaSkillList.defSkillList[floorNoSys.skillNoDhiaDef[0]].correctionType == DhiaSkillList.DefenseSkillStatus.eCorrectionType._HP)
        {

        }

        //������
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
    [SerializeField]
    [CustomLabel("����̖�����")]
    int hitHitRate = 0;

    //����
    void HitSkill()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = hitPower;

            //������
            hitRate = hitHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�R��̈З�")]
    int kickPower = 0;
    [SerializeField]
    [CustomLabel("�R��̖�����")]
    int kickHitRate = 0;
    //�R��
    void KickSkill()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = kickPower;
            //������
            hitRate = kickHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�V�[���h�o�b�V���̈З�")]
    int defatkPower = 0;
    [SerializeField]
    [CustomLabel("�V�[���h�o�b�V���̖�����")]
    int defatkHitRate = 0;

    //�V�[���h�o�b�V��
    void DefensiveAttack()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = def;
            //������
            hitRate = defatkHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]

    [CustomLabel("����肵�܂��I�̖h��␳(%)")]
    public int protectDef = 0;
    [CustomLabel("����肵�܂��I�̌��ʌp���^�[����")]
    public int protectTurnInitial = 0;
    [NonSerialized]
    public int protectTurn = 0;
    [NonSerialized]
    int protectdefTurn = 0;
    //����肵�܂��I�̊Ǘ��t���O
    [NonSerialized]
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

    [Space(10)]
    [CustomLabel("�h��̐��̖h��␳(%)")]
    public int postureDef = 0;
    [CustomLabel("�h��̐��̌��ʌp���^�[����")]
    public int postureTurnInitial = 0;
    [NonSerialized]
    public int postureTurn = 0;
    //�h��̐��̊Ǘ��t���O
    [NonSerialized]
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

    [Space(10)]
    //�h��␳�l�̏����l
    //[NonSerialized]
    public int ririProtectDef = 0;
    [CustomLabel("���̌��ʌp���^�[����")]
    public int ririProtectTurnInitial = 0;
    [NonSerialized]
    public int ririProtectTurn = 0;
    //���̊Ǘ��t���O
    [NonSerialized]
    public bool ririProtectFlag = false;

    //���
    void Protect()
    {
        if (!button)
        {
            //�^�[�����̏�����
            ririProtectTurn = ririProtectTurnInitial;

            //�h��␳�l�̉��Z
            ririScript.defCorrectionValue += ririProtectDef;

            //�h��A�j���[�V�����̍Đ�
            dhiaAnim.SetBool("D_Shield", true);
            //���̃t���O��true��
            ririDefenseFlag = true;
            //���d�����h�~
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�a���̈З�")]
    int cutPower = 0;
    [SerializeField]
    [CustomLabel("�a���̖�����")]
    int cutHitRate = 0;
    //�؂�
    void CutSkil()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = cutPower;

            //������
            hitRate = cutHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�ˌ��̈З�")]
    int destroyPower = 0;
    [SerializeField]
    [CustomLabel("�ˌ��̖�����")]
    int destroyHitRate = 0;

    //����
    void Destroy()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = destroyPower;
            //������
            hitRate = destroyHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�؂�􂭂̈З�")]
    int cutUpPower = 0;
    [SerializeField]
    [CustomLabel("�؂�􂭂̖�����")]
    int cutUpPowerHitRate = 0;

    //�؂��
    void CutUp()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = cutUpPower;
            //������
            hitRate = cutUpPowerHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("���˂̈З�")]
    int blindlyPower = 0;
    [SerializeField]
    [CustomLabel("���˂̖�����")]
    int blindlyHitRate = 0;
    [SerializeField]
    [CustomLabel("���˂̍U����")]
    int blindlyAtkFrequency = 0;

    //����
    void FiringBlindly()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�U����
            attackFrequency = blindlyAtkFrequency;
            //�����_���U���t���O���I��
            rndAtk = true;

            //�З�
            power = blindlyPower;
            //������
            hitRate = blindlyHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("�W���ˌ��̈З�")]
    int focusedShotPower = 0;
    [SerializeField]
    [CustomLabel("�W���ˌ��̖�����")]
    int focusedShotHitRate = 0;

    //�W���ˌ�
    void FocusedShot()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = focusedShotPower;
            //������
            hitRate = focusedShotHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("���؂�̈З�")]
    int choppingIntoChunksPower = 0;
    [SerializeField]
    [CustomLabel("���؂�̖�����")]
    int choppingIntoChunksHitRate = 0;
    [SerializeField]
    [CustomLabel("���؂�̍U����")]
    int choppingIntoChunksFrequency = 0;

    //���؂�
    void ChoppingIntoChunks()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�U����
            attackFrequency = choppingIntoChunksFrequency;
            //�����_���U���t���O���I��
            rndAtk = true;

            //�З�
            power = choppingIntoChunksPower;
            //������
            hitRate = choppingIntoChunksHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�����؂�̈З�")]
    int iaiCuttingPower = 0;
    [SerializeField]
    [CustomLabel("�����؂�̖�����")]
    int iaiCuttingHitRate = 0;

    //�����؂�
    void IaiCutting()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = iaiCuttingPower;
            //������
            hitRate = iaiCuttingHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�Z�ӂ��̈З�")]
    int armourCrushingPower = 0;
    [SerializeField]
    [CustomLabel("�Z�ӂ��̖�����")]
    int armourCrushingHitRate = 0;

    //�Z�ӂ�
    void ArmourCrushing()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = armourCrushingPower;
            //������
            hitRate = armourCrushingHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�t�n�a��̈З�")]
    int reverseClippingPower = 0;
    [SerializeField]
    [CustomLabel("�t�n�a��̖�����")]
    int reverseClippingHitRate = 0;
    [SerializeField]
    [CustomLabel("�t�n�a��̔����_���[�W(��)")]
    int reverseClippingDamageRate = 0;


    //�t�n�a��̃t���O
    bool reverseClippingFlag = false;

    //�t�n�a��@
    void ReverseClipping()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = reverseClippingPower;
            //������
            hitRate = reverseClippingHitRate;

            //�t���O���I����
            reverseClippingFlag = true;


            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("��]�a��̈З�")]
    int revolvingSlashPower = 0;
    [SerializeField]
    [CustomLabel("��]�a��̖�����")]
    int revolvingSlashHitRate = 0;

    //��]�a��
    void RevolvingSlash()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = revolvingSlashPower;
            //������
            hitRate = revolvingSlashHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("�}�W�b�N�A���[�̈З�")]
    int magicArrowsPower = 0;
    [SerializeField]
    [CustomLabel("�}�W�b�N�A���[�̖�����")]
    int magicArrowsHitRate = 0;

    //�}�W�b�N�A���[
    void MagicArrows()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = magicArrowsPower;
            //������
            hitRate = magicArrowsHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("�}�W�b�N�~�T�C���̈З�")]
    int magicMissilePower = 0;
    [SerializeField]
    [CustomLabel("�}�W�b�N�~�T�C���̖�����")]
    int magicMissileHitRate = 0;

    //�}�W�b�N�~�T�C��
    void MagicMissile()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = magicMissilePower;
            //������
            hitRate = magicMissileHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }


    [Space(10)]
    [SerializeField]
    [CustomLabel("���C�g�j���O�A���[�̈З�")]
    int lightningArrowPower = 0;
    [SerializeField]
    [CustomLabel("���C�g�j���O�A���[�̖�����")]
    int lightningArrowHitRate = 0;
    [SerializeField]
    [CustomLabel("���C�g�j���O�A���[�̍s���s�\�m��(��)")]
    int lightningArrowRate = 0;
    //���C�g�j���O�A���[�̃t���O
    bool lightningArrowFlag = false;

    //���C�g�j���O�A���[
    void LightningArrow()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = lightningArrowPower;
            //������
            hitRate = lightningArrowHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    [Space(10)]
    [SerializeField]
    [CustomLabel("���C�g�j���O�{���g�̈З�")]
    int lightningBoltPower = 0;
    [SerializeField]
    [CustomLabel("���C�g�j���O�{���g�̖�����")]
    int lightningBoltHitRate = 0;
    [SerializeField]
    [CustomLabel("���C�g�j���O�A���[�̍s���s�\�m��(��)")]
    int lightningBoltRate = 0;
    //���C�g�j���O�{���g�̃t���O
    bool lightningBoltFlag = false;



    //���C�g�j���O�{���g
    void LightningBolt()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            //�З�
            power = lightningBoltPower;
            //������
            hitRate = lightningBoltHitRate;

            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }
}
