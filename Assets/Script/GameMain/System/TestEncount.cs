using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseEquipment;

public class TestEncount : MonoBehaviour
{
    //�o�g���p
    public enum MainTurn
    {
        WAIT,

        STRATRUN,


        RIRILOOPINIT,
        RIRIMOVE,
        RIRIANIM,
        RIRIEFFECT,


        DHIALOOPINIT,
        DHIAATKDEFSLECT,
        DHIAMOVE,
        DHIAANIM,
        DHIAEFFECT,

        ENEMY1LOOPINIT,
        ENEMY1MOVE,
        ENEMY1ANIM,
        ENEMY1EFFECT,

        ENEMY2LOOPINIT,
        ENEMY2MOVE,
        ENEMY2ANIM,
        ENEMY2EFFECT,

        GAMEOVER,

        ENDRUN
    }
    //���̉�
    public MainTurn mainTurn;

    //�o�g����̃p�[�c�\���p
    enum PartsMode
    {
        //�p�[�c�̕\������
        DISP,
        //�p�[�c�̑I��ҋ@��
        WAIT,
        //�p�[�c�I����
        END
    }
    //���̉�
    PartsMode partsMode = PartsMode.DISP;

    //�o�g���R�}���h�̃e�L�X�g
    [Header("�o�g���R�}���h�̃e�L�X�g")]
    [SerializeField]
    public TextMeshProUGUI windowsMes = null;
    [SerializeField]
    public TextMeshProUGUI command1Text = null;
    [SerializeField]
    public TextMeshProUGUI command2Text = null;
    [SerializeField]
    public TextMeshProUGUI command3Text = null;

    //�X�N���v�g�Q��
    [SerializeField]
    Riri ririScript = null;
    [SerializeField]
    Dhia dhiaScript = null;
    [SerializeField]
    public EnemyManager enemyScript = null;
    [SerializeField]
    EnemyFloorRunSys enemyFloorRunSysObj = null;
    FloorNoSys floorNoSysScript = null;

    //�ҋ@����
    [SerializeField]
    public float waitTime = 0;
    //�G�t�F�N�g�̑ҋ@���Ԑݒ�p
    public float effectWaitTime = 0;
    //�G�t�F�N�g�̑ҋ@���ԃJ�E���g�p
    public float effectWaitTimer = 0;


    [SerializeField]
    GameObject floorNoSysObj = null;

    [Space(10)]

    //�̗̓Q�[�W�p
    [Header("�̗̓Q�[�W")]
    [SerializeField, Tooltip("�����[�̗̑̓Q�[�W")]
    Slider ririSlider = null;
    [SerializeField, Tooltip("�f�B�A�̗̑̓Q�[�W")]
    Slider dhiaSlider = null;

    //�e�L�����N�^�[�̎��S�t���O
    [Space(10)]
    [Header("�e�L�����N�^�[�̎��S�t���O")]
    bool ririDeath = false;
    bool dhiaDeath = false;
    bool enemyDeath = false;


    [Space(10)]

    //�����[,�f�B�A��Obj
    [Header("�e�L�����N�^�[�̃I�u�W�F�N�g")]
    [SerializeField]
    GameObject ririObj;
    [SerializeField]
    GameObject dhiaObj;

    [Header("�e�L�����N�^�[�̃X�e�[�^�X")]
    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    Status dhiaStatus = null;


    [Space(10)]

    [Header("�e�L�����N�^�[�̃R�}���hUI")]
    [SerializeField]
    GameObject ririCommand = null;
    [SerializeField]
    GameObject dhiaCommand = null;


    [Space(10)]

    [Header("�R�}���h�̉摜")]
    [SerializeField]
    Sprite ririCommandSp = null;
    [SerializeField]
    Sprite dhiaCommandSp = null;

    [Space(10)]

    [Header("�R�}���h�̃I�u�W�F�N�g")]
    [SerializeField]
    Image[] commnadImage = null;
    [SerializeField]
    GameObject atkDefSlectWin = null;

    [Space(10)]

    //�x�e�K�̃t���O
    //[NonSerialized]
    public bool restFlag = false;

    //�{�X�K�̃t���O
    [NonSerialized]
    public bool bossFlag = false;

    [SerializeField]
    GameObject[] enemyObj = null;

    //�G�̎�ނ̒��I�p
    public int[] typeRnd = null;

    //�G�̐��̒��I�p
    public int numberRnd = 0;

    float hpMoveTimer = 0;
    bool hpMoveTimerFlag = false;

