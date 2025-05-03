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

        //�G
        if (sceneKindsNo == 0)
        {
            nextSceneName = ("EncountScene");
        }
        //�C�x���g
        if (sceneKindsNo == 1)
        {
            nextSceneName = ("Event");
        }
        //�x�e
        if (sceneKindsNo == 2)
        {
            nextSceneName = ("Stay");
        }
        //��
        if (sceneKindsNo == 3)
        {
            nextSceneName = ("Treasure");
        }
        //�{�X
        if (sceneKindsNo == 4)
        {
            nextSceneName = ("Boss");
        }


        StartCoroutine(LoadNextScene());
        
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1);

        //���[�h�V�[���̓ǂݍ���
        SceneManager.LoadScene(nextSceneName);
    }

}
