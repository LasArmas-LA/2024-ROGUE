using UnityEngine;
using UnityEngine.Networking;
using static SheetData;
using System.Collections.Generic;


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
    public CharacterParamDataRecord[] characterParamDataRecord;    // シートデータのリスト
    public ArmorParamRecord[] armorParamRecord;    // シートデータのリスト

    [SerializeField] string url;    // スプレットシートのURL

    [System.Serializable]
    public class CharacterParamDataRecord
    {
        /////////////////////////////////////////////
        // スプレットシートの列に対応する変数を定義
        // 好きに変更してください
        public string C_NAME;
        public float C_MAXHP;
        public float C_MAXMP;
        public float C_ATK;
        public float C_DEF;
                          /////////////////////////////////////////////
    }
    [System.Serializable]
    public class ArmorParamRecord
    {
        /////////////////////////////////////////////
        // スプレットシートの列に対応する変数を定義
        // 好きに変更してください
        public string A_NAME;//名前
        public float A_MAXHP;//HP
        public float A_MAXMP;//MP
        public float A_ATK;//攻撃力
        public float A_DEF;//防御力
        public float A_AVOIDANCE;//回避率
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
        characterParamDataRecord = CSVSerializer.Deserialize<CharacterParamDataRecord>(request.downloadHandler.text);
        armorParamRecord = CSVSerializer.Deserialize<ArmorParamRecord>(request.downloadHandler.text);
         
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