using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class Fade : MonoBehaviour
{

    private float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    private float red, green, blue, alfa;   //パネルの色、不透明度を管理

    private bool m_FadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    private bool m_FadeIn = true;   //フェードイン処理の開始、完了を管理するフラグ

    Image fadeImage;                //透明度を変更するパネルのイメージ

    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;        //赤
        green = fadeImage.color.g;      //緑
        blue = fadeImage.color.b;       //青
        alfa = fadeImage.color.a;       //α値
    }

    void Update()
    {
        //フェードインを行う
        if (m_FadeIn)
        {
            StartFadeIn();
        }

        //フェードアウトを行う
        if (m_FadeOut)
        {
            StartFadeOut();
        }
    }

    //======================================================================================= 
    //! @brief      フェードインを行う関数
    //=======================================================================================
    void StartFadeIn()
    {
        alfa -= fadeSpeed;                  //不透明度を徐々に下げる
        SetAlpha();                         //変更した不透明度パネルに反映する
        m_FadeOut = false;                  // フェードイン中はフェードアウトを行えないようにする
        if (alfa <= 0)                      //完全に透明になったら処理を抜ける
        {                  
            m_FadeIn = false;
            fadeImage.enabled = false;      //パネルの表示をオフにする
        }
    }

    //======================================================================================= 
    //! @brief      フェードアウトを行う関数
    //=======================================================================================
    void StartFadeOut()
    {
        fadeImage.enabled = true;           //パネルの表示をオンにする
        alfa += fadeSpeed;                  //不透明度を徐々にあげる
        SetAlpha();                         //変更した透明度をパネルに反映する
        m_FadeIn = false;                   // フェードアウト中はフェードインを行えないようにする
        if (alfa >= 1)
        {                                   //完全に不透明になったら処理を抜ける
            m_FadeOut = false;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    // 取得設定関数
    public bool FadeIn { get { return m_FadeIn; } set { m_FadeIn = value; } }

    // 取得設定関数
    public bool FadeOut { get { return m_FadeOut; } set { m_FadeOut = value; } }
}