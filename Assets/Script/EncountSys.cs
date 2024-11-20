using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditorInternal;

public class EncountSys : MonoBehaviour
{
    //�o�g���R�}���h�̃e�L�X�g
    [SerializeField]
    TextMeshProUGUI windowsMes = null;

    bool ririMoveFlag = false;
    bool dhiaMoveFlag = false;
    bool enemyMoveFlag = false;
    bool button = false;

    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;
    [SerializeField]
    Enemy enemy = null;

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    void Init()
    {
        ririMoveFlag = false;
        dhiaMoveFlag = false;
        enemyMoveFlag = false;
        windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
        RiriMove();
    }

    #region ���[�vInit����
    void RiriInit()
    {

    }

    void DhiaInit()
    {

    }
    #endregion

    #region ���[�u����
    void RiriMove()
    {
        Debug.Log("�����[");
        ririMoveFlag = true;
        StartCoroutine(RiriEnterWait());
    }

    void DhiaMove()
    {
        Debug.Log("�f�B�A");
        dhiaMoveFlag = true;
        ririMoveFlag = false;
        windowsMes.text = "�f�B�A�̍s�����ɂイ��傭���Ă�������";
        StartCoroutine(DhiaEnterWait());
    }

    void EnemyMove()
    {
        Debug.Log("�G�l�~�[");
        enemyMoveFlag = true;
        windowsMes.text = "�Ă��̂��������I" + enemy.power + "�̃_���[�W!";
        button = true;
        StartCoroutine(EnemyEnterWait());
    }
    #endregion


    #region �{�^�����莞����
    public void AttackButton()
    {
        button = true;
        if(ririMoveFlag)
        {
            windowsMes.text = "�����[�̂��������I" + riri.power + "�̃_���[�W!";
            StartCoroutine(RiriEnterWait());
        }
        if(dhiaMoveFlag)
        {
            windowsMes.text = "�f�B�A�̂��������I" + riri.power + "�̃_���[�W!";
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
        if (button)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            ririMoveFlag = false;
            button = false;
            DhiaMove();
        }
    }
    IEnumerator DhiaEnterWait()
    {
        if (button)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            dhiaMoveFlag = false;
            button = false;
            EnemyMove();
        }
    }
    IEnumerator EnemyEnterWait()
    {
        if (button)
        {
            yield return new WaitForSeconds(1.5f);
            windowsMes.text = "�����[�̍s�����ɂイ��傭���Ă�������";
            button = false;
            enemyMoveFlag = false;
            RiriMove();
        }
    }
    #endregion
}
