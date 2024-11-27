using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSys : MonoBehaviour
{

    int rnd;
    string sceneName = null;
    void Start()
    {
        rnd = Random.Range(1, 4);

        //�G�t���A
        if (rnd == 1)
        {
            sceneName = "EncountFloorScene";
        }
        //�`�F�X�g�t���A
        if(rnd == 2)
        {
            sceneName = "ChestFloorScene";
        }
        //�x�e�t���A
        if(rnd == 3)
        {
            sceneName = "RestFloorScene";
        }

        Invoke("SceneChenge", 1.0f);
    }

    void Update()
    {
        rnd = Random.Range(1, 4);
    }

    void SceneChenge()
    {
        SceneManager.LoadScene(sceneName);
    }
}
