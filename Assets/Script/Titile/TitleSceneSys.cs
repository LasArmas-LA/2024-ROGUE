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
        //1秒待ってから関数を呼び出し
        Invoke("LoadScene", 1.0f);
    }

    public void OnEndButton()
    {
        //ビルドデータかエディターモードの判別
#if UNITY_EDITOR
        //ゲームプレイ終了
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //ゲームプレイ終了
            Application.Quit();//ゲームプレイ終了
#endif
    }

    void LoadScene()
    {
        //ロードシーンの読み込み
        SceneManager.LoadScene("LoadScene");
    }
}
