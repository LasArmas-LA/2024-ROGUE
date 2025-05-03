using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSys : MonoBehaviour
{
    enum eMapMode
    {
        Stay,
        Disp,
        MoveWait,
        End
    }

    
    [Header("��������{�^���̍��W���w�肵�Ă�������"),SerializeField]
    private Vector3[] buttonPos = null;

    [Header("�}�b�v�̃X�N���[���X�s�[�h���w�肵�Ă�������"), SerializeField]
    float scrollSpeed = 1;


    [Space(20)]

    [Tooltip("�N���[���������ꂽ�{�^���I�u�W�F�N�g���i�[����L�����o�X���w�肵�Ă�������"), SerializeField]
    private Transform buttonCanvas = null;

    [Tooltip("�M�Y���\���p�̃}�b�v�A�C�R�����w�肵�Ă�������"), SerializeField]
    private Texture mapTexture = null;

    [Tooltip("�{�^���̃Q�[���I�u�W�F�N�g���w�肵�Ă�������"), SerializeField]
    private List<ButtonList> buttonNoList = new List<ButtonList>();

    [SerializeField]
    int buttonListNo;

    [Tooltip("�N���[���������ꂽ�{�^���̃I�u�W�F�N�g���i�[����Ă��܂�")]
    private GameObject[] cloneButtonObj = null;
    [Tooltip("�N���[�����ꂽ�{�^���I�u�W�F�N�g�̃{�^���R���|�[�l���g���i�[����Ă��܂�")]
    private Button[] cloneButton = null;

    [Tooltip("���C���J�������w�肵�Ă�������"),SerializeField]
    GameObject mainCamera;
    [Tooltip("�J����Y���̂̍ő�ړ����W���w�肵�Ă�������"), SerializeField]
    int cameraYMoveMax = 0;
    [Tooltip("�J����Y���̂̍ŏ��ړ����W���w�肵�Ă�������"), SerializeField]
    int cameraYMoveMin = 0;


    [System.Serializable]
    public class ButtonList
    {
        public List<GameObject> buttonObjList = new List<GameObject>();
    }

    void Start()
    {
        Init();
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        //�z��̃T�C�Y���g��
        Array.Resize(ref cloneButtonObj, buttonPos.Length);
        Array.Resize(ref cloneButton, buttonPos.Length);

        InitDisp();
        InitLimited();
    }

    /// <summary>
    /// �}�b�v�̕\������������
    /// </summary>
    private void InitDisp()
    {
        //�{�^���𐶐����鏈��
        for (int i = 0; i < buttonPos.Length; i++)
        {
            //�w��̃L�����o�X���Ƀ{�^�����N���[�������z��Ɋi�[
            cloneButtonObj[i] = Instantiate(buttonNoList[buttonListNo].buttonObjList[i], buttonPos[i], Quaternion.identity, buttonCanvas);

            //�킩��₷���悤�ɖ��O��1,2,3,4�c�̂悤�ɕύX
            cloneButtonObj[i].name = (i).ToString();

            cloneButton[i] = cloneButtonObj[i].GetComponent<Button>();

            int ii = i + 0;

            //�{�^���N���b�N���̃C�x���g���֐��Ɩ߂�l�̐ݒ�
            cloneButton[i].onClick.AddListener(() => ButtonChecker((ii)));
        }
    }

    //��
    FloorNoSys floorNoSys;
    int[] limitlessNo;
    int slectButtonNo;

    /// <summary>
    /// �}�b�v�Ŏ��ɐi�߂�ꏊ�̐��������鏈��
    /// </summary>
    private void InitLimited()
    {
        slectButtonNo = PlayerPrefs.GetInt("FloorNo", -1);

        //��U�{�^����S�ĉ����Ȃ����鏈��
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
        }

        #region
        if (slectButtonNo == -1)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 0;
        }
        if (slectButtonNo == 0)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 1;
            limitlessNo[1] = 2;
        }
        if (slectButtonNo == 1)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 3;
            limitlessNo[1] = 4;
        }
        if (slectButtonNo == 2)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 5;
        }
        if (slectButtonNo == 3)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 6;
        }
        if (slectButtonNo == 4)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 6;
        }
        if (slectButtonNo == 5)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 7;
        }
        if (slectButtonNo == 6)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 8;
        }
        if (slectButtonNo == 7)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 9;
        }
        if (slectButtonNo == 8)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 10;
            limitlessNo[1] = 11;
        }
        if (slectButtonNo == 9)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 11;
            limitlessNo[1] = 12;
        }
        if (slectButtonNo == 10)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 13;
            limitlessNo[1] = 14;
        }
        if (slectButtonNo == 11)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 14;
            limitlessNo[1] = 15;
        }
        if (slectButtonNo == 12)
        {
            limitlessNo = new int[2];

            limitlessNo[0] = 15;
            limitlessNo[1] = 16;
        }
        if (slectButtonNo == 13)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 17;
        }
        if (slectButtonNo == 14)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 17;
        }
        if (slectButtonNo == 15)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 18;
        }
        if (slectButtonNo == 16)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 18;
        }
        if (slectButtonNo == 17)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 19;
        }
        if (slectButtonNo == 18)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 19;
        }
        if (slectButtonNo == 19)
        {
            limitlessNo = new int[1];

            limitlessNo[0] = 20;
        }
        #endregion

        //���ɐi�߂�{�^����������悤�ɂ��鏈��
        for (int k = 0; k < limitlessNo.Length; ++k)    
        {
            cloneButton[limitlessNo[k]].interactable = true;
        }
    }

    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂�܂�
    /// </summary>
    /// <param name="buttonNo">�{�^�����Ƃɐݒ肳�ꂽ���ʔԍ�</param>
    private void ButtonChecker(int buttonNo)
    {
        PlayerPrefs.SetInt("FloorNo", buttonNo);

        

        //��U�{�^����S�ĉ����Ȃ����鏈��
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
        }

    }

    void Update()
    {
        MouseScroll();
    }

    /// <summary>
    /// �}�b�v�̃}�E�X�X�N���[������
    /// </summary>
    void MouseScroll()
    {
        //�}�E�X�X�N���[��
        if (mainCamera.transform.position.y >= cameraYMoveMin && mainCamera.transform.position.y <= cameraYMoveMax)
        {
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.up * scroll * scrollSpeed;
        }

        Vector3 cameraPos = mainCamera.transform.position;

        //�ړ��ł���͈͂𐧌�
        if (mainCamera.transform.position.y <= cameraYMoveMin)
        {
            cameraPos.y = cameraYMoveMin;
        }
        if (mainCamera.transform.position.y >= cameraYMoveMax)
        {
            cameraPos.y = cameraYMoveMax;
        }

        mainCamera.transform.position = cameraPos;
    }


    private void OnDrawGizmos()
    {
        for(int i = 0; i < buttonPos.Length;++i)
        {
            Gizmos.DrawGUITexture(new Rect(buttonPos[i].x-2, buttonPos[i].y+2, 6, -6), mapTexture);
        }
    }
}
