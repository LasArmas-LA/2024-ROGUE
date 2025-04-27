using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public bool loadStop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(LoadNextScene());
        
    }

    private IEnumerator LoadNextScene()
    {
        float time;
        if (loadStop)
        { time = 10.0f; }
        else { time = 0.1f; }
        yield return new WaitForSeconds(time);

        //ロードシーンの読み込み
        SceneManager.LoadScene("Lobby");
    }

}
