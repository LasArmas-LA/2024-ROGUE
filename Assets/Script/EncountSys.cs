using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditorInternal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class EncountSys : MonoBehaviour
{
    //�o�g���R�}���h�̃e�L�X�g
    [SerializeField]
    TextMeshProUGUI windowsMes = null;
    [SerializeField]
    TextMeshProUGUI command1Text = null;
    [SerializeField]
    TextMeshProUGUI command2Text = null;
    [SerializeField]
    TextMeshProUGUI command3Text = null;

    //Move�t���O
    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;

    //�x�e�K�̃t���O
    public bool restFlag = false;

    //�{�X�K�̃t���O
    public bool bossFlag = false;

    bool fastMove = false;

    //�{�^���A�����͗}���p
    bool button = false;

    //�o�C�L���g��Ԃ̔���
    bool powerUpFlag = false;

    //�f�B�A�̎���Ԕ���
    bool defenseFlag = false;

    //�f�B�A�̃����[����Ԕ���
    bool ririDefenseFlag = false;


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

    //�̗̓Q�[�W��Obj
    [SerializeField]
    Slider ririSlider = null;
    [SerializeField]
    Slider dhiaSlider = null;
    [SerializeField]
    Slider enemySlider = null;

    //�����[,�f�B�A,�G�l�~�[��Obj
    [SerializeField]
    GameObject ririObj;
    [SerializeField]
    GameObject dhiaObj;
    [SerializeField]
    GameObject enemyObj;


    void Awake()
    {
        Init();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    #region Init����
    void Init()
    {
        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();

        //�x�e�t���A�t���O�I��
        if (floorNoSys.floorNo % 5 == 0 && floorNoSys.floorNo != 0)
        {
            restFlag = true;
        }
        else
        {
            restFlag = false;
        }
        if (floorNoSys.floorNo % 10 == 0 && floorNoSys.floorNo != 0)
        {
            bossFlag = true;
        }
        else
        {
            bossFlag = false;
        }

        //���[�u�t���O�̏�����
        ririMoveFlag = false;
        dhiaMoveFlag = false;
        enemyMoveFlag = false;

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

        fastMove = true;
        //windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
        //RiriMove();
    }
    #endregion

    #region ���[�vInit����
    void RiriInit()
    {

    }

    void DhiaInit()
    {

    }
    #endregion

    #region ���[�u����
    public void RiriMove()
    {
        command1Text.text = "�q�[��";
        command2Text.text = "�I�[���q�[��";
        command3Text.text = "�o�C�L���g";

        if (enemy.deathFlag)
        {
            windowsMes.text = "�G��|�����I";
            enemyObj.SetActive(false);
        }
        else
        {
            if(!ririMoveFlag && fastMove)
            {
                if(bossFlag)
                {
                    windowsMes.text = "�{�X�����ꂽ�I";
                }
                else
                {
                    windowsMes.text = "�G�����ꂽ�I";
                }
            }
            Debug.Log("�����[");
            ririMoveFlag = true;
            StartCoroutine(RiriEnterWait());
        }
    }

    void DhiaMove()
    {
        command1Text.text = "����";
        command2Text.text = "�h��̐�";
        command3Text.text = "���";
        ririDefenseFlag = false;
        defenseFlag = false;

        if (enemy.deathFlag)
        {
            windowsMes.text = "�G��|�����I";
            Invoke("EnemyDeat", 1.0f);
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("�f�B�A");
            dhiaMoveFlag = true;
            ririMoveFlag = false;
            windowsMes.text = "�f�B�A�̍s�����ɂイ��傭���Ă�������";
            button = false;
            StartCoroutine(DhiaEnterWait());
        }
    }

    void EnemyMove()
    {
        if (enemy.deathFlag)
        {
            windowsMes.text = "�G��|�����I";
            Invoke("EnemyDeat", 1.0f);
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("�G�l�~�[");
            enemyMoveFlag = true;
            if(defenseFlag)
            {
                windowsMes.text = "�Ă��̂��������I" + enemy.power * 0.5f + "�̃_���[�W!";
                dhia.hp -= (enemy.power * 0.5f);
            }
            else
            {
                windowsMes.text = "�Ă��̂��������I" + enemy.power + "�̃_���[�W!";
                dhia.hp -= enemy.power;
            }
            ririSlider.value *= (riri.hp / riri.maxhp);
            dhiaSlider.value *= (dhia.hp / dhia.maxhp);
            button = true;
            StartCoroutine(EnemyEnterWait());
        }
    }
    #endregion


    #region ���S����

    void EnemyDeat()
    {
        enemyFloorRunSysObj.battleEndFlag = true;
        bool enemyDeat = false;
        if(!enemyDeat)
        {
            //floorNoSys.floorNo += 1;
            enemyDeat = true;
        }
    }

    void RiriDeath()
    {

    }

    void DhiaDeath()
    {

    }

    #endregion

    #region �{�^�����莞����
    public void Command1Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if(dhia.maxhp < dhia.hp + 50)
            {
                windowsMes.text = "�����[�̓q�[�����������I\n" + "�f�B�A" + "��HP��"+  (dhia.maxhp - dhia.hp)  +"�񕜂���!";
                dhia.hp = dhia.maxhp;
                dhiaSlider.value = dhiaSlider.maxValue;
            }
            else
            {
                windowsMes.text = "�����[�̓q�[�����������I\n" + "�Z�Z" + "��HP��50�񕜂���!";
                dhia.hp += 50;
                dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
            }
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if(powerUpFlag)
            {
                windowsMes.text = "�f�B�A�̂��������I" + dhia.power * 1.5f + "�̃_���[�W!";
                enemy.hp -= (dhia.power * 1.5f);
                enemySlider.value *= (enemy.hp / enemy.maxhp);
                powerUpFlag = false;
            }
            else
            {
                windowsMes.text = "�f�B�A�̂��������I" + dhia.power + "�̃_���[�W!";
                enemy.hp -= dhia.power;
                enemySlider.value *= (enemy.hp / enemy.maxhp);
            }
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    public void Command2Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "�����[�̓I�[���q�[�����������I\n2�l��HP��20���񕜂���!";
            riri.hp += 20;
            dhia.hp += 20;
            ririSlider.value = (ririSlider.maxValue * (riri.hp / riri.maxhp));
            dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "�f�B�A�͐g������Ă���B";
            defenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    public void Command3Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "�����[�̓o�C�L���g���������I\n�f�B�A�̍U���͂��㏸����!";
            powerUpFlag = true;
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "�f�B�A�̓����[������Ă���B";
            ririDefenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    #endregion


    #region �s����̑ҋ@����
    IEnumerator RiriEnterWait()
    {
        yield return new WaitForSeconds(2.0f);
        if(fastMove)
        {
            Debug.Log("�����[�G���^�[�E�F�C�g");
            windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
            fastMove = false;
        }
        if (button)
        {
            ririMoveFlag = false;
            button = false;
            DhiaMove();
        }
    }
    IEnumerator DhiaEnterWait()
    {
        yield return new WaitForSeconds(2.0f);

        if (button && dhiaMoveFlag)
        {
            dhiaMoveFlag = false;
            button = false;
            EnemyMove();
        }
    }
    IEnumerator EnemyEnterWait()
    {
        yield return new WaitForSeconds(2.0f);

        windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
        button = false;
        enemyMoveFlag = false;
        RiriMove();
    }
    #endregion
}
