using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField]
    Outline[] button = null;
    [SerializeField]
    Image[] buttonImage = null;

    [SerializeField]
    float scrollSpeed = 1;
    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject floorNoSysObj = null;
    [SerializeField]
    FloorNoSys floorNoSys = null;

    //�{�^���̈ʒu��ۑ�
    [SerializeField]
    Vector3[] buttonPos = null;

    
    [SerializeField]
    GameObject[] buttonObj = null;

    [SerializeField]
    GameObject[] buttonKinds = null;

    //�{�^���𐶐�����L�����o�X
    [SerializeField]
    Transform backCanvas = null;

    //�N���[�������{�^�����i�[
    [SerializeField]
    GameObject[] cloneButtonObj = null;

    [SerializeField]
    Button[] cloneButton = null;

    [SerializeField]
    GameObject floorNoSysObjClone = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        //�I�u�W�F�N�g�̏d���`�F�b�N
        if (GameObject.Find("FloorNo") == null)
        {
            //���݂��Ȃ���ΐ�������DontDestroyOnLoad�ŕۑ�
            floorNoSysObjClone = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSysObjClone);

            //�N���[�������I�u�W�F�N�g�̖��O��ύX
            floorNoSysObjClone.name = "FloorNo";
        }
        
        floorNoSys = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();
        

        //�{�^���𐶐����鏈��
        for (int i = 0; i < buttonPos.Length; i++)
        {
            //�w��̃L�����o�X���Ƀ{�^�����N���[�������z��Ɋi�[
            cloneButtonObj[i] = Instantiate(buttonObj[i], buttonPos[i], Quaternion.identity, backCanvas);

            //�킩��₷���悤�ɖ��O��1,2,3,4�c�̂悤�ɕύX
            cloneButtonObj[i].name = (i + 1).ToString();

            cloneButton[i] = cloneButtonObj[i].GetComponent<Button>();

            int ii = i + 0;

            //�{�^���N���b�N���̃C�x���g���֐��Ɩ߂�l�̐ݒ�
            cloneButton[i].onClick.AddListener(() => ButtonChecker((ii)));

            //�A�E�g���C���Ō��݈ʒu�̏���\������̂Ŏ擾
            button[i] = cloneButtonObj[i].GetComponent<Outline>();
        }

    }

    void Update()
    {
        ButtonColorChenge();
        MouseScroll();
    }

    void MouseScroll()
    {
        //�}�E�X�X�N���[��
        if (mainCamera.transform.position.y >= -500 && mainCamera.transform.position.y <= 500)
        {
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.up * scroll * scrollSpeed;
        }
        if (mainCamera.transform.position.y <= -500)
        {
            mainCamera.transform.position = new Vector3(0, -500, -10);
        }
        if (mainCamera.transform.position.y >= 500)
        {
            mainCamera.transform.position = new Vector3(0, 500, -10);
        }
    }

    void ButtonColorChenge()
    {
        button[floorNoSys.slectButtonNo].effectColor = Color.yellow;
        button[floorNoSys.slectButtonNo].effectDistance = new Vector2(10, 10);
    }

    //�ǂ̃{�^���������ꂽ���̔���
    public void ButtonChecker(int buttonNo)
    {
        //�I�΂ꂽ�{�^���̔ԍ���DontDestroyOnLoad�I�u�W�F�N�g�̕ϐ��Ɋi�[
        Debug.Log(buttonNo);
        floorNoSys.slectButtonNo = buttonNo;
    }

    //�V�[���̐؂�ւ�
    public void SceneChenge (int sceneKindsNo)
    {
        //�G
        if (sceneKindsNo == 0)
        {
            SceneManager.LoadScene("EncountScene");
        }
        //�C�x���g
        if(sceneKindsNo == 1)
        {
            SceneManager.LoadScene("Event");
        }
        //�x�e
        if (sceneKindsNo == 2)
        {
            SceneManager.LoadScene("Stay");
        }
        //��
        if (sceneKindsNo == 3)
        {
            SceneManager.LoadScene("Treasure");
        }
        //�{�X
        if (sceneKindsNo == 4)
        {
           SceneManager.LoadScene("Boss");
        }
    }
}
