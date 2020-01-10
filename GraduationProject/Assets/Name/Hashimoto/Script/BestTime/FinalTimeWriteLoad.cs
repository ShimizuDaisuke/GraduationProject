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
    private string FilePath = @"Resources/CSV";
 
    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 指定されたファイルの階層ファルダで、相対パス(〇/□/～)から絶対パス(フルパス C:○/～) へ変える
        FilePath = Application.dataPath + @"/" + FilePath;

        // ファイル名にファイルパスを含ませる
        FileName = FilePath + @"/" + FileName;


        // テスト ----------------------------------------------------------------
        
        // 削除
        DeleteFile(FileName);
        // 作成
        CheckAndCreateNewFile(FileName,FilePath);

        // 書き込み
        int a1 = UnityEngine.Random.Range(3, 6);
        for(int i = 0;i<a1;i++)
        {
            int a2= UnityEngine.Random.Range(1, 5);
            int a3 = UnityEngine.Random.Range(1, 500);
            for(int j=0;j < a2; j++)
            {
                Write(a3, FileName);
            }

        }        
        // 読み込み
        int[] data = Load(FileName);


        // 表示

        // 行数
        int row = 1;
        foreach(int part in data)
        {
            Debug.Log(row +"段目 "+FindJuni(data, part) + "位:" + part + "秒");
            row++;
        }

       
        //------------------------------------------------------------------------


    }

    // --------------------------------------------------------------------------------------------------

    /// <summary>
    /// 順位決めする
    /// </summary>
    /// <param name="juni">順位</param>
    /// <param name="junitotal">順位の総合</param>
    /// <param name="besttimescore">最速タイムスコア</param>
    /// <param name="nowtimescore">現在のタイムスコア</param>
    public void DecideJuni(ref int juni, ref int junitotal,ref int besttimescore, int nowtimescore = 0)
    {
        // 現在のタイムスコアがない場合、何もしない
        if (nowtimescore == 0) return;

        // 現在のタイムスコアを保存する
        Write(nowtimescore, FileName);

        // これまでに保存してきたタイムスコアを書き出す
        int[] saveddata = Load(FileName);

        // 現在のタイムスコアの順位を探す
        juni = FindJuni(saveddata, nowtimescore);

        // 順位の総合を取得する
        junitotal = saveddata.Length;

        // これまでに保存してきたデータの中で、一番速いタイムスコアを取得する
        besttimescore = saveddata[0];

    }

    /// <summary>
    /// これまでに保存してきたタイムスコアを消す
    /// </summary>
    public void DeleteSavedData()
    {
        // これまでに保存してきたタイムスコアを削除する
        DeleteFile(FileName);

        // 新たにタイムスコアを保存できるように、基盤を作成する
        CheckAndCreateNewFile(FileName, FilePath);
    }


    // --------------------------------------------------------------------------------------------------

    /// <summary>
    /// データをファイルへ追記する
    /// </summary>
    /// <param name="newdata">ファイルに書き込むデータ</param>
    /// <param name="filename">ファイル名(ファイルパスも含む)</param>
    /// <param name="filepath">そのファイルにある階層フォルダー(絶対パス(フルパス C:○/～))</param>
    private void Write(int newtimescore,string filename,string filepath="")
    {
        // 階層フォルダーが指定されている場合、
        if(filepath != "")
        {
            // 指定されたファイルが存在しているか確認する
            CheckAndCreateNewFile(filename, filepath);
        }

        // タイムスコアをファイルへ追記する
        File.AppendAllText(filename, newtimescore.ToString()+"\n");
    }

    /// <summary>
    /// ファイルのデータを読み込む
    /// </summary>
    /// <param name="filename">ファイル名(ファイルパスも含む)</param>
    /// <param name="filepath">そのファイルにある階層フォルダー(絶対パス(フルパス C:○/～))</param>
    /// <returns></returns>
    private int[] Load(string filename, string filepath = "")
    {
        // 階層フォルダーが指定されている場合、
        if (filepath != "")
        {
            // 指定されたファイルが存在しているか確認する
            CheckAndCreateNewFile(filename, filepath);
        }

        // ファイルに書かれていた内容を、一括で文字として読み込む
        string allText = File.ReadAllText(filename);

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
    /// <param name="filename">ファイル名(ファイルパスも含む)</param>
    /// <param name="filepath">そのファイルにある階層フォルダー(絶対パス(フルパス C:○/～))</param>
    private void CheckAndCreateNewFile(string filename,string filepath)
    {
        // ファイルが存在していない場合
        if(!File.Exists(filename))
        {
            // 指定された階層パスのファルダを作成する
            Directory.CreateDirectory(filepath);

            // ファイルを作成する
            using (File.Create(filename)) { }
        }
    }

    /// <summary>
    /// ファイルを削除する
    /// </summary>
    /// <param name="filename">ファイル名(ファイルパスも含む)</param>
    private void DeleteFile(string filename)
    {
        // 既にファイルが存在していない場合、何もしない
        if (!File.Exists(filename)) return;

        // ファイルを削除する
        File.Delete(filename);
    }

    // =================================================================================================================

    /// <summary>
    /// 指定したスコアが順位が何位なのか探す
    /// </summary>
    /// <param name="data">これまでのタイムスコア</param>
    /// <param name="nowdata">現在のタイムスコア</param>
    /// <returns>順位</returns>
    private int FindJuni(int[] timescore,int nowdata)
    {
        // これまでのタイムスコアの量
        int total = timescore.Length;

        //　順位
        int juni = 0;

        // これまでのタイムスコアの順番
        int num = 0;
        while(num < total)
        {
            // これまでのスコアの中に指定したスコアがあった場合
            if(timescore[num] == nowdata)
            {
                // 順位は「0位」から数えたため、一つ増やす
                juni++;

                // そのタイムスコアの順位を渡す
                return juni;
            }

            // 次のデータを注目する
            num++;

            // 現在見ているこれまでのスコアデータは、前のスコアデータと一致していない場合            
            if (timescore[num] != timescore[num - 1])
            {
                // 順位をそろえる
                juni = num;
            }
        }


        // 現在のスコアがこれまでのスコアに保存されていなかった
        return -1;
    }


}
