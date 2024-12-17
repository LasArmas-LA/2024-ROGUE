using UnityEngine;

public class LobbyMainSys : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj = null;

    bool mouseFlag = false;

    Vector3 mousePos;
    [SerializeField]
    float speed = 0;

    [SerializeField]
    GameObject mousePosSp = null;
    [SerializeField]
    GameObject mainCanvas = null;

    GameObject mouseSp = null;

    void Start()
    {
        
    }

    void Update()
    {
        KeyDown();
        PlayerMove();
    }

    void KeyDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseFlag = true;
            mousePos = Input.mousePosition;

            GameObject cloneMousePos = GameObject.Find("CloneMousePos");

            if(cloneMousePos != null)
            {
                Destroy(cloneMousePos);
            }

            // ゲームオブジェクトを複製
            mouseSp = Instantiate(mousePosSp);

            // GameManagerを親に指定
            mouseSp.transform.parent = mainCanvas.transform;
                                                            
            // 必要に応じて座標の調整
            mouseSp.transform.position = mousePos;

            mouseSp.name = "CloneMousePos";
        }
    }

    void PlayerMove()
    {
        //ワールド座標と自身の座標を比較しループ
        if(mouseFlag)
        {
            //指定した座標に向かって移動
            playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, mousePos, speed * Time.deltaTime);
        }
        if(playerObj.transform.position == mousePos)
        {
            mouseFlag= false;
        }
    }

}
