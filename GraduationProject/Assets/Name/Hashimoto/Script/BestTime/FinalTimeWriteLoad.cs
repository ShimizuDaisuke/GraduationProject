//=======================================================================================
//! @file   FinalTimeWriteLoad.cs
//!
//! @brief  テキストにデータを追加したり読み込んだりする  
//!
//! @author 橋本奉武
//!
//! @date   12月24日
//!
//! @note <参考>https://gametukurikata.com/csharp/readwritefile
//!             
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FinalTimeWriteLoad : MonoBehaviour
{
    // ファイル名(ファイルパスは含らない)
    private string FileName = "timescore.csv";

    // 上記のファイルがある相対パス(「@」を付けると、エスケープを行わずに「\」を文字列に含まれる)
    private string Filepass = @"Resources/CSV";

  
    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // ファイル名をファイルパスに含ませる
        FileName = Application.dataPath + @"/" + Filepass + @"/" + FileName;

        Write(FileName, 200);
        Write(FileName, 100);
        Write(FileName, 300);
        int[] a = Load(FileName);
        Debug.Log(" 1位" + a[0] + " 2位" + a[1] + " 3位" + a[2]);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// データをファイルへ追記する
    /// </summary>
    /// <param name="filepass">ファイル名(ファイルパスも含む)</param>
    /// <param name="newdata">ファイルに書き込むデータ</param>
    private void Write(string filepass,int newtimescore)
    {
        // 指定されたファイルが存在しているか確認する
        CheckAndCreateNewFile(filepass);

        // タイムスコアをファイルへ追記する
        File.AppendAllText(filepass, newtimescore.ToString()+"\n");
    }

    /// <summary>
    /// ファイルのデータを読み込む
    /// </summary>
    /// <param name="filepass">ファイル名(ファイルパスも含む)</param>
    /// <returns></returns>
    private int[] Load(string filepass)
    {
        // 指定されたファイルが存在しているか確認する
        CheckAndCreateNewFile(filepass);

        // ファイルに書かれていた内容を、一括で文字として読み込む
        string allText = File.ReadAllText(filepass);

        // 上記で読み込んだ内容を改行ごとに分ける
        string[] textMessage = allText.Split('\n');

        // 上記で読み込んだ内容の量
        // ※ -1 : 今回は語尾の文字[""]を除くため
        int datatotal = textMessage.Length - 1;

        // タイムスコア用のデータの入れ物を作成する
        int[] timescore = new int[datatotal];

        // それぞれタイムスコア用のデータの入れ物に、上記で読み込んだ内容を入れる
        for(int i = 0; i < datatotal; i++)
        {
            timescore[i] = int.Parse(textMessage[i]);
        }

        // タイムが早い順に並び変える
        Array.Sort(timescore);

        // 早い順に並び変えたタイムを返す
        return timescore;
    }

    /// <summary>
    /// ファイルが存在していない場合、ファイルを新規作成する
    /// </summary>
    /// <param name="filepass">ファイル名(ファイルパスも含む)</param>
    private void CheckAndCreateNewFile(string filepass)
    {
        // ファイルが存在していない場合
        if(!File.Exists(filepass))
        {
            // 指定された階層パス
            string pass = Application.dataPath + @"/" + Filepass;

            // 指定された階層パスのファルダを作成する
            Directory.CreateDirectory(pass);

            // ファイルを作成する
            using (File.Create(filepass)) { }
        }
    }

    /// <summary>
    /// ファイルを削除する
    /// </summary>
    /// <param name="filepass">ファイル名(ファイルパスも含む)</param>
    private void DeleteFile(string filepass)
    {
        // 既にファイルが存在していない場合、何もしない
        if (!File.Exists(filepass)) return;

        // ファイルを削除する
        File.Delete(filepass);
    }
}
