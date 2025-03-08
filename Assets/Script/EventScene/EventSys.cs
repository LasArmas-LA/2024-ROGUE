using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static BaseEquipment;
using System.ComponentModel;

public class EventSys : MonoBehaviour
{
    //�C�x���g�̎�ފǗ��p
    public enum EventKinds
    {
        //���͒e���p
        WAIT,
        //���̋���
        CADAVER,
        //�ēx���̋���p
        RECADAVER,
        //�r�炳�ꂽ�K
        DIRTY,
        //�����[��ꂽ
        TIRED,
        //�C�x���g�I������
        END

    }

    //�����������������������������������p�[�c�p

    //�ǂ̃h���b�v�����p�[�c��I�����Ă��邩�̊m�F�p
    [SerializeField, Header("�p�[�c�Ǘ��p")]
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

    //�����������_���œ��肷�郍�W�b�N�g�݂̃V�X�e��
    [SerializeField, Header("�h���b�v���������_�����V�X�e��")]
    EquipmentManager equipmentManager = null;

    //�f�B�A�̃X�e�[�^�X
    [SerializeField, Header("�f�B�A�̃X�e�[�^�X�Ǘ��p")]
    Status dhiaStatus = null;
    //�����[�̃X�e�[�^�X
    [SerializeField, Header("�����[�̃X�e�[�^�X�Ǘ��p")]
    Status ririStatus = null;

    bool partsButton = false;

    //���������������������������������p�[�c�p


    public EventKinds eventKinds;

    int slectNo = 0;
    bool button = false;

    [SerializeField]
    [NamedArrayAttribute(new string[] {"���C��","�{�^��1", "�{�^��2", "�{�^��3", "�{�^��4"})]
    TextMeshProUGUI[] buttonText = null;

    [SerializeField]
    [NamedArrayAttribute(new string[] {"�{�^��1", "�{�^��2", "�{�^��3", "�{�^��4" })]
    GameObject[] buttonObj = null;

    [SerializeField, Header("�K�w�f�[�^�Ǘ��p�V�X�e��")]
    GameObject floorNoSysObj = null;

    //�t�F�[�h�A�j���[�V�����p
    [SerializeField]
    Animator fadeAnim = null;

    void Awake()
    {

    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        //�p�[�c�̃����_���h���b�v
        equipmentManager.LoopInit();

        //�����̏�����
        InitFind();

        //�C�x���g�̏�����
        InitEvent();
    }

