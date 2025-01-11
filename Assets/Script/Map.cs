using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField]
    Outline[] button = null;
    [SerializeField]
    Image[] buttonImage = null;

    [SerializeField]
    float scrollSpeed = 1;
    [SerializeField]
    GameObject mainCamera;

    void Start()
    {

    }

    void Update()
    {
        if (mainCamera.transform.position.y >= 100 && mainCamera.transform.position.y <= 1000)
        {
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.up * scroll * scrollSpeed;
        }
        if (mainCamera.transform.position.y <= 100)
        {
            mainCamera.transform.position = new Vector3(960,100,-10);
        }
        if(mainCamera.transform.position.y >= 1000)
        {
            mainCamera.transform.position = new Vector3(960, 1000, -10);
        }
    }

    public void SceneChenge (int sceneNo)
    {
        //“G
        if(sceneNo == 0)
        {
            //SceneManager.LoadScene("R_EncountFloorScene Old");
        }
        //ƒCƒxƒ“ƒg
        if(sceneNo == 1)
        {
            //SceneManager.LoadScene("Event");
        }
        //‹xŒe
        if (sceneNo == 2)
        {
           // SceneManager.LoadScene("Stay");
        }
        //•ó
        if (sceneNo == 3)
        {
            //SceneManager.LoadScene("Treasure");
        }
        //ƒ{ƒX
        if (sceneNo == 4)
        {
           // SceneManager.LoadScene("Boss");
        }
    }

    public void ButtonControl(int buttonNo)
    {
        for (int i = 0; i <= button.Length - 1; i++)
        {
            button[i].effectColor = Color.white;
            button[i].effectDistance = new Vector2(0, 0);

            Debug.Log(i);
        }

        if (buttonNo == 0)
        {
            button[0].effectColor = Color.yellow;
            button[0].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 1)
        {
            button[0].effectColor = Color.red;
            button[0].effectDistance = new Vector2(10, 10);


            button[1].effectColor = Color.yellow;
            button[1].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 2)
        {
            button[0].effectColor = Color.red;
            button[0].effectDistance = new Vector2(10, 10);

            button[2].effectColor = Color.yellow;
            button[2].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 3)
        {
            button[1].effectColor = Color.red;
            button[1].effectDistance = new Vector2(10, 10);


            button[3].effectColor = Color.yellow;
            button[3].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 4)
        {
            button[1].effectColor = Color.red;
            button[2].effectColor = Color.red;
            button[1].effectDistance = new Vector2(10, 10);
            button[2].effectDistance = new Vector2(10, 10);


            button[4].effectColor = Color.yellow;
            button[4].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 5)
        {
            button[3].effectColor = Color.red;
            button[3].effectDistance = new Vector2(10, 10);


            button[5].effectColor = Color.yellow;
            button[5].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 6)
        {
            button[3].effectColor = Color.red;
            button[4].effectColor = Color.red;

            button[3].effectDistance = new Vector2(10, 10);
            button[4].effectDistance = new Vector2(10, 10);


            button[6].effectColor = Color.yellow;
            button[6].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 7)
        {
            button[4].effectColor = Color.red;
            button[4].effectDistance = new Vector2(10, 10);


            button[7].effectColor = Color.yellow;
            button[7].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 8)
        {
            button[5].effectColor = Color.red;
            button[6].effectColor = Color.red;
            button[5].effectDistance = new Vector2(10, 10);
            button[6].effectDistance = new Vector2(10, 10);


            button[8].effectColor = Color.yellow;
            button[8].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 9)
        {
            button[6].effectColor = Color.red;
            button[7].effectColor = Color.red;
            button[6].effectDistance = new Vector2(10, 10);
            button[7].effectDistance = new Vector2(10, 10);


            button[9].effectColor = Color.yellow;
            button[9].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 10)
        {
            button[7].effectColor = Color.red;
            button[7].effectDistance = new Vector2(10, 10);


            button[10].effectColor = Color.yellow;
            button[10].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 11)
        {
            button[8].effectColor = Color.red;
            button[8].effectDistance = new Vector2(10, 10);

        
            button[11].effectColor = Color.yellow;
            button[11].effectDistance = new Vector2(10, 10); 
        }
        if (buttonNo == 12)
        {
            button[8].effectColor = Color.red;
            button[9].effectColor = Color.red;
            button[8].effectDistance = new Vector2(10, 10);
            button[9].effectDistance = new Vector2(10, 10);


            button[12].effectColor = Color.yellow;
            button[12].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 13)
        {
            button[9].effectColor = Color.red;
            button[10].effectColor = Color.red;
            button[9].effectDistance = new Vector2(10, 10);
            button[10].effectDistance = new Vector2(10, 10);


            button[13].effectColor = Color.yellow;
            button[13].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 14)
        {
            button[10].effectColor = Color.red;
            button[10].effectDistance = new Vector2(10, 10);


            button[14].effectColor = Color.yellow;
            button[14].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 15)
        {
            button[11].effectColor = Color.red;
            button[12].effectColor = Color.red;
            button[11].effectDistance = new Vector2(10, 10);
            button[12].effectDistance = new Vector2(10, 10);


            button[15].effectColor = Color.yellow;
            button[15].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 16)
        {
            button[13].effectColor = Color.red;
            button[14].effectColor = Color.red;
            button[13].effectDistance = new Vector2(10, 10);
            button[14].effectDistance = new Vector2(10, 10);


            button[16].effectColor = Color.yellow;
            button[16].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 17)
        {
            button[15].effectColor = Color.red;
            button[15].effectDistance = new Vector2(10, 10);


            button[17].effectColor = Color.yellow;
            button[17].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 18)
        {
            button[15].effectColor = Color.red;
            button[16].effectColor = Color.red;
            button[15].effectDistance = new Vector2(10, 10);
            button[16].effectDistance = new Vector2(10, 10);


            button[18].effectColor = Color.yellow;
            button[18].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 19)
        {
            button[16].effectColor = Color.red;
            button[16].effectDistance = new Vector2(10, 10);


            button[19].effectColor = Color.yellow;
            button[19].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 20)
        {
            button[17].effectColor = Color.red;
            button[18].effectColor = Color.red;
            button[17].effectDistance = new Vector2(10, 10);
            button[18].effectDistance = new Vector2(10, 10);


            button[20].effectColor = Color.yellow;
            button[20].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 21)
        {
            button[18].effectColor = Color.red;
            button[19].effectColor = Color.red;
            button[18].effectDistance = new Vector2(10, 10);
            button[19].effectDistance = new Vector2(10, 10);


            button[21].effectColor = Color.yellow;
            button[21].effectDistance = new Vector2(10, 10);
        }
        if (buttonNo == 22)
        {
            button[20].effectColor = Color.red;
            button[21].effectColor = Color.red;
            button[20].effectDistance = new Vector2(10, 10);
            button[21].effectDistance = new Vector2(10, 10);


            button[22].effectColor = Color.yellow;
            button[22].effectDistance = new Vector2(10, 10);
        }
    }
}
