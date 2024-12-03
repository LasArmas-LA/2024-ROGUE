using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneSys : MonoBehaviour
{

    [SerializeField]
    Image fade = null;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnStratButton()
    {
        Invoke("LoadScene", 1.0f);
    }

    public void OnEndButton()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        #else
            Application.Quit();//�Q�[���v���C�I��
        #endif
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
