//=======================================================================================
//! @file   CameraSwitchButton.cs
//! @brief  カメラの切り替えボタンの処理
//! @author 田中歩夢
//! @date   10月04日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//カメラの切り替えボタンの処理
public class CameraSwitchButton : MonoBehaviour
{
    //2Dカメラ ↔ 3Dカメラへ動くクラス
    [SerializeField]
    private CameraDirector m_cameradirector = default;

    //ゲームオブジェクト（ボタン）
    [SerializeField]
    private Button m_button = default;

    //カラー設定
    //白色
    static readonly Color m_buttonColor1 = Color.white;
    //灰色
    static readonly Color m_buttonColor2 = Color.gray;

    //ボタンの小サイズ
    private Vector2 m_btnSmallScale = default;
    //ボタンの元のサイズ
    private Vector2 m_btnDefaultScale = default;

    // Start is called before the first frame update
    void Start()
    {
        //ボタンの色を白色に設定
        m_button.image.color = m_buttonColor1;

        m_btnSmallScale = new Vector2(140, 110);
        m_btnDefaultScale = new Vector2(150, 120);

        GetComponent<RectTransform>().sizeDelta = m_btnDefaultScale;
    }

    // Update is called once per frame
    void Update()
    {
        //２Dと３Dのカメラの切り替え中フラグ
        bool cameraSwitch2D3D = m_cameradirector.IsMove2D3DCameraPos;

        //２Dと３Dのカメラの切り替え中かどうか
        if (!cameraSwitch2D3D)
        {
            //ボタンの色を白色に設定
            m_button.image.color = m_buttonColor1;
        }
        else
        {
            //ボタンの色を灰色に設定
            m_button.image.color = m_buttonColor2;
        }
    }

    //ボタンが押されたとき
    public void OnClick()
    {
        m_cameradirector.ChangeCamera2D3D();

    }

    public void PointUp()
    {
        GetComponent<RectTransform>().sizeDelta = m_btnDefaultScale;
    }

    public void PointDown()
    {
        GetComponent<RectTransform>().sizeDelta = m_btnSmallScale;
    }
}
