using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseEquipment;
using static TestEncount;

public class EnemyFloorRunSys : MonoBehaviour
{
    //���C���J����
    public Camera maincamera = null;

    //�G�̏ꏊ�܂ŕ����t���O
    bool runStratFlag = false;
    //�G��|���Ĕ��܂ŕ������̃t���O
    public bool battleEndFlag = false;
    //���ɒ����Ă��̊K���I�����鎞�̃t���O
    bool floorEndFlag = false;
    //�t�F�[�h�A�E�g�p
    [SerializeField,Header("�t�F�[�h�A�E�g�p")]
    Image fade = null;
    //�ŏ���1�񂾂��Ăяo����������
    bool fast = false;
    //�{�^���̑��i�����h�~
    bool button = false;

    [Space(10)]

    //�ǂ̃h���b�v�����p�[�c��I�����Ă��邩�̊m�F�p
    [SerializeField,Header("�p�[�c�Ǘ��p")]
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

    [Space(10)]

    //�ŏ���1�񂾂��Ăяo����������
    bool fastMove = true;

    //�Q�[���I�[�o�[�t���O
    [NonSerialized]
    public bool gameOverFlag = false;


    //�R�}���h��I������E�B���h�E
    [SerializeField,Header("�R�}���h�Ǘ��p")]
    public GameObject commandWin = null;
    [SerializeField]
    public GameObject commandMain = null;

    [Space(10)]
    //�J�����̓������x
    [SerializeField,Header("�J�����Ǘ��p")]
    Vector3 characterMoveSpeed = Vector3.zero;

    [Space(10)]

    //�K�w�f�[�^�Ǘ��V�X�e��
    FloorNoSys floorNoSys = null;
    [SerializeField,Header("�K�w�f�[�^�Ǘ��p�V�X�e��")]
    GameObject floorNoSysObj = null;

    [Space(10)]

    //�G���J�E���g���Ǘ�����V�X�e��
    [SerializeField,Header("�G���J�E���g�Ǘ��p�V�X�e��")]
    TestEncount encountSys = null;

    [Space(10)]

    //�����������_���œ��肷�郍�W�b�N�g�݂̃V�X�e��
    [SerializeField,Header("�h���b�v���������_�����V�X�e��")]
    EquipmentManager equipmentManager = null;

    [Space(10)]

    //�f�B�A�̃X�e�[�^�X
    [SerializeField,Header("�f�B�A�̃X�e�[�^�X�Ǘ��p")]
    Status dhiaStatus = null;

    [Space(10)]

    [SerializeField,Header("�I�u�W�F�N�g�Ǘ��p")]
    GameObject enemyObj = null;
    [SerializeField]
    GameObject restObj = null;
    [SerializeField]
    GameObject doorObj = null;

    //�e�L�����N�^�[�̃I�u�W�F�N�g
    [SerializeField]
    GameObject ririObj = null;
    [SerializeField]
    GameObject dhiaObj = null;
    //�L�����N�^�[�̐e�I�u�W�F�N�g
    [SerializeField]
    GameObject characterMainObj = null;


    [Space(10)]
    [SerializeField, Header("�X�N���v�g�Q��")]
    Riri ririScript = null;
    [SerializeField]
    Dhia dhiaScript = null;

    [Space(10)]

    //�A�j���[�V����
    [SerializeField,Header("�A�j���[�V�����Ǘ��p")]
    Animator ririAnim = null;
    [SerializeField]
    Animator dhiaAnim = null;



    public TextMeshProUGUI windowMes = null;

    void Awake()
    {
        Init();
    }

    void Init()
    {

        InitFind();
        InitActive();
        InitAnim();

        //����̒��I
        equipmentManager.LoopInit();
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();
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

    void Update()
    {
        CharMove();
        //CameraMove();

        //�t�F�[�h�A�E�g����
        if (floorEndFlag)
        {
            //Invoke("LoadScene", 1.0f);
            LoadScene();
            if (fade != null)
            {

                floorEndFlag = false;
            }

            if (battleEndFlag)
            {
                //floorNoSys.floorCo += 1;
                Debug.Log("�K�w�A�b�v");
                battleEndFlag = false;
            }
        }

        if (gameOverFlag)
        {
            GameOver();
        }
    }

    void CameraMove() 
    {
        if (maincamera.transform.position.x <= 227)
        {
            Vector3 cameraPos = maincamera.transform.position;
            cameraPos.x = characterMainObj.transform.position.x + 50;
            maincamera.transform.position = cameraPos;
        }
    }

    bool fastFloorNo = true;
    void CharMove()
    {
        if (encountSys.mainTurn == MainTurn.WAIT)
        {
                //1�񂾂��Ăяo��
                if (!fast)
                {
                    //�X�e�[�^�X�̕ύX
                    encountSys.mainTurn = MainTurn.RIRIMOVE;

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
        if (encountSys.mainTurn == MainTurn.ENDRUN)
        {
            if (encountSys.restFlag)
            {
                if (characterMainObj.transform.position.x <= restObj.transform.position.x + 20)
                {
                    windowMes.text = "�T����";
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);

                    //�����A�j���[�V�������J�n
                    ririAnim.SetBool("R_Walk", true);
                    dhiaAnim.SetBool("D_Walk", true);
                    characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "�x�e��";
                    //�����A�j���[�V�������~
                    ririAnim.SetBool("R_Walk", false);
                    dhiaAnim.SetBool("D_Walk", false);

                    //HP����
                    //ririScript.hp = ririScript.maxhp;
                    //dhiaScript.hp = dhiaScript.maxhp;

                    //�R�}���h���\��
                    commandWin.SetActive(false);
                    commandMain.SetActive(false);
                    StartCoroutine(RestStay());
                }
            }
            else
            {
                commandWin.SetActive(false);
                commandMain.SetActive(false);

                if (!allPartsSlect)
                {
                    if (fastMove)
                    {
                        //�h���b�v�����̕\������
                        slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName;
                        slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName;
                        slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName;

                        //�h���b�v�����̉摜�\������
                        dropPartsSp[0].sprite = equipmentManager.randomEquip[equipmentManager.rnd[0]].sprite;
                        dropPartsSp[1].sprite = equipmentManager.randomEquip[equipmentManager.rnd[1]].sprite;
                        dropPartsSp[2].sprite = equipmentManager.randomEquip[equipmentManager.rnd[2]].sprite;

                        fastMove = false;
                    }

                    partsSlectWin.SetActive(true);
                }
                //�������I�΂ꂽ���ʊO�܂ňړ����鏈��
                else
                {
                    if (characterMainObj.transform.position.x <= doorObj.transform.position.x + 70)
                    {
                        windowMes.text = "�T����";
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
                        windowMes.text = "�����������I \n���̊K�ɐi����";
                        //�����A�j���[�V�������~
                        ririAnim.SetBool("R_Walk", false);
                        dhiaAnim.SetBool("D_Walk", false);
                        commandWin.SetActive(false);
                        commandMain.SetActive(false);
                        if (fastFloorNo)
                        {
                            //floorNoSys.floorCo += 1;
                            fastFloorNo = false;
                        }

                        commandMain.SetActive(false);
                        StartCoroutine(FloorEnd());
                    }
                }
            }
        }

    }

    void GameOver()
    {

    }

    public void PartsSlect1()
    {
        if(!button)
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
        if (!button)
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
        if (!button)
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
        if (partsSlect[0] || partsSlect[1] || partsSlect[2])
        {
            button = true;
            allPartsSlect = true;
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
        floorEndFlag = true;
    }
    IEnumerator RestStay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        encountSys.restFlag = false;
    }
}
