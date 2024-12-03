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
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
