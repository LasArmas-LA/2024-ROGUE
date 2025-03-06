using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading;

public class EncountSys : MonoBehaviour
{
   /* //�o�g���R�}���h�̃e�L�X�g
    [Header("�o�g���R�}���h�̃e�L�X�g")]
    [SerializeField]
    TextMeshProUGUI windowsMes = null;
    [SerializeField]
    TextMeshProUGUI command1Text = null;
    [SerializeField]
    TextMeshProUGUI command2Text = null;
    [SerializeField]
    TextMeshProUGUI command3Text = null;

    [SerializeField]
    GameObject recoveryWin = null;

    [Space(10)]

    //Move�t���O
    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;

    //�ΏۑI�����̃t���O
    bool ririSelectFlag = false;
    bool dhiaSelectFlag = false;

    //�x�e�K�̃t���O
    [NonSerialized]
    public bool restFlag = false;

    //�{�X�K�̃t���O
    [NonSerialized]
    public bool bossFlag = false;

    //����^�[���t���O
    bool fastMove = false;

    //�{�^���A�����͗}���p
    bool button = false;

    //�o�C�L���g��Ԃ̔���
    bool powerUpFlag = false;

    //�f�B�A�̎���Ԕ���
    bool defenseFlag = false;

    //�f�B�A�̃����[����Ԕ���
    bool ririDefenseFlag = false;

    //�^�[���؂�ւ��̑ҋ@����
    [Header("�^�[���؂�ւ��ҋ@����")]
    [SerializeField, Tooltip("�����[�̃^�[���؂�ւ��ҋ@����")]
    float ririWaitTime = 0f;
    [SerializeField, Tooltip("�f�B�A�̃^�[���؂�ւ��ҋ@����")]
    float DhiaWaitTime = 0f;
    [SerializeField, Tooltip("�G�l�~�[�̃^�[���؂�ւ��ҋ@����")]
    float enemyWaitTime = 0f;
    
    [Space(10)]

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

    //�̗̓Q�[�W��Obj
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

    private enum GameState
    { 
        WAIT,

        RIRI_TRUN,
        RIRI_ANIMATION,

        DHIA_TRUN,
        DHIA_ANIMATION,

        ENEMY_TRUN,
        ENEMY_ANIMATION,
        
        RESULT,
        GAME_END
    }
    GameState gameState;

    private enum Command
    {
        Command1,
        Command2,
        Command3,
    }
    Command playerCommand;

    [SerializeField]
    [Tooltip("�R�}���h�I�����̕\���e�L�X�g")]
    String[] texts1 = null;

    void Awake()
    {

    }
    void Start()
    {
        Init();
    }

    private float waitT;

    void Update()
    {
        switch (gameState)
        {
            case GameState.WAIT:

                break;
            case GameState.RIRI_TRUN:
                // �L�[���͑҂�

                break;
            case GameState.RIRI_ANIMATION:
                // �ҋ@����
                waitT += Time.deltaTime;
                if(waitT > 100){
                    gameState = GameState.ENEMY_TRUN;
                }
                break;
            case GameState.DHIA_TRUN:
                break;
            case GameState.DHIA_ANIMATION:
                break;
            case GameState.ENEMY_TRUN:
                break;
            case GameState.ENEMY_ANIMATION:
                break;
            case GameState.RESULT:
                gameState = GameState.GAME_END;
                // �v���C���[��HP��0��������
                // �Q�[���I�[�o�[
                Debug.Log(texts1[(int)Command.Command1]);

                break;
        }
    }

    #region Init����
    void Init()
    {
        // VSyncCount �� Dont Sync �ɕύX
        QualitySettings.vSyncCount = 0;
        // 60fps��ڕW�ɐݒ�
        Application.targetFrameRate = 60;

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
        //�{�X�t���A�t���O�I��
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

        //Max��HP�����݂�HP�Ɋi�[
        ririSlider.value *= (riri.hp / riri.maxhp);
        dhiaSlider.value *= (dhia.hp / dhia.maxhp);
        enemySlider.value *= (enemy.hp / enemy.maxhp);

        fastMove = true;
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
        gameState = GameState.RIRI_TRUN;

        if (riri.hp <= 0)
        {
           RiriDeath();
        }
        if(dhia.hp <= 0)
        {
           DhiaDeath();
        }

        command1Text.text = "�q�[��";
        command2Text.text = "�I�[���q�[��";
        command3Text.text = "�o�C�L���g";
        if(riri.deathFlag)
        {
            //�����[�����S���Ă���ꍇ�^�[�����X�L�b�v
            DhiaMove();
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
            return;
        }
    }

    void DhiaMove()
    {
        gameState = GameState.DHIA_TRUN;
        if (dhia.deathFlag)
        {
            //�f�B�A�����S���Ă���ꍇ�f�B�A�̃^�[�����X�L�b�v
            EnemyMove();
        }
        else
        {
            Debug.Log("�f�B�A");
            //�R�}���h�̃e�L�X�g����
            command1Text.text = "����";
            command2Text.text = "�h��̐�";
            command3Text.text = "���";

            //���̃t���O��������
            ririDefenseFlag = false;
            defenseFlag = false;

            dhiaMoveFlag = true;
            ririMoveFlag = false;
            windowsMes.text = "�f�B�A�̍s�����ɂイ��傭���Ă�������";
            button = false;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }

    void EnemyMove()
    {
        gameState = GameState.ENEMY_TRUN;
        if (enemyMoveFlag) 
        {
            windowsMes.text = "�G��|�����I";
            Invoke("EnemyDeath", 1.0f);
            enemyObj.SetActive(false);
        }
        else
        {
            Debug.Log("�G�l�~�[");
            enemyMoveFlag = true;

            int rnd = 0;
            for (int i = 0; i < 5;i++)
            {
                rnd = UnityEngine.Random.Range(0, 2);
            }
            if(dhiaDeath)
            {
                rnd = 0;
            }

            //�U���Ώۃ����[
            if(rnd == 0)
            {
                //70%�y��
                if (ririDefenseFlag)
                {
                    windowsMes.text = "�Ă��̂��������I�f�B�A�������[��������I�f�B�A��" + enemy.power * 0.3f + "�̃_���[�W!";
                    dhia.hp -= (enemy.power * 0.3f);
                }
                else
                {
                    windowsMes.text = "�Ă��̂��������I�����[��" + enemy.power + "�̃_���[�W!";
                    riri.hp -= enemy.power;
                }

            }
            //�U���Ώۃf�B�A
            else if (rnd == 1)
            {
                if (defenseFlag)
                {
                    windowsMes.text = "�Ă��̂��������I�f�B�A��" + enemy.power * 0.5f + "�̃_���[�W!";
                    dhia.hp -= (enemy.power * 0.5f);
                }
                else
                {
                    windowsMes.text = "�Ă��̂��������I�f�B�A��" + enemy.power + "�̃_���[�W!";
                    dhia.hp -= enemy.power;
                }
            }
            ririSlider.value *= (riri.hp / riri.maxhp);
            dhiaSlider.value *= (dhia.hp / dhia.maxhp);
            button = true;
            StartCoroutine(EnemyEnterWait());
            return;
        }
    }
    #endregion


    #region ���S����

    void EnemyDeath()
    {
        enemyFloorRunSysObj.battleEndFlag = true;
    }

    void RiriDeath()
    {
        enemyFloorRunSysObj.gameOverFlag = true;
    }

    void DhiaDeath()
    {
        dhiaDeath = true;
        dhiaObj.SetActive(false);
    }

    #endregion

    #region �{�^�����莞����
    public void Command1Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.RIRI_TRUN) { return; }

            windowsMes.text = "�񕜑Ώۂ�I��ł�������";

            if (ririSelectFlag || dhiaSelectFlag)
            {
                if (ririSelectFlag)
                {
                    if (riri.maxhp < riri.hp + 50)
                    {
                        Debug.Log("�R�}���h1�����[HP�}�b�N�X��");
                        windowsMes.text = "�����[�̓q�[�����������I\n" + "�����[" + "��HP��" + (riri.maxhp - riri.hp) + "�񕜂���!";
                        riri.hp = riri.maxhp;
                        ririSlider.value = ririSlider.maxValue;
                    }
                    else
                    {
                        Debug.Log("�R�}���h1�����[HP������");
                        windowsMes.text = "�����[�̓q�[�����������I\n" + "�����[" + "��HP��50�񕜂���!";
                        riri.hp += 50;
                        ririSlider.value = (ririSlider.maxValue * (riri.hp / riri.maxhp));
                    }
                }
                if (dhiaSelectFlag)
                {
                    if (dhia.maxhp < dhia.hp + 50)
                    {
                        Debug.Log("�R�}���h1�����[HP�}�b�N�X��");
                        windowsMes.text = "�����[�̓q�[�����������I\n" + "�f�B�A" + "��HP��" + (dhia.maxhp - dhia.hp) + "�񕜂���!";
                        dhia.hp = dhia.maxhp;
                        dhiaSlider.value = dhiaSlider.maxValue;
                    }
                    else
                    {
                        Debug.Log("�R�}���h1�����[HP������");
                        windowsMes.text = "�����[�̓q�[�����������I\n" + "�f�B�A" + "��HP��50�񕜂���!";
                        dhia.hp += 50;
                        dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
                    }
                    dhiaSelectFlag = false;
                }
                button = true;
                StartCoroutine(RiriEnterWait());
                return;
            }
            else
            {
                recoveryWin.SetActive(true);
            }
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.DHIA_TRUN) { return; }

            if (powerUpFlag)
            {
                Debug.Log("�R�}���h1�f�B�A�p���[�A�b�v�U��");

                windowsMes.text = "�f�B�A�̂��������I" + dhia.power * 1.5f + "�̃_���[�W!";
                enemy.hp -= (dhia.power * 1.5f);
                enemySlider.value *= (enemy.hp / enemy.maxhp);
                powerUpFlag = false;
            }
            else
            {
                Debug.Log("�R�}���h1�f�B�A�ʏ�U��");
                windowsMes.text = "�f�B�A�̂��������I" + dhia.power + "�̃_���[�W!";
                enemy.hp -= dhia.power;
                enemySlider.value *= (enemy.hp / enemy.maxhp);
            }
            button = true;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }
    public void Command2Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.RIRI_TRUN) { return; }

            Debug.Log("�R�}���h2�����[");
            
            if(riri.maxhp > riri.hp + 20 && dhia.maxhp > dhia.hp + 20)
            {
                riri.hp += 20;
                dhia.hp += 20;
                windowsMes.text = "�����[�̓I�[���q�[�����������I\n�����[�ƃf�B�A��HP��20���񕜂���!";
            }
            else
            {
                if(riri.maxhp < riri.hp + 20)
                {
                    riri.hp = riri.maxhp;
                }
                if(dhia.maxhp < dhia.hp + 20)
                {
                    dhia.hp = dhia.maxhp;
                }
                windowsMes.text = "�����[�̓I�[���q�[�����������I\n�����[��HP��"+ (riri.maxhp - riri.hp) + "�f�B�A��HP��"+ (dhia.maxhp - dhia.hp) + "�񕜂���!";
            }
            ririSlider.value = (ririSlider.maxValue * (riri.hp / riri.maxhp));
            dhiaSlider.value = (dhiaSlider.maxValue * (dhia.hp / dhia.maxhp));
            button = true;
            StartCoroutine(RiriEnterWait());
            return;
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.DHIA_TRUN) { return; }

            Debug.Log("�R�}���h2�f�B�A");
            windowsMes.text = "�f�B�A�͐g������Ă���B";
            defenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }
    public void Command3Button()
    {
        if(ririMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.RIRI_TRUN) { return; }

            Debug.Log("�R�}���h3�����[");
            windowsMes.text = "�����[�̓o�C�L���g���������I\n�f�B�A�̍U���͂��㏸����!";
            powerUpFlag = true;
            button = true;
            StartCoroutine(RiriEnterWait());
            return;
        }
        if(dhiaMoveFlag && !button && !fastMove)
        {
            if (gameState != GameState.DHIA_TRUN) { return; }

            Debug.Log("�R�}���h3�f�B�A");
            windowsMes.text = "�f�B�A�̓����[������Ă���B";
            ririDefenseFlag = true;
            button = true;
            StartCoroutine(DhiaEnterWait());
            return;
        }
    }

    public void RiriSlect()
    {
        if(gameState != GameState.RIRI_TRUN) { return; }

        ririSelectFlag = true;
        recoveryWin.SetActive(false);
        Command1Button();
    }
    public void DhiaSlect()
    {
        if (gameState != GameState.RIRI_TRUN) { return; }

        dhiaSelectFlag = true;
        recoveryWin.SetActive(false);
        Command1Button();
    }
    #endregion

    public void Result(int no)
    {
        switch (playerCommand)
        { 
            case Command.Command1:
                // �U��
                gameState = GameState.RIRI_ANIMATION;
                // �R���[�`���J�n
                

                break;
            case Command.Command2:
                // �h��

                break;
        }
    }


    #region �s����̑ҋ@����
    IEnumerator RiriEnterWait()
    {
        if (gameState != GameState.RIRI_TRUN) { yield return null; }

        yield return new WaitUntil(() => ririMoveFlag);
        yield return new WaitForSeconds(ririWaitTime);
        if (fastMove)
        {
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
        if (gameState != GameState.DHIA_TRUN) { yield return null; }

        yield return new WaitUntil(() => dhiaMoveFlag);
        yield return new WaitForSeconds(DhiaWaitTime);

        if (button && dhiaMoveFlag)
        {
            dhiaMoveFlag = false;
            EnemyMove();
            button = false;
        }
    }
    IEnumerator EnemyEnterWait()
    {
        if (gameState != GameState.ENEMY_TRUN) { yield return null; }

        yield return new WaitForSeconds(enemyWaitTime);

        windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
        enemyMoveFlag = false;
        RiriMove();
        button = false;
    }
    #endregion
   */
}