    [Space(5)]
    [Header("�p�����[�^�[����")]
    [SerializeField]
    float hpLowSpeed = 1;

    [Space(5)]
    [Header("�p�[�c�Ǘ��p")]
    [SerializeField]
    bool[] partsSlect;
    //�p�[�c�I�����̕\���؂�ւ��p
    [SerializeField]
    GameObject[] partsObj = null;
    [SerializeField]
    Image[] partsImage = null;
    [SerializeField]
    Sprite slectOnSp = null;
    [SerializeField]
    Sprite slectOffSp = null;
    [SerializeField]
    GameObject[] arrowObj = null;


    //�p�[�c�̖��O
    string[] partsName = { "RightHand", "LeftHand", "Head", "Body", "Feet" };

    //�h���b�v�����p�[�c�̏��\���p
    [SerializeField]
    TextMeshProUGUI[] slectText;

    //�h���b�v�����p�[�c�̉摜�\���p
    [SerializeField]
    Image[] dropPartsSp = null;

    //���ݑ������Ă���p�[�c�̏��\���p
    [SerializeField]
    TextMeshProUGUI[] slectNowText;

    //�p�[�c��I����m�肳�������̔��f
    bool allPartsSlect;

    //�p�[�c��I������E�B���h�E
    [SerializeField]
    GameObject partsSlectWin = null;

    [Space(5)]

    //�����������_���œ��肷�郍�W�b�N�g�݂̃V�X�e��
    [SerializeField, Header("�h���b�v���������_�����V�X�e��")]
    EquipmentManager equipmentManager = null;

    //�A�j���[�V����
    [SerializeField, Header("�A�j���[�V�����Ǘ��p")]
    Animator ririAnim = null;
    [SerializeField]
    Animator dhiaAnim = null;

    //�R�}���h��I������E�B���h�E
    [SerializeField, Header("�R�}���h�Ǘ��p")]
    public GameObject commandWin = null;
    [SerializeField]
    public GameObject commandMain = null;

    //�G�̏ꏊ�܂ŕ����t���O
    bool runStratFlag = false;
    //���ɒ����Ă��̊K���I�����鎞�̃t���O
    bool floorEndFlag = false;
    //�ŏ���1�񂾂��Ăяo����������
    bool partFastMove = true;


    //�L�����N�^�[�̐e�I�u�W�F�N�g
    [SerializeField]
    GameObject characterMainObj = null;

    [Space(10)]
    //�J�����̓������x
    [SerializeField, Header("�J�����Ǘ��p")]
    Vector3 characterMoveSpeed = Vector3.zero;

    [Space(10)]

    [SerializeField, Header("�I�u�W�F�N�g�Ǘ��p")]
    GameObject restObj = null;
    [SerializeField]
    GameObject doorObj = null;

    void Awake()
    {
        //Find�����̏�����
        InitFind();

        //�����[��Find�̏�����
        ririScript.InitFind();
        //�f�B�A��Find�̏�����
        dhiaScript.InitFind();

        //�����[��HP�̏�����
        ririScript.InitStatus();
        //�f�B�A��HP�̏�����
        dhiaScript.InitStatus();
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        //�X�e�[�^�X��ҋ@��ԂɕύX
        mainTurn = MainTurn.WAIT;

        //�G�̒��I
        InitEnemy();
        //HP�̏�����
        InitHp();
        //�A�N�e�B�u��Ԃ̏�����
        InitActive();
        //�A�j���[�V�����̏�����
        InitAnim();

        //����̒��I
        equipmentManager.LoopInit();

        //Time.timeScale = 100.0f;
    }

    void InitEnemy()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        numberRnd = UnityEngine.Random.Range(0, 2);
        numberRnd = 1;


        //�G�̐����f�[�^���i�[
        if (numberRnd == 0)
        {
            //�G�l�~�[�̎�ޒ��I�p
            typeRnd[0] = UnityEngine.Random.Range(0, enemyObj.Length - 2);

            //�����_���őI�΂ꂽ�G�l�~�[�I�u�W�F�N�g�̕\��
            enemyObj[typeRnd[0]].transform.localScale = new Vector3(1, 1, 1);
        }
        if (numberRnd == 1)
        {
            //�G�l�~�[�̎�ޒ��I�p
            typeRnd[0] = UnityEngine.Random.Range(0, enemyObj.Length - 2);

            //�����_���őI�΂ꂽ�G�l�~�[�I�u�W�F�N�g�̕\��
            enemyObj[typeRnd[0]].transform.localScale = new Vector3(1, 1, 1);

            //�G�l�~�[�̎�ޒ��I�p
            typeRnd[1] = UnityEngine.Random.Range(3, enemyObj.Length + 1);

            //�����_���őI�΂ꂽ�G�l�~�[�I�u�W�F�N�g�̕\��
            enemyObj[typeRnd[1 -1]].transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }

