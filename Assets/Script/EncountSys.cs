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
            windowsMes.text = "�Ă��̂��������I" + enemy.power + "�̃_���[�W!";
            dhia.hp -= enemy.power;
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
    public void AttackButton()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "�����[�̂��������I" + riri.power + "�̃_���[�W!";
            enemy.hp -= riri.power;
            enemySlider.value *= (enemy.hp / enemy.maxhp);
            button = true;
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            windowsMes.text = "�f�B�A�̂��������I" + dhia.power + "�̃_���[�W!";
            enemy.hp -= dhia.power;
            enemySlider.value *= (enemy.hp / enemy.maxhp);
            button = true;
            StartCoroutine(DhiaEnterWait());
        }
    }
    public void DefenseButton()
    {
        button = true;
        if(ririMoveFlag)
        {
            windowsMes.text = "�����[�͂ڂ����債��!";
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag)
        {
            windowsMes.text = "�f�B�A�͂ڂ����債��!";
            StartCoroutine(DhiaEnterWait());
        }
    }

    #endregion


    #region �s����̑ҋ@����
    IEnumerator RiriEnterWait()
    {
        yield return new WaitForSeconds(1.0f);
        if(fastMove)
        {
            windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
            fastMove = false;
        }
        if (button)
        {
            ririMoveFlag = false;
            DhiaMove();
            button = false;
        }
    }
    IEnumerator DhiaEnterWait()
    {
        if (button)
        {
            yield return new WaitForSeconds(1.0f);
            dhiaMoveFlag = false;
            button = false;
            EnemyMove();
        }
    }
    IEnumerator EnemyEnterWait()
    {
        if (button)
        {
            yield return new WaitForSeconds(1.0f);
            windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
            RiriMove();
            button = false;
            enemyMoveFlag = false;
        }
    }
    #endregion
}
