using UnityEngine;
using UnityEngine.Networking;
using static SheetData;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

// Prject�r���[�̉E�N���b�N���j���[��ScriptableObject�𐶐����郁�j���[��ǉ�
// fileName: ���������ScriptableObject�̃t�@�C����
// menuName: criptableObject�𐶐����郁�j���[�̖��O
// order: ���j���[�̕\����(0�Ȃ̂ň�ԏ�ɕ\�������)
[CreateAssetMenu(fileName = "SheetData", menuName = "ScriptableObject�̐���/SheetData�̐���", order = 0)]

// �V�[�g�f�[�^���Ǘ�����ScriptableObject
public class SheetData : ScriptableObject
{
    public CharacterParamDataRecord[] characterParamDataRecord;    // �V�[�g�f�[�^�̃��X�g
    public ArmorParamRecord[] armorParamRecord;    // �V�[�g�f�[�^�̃��X�g

    [SerializeField] string url;    // �X�v���b�g�V�[�g��URL

    [System.Serializable]
    public class CharacterParamDataRecord
    {
        /////////////////////////////////////////////
        // �X�v���b�g�V�[�g�̗�ɑΉ�����ϐ����`
        // �D���ɕύX���Ă�������
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
        // �X�v���b�g�V�[�g�̗�ɑΉ�����ϐ����`
        // �D���ɕύX���Ă�������
        public string A_NAME;//���O
        public float A_MAXHP;//HP
        public float A_MAXMP;//MP
        public float A_ATK;//�U����
        public float A_DEF;//�h���
        public float A_AVOIDANCE;//���
                          /////////////////////////////////////////////
    }



#if UNITY_EDITOR
        //�X�v���b�g�V�[�g�̏���sheetDataRecord�ɔ��f�����郁�\�b�h
        public void LoadSheetData()
    {
        // url����CSV�`���̕�������_�E�����[�h����
        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
        }

        // �_�E�����[�h����CSV���f�V���A���C�Y(SerializeField�ɓ���)����
        characterParamDataRecord = CSVSerializer.Deserialize<CharacterParamDataRecord>(request.downloadHandler.text);
        armorParamRecord = CSVSerializer.Deserialize<ArmorParamRecord>(request.downloadHandler.text);
         
        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" �f�[�^�̍X�V���������܂���");
    }
#endif
}

//SheetData�̃C���X�y�N�^��LoadSheetData()���Ăяo���{�^����\������N���X
#if UNITY_EDITOR
[CustomEditor(typeof(SheetData))]
public class SheetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �f�t�H���g�̃C���X�y�N�^��\��
        base.OnInspectorGUI();

        // �f�[�^�X�V�{�^����\��
        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((SheetData)target).LoadSheetData();
        }
    }
}
#endif