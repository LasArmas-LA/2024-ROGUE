using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneSys : MonoBehaviour
{

    [SerializeField]
    Image fade = null;

    void Start()
    {
        Init();
    }

    void Init()
    {

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
