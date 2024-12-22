using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSys : MonoBehaviour
{
    int rnd;
    string sceneName = null;

    [SerializeField]
    [Range(0, 100)]
    int enemyFloor = 0;

    [Range(0, 100)]
    int chestFloor = 0;

    int floorNo = 0;
    void Start()
    {
        floorNo += 1;

        rnd = Random.Range(1, 101);

        chestFloor = 100 - enemyFloor;

        //�G�t���A
        if (rnd <= enemyFloor)
        {
            sceneName = "EncountFloorScene";
        }
        //�`�F�X�g�t���A
        if (rnd >= enemyFloor)
        {
            sceneName = "ChestFloorScene";
        }
        Invoke("SceneChenge", 1.0f);
    }

    void Update()
    {
        rnd = Random.Range(1, 101);    
    }

    void SceneChenge()
    {
        PlayerPrefs.SetInt("�t���A�K�w", floorNo);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
    }
}
