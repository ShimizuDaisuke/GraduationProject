//=======================================================================================
//! @file CreateQREditorWindow
//! @brief QRコード作成エディタクラス
//! @author 志水大輔
//! @date 9/26
//! @note 解読中
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateQREditorWindow : EditorWindow
{
    // QRコードの大きさ
   public enum QRImageSize
    {
        SIZE_128 = 7,
        SIZE_256,
    }

    // Window/QRに作成
    [MenuItem("Window/QR")]

    //=======================================================================================
    //! @brief 初期化処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    static void Init()
    {
        // エディタの作成
        CreateQREditorWindow window = EditorWindow.GetWindow<CreateQREditorWindow>();
        // 見えるようにする
        window.Show();
    }

    // サイズの変数、初期化
    QRImageSize _size = QRImageSize.SIZE_256;
    // バーコードで表したい内容
    string _content = "";

    //=======================================================================================
    //! @brief 毎フレーム描画されている
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void OnGUI()
    {
        // ゲームデータのフォルダパスを返す
        string savePath = Application.dataPath + "/Name/Shimizu/qr.png";
        // contentに書き込みする
        _content = GUILayout.TextArea(_content, GUILayout.Height(30f));
        // enumをポップアップして選択するフィールドを作成
        _size = (QRImageSize)EditorGUILayout.EnumPopup(_size);
        // GUIグループ内のGUI要素を操作不可にする場合に使用
        EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_content));
        // セーブボタンを押したら
        if(GUILayout.Button("Save"))
        {
            // サイズの設定
            int size = (int)Mathf.Pow(2f, (int)_size);
            // QRCodeの作成
            Texture2D tex = QRCodeHelper.CreateQRCode(_content, size, size);
            // ファイルを開き自動で閉じるようにする
            using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                // テクスチャをPNG形式にエンコード
                byte[] b = tex.EncodeToPNG();
                // 書き込む
                fs.Write(b, 0, b.Length);
            }
            // 何かしら変更があったアセットをすべてインポート
            AssetDatabase.Refresh();
        }
        // BeginDisabledGroupで開始された無効なグループを終了
        EditorGUI.EndDisabledGroup();
    }
}