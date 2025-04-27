using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSys : MonoBehaviour
{


    int rnd;
    //シーン名
    [Header("シーン名")]
    [NamedArrayAttribute(new string[] { "タイトル","マップ", "ロビー", "ロード","ストーリー","敵フロア","宝箱フロア","ゲームオーバー","ゲームクリア"})]
    public string[] sceneName = null;

    string loadSceneName = null;

    //敵フロアの割合(確率)
    [SerializeField]
    [Range(0, 100)]
    int enemyFloor = 0;

    //チェストフロアの割合(確率)
    [Range(0, 100)]
    int chestFloor = 0;

    //シーン切り替えの待機時間
    [SerializeField]
    float chengeWaitTime = 0;

    //フェード用アニメーション
    [SerializeField]
    Animator fadeAnim = null;

    void Start()
    {
        


        //待機後シーンを切り替え
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
