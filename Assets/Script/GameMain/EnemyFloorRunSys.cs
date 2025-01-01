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
    [SerializeField]
    Image fade = null;
    //�ŏ���1�񂾂��Ăяo����������
    bool fast = false;
    //�{�^���̑��i�����h�~
    bool button = false;

    //�ǂ̃h���b�v�����p�[�c��I�����Ă��邩�̊m�F�p
    [SerializeField]
    bool[] partsSlect;

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

    //�ŏ���1�񂾂��Ăяo����������
    bool fastMove = true;

    //�Q�[���I�[�o�[�t���O
    [NonSerialized]
    public bool gameOverFlag = false;

    //�p�[�c��I������E�B���h�E
    [SerializeField]
    GameObject partsSlectWin = null;

    //�R�}���h��I������E�B���h�E
    [SerializeField]
    GameObject commandWin = null;

    //�J�����̓������x
    [SerializeField]
    Vector3 characterMoveSpeed = Vector3.zero;

    //�`�F�X�g�̃I�u�W�F�N�g
    [SerializeField]
    GameObject chestObj = null;

    //�K�w�f�[�^�Ǘ��V�X�e��
    FloorNoSys floorNoSys = null;
    [SerializeField]
    GameObject floorNoSysObj = null;

    //�G���J�E���g���Ǘ�����V�X�e��
    [SerializeField]
    TestEncount encountSys = null;

    //�����������_���œ��肷�郍�W�b�N�g�݂̃V�X�e��
    [SerializeField]
    EquipmentManager equipmentManager = null;

    //�f�B�A�̃X�e�[�^�X
    [SerializeField]
    Status dhiaStatus = null;

    [SerializeField]
    GameObject enemyObj = null;
    [SerializeField]
    GameObject restObj = null;
    [SerializeField]
    GameObject doorObj = null;

    //�A�j���[�V����
    [SerializeField]
    Animator ririAnim = null;
    [SerializeField]
    Animator dhiaAnim = null;

    //�e�L�����N�^�[�̃I�u�W�F�N�g
    [SerializeField]
    GameObject ririObj = null;
    [SerializeField]
    GameObject dhiaObj = null;
    //�L�����N�^�[�̐e�I�u�W�F�N�g
    [SerializeField]
    GameObject characterMainObj = null;


    public TextMeshProUGUI windowMes = null;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        windowMes.text = "�T����";
        if(GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys =  Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();
        commandWin.SetActive(false);
        equipmentManager.Start();

        //�����A�j���[�V�������J�n
        ririAnim.SetBool("R_Walk",true);
        dhiaAnim.SetBool("D_Walk",true);
    }
    void Update()
    {
        CharMove();
        CameraMove();

        //�t�F�[�h�A�E�g����
        if (floorEndFlag)
        {
            Invoke("LoadScene", 1.0f);
            if (fade != null)
            {

                floorEndFlag = false;
            }
        }

        if(gameOverFlag)
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

    void CharMove()
    {
        if (encountSys.mainTurn == MainTurn.WAIT)
        {
            if (characterMainObj.transform.position.x >= enemyObj.transform.position.x - 100)
            {
                //1�񂾂��Ăяo��
                if (!fast)
                {
                    //�X�e�[�^�X�̕ύX
                    encountSys.mainTurn = TestEncount.MainTurn.RIRIMOVE;

                    //�����A�j���[�V�������~
                    ririAnim.SetBool("R_Walk", false);
                    dhiaAnim.SetBool("D_Walk", false);

                    //�R�}���h��\��
                    commandWin.SetActive(true);
                    fast = true;
                    runStratFlag = false;
                }
                //StartCoroutine(ChestWait());
            }
            else
            {
                commandWin.SetActive(false);
                //�L�����N�^�[�e�I�u�W�F�N�g�̈ړ�����
                characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
            }
        }
        if (encountSys.mainTurn == MainTurn.END)
        {
            if (encountSys.restFlag)
            {
                if (characterMainObj.transform.position.x <= restObj.transform.position.x - 50)
                {
                    windowMes.text = "�T����";
                    commandWin.SetActive(false);

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

                    //�R�}���h���\��
                    commandWin.SetActive(false);
                    StartCoroutine(RestStay());
                }
            }
            else
            {
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

                        //�������p�[�c�̕\������
                        /*for (int i = 0; i < slectNowText.Length;)
                        {
                            //�E��
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.RightHand)
                            {
                                if (dhiaStatus.righthandPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.righthandPartsData.equipmentName + "\nATK :" + dhiaStatus.righthandPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //����
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.LeftHand)
                            {
                                if (dhiaStatus.lefthandPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.lefthandPartsData.equipmentName + "\nATK :" + dhiaStatus.lefthandPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //��
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.Feet)
                            {
                                if (dhiaStatus.legPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.legPartsData.equipmentName + "\nATK :" + dhiaStatus.legPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //��
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.Body)
                            {
                                if (dhiaStatus.bodyPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.bodyPartsData.equipmentName + "\nATK :" + dhiaStatus.bodyPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            //��
                            if (equipmentManager.randomEquip[equipmentManager.rnd[i]].equipmentType == EquipmentType.Head)
                            {
                                if (dhiaStatus.headPartsData != null)
                                {
                                    slectNowText[i].text = dhiaStatus.headPartsData.equipmentName + "\nATK :" + dhiaStatus.headPartsData.ATK;
                                }
                                else
                                {
                                    slectNowText[i].text = "";
                                }
                            }
                            i++;
                        }
                        */
                        fastMove = false;
                    }

                    partsSlectWin.SetActive(true);
                }
                //�������I�΂ꂽ��h�A�܂ňړ����鏈��
                else
                {
                    if (characterMainObj.transform.position.x <= doorObj.transform.position.x - 10)
                    {
                        windowMes.text = "�T����";
                        commandWin.SetActive(false);
                        characterMainObj.transform.position += characterMoveSpeed * Time.deltaTime;
                        //�����A�j���[�V�������J�n
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
            partsSlect[0] = true;
            partsSlect[1] = false;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect2()
    {
        if (!button)
        {
            partsSlect[0] = false;
            partsSlect[1] = true;
            partsSlect[2] = false;
        }
    }
    public void PartsSlect3()
    {
        if (!button)
        {
            partsSlect[0] = false;
            partsSlect[1] = false;
            partsSlect[2] = true;
        }
    }

    public void PartsSlecteEnd()
    {
        button = true;
        allPartsSlect = true;
        partsSlectWin.SetActive(false);

        //�Y�����镔�ʂɃp�[�c�f�[�^���i�[���鏈��
        if (partsSlect[0])
        {
            //�E��
            if(equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentType == EquipmentType.RightHand)
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

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }

    //�h�A�܂œ����������̏���
    IEnumerator FloorEnd()
    {
        yield return new WaitForSeconds(1.0f);
        if (battleEndFlag)
        {
            floorNoSys.floorNo += 1;
        }
        battleEndFlag = false;
        floorEndFlag = true;
    }
    IEnumerator RestStay()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        encountSys.restFlag = false;
    }
}
