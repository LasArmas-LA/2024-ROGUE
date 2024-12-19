using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestEncount : MonoBehaviour
{
    enum MainTurn
    {
        WAIT,

        RIRIMOVE,
        RIRIANIM,

        DHIAMOVE,
        DHIAANIM,

        ENEMYMOVE,
        ENEMYANIM,

        END
    }
    MainTurn mainTurn;

    //�o�g���R�}���h�̃e�L�X�g
    [Header("�o�g���R�}���h�̃e�L�X�g")]
    [SerializeField]
    TextMeshProUGUI windowsMes = null;
    [SerializeField]
    TextMeshProUGUI command1Text = null;
    [SerializeField]
    TextMeshProUGUI command2Text = null;
    [SerializeField]
    TextMeshProUGUI command3Text = null;

    //�X�N���v�g�Q��
    [SerializeField]
    Riri ririScript = null;
    [SerializeField]
    Dhia dhiaScript = null;
    [SerializeField]
    Enemy enemyScript = null;

    //�ҋ@����
    [SerializeField]
    float waitTime = 0;
    float timer = 0;

    [Header("�N���X�Q��")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;
    [SerializeField]
    Enemy enemy = null;
    FloorNoSys floorNoSys = null;
    GameObject floorNoSysObj = null;
    [SerializeField]
    EnemyFloorRunSys enemyFloorRunSysObj = null;

    [Space(10)]

    [Header("�̗̓Q�[�W")]
    [SerializeField, Tooltip("�����[�̗̑̓Q�[�W")]
    Slider ririSlider = null;
    [SerializeField, Tooltip("�f�B�A�̗̑̓Q�[�W")]
    Slider dhiaSlider = null;
    [SerializeField, Tooltip("�G�̗̑̓Q�[�W")]
    Slider enemySlider = null;

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
    [SerializeField]
    GameObject enemyObj;


    void Start()
    {
        Init();
    }

    void Init()
    {
        mainTurn = MainTurn.RIRIMOVE;

        //MaxHP�̊i�[
        ririSlider.maxValue = riri.maxhp;
        dhiaSlider.maxValue = dhia.maxhp;
        enemySlider.maxValue = enemy.maxhp;

        //MinHP�̊i�[
        ririSlider.minValue = 0;
        dhiaSlider.minValue = 0;
        enemySlider.minValue = 0;

        //Max��HP�����݂�HP�Ɋi�[
        ririSlider.value = ririSlider.maxValue;
        dhiaSlider.value = dhiaSlider.maxValue;
        enemySlider.value = enemySlider.maxValue;

        //Max��HP�����݂�HP�Ɋi�[
        ririSlider.value *= (riri.hp / riri.maxhp);
        dhiaSlider.value *= (dhia.hp / dhia.maxhp);
        enemySlider.value *= (enemy.hp / enemy.maxhp);

    }

    void FixedUpdate()
    {
        switch (mainTurn)
        {
            case MainTurn.WAIT:
                break;
            case MainTurn.RIRIMOVE:
                break;
            case MainTurn.RIRIANIM:
                break;
            case MainTurn.DHIAMOVE:
                break;
            case MainTurn.DHIAANIM:
                break;
            case MainTurn.ENEMYMOVE:
                break;
            case MainTurn.ENEMYANIM:
                break;
            case MainTurn.END:
                break;
        }
        
        RiriMove();
        DhiaMove();
        EnemyMove();
    }

    //�R�}���h����
    bool command1 = false;
    bool command2 = false;
    bool command3 = false;
    bool button = false;

    public void Command1()
    {
        if(!button)
        {
            button = true;
            command1 = true;
        }
    }
    public void Command2()
    {
        if(!button)
        {
            button = true;
            command2 = true;
        }
    }
    public void Command3()
    {
        if (!button)
        {
            button = true;
            command3 = true;
        }
    }

    void RiriMove()
    {
        if (mainTurn == MainTurn.RIRIMOVE || mainTurn == MainTurn.RIRIANIM)
        {
            windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
            command1Text.text = "�q�[��";
            command2Text.text = "�I�[���q�[��";
            command3Text.text = "�o�C�L���g";

            Debug.Log("�����[�̃^�[��");
            if (command1 || command2 || command3)
            {
                //�^�C�}�[�J�n
                timer += Time.deltaTime;

                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.RIRIANIM;

                if (command1)
                {
                    ririScript.Skil1();
                }
                if (command2)
                {
                    ririScript.Skil2();
                }
                if (command3)
                {
                    ririScript.Skil3();
                }
            }
            //�ҋ@���Ԃ𒴂�����
            if (timer >= waitTime)
            {
                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.DHIAMOVE;
                timer = 0;
                button = false;
                command1 = false;
                command2 = false;
                command3 = false;
            }
        }
    }
    void DhiaMove()
    {
        if (mainTurn == MainTurn.DHIAMOVE || mainTurn == MainTurn.DHIAANIM)
        {
            Debug.Log("�f�B�A�̃^�[��");
            windowsMes.text = "�f�B�A�̍s�����ɂイ��傭���Ă�������";
            command1Text.text = "����";
            command2Text.text = "�h��̐�";
            command3Text.text = "���";

            if (command1 || command2 || command3)
            {
                //�^�C�}�[�J�n
                timer += Time.deltaTime;

                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.DHIAANIM;

                if (command1)
                {
                    dhiaScript.Skil1();
                }
                if (command2)
                {
                    dhiaScript.Skil2();
                }
                if (command3)
                {
                    dhiaScript.Skil3();
                }

                //�ҋ@���Ԃ𒴂�����
                if (timer >= waitTime)
                {
                    //�X�e�[�^�X��ύX
                    mainTurn = MainTurn.ENEMYMOVE;
                    timer = 0;
                    button = false;
                    command1 = false;
                    command2 = false;
                    command3 = false;
                }
            }
        }
    }
    void EnemyMove()
    {
        if (mainTurn == MainTurn.ENEMYMOVE || mainTurn == MainTurn.ENEMYANIM)
        {
            windowsMes.text = "�G�l�~�[�̍U��";

            //�^�C�}�[�J�n
            timer += Time.deltaTime;

            //�X�e�[�^�X��ύX
            mainTurn = MainTurn.ENEMYANIM;

            Debug.Log("�G�l�~�[�̃^�[��");
            enemyScript.Skil();

            //�ҋ@���Ԃ𒴂�����
            if (timer >= waitTime)
            {
                //�X�e�[�^�X��ύX
                mainTurn = MainTurn.RIRIMOVE;
                timer = 0;
            }
        }
    }
}
