using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BaseEquipment;

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
    Vector3 cameraMoveSpeed = Vector3.zero;

    //�`�F�X�g�̃I�u�W�F�N�g
    [SerializeField]
    GameObject chestObj = null;

    //�K�w�f�[�^�Ǘ��V�X�e��
    FloorNoSys floorNoSys = null;
    GameObject floorNoSysObj = null;

    //�G���J�E���g���Ǘ�����V�X�e��
    [SerializeField]
    EncountSys encountSys = null;

    //�����������_���œ��肷�郍�W�b�N�g�݂̃V�X�e��
    [SerializeField]
    EquipmentManager equipmentManager = null;

    //�f�B�A�̃X�e�[�^�X
    [SerializeField]
    Status dhiaStatus = null;


    public TextMeshProUGUI windowMes = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        windowMes.text = "�T����";
        floorNoSysObj = GameObject.Find("FloorNo");
        floorNoSys = floorNoSysObj.GetComponent<FloorNoSys>();
        commandWin.SetActive(false);
        equipmentManager.Start();
    }
    void Update()
    {
        KeyIn();
        if (runStratFlag)
        {
            if (maincamera.transform.position.x >= 150)
            {
                //1�񂾂��Ăяo��
                if(!fast)
                {
                    encountSys.RiriMove();
                    commandWin.SetActive(true);
                    fast = true;
                    runStratFlag = false;
                }
                //StartCoroutine(ChestWait());
            }
            else
            {
                commandWin.SetActive(false);
                //�J�����̈ړ�����
                maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
            }
        }
        if (battleEndFlag)
        {
            if (encountSys.restFlag)
            {
                if (maincamera.transform.position.x <= 200)
                {
                    windowMes.text = "�T����";
                    commandWin.SetActive(false);
                    maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                }
                else
                {
                    windowMes.text = "�x�e��";
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
                        slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd[0]].ATK;
                        slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd[1]].ATK;
                        slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName + "\nATK :" + equipmentManager.randomEquip[equipmentManager.rnd[2]].ATK;

                        for (int i = 0; i < slectNowText.Length;)
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
                        fastMove = false;
                    }

                    partsSlectWin.SetActive(true);
                }
                else
                {
                    if (maincamera.transform.position.x <= 300)
                    {
                        windowMes.text = "�T����";
                        commandWin.SetActive(false);
                        maincamera.transform.position += cameraMoveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        windowMes.text = "�����������I \n���̊K�ɐi����";
                        commandWin.SetActive(false);
                        StartCoroutine(FloorEnd());
                    }
                }
            }
        }

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

    void KeyIn()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !battleEndFlag)
        {
            runStratFlag = true;
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
