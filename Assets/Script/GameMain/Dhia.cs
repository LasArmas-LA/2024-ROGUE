using System;
using UnityEngine;
using static TestEncount;
using static UnityEngine.EventSystems.EventTrigger;

public class Dhia : MonoBehaviour
{
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
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
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

        enemyDamage = DamageCalculation(power, encountSys.enemyScript.def[0]);
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
        //�G�̑I���E�B���h�E�\��
        if (!button)
        {
            enemySelectWin.SetActive(true);
            commandButton.SetActive(false);
            button = true;
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
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            encountSys.windowsMes.text = "�f�B�A�͐g������Ă���B";
            defenseFlag = true;
            button = true;
        }
    }
    public void Skil3()
    {
        if (!button)
        {
            dhiaAnim.SetBool("D_Shield", true);
            Debug.Log("�R�}���h3�f�B�A");
            encountSys.windowsMes.text = "�f�B�A�̓����[������Ă���B";
            ririDefenseFlag = true;
            button = true;
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

}
