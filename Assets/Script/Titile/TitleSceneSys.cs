using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneSys : MonoBehaviour
{
    //�t�F�[�h�p�̃C���[�W
    [SerializeField]
    Image fade = null;

    //�K�w�f�[�^�ۑ��p
    [SerializeField]
    GameObject floorNoSys = null;
    FloorNoSys floorNoSysScript = null;

    //BGM��SE�̉��ʒ��ߗp�̃X���C�_�[
    [SerializeField]
    Slider bgmVolObj = null;
    [SerializeField]
    Slider seVolObj = null;

    //�I�[�f�B�I�\�[�X
    [SerializeField]
    AudioSource[] audioSources = null;

    //�I�[�f�B�I�N���b�v
    [SerializeField]
    AudioClip[] audioClip = null;
    bool fast = true;

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
        bgmVolObj.maxValue = 1;
        bgmVolObj.minValue = 0;
        bgmVolObj.value = 0.5f;
        seVolObj.maxValue = 1;
        seVolObj.minValue = 0;
        seVolObj.value = 0.5f;
    }


    void Update()
    {
        KeyIn();
    }

    void KeyIn()
    {
        if (Input.anyKeyDown && fast && !Input.GetKey(KeyCode.Escape))
        {
            fast = false;
            OnStratButton();
        }
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
