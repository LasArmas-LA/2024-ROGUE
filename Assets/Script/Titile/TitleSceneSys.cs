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

        if(Input.GetKeyDown(KeyCode.Escape))
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
        dhiaMenu.SetActive(true);
    }
    public void BackDhiaMenu()
    {
        dhiaMenu.SetActive(false);
    }



    public void OnStratButton()
    {
        //1�b�҂��Ă���֐����Ăяo��
        Invoke("LoadScene", 1.0f);
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
