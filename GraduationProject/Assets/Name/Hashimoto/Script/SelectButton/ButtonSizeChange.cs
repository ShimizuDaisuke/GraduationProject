// -----------------------------------------------------------------------------------------
//! @file       ButtonSizeChange.cs
//!
//! @brief      ボタンが押されたとき、ボタンのサイズを縮める
//!
//! @author     橋本 奉武
//!
//! @date       2019.1.29
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeChange : MonoBehaviour
{
    // ボタン
    [SerializeField]
    private Button button = default;

    // ボタンサイズを縮める量
    [SerializeField]
    private float smallSize = 10.0f;

    // ボタンの元のサイズ
    private Vector3 baseSize;
    

    void Start()
    {
        // ボタンに何も当てはめていなかった場合
        if(button == null)
        {
            // ボタンはこのスクリプトにアタッチされたオブジェクトにする
            button = gameObject.GetComponent<Button>();
        }

        // ボタンの元のサイズを取得する
        baseSize = button.GetComponent<RectTransform>().sizeDelta;
    }

    /// <summary>
    /// ボタンを押した瞬間
    /// </summary>
    public void PointUp()
    {
        // ボタンのサイズを元に戻す
        button.GetComponent<RectTransform>().sizeDelta = baseSize;
    }

    /// <summary>
    /// ボタンから離した瞬間
    /// </summary>
    public void PointDown()
    {
        // ボタンのサイズを縮める
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(baseSize.x - smallSize, baseSize.y - smallSize);
    }
}
