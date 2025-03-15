using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    enum MapMode
    {
        CameraMove,
        Wait,
        End
    }
    //���̉�
    MapMode mapMode = MapMode.CameraMove;

    [Space(10), Header("�{�^��")]

    [SerializeField]
    Outline[] button = null;
    [SerializeField]
    Image[] buttonImage = null;

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

    //�I�ׂ�}�X�A�I�ׂȂ��}�X�𐧌䂷�鏈��
    int[] limitlessNo = null;


    [Space(10), Header("�}�E�X����")]
    [SerializeField]
    float scrollSpeed = 1;


    [Space(10),Header("�J����")]
    //���C���J�����̃Q�[���I�u�W�F�N�g
    [SerializeField]
    GameObject mainCamera;
    //�J����Y���ړ��̍ő���W
    [SerializeField]
    int cameraYMoveMax = 0;
    //�J����Y���ړ��̍ŏ����W
    [SerializeField]
    int cameraYMoveMin = 0;

    [Space(10), Header("�K�w�f�[�^")]

    [SerializeField]
    GameObject floorNoSysObj = null;
    [SerializeField]
    FloorNoSys floorNoSys = null;

    [SerializeField]
    GameObject floorNoSysObjClone = null;

    [Space(10), Header("�t�F�[�h")]

    //�t�F�[�h�p
    [SerializeField]
    Animator fadeAnim = null;

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

        floorNoSys.floorCo++;

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

        //��U�{�^����S�ĉ����Ȃ����鏈��
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
            cloneButton[i].GetComponent<Image>().color = Color.clear;
        }

        //�i�߂�ꏊ���i�[
        {
            if (floorNoSys.slectButtonNo == -1)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 0;
            }
            if (floorNoSys.slectButtonNo == 0)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 1;
                limitlessNo[1] = 2;
            }
            if (floorNoSys.slectButtonNo == 1)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 3;
                limitlessNo[1] = 4;
            }
            if (floorNoSys.slectButtonNo == 2)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 5;
            }
            if (floorNoSys.slectButtonNo == 3)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 4)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 5)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 7;
            }
            if (floorNoSys.slectButtonNo == 6)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 8;
            }
            if (floorNoSys.slectButtonNo == 7)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 9;
            }
            if (floorNoSys.slectButtonNo == 8)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 10;
                limitlessNo[1] = 11;
            }
            if (floorNoSys.slectButtonNo == 9)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 11;
                limitlessNo[1] = 12;
            }
            if (floorNoSys.slectButtonNo == 10)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 13;
                limitlessNo[1] = 14;
            }
            if (floorNoSys.slectButtonNo == 11)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 14;
                limitlessNo[1] = 15;
            }
            if (floorNoSys.slectButtonNo == 12)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 15;
                limitlessNo[1] = 16;
            }
            if (floorNoSys.slectButtonNo == 13)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 14)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 15)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 16)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 17)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 18)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 19)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 20;
            }
        }


        //���ɐi�߂�{�^����������悤�ɂ��鏈��
        for (int k = 0; k < limitlessNo.Length; k++)
        {
            cloneButton[limitlessNo[k]].interactable = true;
            cloneButton[limitlessNo[k]].GetComponent<Image>().color = Color.yellow;
        }


    }

    void Update()
    {
        switch(mapMode)
        {
            case MapMode.CameraMove:
                CameraMove();
                break;
            case MapMode.Wait:
                MouseScroll();
                break;
            case MapMode.End:
                break;
        }
    }

    float cameraSpeed = 100f;
    void CameraMove()
    {
        if(mainCamera.transform.position.y <= cloneButtonObj[limitlessNo[0]].transform.position.y && mainCamera.transform.position.y < 500)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.y += cameraSpeed * Time.deltaTime;
            mainCamera.transform.position = cameraPos;

            cameraSpeed += 350f * Time.deltaTime;
        }
        else 
        {
            mapMode = MapMode.Wait;
        }
    }

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

    //�ǂ̃{�^���������ꂽ���̔���
    public void ButtonChecker(int buttonNo)
    {
        //�I�΂ꂽ�{�^���̔ԍ���DontDestroyOnLoad�I�u�W�F�N�g�̕ϐ��Ɋi�[
        Debug.Log(buttonNo);
        floorNoSys.slectButtonNo = buttonNo;

        //��U�{�^����S�ĉ����Ȃ����鏈��
        for (int i = 0; i < buttonPos.Length; i++)
        {
            cloneButton[i].interactable = false;
        }

        //�i�߂�ꏊ���i�[
        {
            if (floorNoSys.slectButtonNo == -1)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 0;
            }
            if (floorNoSys.slectButtonNo == 0)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 1;
                limitlessNo[1] = 2;
            }
            if (floorNoSys.slectButtonNo == 1)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 3;
                limitlessNo[1] = 4;
            }
            if (floorNoSys.slectButtonNo == 2)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 5;
            }
            if (floorNoSys.slectButtonNo == 3)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 4)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 6;
            }
            if (floorNoSys.slectButtonNo == 5)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 7;
            }
            if (floorNoSys.slectButtonNo == 6)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 8;
            }
            if (floorNoSys.slectButtonNo == 7)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 9;
            }
            if (floorNoSys.slectButtonNo == 8)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 10;
                limitlessNo[1] = 11;
            }
            if (floorNoSys.slectButtonNo == 9)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 11;
                limitlessNo[1] = 12;
            }
            if (floorNoSys.slectButtonNo == 10)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 13;
                limitlessNo[1] = 14;
            }
            if (floorNoSys.slectButtonNo == 11)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 14;
                limitlessNo[1] = 15;
            }
            if (floorNoSys.slectButtonNo == 12)
            {
                limitlessNo = new int[2];

                limitlessNo[0] = 15;
                limitlessNo[1] = 16;
            }
            if (floorNoSys.slectButtonNo == 13)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 14)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 17;
            }
            if (floorNoSys.slectButtonNo == 15)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 16)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 18;
            }
            if (floorNoSys.slectButtonNo == 17)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 18)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 19;
            }
            if (floorNoSys.slectButtonNo == 19)
            {
                limitlessNo = new int[1];

                limitlessNo[0] = 20;
            }
        }
            //���ɐi�߂�{�^����������悤�ɂ��鏈��
            for (int k = 0; k < limitlessNo.Length; k++)
            {
                cloneButton[limitlessNo[k]].interactable = true;
            }

    }

    //����p
    String sceneName = null;
    //�V�[���̐؂�ւ�
    public void SceneChenge (int sceneKindsNo)
    {
        //�G
        if (sceneKindsNo == 0)
        {
            sceneName = ("EncountScene");
        }
        //�C�x���g
        if(sceneKindsNo == 1)
        {
            sceneName = ("Event");
        }
        //�x�e
        if (sceneKindsNo == 2)
        {
            sceneName = ("Stay");
        }
        //��
        if (sceneKindsNo == 3)
        {
            sceneName = ("Treasure");
        }
        //�{�X
        if (sceneKindsNo == 4)
        {
            sceneName = ("Boss");
        }

        Invoke("SceneCheger", 1.0f);
    }

    void SceneCheger()
    {
        SceneManager.LoadScene(sceneName);
    }
}
