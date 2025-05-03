using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static string nextSceneName = "Null";
    public bool loadStop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int sceneKindsNo = PlayerPrefs.GetInt("sceneKindsNo", 0);

        //敵
        if (sceneKindsNo == 0)
        {
            nextSceneName = ("EncountScene");
        }
        //イベント
        if (sceneKindsNo == 1)
        {
            nextSceneName = ("Event");
        }
        //休憩
        if (sceneKindsNo == 2)
        {
            nextSceneName = ("Stay");
        }
        //宝
        if (sceneKindsNo == 3)
        {
            nextSceneName = ("Treasure");
        }
        //ボス
        if (sceneKindsNo == 4)
        {
            nextSceneName = ("Boss");
        }


        StartCoroutine(LoadNextScene());
        
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1);

        //ロードシーンの読み込み
        SceneManager.LoadScene(nextSceneName);
    }

}
