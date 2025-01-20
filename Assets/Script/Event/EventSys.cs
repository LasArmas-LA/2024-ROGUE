using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        TIRED

    }

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


    void Awake()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            GameObject floorNoSys = Instantiate(floorNoSysObj);
            DontDestroyOnLoad(floorNoSys);

            floorNoSys.name = "FloorNo";
        }

    }

    void Start()
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
                        buttonText[0].text = "�g�������ȑ������������I�܂��T���H";

                        buttonText[1].text = "�͂�";
                        buttonText[2].text = "������";

                        battleNo += 10;
                        partsNo -= 10;

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
                    eventKinds = EventKinds.WAIT;
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
                    eventKinds = EventKinds.WAIT;
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
                    buttonText[0].text = "�������I���ɂԂ����ăP�K���Ă��܂����I\n�������������������I";
                }
                if (slectNo == 1)
                {
                    buttonText[0].text = "�����悯�Đ�ɐi��";
                    eventKinds = EventKinds.WAIT;
                }
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

                }
                if(slectNo == 1)
                {
                    //������1�I��Ń��x���A�b�v

                }
                eventKinds = EventKinds.WAIT;
            }
        }
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
