using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneSys : MonoBehaviour
{

    [SerializeField]
    Image fade = null;

    [SerializeField]
    GameObject floorNoSys = null;

    [SerializeField]
    Slider bgmVolObj = null;
    [SerializeField]
    Slider seVolObj = null;

    [SerializeField]
    AudioSource[] audioSources = null;

    [SerializeField]
    AudioClip[] audioClip = null;

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
        audioSources[0].volume = bgmVolObj.value;
        audioSources[1].volume = seVolObj.value;
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
