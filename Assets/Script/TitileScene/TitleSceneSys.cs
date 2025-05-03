using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneSys : MonoBehaviour
{
    //�t�F�[�h�p�̃C���[�W
    [SerializeField]
    Image fade = null;

    //�K�w�f�[�^�ۑ��p
    [SerializeField]
    GameObject floorNoSys = null;
    FloorNoSys floorNoSysScript = null;

    //Master��BGM��SE�̉��ʒ��ߗp�̃X���C�_�[
    [SerializeField]
    Slider masterVolObj = null;
    [SerializeField]
    Slider bgmVolObj = null;
    [SerializeField]
    Slider seVolObj = null;

    //�I�[�f�B�I�\�[�X
    //MASTER�ABGM�ASE
    [SerializeField]
    AudioSource[] audioSources = null;

    //�I�[�f�B�I�N���b�v
    [SerializeField]
    AudioClip[] audioClip = null;

    //�I�[�f�B�I�{�����[���̕\���e�L�X�g�p
    [SerializeField]
    TextMeshProUGUI[] audioVolText = null;

    bool fast = true;

    //�I�v�V�������
    [SerializeField]
    GameObject optionMenu = null;
    //�f�B�A���
    [SerializeField]
    GameObject dhiaMenu = null;

    //�f�B�A��ʂ̃X�e�[�^�X�e�L�X�g�\���p
    [SerializeField]
    Status ririStatus = null;
    [SerializeField]
    Status dhiaStatus = null;
    [SerializeField]
    [NamedArrayAttribute(new string[] { "MAXHP", "HP", "ATK", "DEF"})]
    TextMeshProUGUI[] ririStatusText = new TextMeshProUGUI[4];
    [SerializeField]
    [NamedArrayAttribute(new string[] { "MAXHP", "HP", "ATK", "DEF"})]
    TextMeshProUGUI[] dhiaStatusText = new TextMeshProUGUI[4];


    void Start()
    {
        Init();
    }

    void Init()
    {
        if (GameObject.Find("FloorNo") == null)
        {
            //���̉�
            GameObject floorNoSysClone = Instantiate(floorNoSys);

            //���O�̕ύX
            floorNoSysClone.name = "FloorNo";

            DontDestroyOnLoad(floorNoSysClone);
        }

        floorNoSysScript = GameObject.Find("FloorNo").GetComponent<FloorNoSys>();

        //Audio�̏�����
        masterVolObj.maxValue = 1;
        masterVolObj.minValue = 0;
        bgmVolObj.maxValue = 1;
        bgmVolObj.minValue = 0;
        seVolObj.maxValue = 1;
        seVolObj.minValue = 0;

        //�ۑ�����Ă���{�����[������
        masterVolObj.value = floorNoSysScript.masterVol;
        bgmVolObj.value = floorNoSysScript.bgmVol;
        seVolObj.value = floorNoSysScript.seVol;
    }


    void Update()
    {
        KeyIn();
        VolChenge();
    }

    void KeyIn()
    {
        if (Input.GetKeyDown(KeyCode.S) && fast)
        {
            fast = false;
            OnStratButton();
        }

        if (Input.GetKeyDown(KeyCode.Delete) && fast)
        {
            PlayerPrefs.DeleteKey("sceneKindsNo");
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //�I�v�V����
            optionMenu.SetActive(false);
            //�f�B�A
            dhiaMenu.SetActive(false);

        }
    }

    void VolChenge() 
    {
        audioSources[0].volume = masterVolObj.value;
        audioSources[1].volume = bgmVolObj.value;
        audioSources[2].volume = seVolObj.value;

        audioVolText[0].text = "" + (masterVolObj.value * 100).ToString("F0") + "%";
        audioVolText[1].text = "" + (bgmVolObj.value * 100).ToString("F0") + "%";
        audioVolText[2].text = "" + (seVolObj.value * 100).ToString("F0") + "%";

        floorNoSysScript.masterVol = audioSources[0].volume;
        floorNoSysScript.bgmVol = audioSources[1].volume;
        floorNoSysScript.seVol = audioSources[2].volume;
    }

    void StatusChenge()
    {
        //�����[�̃X�e�[�^�X�\���p
        ririStatusText[0].text = ririStatus.MAXHP.ToString();
        ririStatusText[1].text = ririStatus.HP.ToString();
        ririStatusText[2].text = ririStatus.ATK.ToString();
        ririStatusText[3].text = ririStatus.DEF.ToString();

        //�f�B�A�̃X�e�[�^�X�\���p
        dhiaStatusText[0].text = dhiaStatus.MAXHP.ToString();
        dhiaStatusText[1].text = dhiaStatus.HP.ToString();
        dhiaStatusText[2].text = dhiaStatus.ATK.ToString();
        dhiaStatusText[3].text = dhiaStatus.DEF.ToString();
    }


    //�I�v�V������ʗp
    public void MenuBackButton()
    {
        optionMenu.SetActive(false);
    }

    public void OptionButton()
    {
        optionMenu.SetActive(true);
    }


    //�f�B�A���j���[�\���p
    public void DhiaMenu()
    {
        StatusChenge();

        dhiaMenu.SetActive(true);
    }
    public void BackDhiaMenu()
    {
        dhiaMenu.SetActive(false);
    }


    //�A�j���[�V�����p
    [SerializeField]
    Animator fadeAnim = null;
    public void OnStratButton()
    {
        fadeAnim.SetBool("FadeOut", true);
        //1�b�҂��Ă���֐����Ăяo��
        Invoke("LoadScene", 0.8f);
    }

    public void OnEndButton()
    {
        //�r���h�f�[�^���G�f�B�^�[���[�h�̔���
#if UNITY_EDITOR
        //�Q�[���v���C�I��
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //�Q�[���v���C�I��
            Application.Quit();//�Q�[���v���C�I��
#endif
    }

    void LoadScene()
    {
        //���[�h�V�[���̓ǂݍ���
        SceneManager.LoadScene("LoadScene");
    }
}
