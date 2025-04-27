using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAnim : MonoBehaviour
{
    [SerializeField]
    private string LoadSceneName = "Null"; 
    
    [SerializeField]
    private Animator animator;

    public void LoadScene1()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        animator.SetTrigger("LoadAnumTrigger");
        yield return new WaitForSeconds(0.5f);

        //ロードシーンの読み込み
        SceneManager.LoadScene("LoadScene");
    }
}
