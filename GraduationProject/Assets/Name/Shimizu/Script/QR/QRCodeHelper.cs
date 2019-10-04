//=======================================================================================
//! @file   QRCodeHelper
//! @brief  読み書きするクラス
//! @author 志水大輔
//! @date   9/26
//! @note   書き換える可能性あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCodeHelper
{
    //=======================================================================================
    //! @brief QRの読み込み
    //! @param[in] tex 読み込んだ画像
    //! @param[out] なし
    //! @return r.Text
    //=======================================================================================
    static public string Read(Texture2D tex)
    {
        BarcodeReader reader = new BarcodeReader();

        // 読み込んだ画像の幅,高さ
        int w = tex.width;
        int h = tex.height;

        // Color32形式のピクセルカラー配列の取得
        var pixel32s = tex.GetPixels32();
        // QRコードの解析
        var r = reader.Decode(pixel32s, w, h);
        return r.Text;
    }

    //=======================================================================================
    //! @brief QRコードの読み込み(解析)
    //! @param[in] tex 読み込んだ画像
    //! @param[out] 関数内で値を変える引数名 なんという引数なのか
    //! @return r.Text, error  
    //=======================================================================================
    public static string Read(WebCamTexture tex)
    {
        BarcodeReader reader = new BarcodeReader();

        // 読み込んだ画像の幅,高さ
        int w = tex.width;
        int h = tex.height;
        // Color32形式のピクセルカラー配列の取得
        var pixel32s = tex.GetPixels32();

        // コードの解析
        var r = reader.Decode(pixel32s, w, h);
        if (r != null)
        {
            return r.Text;
        }
        else
        {
            return "error";
        }
    }

    //=======================================================================================
    //! @brief QRコードの読み込み
    //! @param[in] tex 読み込んだ画像
    //! @param[out] 関数内で値を変える引数名 なんという引数なのか
    //! @return r.Text, error  
    //=======================================================================================
    static public Result Read2(WebCamTexture tex)
    {
        BarcodeReader reader = new BarcodeReader();

        // 読み込んだ画像の幅,高さ
        int w = tex.width;
        int h = tex.height;
        // Color32形式のピクセルカラー配列の取得
        var pixel32s = tex.GetPixels32();

        // QRコードの解析
        Result r = reader.Decode(pixel32s, w, h);
        // 解析したQRコードを返す
        return r;
    }

    //=======================================================================================
    //! @brief QRコードの作成
    //! @param[in] str = 文字を
    //! @param[out] 関数内で値を変える引数名 なんという引数なのか
    //! @return r.Text, error  
    //=======================================================================================
    static public Texture2D CreateQRCode(string str, int w, int h)
    {
        // QRコードの細かい色設定
        var tex = new Texture2D(w, h, TextureFormat.ARGB32, false);

        // 色の情報を書き込む
        var content = Write(str, w, h);

        // ピクセルカラーの配列の設定
        tex.SetPixels32(content);
        // テクスチャの作成
        tex.Apply();
        // 作成されたテクスチャを渡す
        return tex;
    }
    //=======================================================================================
    //! @brief 色の情報を書き込む関数
    //! @param[in] content = 書き込まれる情報の引数, w = 幅, h = 高さ
    //! @param[out] なし
    //! @return writer.Write(content) 
    //=======================================================================================
    static Color32[] Write(string content, int w, int h)
    {
        // Debug
        Debug.Log(content + " / " + w + " / " + h);

        BarcodeWriter writer = new BarcodeWriter
        {
            // このパッケージが認識しているバーコード形式を列挙します
            Format = BarcodeFormat.QR_CODE,
            // 新規のQRコード作成する,高さ幅を設定
            Options = new QrCodeEncodingOptions
            {
                Width = w,
                Height = h
            }
        };
        // contentという情報のQRコードの書き込み
        return writer.Write(content);
    }
}
