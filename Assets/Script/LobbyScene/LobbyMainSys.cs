using UnityEngine;

public class LobbyMainSys : MonoBehaviour
{
    //プレイヤーのオブジェクト
    [SerializeField]
    GameObject playerObj = null;

    //プレイヤーの移動中フラグ
    bool mouseFlag = false;

    //マウスの左クリックされた時のポジションを保存用
    Vector3 mousePos;

    //移動スピード
    [SerializeField]
    float speed = 0;

    //移動先に表示するprefabオブジェクト
    [SerializeField]
    GameObject mousePosSp = null;

    //レイヤーがプレイヤーオブジェの1つ後ろのキャンバス
    [SerializeField]
    GameObject mainCanvas = null;

    //クローンされたマウスprefabを格納用
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
        //マウスの左クリックの入力状況
        if(Input.GetMouseButtonDown(0))
        {
            mouseFlag = true;
            //入力された時のマウスポジションを保存
            mousePos = Input.mousePosition;

            //CloneMousePosという名前のオブジェクトを検索
            GameObject cloneMousePos = GameObject.Find("CloneMousePos");

            //cloneMousePosが存在する時(エラー回避)
            if (cloneMousePos != null)
            {
                //そのオブジェクトを消す
                Destroy(cloneMousePos);
            }

            // ゲームオブジェクトを複製
            mouseSp = Instantiate(mousePosSp);

            // GameManagerを親に指定
            mouseSp.transform.parent = mainCanvas.transform;
                                                            
            // 必要に応じて座標の調整
            mouseSp.transform.position = mousePos;

            //クローンしたオブジェクトの名前を変更
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
        //指定した場所までたどり着いたらフラグをオフにする
        if(playerObj.transform.position == mousePos)
        {
            mouseFlag= false;
        }
    }

}
