using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSys : MonoBehaviour
{
    int rnd;
    //シーン名
    [Header("シーン名")]
    [NamedArrayAttribute(new string[] { "タイトル", "ロビー", "ロード","ストーリー","敵フロア","宝箱フロア","ゲームオーバー","ゲームクリア"})]
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

    void Start()
    {
        //シード値の変更
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        rnd = Random.Range(1, 101);


        chestFloor = 100 - enemyFloor;

        //敵フロア
        if (rnd <= enemyFloor)
        {
            loadSceneName = sceneName[4];
        }
        //チェストフロア
        if (rnd >= enemyFloor)
        {
            loadSceneName = sceneName[5];
        }

        //待機後シーンを切り替え
        Invoke("SceneChenge", chengeWaitTime);
    }

    void SceneChenge()
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
