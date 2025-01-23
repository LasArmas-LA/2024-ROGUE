using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestEncount : MonoBehaviour
{
    public enum MainTurn
    {
        WAIT,

        DHIAATKDEFSLECT,

        RIRIMOVE,
        RIRIANIM,

        DHIAMOVE,
        DHIAANIM,

        ENEMY1MOVE,
        ENEMY1ANIM,
        ENEMY2MOVE,
        ENEMY2ANIM,
        
        GAMEOVER,

        END
    }
    public MainTurn mainTurn;

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
    FloorNoSys floorNoSys = null;

    //�ҋ@����
    [SerializeField]
    public float waitTime = 0;
    public float timer = 0;

    GameObject floorNoSysObj = null;

    [Space(10)]

    [Header("�̗̓Q�[�W")]
    [SerializeField, Tooltip("�����[�̗̑̓Q�[�W")]
    Slider ririSlider = null;
    [SerializeField, Tooltip("�f�B�A�̗̑̓Q�[�W")]
    Slider dhiaSlider = null;

    [Space(10)]
    [Header("�e�L�����N�^�[�̎��S�t���O")]
    bool ririDeath = false;
    bool dhiaDeath = false;
    bool enemyDeath = false;


    [Space(10)]

    [Header("�e�L�����N�^�[�̃I�u�W�F�N�g")]
    //�����[,�f�B�A,�G�l�~�[��Obj
    [SerializeField]
    GameObject ririObj;
    [SerializeField]
    GameObject dhiaObj;

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
    [NonSerialized]
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

    void Start()
    {
        Init();
    }

    void Init()
    {
        //�X�e�[�^�X��ҋ@��ԂɕύX
        mainTurn = MainTurn.WAIT;

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
            typeRnd[1] = UnityEngine.Random.Range(3, enemyObj.Length);

            //�����_���őI�΂ꂽ�G�l�~�[�I�u�W�F�N�g�̕\��
            enemyObj[typeRnd[1]].transform.localScale = new Vector3(1, 1, 1);
        }

        //MaxHP�̊i�[
        ririSlider.maxValue = ririScript.maxhp;
        dhiaSlider.maxValue = dhiaScript.maxhp;

        //MinHP�̊i�[
        ririSlider.minValue = 0;
        dhiaSlider.minValue = 0;

        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();

        //�t���A��1�K�̎�
        if (floorNoSys.floorCo == 1)
        {
            //Max��HP�����݂�HP�Ɋi�[
            ririSlider.value = ririSlider.maxValue;
            dhiaSlider.value = dhiaSlider.maxValue;
        }
        else
        {
            //Hp�o�[���chp�̊����œK�p
            ririSlider.value = ririSlider.maxValue * (ririScript.hp / ririScript.maxhp);
            dhiaSlider.value = dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp);
        }
        if(floorNoSys.floorCo % 5 == 0)
        {
            restFlag = true;
        }
        if(floorNoSys.floorCo % 10 == 0)
        {
            bossFlag = true;
        }
    }

    bool fast = true;
    float ririhpdf = 0;
    float dhiahpdf = 0;

    void Update()
    {

        switch (mainTurn)
        {
            case MainTurn.WAIT:
                break;
            case MainTurn.RIRIMOVE:
                //�����[���S���Q�[���I�[�o�[
                if(ririScript.deathFlag)
                {
                    mainTurn = MainTurn.GAMEOVER;
                }
                if(fast)
                {
                    //�R�}���h�����̕\���؂�ւ�
                    dhiaCommand.SetActive(false);
                    ririCommand.SetActive(true);

                    dhiaScript.button = false;

                    //�_���[�W���󂯂����𔻕ʂł���悤�Ɋi�[
                    ririhpdf = ririScript.hp;
                    fast = false;
                }
                break;
            case MainTurn.RIRIANIM:
                if (!fast)
                {
                    fast = true;
                }

                break;
            case MainTurn.DHIAATKDEFSLECT:

                //�R�}���h�����̕\���؂�ւ�
                dhiaCommand.SetActive(true);
                ririCommand.SetActive(false);

                break;
            case MainTurn.DHIAMOVE:
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

                    enemyScript.enemyHpDef[0] = enemyScript.hp[0];
                    enemyScript.enemyHpDef[1] = enemyScript.hp[1];

                    //�_���[�W���󂯂����𔻕ʂł���悤�Ɋi�[
                    dhiahpdf = dhiaScript.hp;
                    fast = false;
                    ririScript.button = false;
                }
                break;
            case MainTurn.DHIAANIM:
                if (enemyScript.deathFlag)
                {
                    //�^�C�}�[�J�n
                    timer += Time.deltaTime;

                    if(timer >= 3)
                    {
                        mainTurn = MainTurn.END;
                        timer = 0;
                    }
                }
                if(!fast)
                {
                    fast = true;
                }
                break;
            case MainTurn.ENEMY1MOVE:
                break;
            case MainTurn.ENEMY1ANIM:
                break;
            case MainTurn.ENEMY2MOVE:
                break;
            case MainTurn.ENEMY2ANIM:
                break;
            case MainTurn.GAMEOVER:
                timer += Time.deltaTime;

                if (timer >= 2)
                {
                    SceneManager.LoadScene("GameOver");
                    timer = 0;
                }

                break;
            case MainTurn.END:
                break;
        }
        
        RiriMove();
        DhiaMove();
        Enemy1Move();
        Enemy2Move();

        //�����[��HP�����ꂽ��
        if (ririhpdf > ririScript.hp)
        {
            Debug.Log("�����[���U�����󂯂�");
            ririSlider.value -= ((ririSlider.maxValue * (ririScript.hp / ririScript.maxhp)) * Time.deltaTime);

            if(ririSlider.value <= ririScript.hp)
            {
                ririhpdf = ririScript.hp;
                ririSlider.value = ririScript.hp;
            }
        }

        //�����[��HP���񕜂��ꂽ��
        if (ririhpdf < ririScript.hp && ririhpdf != 0)
        {
            Debug.Log("�����[���񕜂���");
            ririSlider.value += ((ririSlider.maxValue * (ririScript.hp / ririScript.maxhp)) * Time.deltaTime);

            if(ririSlider.value >= ririScript.hp)
            {
                ririhpdf = ririScript.hp;
                ririSlider.value = ririScript.hp;
            }
        }

        //�f�B�A��HP�����ꂽ��
        if (dhiahpdf > dhiaScript.hp)
        {
            Debug.Log("�f�B�A���U�����󂯂�");
            dhiaSlider.value -= ((dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp)) * Time.deltaTime);

            if(dhiaSlider.value <= dhiaScript.hp)
            {
                dhiahpdf = dhiaScript.hp;
                dhiaSlider.value = dhiaScript.hp;
            }
        }
        //�f�B�A��HP���񕜂��ꂽ��
        if (dhiahpdf < dhiaScript.hp && dhiahpdf != 0)
        {
            Debug.Log("�f�B�A���񕜂���");
            dhiaSlider.value += ((dhiaSlider.maxValue * (dhiaScript.hp / dhiaScript.maxhp)) * Time.deltaTime);

            if(dhiaSlider.value >= dhiaScript.hp)
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

    int dhiaSlectNomber = 0;
    public void DhiaAtkDefSlect(int number)
    {
        if (!button)
        {
            button = true;
            dhiaSlectNomber = number;
        }
    }

    void RiriMove()
    {
        if (mainTurn == MainTurn.RIRIMOVE)
        {

            command1Text.text = "�q�[��";
            command2Text.text = "�����Ȃ���";
            command3Text.text = "�U���A�b�v";

            commnadImage[0].sprite = ririCommandSp;
            commnadImage[1].sprite = ririCommandSp;
            commnadImage[2].sprite = ririCommandSp;

            //Debug.Log("�����[�̃^�[��");
            if (command1 || command2 || command3)
            {
                //�^�C�}�[�J�n
                if (!command1)
                {
                    timer += Time.deltaTime;
                }


                if (!coLock)
                {
                    if (command1)
                    {
                        ririScript.Skil1();
                    }
                    if (command2)
                    {
                        //�X�e�[�^�X��ύX
                        mainTurn = MainTurn.RIRIANIM;

                        ririScript.Skil2();
                    }
                    if (command3)
                    {
                        //�X�e�[�^�X��ύX
                        mainTurn = MainTurn.RIRIANIM;

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
        if (mainTurn == MainTurn.RIRIANIM)
        {
            //�R�}���h��\���̏���
            enemyFloorRunSysObj.commandMain.SetActive(false);
            enemyFloorRunSysObj.commandWin.SetActive(false);

            //�^�C�}�[�J�n
            timer += Time.deltaTime;

            //�ҋ@���Ԃ𒴂��ēG�������Ă��鎞
            if (timer >= waitTime && !enemyDeath)
            {
                timer = 0;

                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;

                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.DHIAATKDEFSLECT;
            }

        }
    }
    void DhiaMove()
    {
        if (mainTurn == MainTurn.DHIAATKDEFSLECT)
        {
            atkDefSlectWin.SetActive(true);
            enemyFloorRunSysObj.commandWin.SetActive(true);

            if (button)
            {
                //�A�^�b�N�X�L����I��
                if (dhiaSlectNomber == 0)
                {
                    command1Text.text = dhiaScript.atkSkillName[0];
                    command2Text.text = dhiaScript.atkSkillName[1];
                    command3Text.text = dhiaScript.atkSkillName[2];

                    dhiaScript.atkDefSlect = Dhia.AtkDefSlect.ATK;
                }
                //�f�B�t�F���X�X�L����I��
                if (dhiaSlectNomber == 1)
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

        if (mainTurn == MainTurn.DHIAMOVE)
        {

            commnadImage[0].sprite = dhiaCommandSp;
            commnadImage[1].sprite = dhiaCommandSp;
            commnadImage[2].sprite = dhiaCommandSp;

            if (command1 || command2 || command3)
            {
                if (!coLock)
                {
                    if (command1)
                    {
                        dhiaScript.Skil1();
                        //�U���I�����͑Ώۂ�I�΂���
                        if (dhiaSlectNomber == 0)
                        {
                            if (numberRnd != 1)
                            {
                                //�X�e�[�^�X��ύX
                                mainTurn = MainTurn.DHIAANIM;
                            }
                        }
                        //�h�䎞�͑Ώۂ�I�΂��Ȃ�
                        if(dhiaSlectNomber == 1)
                        {
                            //�X�e�[�^�X��ύX
                            mainTurn = MainTurn.DHIAANIM;   
                        }

                    }
                    if (command2)
                    {
                        dhiaScript.Skil2();
                        //�U���I�����͑Ώۂ�I�΂���
                        if (dhiaSlectNomber == 0)
                        {
                            if (numberRnd != 1)
                            {
                                //�X�e�[�^�X��ύX
                                mainTurn = MainTurn.DHIAANIM;
                            }
                        }
                        //�h�䎞�͑Ώۂ�I�΂��Ȃ�
                        if (dhiaSlectNomber == 1)
                        {
                            //�X�e�[�^�X��ύX
                            mainTurn = MainTurn.DHIAANIM;
                        }
                    }
                    if (command3)
                    {
                        dhiaScript.Skil3();
                        //�U���I�����͑Ώۂ�I�΂���
                        if (dhiaSlectNomber == 0)
                        {
                            if (numberRnd != 1)
                            {
                                //�X�e�[�^�X��ύX
                                mainTurn = MainTurn.DHIAANIM;
                            }
                        }
                        //�h�䎞�͑Ώۂ�I�΂��Ȃ�
                        if (dhiaSlectNomber == 1)
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

                windowsMes.text = "�f�B�A�̍s�����ɂイ��傭���Ă�������";
            }
        }
        //�A�j���[�V�����̎���
        if (mainTurn == MainTurn.DHIAANIM)
        {
            //�R�}���h��\���̏���
            enemyFloorRunSysObj.commandMain.SetActive(false);
            enemyFloorRunSysObj.commandWin.SetActive(false);

            //�^�C�}�[�J�n
            timer += Time.deltaTime;

            //�ҋ@���Ԃ𒴂��ēG�������Ă��鎞
            if (timer >= waitTime && !enemyDeath)
            {
                timer = 0;
                button = false;
                coLock = false;
                command1 = false;
                command2 = false;
                command3 = false;

                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.ENEMY1MOVE;
            }
        }
    }
    void Enemy1Move()
    {
        if (mainTurn == MainTurn.ENEMY1MOVE)
        {
            if (enemyScript.deathLook[0])
            {
                mainTurn = MainTurn.ENEMY2MOVE;
                return;
            }

            //�^�C�}�[�J�n
            timer += Time.deltaTime;

            //�X�e�[�^�X��ύX
            //mainTurn = MainTurn.ENEMYANIM;

            if (!coLock)
            {
                //Init���ɑI�����ꂽ�G�l�~�[�̃X�L���֐����Ăяo��
                enemyScript.Move();
                coLock = true;
            }

            //�ҋ@���Ԃ𒴂�����
            if (timer >= waitTime)
            {
                coLock = false;
                timer = 0;

                //�X�e�[�^�X��ύX
                //�G��2�̂̎�
                if (numberRnd == 1)
                {
                    mainTurn = MainTurn.ENEMY2MOVE;
                }
                else
                {
                    mainTurn = MainTurn.RIRIMOVE;
                }
            }
        }
    }
    void Enemy2Move()
    {
        if (mainTurn == MainTurn.ENEMY2MOVE)
        {

            if (numberRnd == 0 || enemyScript.deathLook[1])
            {
                mainTurn = MainTurn.RIRIMOVE;
                return;
            }

            //�^�C�}�[�J�n
            timer += Time.deltaTime;

            if (!coLock)
            {
                //Init���ɑI�����ꂽ�G�l�~�[�̃X�L���֐����Ăяo��
                enemyScript.Move();
                coLock = true;
            }

            //�ҋ@���Ԃ𒴂�����
            if (timer >= waitTime)
            {
                coLock = false;
                timer = 0;

                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.RIRIMOVE;
            }
        }
    }
}
