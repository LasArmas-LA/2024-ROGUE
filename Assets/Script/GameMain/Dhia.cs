using System;
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

    [Header("�N���X�Q��")]
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

    //�A�j���[�V�����Ǘ��p
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
        //�G���[���
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

        //�p�[�c�̃X�e�[�^�X�𔽉f���鏈��
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
                atkSkillName[0] = "����";
                break;
            case DhiaAtkSkill1.KickSkill:
                atkSkillName[0] = "�R��";
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

        switch (dhiaAtkSkill2)
        {
            case DhiaAtkSkill2.HitSkill:
                atkSkillName[1] = "����";
                break;
            case DhiaAtkSkill2.KickSkill:
                atkSkillName[1] = "�R��";
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

        switch (dhiaAtkSkill3)
        {
            case DhiaAtkSkill3.HitSkill:
                atkSkillName[2] = "����";
                break;
            case DhiaAtkSkill3.KickSkill:
                atkSkillName[2] = "�R��";
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

    public void Skill1Move(int enemyNumber)
    {
        timerFlag = true;
        dhiaAnim.SetBool("D_Attack", true);
        enemySelectWin.SetActive(false);
        encountSys.mainTurn = MainTurn.DHIAANIM;

        //�G1��I�΂ꂽ��
        if (enemyNumber == 1)
        {
            //�E�T�M�̎�
            if (encountSys.typeRnd[0] == 0)
            {
                Debug.Log("���������I�΂�܂���");
                rabbitScript[0].rabbitAnim.SetBool("Damage2", true);
                rabbitScript[0].timerFlag = true;
            }
            //���̎�
            if (encountSys.typeRnd[0] == 1)
            {
                Debug.Log("�Ƃ肪�I�΂�܂���");
                birdScript[0].birdAnim.SetBool("Eb_Damage2", true);
                birdScript[0].timerFlag = true;
            }
            if (powerUpFlag)
            {
                encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage * 1.5f + "�̃_���[�W!";
                encountSys.enemyScript.hp[0] -= (enemyDamage * 1.5f);
                powerUpFlag = false;
            }
            else
            {
                Debug.Log("�R�}���h1�f�B�A�ʏ�U��");
                encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage + "�̃_���[�W!";
                encountSys.enemyScript.hp[0] -= enemyDamage;
            }
        }
        //�G2��I�����ꂽ��
        if (enemyNumber == 2)
        {
            //�E�T�M�̎�
            if (encountSys.typeRnd[1] == 3)
            {
                rabbitScript[1].rabbitAnim.SetBool("Damage2", true);
                rabbitScript[1].timerFlag = true;
            }
            //���̎�
            if (encountSys.typeRnd[1] == 4)
            {
                birdScript[1].birdAnim.SetBool("Eb_Damage2", true);
                birdScript[1].timerFlag = true;
            }
            if (powerUpFlag)
            {
                encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage * 1.5f + "�̃_���[�W!";
                encountSys.enemyScript.hp[1] -= (enemyDamage * 1.5f);
                powerUpFlag = false;
            }
            else
            {
                Debug.Log("�R�}���h1�f�B�A�ʏ�U��");
                encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage + "�̃_���[�W!";
                encountSys.enemyScript.hp[1] -= enemyDamage;
            }
        }
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
        //�V�[�h�l�̕ύX
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //�f�̃_���[�W�v�Z
        int damage = (attack / 2) - (defense / 4);

        //�_���[�W�U���̌v�Z
        int width = damage / 16 + 1;

        //�_���[�W�U���l�����������v�Z
        damage = UnityEngine.Random.Range(damage - width, damage + width);
        //�Ăяo�����Ƀ_���[�W����Ԃ�
        return damage;
    }


    //�X�L���֐�


    //����
    void HitSkill()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    //�R��
    void KickSkill()
    {
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
        }
    }

    //����肵�܂��I
    void ProtectYou()
    {
        if(!button)
        {
            Debug.Log("����肵�܂�");
            button = true;
        }
    }

    //�h��̐�
    void DefensivePosture()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            encountSys.windowsMes.text = "�f�B�A�͐g������Ă���B";
            defenseFlag = true;
            button = true;
        }
    }

    //���
    void Protect()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            encountSys.windowsMes.text = "�f�B�A�̓����[������Ă���B";
            ririDefenseFlag = true;
            button = true;
        }
    }

    //�؂�
    void CutSkil()
    {

    }

    //����
    void Destroy()
    {
        
    }

    //�؂��
    void CutUp()
    {

    }

    //����
    void FiringBlindly()
    {

    }
}
