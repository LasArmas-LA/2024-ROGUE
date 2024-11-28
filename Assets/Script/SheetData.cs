using UnityEngine;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Prjectビューの右クリックメニューにScriptableObjectを生成するメニューを追加
// fileName: 生成されるScriptableObjectのファイル名
// menuName: criptableObjectを生成するメニューの名前
// order: メニューの表示順(0なので一番上に表示される)
[CreateAssetMenu(fileName = "SheetData", menuName = "ScriptableObjectの生成/SheetDataの生成", order = 0)]

// シートデータを管理するScriptableObject
public class SheetData : ScriptableObject
{
    public SheetDataRecord[] sheetDataRecord;    // シートデータのリスト
    [SerializeField] string url;    // スプレットシートのURL

    [System.Serializable]
    public class SheetDataRecord
    {
        /////////////////////////////////////////////
        // スプレットシートの列に対応する変数を定義
        // 好きに変更してください
        public string name;
        public float maxhp;
        public float maxmp;
        public float power;
        public int probability;
                          /////////////////////////////////////////////
    }

#if UNITY_EDITOR
    //スプレットシートの情報をsheetDataRecordに反映させるメソッド
    public void LoadSheetData()
    {
        // urlからCSV形式の文字列をダウンロードする
        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
        }

        // ダウンロードしたCSVをデシリアライズ(SerializeFieldに入力)する
        sheetDataRecord = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // データの更新が完了したら、ScriptableObjectを保存する
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" データの更新を完了しました");
    }
#endif
}

//SheetDataのインスペクタにLoadSheetData()を呼び出すボタンを表示するクラス
#if UNITY_EDITOR
[CustomEditor(typeof(SheetData))]
public class SheetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクタを表示
        base.OnInspectorGUI();

        // データ更新ボタンを表示
        if (GUILayout.Button("データ更新"))
        {
            ((SheetData)target).LoadSheetData();
        }
    }
}
#endif