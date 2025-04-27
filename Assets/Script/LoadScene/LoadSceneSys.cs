using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSys : MonoBehaviour
{


    int rnd;
    //�V�[����
    [Header("�V�[����")]
    [NamedArrayAttribute(new string[] { "�^�C�g��","�}�b�v", "���r�[", "���[�h","�X�g�[���[","�G�t���A","�󔠃t���A","�Q�[���I�[�o�[","�Q�[���N���A"})]
    public string[] sceneName = null;

    string loadSceneName = null;

    //�G�t���A�̊���(�m��)
    [SerializeField]
    [Range(0, 100)]
    int enemyFloor = 0;

    //�`�F�X�g�t���A�̊���(�m��)
    [Range(0, 100)]
    int chestFloor = 0;

    //�V�[���؂�ւ��̑ҋ@����
    [SerializeField]
    float chengeWaitTime = 0;

    //�t�F�[�h�p�A�j���[�V����
    [SerializeField]
    Animator fadeAnim = null;

    void Start()
    {
        


        //�ҋ@��V�[����؂�ւ�
        Invoke("SceneChenge", chengeWaitTime);
        Invoke("SceneChengeFade", chengeWaitTime - 1);
    }

    void SceneChenge()
    {
    //    SceneManager.LoadScene(nextSceneName);
    }

    void SceneChengeFade()
    {
        fadeAnim.SetBool("FadeOut", true);
    }
}
