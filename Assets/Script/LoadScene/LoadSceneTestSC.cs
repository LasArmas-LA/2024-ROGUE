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
        
        StartCoroutine(LoadNextScene());
        
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1);

        //���[�h�V�[���̓ǂݍ���
        SceneManager.LoadScene(nextSceneName);
    }

}
