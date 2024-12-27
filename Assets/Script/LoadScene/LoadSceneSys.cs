using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSys : MonoBehaviour
{
    int rnd;
    //�V�[����
    [Header("�V�[����")]
    [NamedArrayAttribute(new string[] { "�^�C�g��", "���r�[", "���[�h","�X�g�[���[","�G�t���A","�󔠃t���A","�Q�[���I�[�o�[","�Q�[���N���A"})]
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

    void Start()
    {
        //�V�[�h�l�̕ύX
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        rnd = Random.Range(1, 101);


        chestFloor = 100 - enemyFloor;

        //�G�t���A
        if (rnd <= enemyFloor)
        {
            loadSceneName = sceneName[4];
        }
        //�`�F�X�g�t���A
        if (rnd >= enemyFloor)
        {
            loadSceneName = sceneName[5];
        }

        //�ҋ@��V�[����؂�ւ�
        Invoke("SceneChenge", chengeWaitTime);
    }

    void SceneChenge()
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
