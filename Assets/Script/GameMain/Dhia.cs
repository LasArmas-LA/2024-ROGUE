using System;
using UnityEngine;
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

    [NonSerialized]
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
        enemyDamage = DamageCalculation(power, encountSys.rndEnemy.def);

        if (floorNoSys != null)
        {
            if (floorNoSys.floorNo == 0)
            {
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
    }

    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
            dhiaMain.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        dhiaStatus.HP = hp;
        dhiaStatus.MP = mp;
    }

    public void Skil1()
    {
        if (powerUpFlag)
        {
            Debug.Log("�R�}���h1�f�B�A�p���[�A�b�v�U��");

            encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage * 1.5f + "�̃_���[�W!";
            encountSys.rndEnemy.hp -= (enemyDamage * 1.5f);
            powerUpFlag = false;
        }
        else
        {
            Debug.Log("�R�}���h1�f�B�A�ʏ�U��");
            encountSys.windowsMes.text = "�f�B�A�̂��������I" + enemyDamage + "�̃_���[�W!";
            encountSys.rndEnemy.hp -= enemyDamage;
        }
    }
    public void Skil2()
    {
        encountSys.windowsMes.text = "�f�B�A�͐g������Ă���B";
        defenseFlag = true;
    }
    public void Skil3()
    {
        Debug.Log("�R�}���h3�f�B�A");
        encountSys.windowsMes.text = "�f�B�A�̓����[������Ă���B";
        ririDefenseFlag = true;
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