        //�K�w�f�[�^�ێ��N���X�̌����Ə����i�[
        floorNoSysScript = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
    }

    void InitHp()
    {
        //MaxHP�̊i�[
        ririSlider.maxValue = ririScript.maxhp;
        dhiaSlider.maxValue = dhiaScript.maxhp;

        //MinHP�̊i�[
        ririSlider.minValue = 0;
        dhiaSlider.minValue = 0;

        //�f�o�b�O�p
        //Max��HP�����݂�HP�Ɋi�[
        ririSlider.value = ririSlider.maxValue;
        dhiaSlider.value = dhiaSlider.maxValue;

        //�t���A��1�K�̎�
        if (floorNoSysScript.floorCo == 1)
        {
            //Max��HP�����݂�HP�Ɋi�[
            ririSlider.value = ririSlider.maxValue;
            dhiaSlider.value = dhiaSlider.maxValue;
        }
        //����ȊO
        else
        {
            //Hp�o�[���chp�̊����œK�p
            ririSlider.value = ririSlider.maxValue * (ririScript.hp / ririScript.maxhp);
            dhiaSlider.value = dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp);
        }
        if (floorNoSysScript.floorCo % 5 == 0 && floorNoSysScript.floorCo != 0)
        {
            restFlag = true;
        }
        if (floorNoSysScript.floorCo % 10 == 0)
        {
            bossFlag = true;
        }
    }

    void InitActive()
    {
        commandWin.SetActive(false);
        commandMain.SetActive(false);

        //�p�[�c�I�����̏���������
        arrowObj[0].SetActive(false);
        arrowObj[1].SetActive(false);
        arrowObj[2].SetActive(false);
    }

    void InitAnim()
    {
        //�����A�j���[�V�������J�n
        ririAnim.SetBool("R_Walk", true);
        dhiaAnim.SetBool("D_Walk", true);
    }


    bool fast = true;
    float ririhpdf = 0;
    float dhiahpdf = 0;

    void Update()
    {
        switch (mainTurn)
        {
            case MainTurn.WAIT:
                Wait();
                break;
            case MainTurn.STRATRUN:
                StartRun();
                break;

                //<<<�����[>>>
            //�����[�̃��[�u
            case MainTurn.RIRILOOPINIT:
                RiriLoopInit();
                break;
            //�����[�̃��[�u
            case MainTurn.RIRIMOVE:
                RiriMove();
                break;
            //�����[�̃A�j���[�V����
            case MainTurn.RIRIANIM:
                RiriAnimMove();
                break;
                //�����[�̃G�t�F�N�g
            case MainTurn.RIRIEFFECT:
                RiriEffect();
                break;

�@�@�@�@�@�@     //<<<�f�B�A>>>
            case MainTurn.DHIALOOPINIT:
                DhiaLoopInit();
                break;
            //�f�B�A�̍U�h�I��
            case MainTurn.DHIAATKDEFSLECT:
                DhiaFastSlect();
                break;
            //�f�B�A�̃��[�u
            case MainTurn.DHIAMOVE:
                DhiaMove();
                break;
            //�f�B�A�̃A�j���[�V����
            case MainTurn.DHIAANIM:
                DhiaAnimMove();
                break;
            //�f�B�A�̃G�t�F�N�g
            case MainTurn.DHIAEFFECT:
                DhiaEffect();
                break;

                 //<<<�G1>>>
            case MainTurn.ENEMY1LOOPINIT:
                Enemy1LoopInit();
                break;
            case MainTurn.ENEMY1MOVE:
                Enemy1Move();
                break;
            case MainTurn.ENEMY1ANIM:
                Enemy1AnimMove();
                //�G1�̃G�t�F�N�g
                break;
            case MainTurn.ENEMY1EFFECT:
                Enmey1Effect();
                break;

            //<<<�G2>>>
            case MainTurn.ENEMY2LOOPINIT:
                Enemy2LoopInit();
                break;
            case MainTurn.ENEMY2MOVE:
                Enemy2Move();
                break;
            case MainTurn.ENEMY2ANIM:
                Enemy2AnimMove();
                break;
            //�G2�̃G�t�F�N�g
            case MainTurn.ENEMY2EFFECT:
                Enemy2Effect();
                break;

                //<<<�Q�[���I�[�o�[>>>
            case MainTurn.GAMEOVER:
                GameOver();
                break;

                //<<<�G�|������̈ړ�>>>
            case MainTurn.ENDRUN:
                EndRun();
                break;
        }

        HpCheck();
    }

    void HpCheck()
    {
        //�����[��HP�����ꂽ��
        if (ririhpdf > ririScript.hp)
        {
            /*
            //HP��0�ȉ��ɂȂ��Ă鎞
            if(ririScript.hp <= 0)
            {
                ririScript.hp = 0;
                ririSlider.value -= (ririScript.maxhp * Time.deltaTime);
            }
            */

            ririSlider.value -= ((ririSlider.maxValue * (ririScript.hp / ririScript.maxhp)) * Time.deltaTime) * hpLowSpeed;

            if (ririSlider.value <= ririScript.hp)
            {
                ririhpdf = ririScript.hp;
                ririSlider.value = ririScript.hp;
            }
        }

        //�f�B�A��HP�����ꂽ��
        if (dhiahpdf > dhiaScript.hp)
        {
            /*
            //HP��0�ȉ��ɂȂ��Ă鎞
            if (dhiaScript.hp <= 0)
            {
                dhiaScript.hp = 0;
                dhiaSlider.value -= (dhiaScript.maxhp * Time.deltaTime);
            }
            */

            dhiaSlider.value -= ((dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp)) * Time.deltaTime) * hpLowSpeed;

            if (dhiaSlider.value <= dhiaScript.hp)
            {
                dhiahpdf = dhiaScript.hp;
                dhiaSlider.value = dhiaScript.hp;
            }
        }
    }

    public void HpMoveWait(String charName)
    {
        hpMoveTimer += Time.deltaTime;

        if(hpMoveTimer >= 2f)
        {
            if(charName == "Riri")
            {
                ririhpdf = ririScript.hp;
            }
            if (charName == "Dhia")
            {
                dhiahpdf = dhiaScript.hp;
            }
            hpMoveTimer = 0;
        }
    }

    //�R�}���h����
    public bool command1 = false;
    public bool command2 = false;
    public bool command3 = false;
    bool button = false;
    bool coLock = false;

    public void Command1()
    {
        //���i�����h�~
        if(!button)
        {
            button = true;
            command1 = true;
        }
    }
    public void Command2()
    {
        //���i�����h�~
        if (!button)
        {
            button = true;
            command2 = true;
        }
    }
    public void Command3()
    {
        //���i�����h�~
        if (!button)
        {
            button = true;
            command3 = true;
        }
    }

    int dhiaSlectNumber = 0;
    public void DhiaAtkDefSlect(int number)
    {
        if (!button)
        {
            button = true;
            dhiaSlectNumber = number;
        }
    }

    bool floorFast = false;
    void Wait()
    {
        //1�񂾂��Ăяo��
        if (!floorFast)
        {
            //�X�e�[�^�X�̕ύX
            mainTurn = MainTurn.RIRILOOPINIT;

            //�����A�j���[�V�������~
            ririAnim.SetBool("R_Walk", false);
            dhiaAnim.SetBool("D_Walk", false);

            //�R�}���h��\��
            commandWin.SetActive(true);
            commandMain.SetActive(true);
            fast = true;
            runStratFlag = false;
        }

    }

    void StartRun()
    {

    }


    //�����[�̏�����
    void RiriLoopInit()
    {
        //�X�L���̖��O�\������������
        command1Text.text = ririScript.atkSkillName[0];
        command2Text.text = ririScript.atkSkillName[1];
        command3Text.text = ririScript.atkSkillName[2];

        //�R�}���h�̃e�N�X�`����؂�ւ�
        commnadImage[0].sprite = ririCommandSp;
        commnadImage[1].sprite = ririCommandSp;
        commnadImage[2].sprite = ririCommandSp;

        //�R�}���h�����̕\���A�N�e�B�u��؂�ւ�
        dhiaCommand.SetActive(false);
        ririCommand.SetActive(true);

        dhiaScript.button = false;

        
        if (ririScript.becomeWeakFlag)
        {
            ririScript.BecomeWeakSlect(100);
        }

        //����Ă�����I�̃^�[���o�ߏ���
        if (ririScript.prtectFlag)
        {
            ririScript.prtectTurn--;
            if (ririScript.prtectTurn == 0)
            {
                ririScript.prtectFlag = false;
                dhiaScript.defCorrectionValue = (int)(dhiaScript.defCorrectionValue - (dhiaScript.defCorrectionValue * 0.1f));
            }
        }

        //�_���[�W���󂯂����𔻕ʂł���悤�Ɋi�[
        ririhpdf = ririScript.hp;

        //�X�e�[�^�X�̐؂�ւ�
        mainTurn = MainTurn.RIRIMOVE;
    }

    float rirMoveiTimer = 0;

    void RiriMove()
    {
        //�����[���S���Q�[���I�[�o�[
        if (ririScript.deathFlag)
        {
            mainTurn = MainTurn.GAMEOVER;
        }

        //�{�^�����������őҋ@
        if (command1 || command2 || command3)
        {
            rirMoveiTimer += Time.deltaTime;

            //�{�^���̑��i�����h�~
            if (!coLock)
            {
                if (command1)
                {
                    //�X�e�[�^�X��ύX
                    //mainTurn = MainTurn.RIRIANIM;
                    //�R�}���h��\���̏���
                    enemyFloorRunSysObj.commandMain.SetActive(false);

                    ririScript.Skil1();
                }
                if (command2)
                {
                    //�X�e�[�^�X��ύX
                    //mainTurn = MainTurn.RIRIANIM;
                    //�R�}���h��\���̏���
                    enemyFloorRunSysObj.commandMain.SetActive(false);

                    ririScript.Skil2();
                }
                if (command3)
                {
                    //�X�e�[�^�X��ύX
                    //mainTurn = MainTurn.RIRIANIM;
                    //�R�}���h��\���̏���
                    enemyFloorRunSysObj.commandMain.SetActive(false);

                    ririScript.Skil3();
                }
                coLock = true;
            }
        }
        else
        {
            //�R�}���h�\���̏���
            enemyFloorRunSysObj.commandMain.SetActive(true);
            enemyFloorRunSysObj.commandWin.SetActive(true);
        }
    }

    public float ririAnimTimer = 0f;
    void RiriAnimMove()
    {
        if (!fast)
        {
            fast = true;
        }

        //�R�}���h��\���̏���
        enemyFloorRunSysObj.commandMain.SetActive(false);
        enemyFloorRunSysObj.commandWin.SetActive(false);

        //�^�C�}�[�J�n
        ririAnimTimer += Time.deltaTime;

        //�ҋ@���Ԃ𒴂��ēG�������Ă��鎞
        if (ririAnimTimer >= waitTime && !enemyDeath)
        {
            ririAnimTimer = 0;

            button = false;
            coLock = false;
            command1 = false;
            command2 = false;
            command3 = false;

            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.RIRIEFFECT;
        }
    }

    void RiriEffect()
    {
        effectWaitTimer += Time.deltaTime;

        if(effectWaitTimer >= effectWaitTime)
        {
            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.DHIALOOPINIT;
            effectWaitTimer = 0f;
            effectWaitTime = 0f;
        }
    }


    void DhiaLoopInit()
    {
        //�R�}���h�����̕\���؂�ւ�
        ririCommand.SetActive(false);
        dhiaCommand.SetActive(true);

        //�R�}���h�̃e�N�X�`���̐؂�ւ�
        commnadImage[0].sprite = dhiaCommandSp;
        commnadImage[1].sprite = dhiaCommandSp;
        commnadImage[2].sprite = dhiaCommandSp;


        atkDefSlectWin.SetActive(true);
        enemyFloorRunSysObj.commandWin.SetActive(true);

        enemyScript.enemyHpDef[0] = enemyScript.hp[0];
        enemyScript.enemyHpDef[1] = enemyScript.hp[1];

        //�h��X�L���̏���������
        //����肵�܂��I
        if (dhiaScript.protectFlag)
        {
            dhiaScript.protectTurn--;
            if (dhiaScript.protectTurn <= 0)
            {
                dhiaScript.protectFlag = false;
                //�h��␳�l�����Z
                dhiaScript.defCorrectionValue -= dhiaScript.postureDef;
            }
        }
        //�h��̐�
        if (dhiaScript.postureFlag)
        {
            dhiaScript.postureTurn--;
            if (dhiaScript.postureTurn <= 0)
            {
                dhiaScript.postureFlag = false;
                //�h��␳�l�����Z
                dhiaScript.defCorrectionValue -= dhiaScript.postureDef;
            }
        }

        //���
        if (dhiaScript.ririDefenseFlag)
        {
            dhiaScript.ririProtectTurn--;
            if (dhiaScript.ririProtectTurn <= 0)
            {
                dhiaScript.ririDefenseFlag = false;
                //�h��␳�l�����Z
                dhiaScript.defCorrectionValue -= dhiaScript.ririProtectDef;
            }
        }

        if (dhiaScript.defCorrectionValue <= 100)
        {
            dhiaScript.defCorrectionValue = 100;
        }
        //�f�B�A�̕␳�l�̑��
        //dhiaScript.def = (dhiaScript.def * (dhiaScript.defCorrectionValue / 100));


        dhiaScript.powerUpFlag = false;

        //�_���[�W���󂯂����𔻕ʂł���悤�Ɋi�[
        dhiahpdf = dhiaScript.hp;

        ririScript.button = false;

        mainTurn = MainTurn.DHIAATKDEFSLECT;
    }

    void DhiaFastSlect()
    {
        if (button)
        {
            //�A�^�b�N�X�L����I��
            if (dhiaSlectNumber == 0)
            {
                command1Text.text = dhiaScript.atkSkillName[0];
                command2Text.text = dhiaScript.atkSkillName[1];
                command3Text.text = dhiaScript.atkSkillName[2];

                dhiaScript.atkDefSlect = Dhia.AtkDefSlect.ATK;
            }
            //�f�B�t�F���X�X�L����I��
            if (dhiaSlectNumber == 1)
            {
                command1Text.text = dhiaScript.defSkillName[0];
                command2Text.text = dhiaScript.defSkillName[1];
                command3Text.text = dhiaScript.defSkillName[2];
                dhiaScript.atkDefSlect = Dhia.AtkDefSlect.DEF;
            }

            button = false;
            atkDefSlectWin.SetActive(false);
            mainTurn = MainTurn.DHIAMOVE;
        }
    }


    void DhiaMove()
    {
        //�f�B�A���S���^�[�����X�L�b�v
        if (dhiaScript.deathFlag)
        {
            mainTurn = MainTurn.ENEMY1MOVE;
        }
        if (fast)
        {
            //�R�}���h�����̕\���؂�ւ�
            ririCommand.SetActive(false);
            dhiaCommand.SetActive(true);

            fast = false;
        }

        if (command1 || command2 || command3)
        {
            if (!coLock)
            {
                if (command1)
                {
                    dhiaScript.Skil1();
                    //�U���I�����͑Ώۂ�I�΂���
                    if (dhiaSlectNumber == 0)
                    {
                        if (numberRnd != 1)
                        {
                            //�X�e�[�^�X��ύX
                            mainTurn = MainTurn.DHIAANIM;
                        }
                    }
                    //�h�䎞�͑Ώۂ�I�΂��Ȃ�
                    if(dhiaSlectNumber == 1)
                    {
                        //�X�e�[�^�X��ύX
                        mainTurn = MainTurn.DHIAANIM;   
                    }

                }
                if (command2)
                {
                    dhiaScript.Skil2();
                    //�U���I�����͑Ώۂ�I�΂���
                    if (dhiaSlectNumber == 0)
                    {
                        if (numberRnd != 1)
                        {
                            //�X�e�[�^�X��ύX
                            mainTurn = MainTurn.DHIAANIM;
                        }
                    }
                    //�h�䎞�͑Ώۂ�I�΂��Ȃ�
                    if (dhiaSlectNumber == 1)
                    {
                        //�X�e�[�^�X��ύX
                        mainTurn = MainTurn.DHIAANIM;
                    }
                }
                if (command3)
                {
                    dhiaScript.Skil3();
                    //�U���I�����͑Ώۂ�I�΂���
                    if (dhiaSlectNumber == 0)
                    {
                        if (numberRnd != 1)
                        {
                            //�X�e�[�^�X��ύX
                            mainTurn = MainTurn.DHIAANIM;
                        }
                    }
                    //�h�䎞�͑Ώۂ�I�΂��Ȃ�
                    if (dhiaSlectNumber == 1)
                    {
                        //�X�e�[�^�X��ύX
                        mainTurn = MainTurn.DHIAANIM;
                    }
                }
                coLock = true;
            }
        }
        else
        {
            //�R�}���h�\���̏���
            enemyFloorRunSysObj.commandMain.SetActive(true);
            enemyFloorRunSysObj.commandWin.SetActive(true);

        }
    }

    float endWaitTimer = 0f;
    float dhiaAnimTimer = 0f;
    void DhiaAnimMove()
    {
        //�A�j���[�V�����̎���
        //�G��|�������m�F
        if (enemyScript.deathFlag)
        {
            //�^�C�}�[�J�n
            endWaitTimer += Time.deltaTime;

            if (endWaitTimer >= 3)
            {
                mainTurn = MainTurn.ENDRUN;
                endWaitTimer = 0;
            }
        }
        else
        {
            //�R�}���h��\���̏���
            enemyFloorRunSysObj.commandMain.SetActive(false);
            enemyFloorRunSysObj.commandWin.SetActive(false);

            //�^�C�}�[�J�n
            dhiaAnimTimer += Time.deltaTime;

            //�ҋ@���Ԃ𒴂��ēG�������Ă��鎞
            if (dhiaAnimTimer >= waitTime && !enemyDeath)
            {
                dhiaAnimTimer = 0;
                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;

                //�f�B�A�̃X�e�[�^�X���f
                dhiaScript.def -= dhiaScript.def - (dhiaScript.def * (dhiaScript.defCorrectionValue / 100));

                dhiaScript.def = (dhiaScript.def + (dhiaScript.def * (dhiaScript.defCorrectionValue / 100)));

                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.DHIAEFFECT;
            }
        }
    }

    void DhiaEffect()
    {
        effectWaitTimer += Time.deltaTime;
        if(effectWaitTimer >= effectWaitTime)
        {
            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.ENEMY1LOOPINIT;
            effectWaitTimer = 0;
            effectWaitTime = 0;
        }
    }


    void Enemy1LoopInit()
    {

        mainTurn = MainTurn.ENEMY1MOVE;
    }

    void Enemy1Move()
    {
        //����ł鎞�̓^�[�����X�L�b�v���Ė߂�
        if (enemyScript.enemyDeath[0])
        {
            mainTurn = MainTurn.ENEMY2MOVE;
            return;
        }


        if (!coLock)
        {
            //Init���ɑI�����ꂽ�G�l�~�[�̃X�L���֐����Ăяo��
            enemyScript.Move();
            coLock = true;
        }

        mainTurn = MainTurn.ENEMY1ANIM;
    }

    float enemy1Timer = 0f;
    void Enemy1AnimMove()
    {
        //�^�C�}�[�J�n
        enemy1Timer += Time.deltaTime;

        //�ҋ@���Ԃ𒴂�����
        if (enemy1Timer >= waitTime)
        {
            coLock = false;
            enemy1Timer = 0;

            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.ENEMY1EFFECT;
        }
    }

    void Enmey1Effect()
    {
        effectWaitTimer += Time.deltaTime;

        if (effectWaitTimer >= effectWaitTime)
        {
            //�X�e�[�^�X��ύX
            //�G��2�̂̎�
            if (numberRnd == 1)
            {
                mainTurn = MainTurn.ENEMY2LOOPINIT;
            }
            else
            {
                mainTurn = MainTurn.RIRILOOPINIT;
            }
            effectWaitTimer = 0;
            effectWaitTime = 0;
        }
    }


    void Enemy2LoopInit()
    {



        mainTurn = MainTurn.ENEMY2MOVE;
    }

    void Enemy2Move()
    {

        if (numberRnd == 0 || enemyScript.enemyDeath[1])
        {
            mainTurn = MainTurn.RIRILOOPINIT;
            return;
        }


        if (!coLock)
        {
            //Init���ɑI�����ꂽ�G�l�~�[�̃X�L���֐����Ăяo��
            enemyScript.Move();
            coLock = true;
        }

        //�X�e�[�^�X��ύX
        mainTurn = MainTurn.ENEMY2ANIM;
    }

    float enemy2Timer = 0f;
    void Enemy2AnimMove()
    {
        //�^�C�}�[�J�n
        enemy2Timer += Time.deltaTime;

        //�ҋ@���Ԃ𒴂�����
        if (enemy2Timer >= waitTime)
        {
            coLock = false;
            enemy2Timer = 0;

            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.ENEMY2EFFECT;
        }
    }

    void Enemy2Effect()
    {
        effectWaitTimer += Time.deltaTime;

        if (effectWaitTimer >= effectWaitTime)
        {
            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.RIRILOOPINIT;

            effectWaitTimer = 0;
            effectWaitTime = 0;
        }
    }

    //�Q�[���I�[�o�[�̎��Ԍv���p
    float gameOvertimer = 0f;
    //�Q�[���I�[�o�[�̑J�ڃ^�C���ݒ�p
    [SerializeField]
    float gameOverTime = 0f;
    void GameOver()
    {
        gameOvertimer += Time.deltaTime;

        if (gameOvertimer >= gameOverTime)
        {
            SceneManager.LoadScene("GameOver");
            gameOvertimer = 0;
        }
    }

    [SerializeField]
    int partsDispTime = 0;
    float partsTimer = 0;

    void EndRun()
    {
        switch(partsMode)
        {
            case PartsMode.DISP:
                partsTimer += Time.deltaTime;
                if(partsTimer >= partsDispTime)
                {
                    partsMode = PartsMode.WAIT;
                    partsTimer = 0;
                }
                break;
            case PartsMode.WAIT:
                if (partFastMove)
                {
                    //�h���b�v�����̕\������
                    slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName;
                    slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName;
                    slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName;

                    //�h���b�v�����̉摜�\������
                    dropPartsSp[0].sprite = equipmentManager.randomEquip[equipmentManager.rnd[0]].sprite;
                    dropPartsSp[1].sprite = equipmentManager.randomEquip[equipmentManager.rnd[1]].sprite;
                    dropPartsSp[2].sprite = equipmentManager.randomEquip[equipmentManager.rnd[2]].sprite;

                    partFastMove = false;
                }
                partsSlectWin.SetActive(true);

                break;
            case PartsMode.END:
                if (characterMainObj.transform.position.x <= doorObj.transform.position.x + 70)
                {
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);
                    characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
                    //�����A�j���[�V�������J�n
                    dhiaAnim.SetBool("D_Shield", false);
                    ririAnim.SetBool("R_Walk", true);
                    dhiaAnim.SetBool("D_Walk", true);
                }
                else
                {
                    //�����A�j���[�V�������~
                    ririAnim.SetBool("R_Walk", false);
                    dhiaAnim.SetBool("D_Walk", false);
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);

                    commandMain.SetActive(false);
                    StartCoroutine(FloorEnd());
                }

                break;
        }
        commandWin.SetActive(false);
        commandMain.SetActive(false);

        if (!allPartsSlect)
        {
        }
        //�������I�΂ꂽ���ʊO�܂ňړ����鏈��
        else
        {
        }
    }

    bool[] partsButton = new bool[3];
    public void PartsSlect1()
    {
        if (!partsButton[0])
        {
            partsImage[0].sprite = slectOnSp;
            partsImage[1].sprite = slectOffSp;
            partsImage[2].sprite = slectOffSp;
            partsObj[0].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);
            arrowObj[0].SetActive(true);
            arrowObj[1].SetActive(false);
            arrowObj[2].SetActive(false);

            partsSlect[0] = true;
            partsSlect[1] = false;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect2()
    {
        if (!partsButton[1])
        {
            partsImage[0].sprite = slectOffSp;
            partsImage[1].sprite = slectOnSp;
            partsImage[2].sprite = slectOffSp;
            partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[1].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);
            arrowObj[0].SetActive(false);
            arrowObj[1].SetActive(true);
            arrowObj[2].SetActive(false);

            partsSlect[0] = false;
            partsSlect[1] = true;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect3()
    {
        if (!partsButton[2])
        {
            partsImage[0].sprite = slectOffSp;
            partsImage[1].sprite = slectOffSp;
            partsImage[2].sprite = slectOnSp;
            partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
            partsObj[2].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            arrowObj[0].SetActive(false);
            arrowObj[1].SetActive(false);
            arrowObj[2].SetActive(true);

            partsSlect[0] = false;
            partsSlect[1] = false;
            partsSlect[2] = true;
        }
    }

    public void PartsSlecteEnd()
    {
        button = true;
        partsMode = PartsMode.END;
        partsSlectWin.SetActive(false);

        //�Y�����镔�ʂɃp�[�c�f�[�^���i�[���鏈��
        if (partsSlect[0])
        {
            //�E��
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //����
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[0]];
            }
        }
        if (partsSlect[1])
        {
            //�E��
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //����
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[1]];
            }
        }
        if (partsSlect[2])
        {
            //�E��
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.RightHand)
            {
                dhiaStatus.righthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //����
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.LeftHand)
            {
                dhiaStatus.lefthandPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Feet)
            {
                dhiaStatus.legPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Body)
            {
                dhiaStatus.bodyPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
            //��
            if (equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentType == EquipmentType.Head)
            {
                dhiaStatus.headPartsData = equipmentManager.randomEquip[equipmentManager.rnd[2]];
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }

    //�t�F�[�h�����p
    [SerializeField]
    Animator fadeAnim = null;
    //�h�A�܂œ����������̏���
    IEnumerator FloorEnd()
    {
        fadeAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1.0f);
        LoadScene();
        floorEndFlag = true;
    }

}
