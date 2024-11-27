using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneSys : MonoBehaviour
{

    [SerializeField]
    Image fade = null;

    void Start()
    {
    }

    void Update()
    {

    }

    public void OnStratButton()
    {
        Invoke("LoadScene", 1.0f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
