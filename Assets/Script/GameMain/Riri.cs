using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
using static TestEncount;

public class Riri : MonoBehaviour
{
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

        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();

        if (floorNoSys != null)
        {
            if (floorNoSys.floorNo == 0)
            {
                Debug.Log("0�K�ł�");
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

        if(ririSelectFlag || dhiaSelectFlag)
        {
            encountSys.timer += Time.deltaTime;

            if(encountSys.timer >= 3.5f)
            {
                ririAnim.SetBool("R_Skill", false);
            }
            if (encountSys.timer >= encountSys.waitTime)
            {
                ririSelectFlag = false;
                dhiaSelectFlag = false;
            }
        }

        if(timerFlag)
        {
            timer += Time.deltaTime;

            if(timer >= 3.5f)
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
        if (!button)
        {
            recoveryWin.SetActive(true);
            commandWin.SetActive(false);
        }
    }
    public void Skil2()
    {
        //�A�j���[�V�����̃J�E���g�_�E���ƃA�j���[�V�����X�^�[�g
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

    }
    public void Skil3()
    {

        //�A�j���[�V�����̃J�E���g�_�E���ƃA�j���[�V�����X�^�[�g
        timerFlag = true;
        ririAnim.SetBool("R_Skill", true);

        Debug.Log("�R�}���h3�����[");
        encountSys.windowsMes.text = "�����[�̓o�C�L���g���������I\n�f�B�A�̍U���͂��㏸����!";
        dhia.powerUpFlag = true;
    }

    //�����[�̃q�[���g�p���ɃL�����N�^�[��I������֐��BOnClick�ŌĂ΂��
    public void RiriSlect()
    {
        if (!button)
        {
            button = true;
            ririSelectFlag = true;
            ririAnim.SetBool("R_Skill", true);

            //�X�e�[�^�X��ύX
            encountSys.mainTurn = MainTurn.RIRIANIM;

            RecoveryWin();
        }
    }
    //�����[�̃q�[���g�p���ɃL�����N�^�[��I������֐��BOnClick�ŌĂ΂��
    public void DhiaSlect()
    {
        if (!button)
        {
            button = true;
            dhiaSelectFlag = true;
            ririAnim.SetBool("R_Skill", true);

            //�X�e�[�^�X��ύX
            encountSys.mainTurn = MainTurn.RIRIANIM;

            RecoveryWin();
        }
    }

    public void RecoveryWin()
    {
        if (ririSelectFlag)
        {
            if (maxhp < hp + 50)
            {
                Debug.Log("�R�}���h1�����[HP�}�b�N�X��");
                encountSys.windowsMes.text = "�����[�̓q�[�����������I\n" + "�����[" + "��HP��" + (maxhp - hp) + "�񕜂���!";
                hp = maxhp;
            }
            else
            {
                Debug.Log("�R�}���h1�����[HP������");
                encountSys.windowsMes.text = "�����[�̓q�[�����������I\n" + "�����[" + "��HP��50�񕜂���!";
                hp += 50;
            }
        }
        if (dhiaSelectFlag)
        {
            if (dhia.maxhp < dhia.hp + 50)
            {
                Debug.Log("�R�}���h1�����[HP�}�b�N�X��");
                encountSys.windowsMes.text = "�����[�̓q�[�����������I\n" + "�f�B�A" + "��HP��" + (dhia.maxhp - dhia.hp) + "�񕜂���!";
                dhia.hp = dhia.maxhp;
            }
            else
            {
                Debug.Log("�R�}���h1�����[HP������");
                encountSys.windowsMes.text = "�����[�̓q�[�����������I\n" + "�f�B�A" + "��HP��50�񕜂���!";
                dhia.hp += 50;
            }
        }
        recoveryWin.SetActive(false);
        commandWin.SetActive(true);
    }
}
