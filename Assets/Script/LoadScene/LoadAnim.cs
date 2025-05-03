using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAnim : MonoBehaviour
{
    [SerializeField]
    private string LoadSceneName = "Null"; 
    
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        animator.SetTrigger("LoadEndTrigger");
    }

    public void LoadScene1(string nextScene)
    {
        StartCoroutine(LoadNextScene(nextScene));
    }

    private IEnumerator LoadNextScene(string nextScene)
    {
        animator.SetTrigger("LoadAnumTrigger");
        yield return new WaitForSeconds(0.5f);

        LoadScene.nextSceneName = nextScene;

        //ロードシーンの読み込み
        SceneManager.LoadScene("LoadScene");
    }
}