    void InitFind()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }
    }

    void InitEvent()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int rnd = UnityEngine.Random.Range(1, 4);

        switch (rnd)
        {
            case 1:
                eventKinds = EventKinds.CADAVER;
                buttonText[0].text = "�@�\��~���Ă���@�B������݂����c\n�ǂ����悤���c";
                buttonText[1].text = "���� (�������l���A00%�Ő퓬)";
                buttonText[2].text = "�Ȃɂ����Ȃ�";

                //�s�v�ȃ{�^���폜
                buttonObj[2].SetActive(false);
                buttonObj[3].SetActive(false);
                break;
            case 2:
                eventKinds = EventKinds.DIRTY;
                buttonText[0].text = "�₯�ɕ����U�炩���Ă���c\n�����Ȃ����T���Ă݂悤���c�H";
                buttonText[1].text = "�T�� (HP�����A�����l��)";
                buttonText[2].text = "�T���Ȃ�";

                //�s�v�ȃ{�^���폜
                buttonObj[2].SetActive(false);
                buttonObj[3].SetActive(false);
                break;
            case 3:
                eventKinds = EventKinds.TIRED;
                buttonText[0].text = "�u�˂��f�B�A�����x�e�ɂ��Ȃ��H����ꂿ������v�c\n�����[�͔��Ă���悤��";
                buttonText[1].text = "�x�e (2�l��HP��50%��)";
                buttonText[2].text = "���������(������1�I��Ń��x���A�b�v)";

                //�s�v�ȃ{�^���폜
                buttonObj[2].SetActive(false);
                buttonObj[3].SetActive(false);
                break;
        }
    }

    void Update()
    {
        switch(eventKinds)
        {
            case EventKinds.CADAVER:
                Cadaver();
                break;
            case EventKinds.RECADAVER:
                ReCadaver();
                break;
            case EventKinds.DIRTY:
                Dirty();
                break;
            case EventKinds.TIRED:
                Tired();
                break;
            case EventKinds.END:
                Invoke("SceneEnd", 1f);
                fadeAnim.SetBool("FadeIn", true);
                break;
        }
    }

    void Drop()
    {
        //����������
        partsImage[0].sprite = slectOffSp;
        partsImage[1].sprite = slectOffSp;
        partsImage[2].sprite = slectOffSp;
        partsObj[0].transform.localScale = new Vector3(1f, 1f, 1f);
        partsObj[1].transform.localScale = new Vector3(1f, 1f, 1f);
        partsObj[2].transform.localScale = new Vector3(1f, 1f, 1f);

        arrowObj[0].SetActive(false);
        arrowObj[1].SetActive(false);
        arrowObj[2].SetActive(false);

        partsSlect[0] = false;
        partsSlect[1] = false;
        partsSlect[2] = false;
        partsButton = false;

        //�p�[�c�Z���N�g�̃E�B���h�E�\��
        partsSlectWin.SetActive(true);

        //�h���b�v�����̕\������
        slectText[0].text = equipmentManager.randomEquip[equipmentManager.rnd[0]].equipmentName;
        slectText[1].text = equipmentManager.randomEquip[equipmentManager.rnd[1]].equipmentName;
        slectText[2].text = equipmentManager.randomEquip[equipmentManager.rnd[2]].equipmentName;

        //�h���b�v�����̉摜�\������
        dropPartsSp[0].sprite = equipmentManager.randomEquip[equipmentManager.rnd[0]].sprite;
        dropPartsSp[1].sprite = equipmentManager.randomEquip[equipmentManager.rnd[1]].sprite;
        dropPartsSp[2].sprite = equipmentManager.randomEquip[equipmentManager.rnd[2]].sprite;
    }


    public void PartsSlect1()
    {
        if (!partsButton)
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
        if (!partsButton)
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
        if (!partsButton)
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
        partsButton = true;
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


    float timer = 0f;
    int battleNo = 30;
    int partsNo = 70;
    bool fast = true;
    int rnd = 0;

    //���̋���
    void Cadaver()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                if (slectNo == 0)
                {
                    UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                    rnd = UnityEngine.Random.Range(1, 100);

                    //�퓬
                    if (rnd <= battleNo)
                    {
                        buttonText[0].text = "�@�B�������o�����I";
                        SceneManager.LoadScene("EncountScene");

                    }
                    //�����l��
                    if (rnd > partsNo)
                    {
                        Invoke("Drop", 1.0f);

                        buttonText[0].text = "�g�������ȑ������������I�܂��T���H";

                        buttonText[1].text = "�͂�";
                        buttonText[2].text = "������";

                        battleNo += 10;
                        partsNo -= 10;

                        //���̂܂ܗ���Ȃ��悤�ɏ�����
                        slectNo = 100;

                        timer = 0f;

                        eventKinds = EventKinds.RECADAVER;
                        button = false;
                    }
                }
                if(slectNo == 1)
                {
                    buttonText[0].text = "�������Ȃ����Ƃɂ����I";
                    button = false;
                    eventKinds = EventKinds.END;
                }
            }
        }
    }

    //���̍ċ���
    void ReCadaver()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (fast)
            {
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                rnd = UnityEngine.Random.Range(1, 100);
                fast = false;
            }

            if (timer >= 1)
            {
                if (slectNo == 0)
                {
                    //�퓬
                    if (rnd <= battleNo)
                    {
                        buttonText[0].text = "�@�B�������o�����I";
                        SceneManager.LoadScene("EncountScene");
                        button = false;
                    }
                    //�����l��
                    if (rnd > partsNo)
                    {
                        equipmentManager.LoopInit();
                        Invoke("Drop", 1.0f);

                        buttonText[0].text = "�g�������ȑ������������I�܂��T���H";

                        battleNo += 10;
                        partsNo -= 10;

                        slectNo = 100;

                        timer = 0f;
                        button = false;
                        fast = true;
                    }
                }
                if (slectNo == 1)
                {
                    buttonText[0].text = "���낻���߂Ă��������ȁc";
                    button = false;
                    timer = 0f;
                    eventKinds = EventKinds.END;
                }
            }
        }
    }


    //�r�炳�ꂽ�K
    void Dirty()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                if(slectNo == 0)
                {
                    //3����
                    ririStatus.HP -= (ririStatus.MAXHP * 0.3f);
                    dhiaStatus.HP -= (dhiaStatus.MAXHP * 0.3f);

                    equipmentManager.LoopInit();
                    Invoke("Drop", 1.0f);

                    buttonText[0].text = "�������I���ɂԂ����ăP�K���Ă��܂����I\n�������������������I";
                }
                if (slectNo == 1)
                {
                    buttonText[0].text = "�����悯�Đ�ɐi��";
                }
                eventKinds = EventKinds.END;
            }
        }
    }

    //�����[��ꂽ
    void Tired()
    {
        if (button)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                buttonText[0].text = "�u�����ԋx�e�ł�����I���肪�Ƃ��f�B�A�I�v";

                if (slectNo == 0)
                {
                    //HP��50%��
                    //�����[�̉񕜗ʂ��}�b�N�XHP�𒴂��Ă��܂���
                    if(ririStatus.HP + ririStatus.MAXHP * 0.5f >= ririStatus.MAXHP)
                    {
                        ririStatus.HP = ririStatus.MAXHP;
                    }
                    //�����[��MaxHP�𒴂��Ȃ���
                    else
                    {
                        ririStatus.HP += (ririStatus.MAXHP * 0.5f);
                    }

                    //�f�B�A�̉񕜗ʂ��}�b�N�XHP�𒴂��Ă��܂���
                    if (dhiaStatus.HP + dhiaStatus.MAXHP * 0.5f >= dhiaStatus.MAXHP)
                    {
                        dhiaStatus.HP = dhiaStatus.MAXHP;
                    }
                    //�f�B�A��MaxHP�𒴂��Ȃ���
                    else
                    {
                        dhiaStatus.HP += (dhiaStatus.MAXHP * 0.5f);
                    }
                }
                if (slectNo == 1)
                {
                    //������1�I��Ń��x���A�b�v

                }
                eventKinds = EventKinds.END;
            }
        }
    }

    void SceneEnd()
    {
        SceneManager.LoadScene("LoadScene");
    }


    public void Button1Slect(int number)
    {
        if (!button)
        {
            slectNo = number;
            button = true;
        }
    }
}
