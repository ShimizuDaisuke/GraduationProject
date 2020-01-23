//=======================================================================================
//! @file   QRImage
//! @brief  テキストを読み込んだ値によって変更
//! @author 服部晃大
//! @date   1/23
//! @note   難しい
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRImage : MonoBehaviour
{
    SpriteRenderer MainSpriteRenderer;
    public GameObject image;
    //public enum QRImages
    //{
    //    //0,画像無し(読み込めない)
    //    NULL_IMAGE,

    //    //1,プレイヤーを消しゴムに変更
    //    CHANGE_ERASER,

    //    //2,プレイヤーを鉄に変更
    //    CHANGE_IRON,

    //    //3,シーソーを定規に変更
    //    CHANGE_RULER,

    //    //4,クレヨンが壊れなくなる
    //    STRANG_CREYON,

    //    //5,振り子停止
    //    STOP_PENDULUM,

    //    //6,プレイヤーの速度UP
    //    PLAYER_SPEED_UP,

    //    //7,時間停止
    //    STOP_TIME,

    //    //この列挙型の最大数
    //    MAX
    //}



    [SerializeField]
    public Sprite[] sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    //=======================================================================================
    // ゲーム以外のQRコードを読み込んだ時の画像
    //=======================================================================================
    public void NullQR_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[0];
    }

    //=======================================================================================
    // プレイヤーの変更画像 (消しゴム)
    //=======================================================================================
    public void PlayerEraser_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[1];
    }

    //=======================================================================================
    // プレイヤーの変更画像 (鉄)
    //=======================================================================================
    public void PlayerIron_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[2];
    }

    //=======================================================================================
    // シーソーの変更画像 (シーソー -> 定規)
    //=======================================================================================
    public void Seesaw_Chang_Ruler_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[3];
    }

    //=======================================================================================
    // クレヨンが壊れなくなる画像
    //=======================================================================================
    public void Crayon_No_Break_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[4];
    }

    //=======================================================================================
    // 振り子停止する画像
    //=======================================================================================
    public void Huriko_Stop_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[5];
    }

    //=======================================================================================
    // プレイヤーの速度UP画像
    //=======================================================================================
    public void Player_Speed_Up_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[6];
    }

    //=======================================================================================
    // プレイヤーの速度UP画像
    //=======================================================================================
    public void Stop_Timer_Image()
    {
        image.gameObject.GetComponent<Image>().sprite = sprite[7];
    }
}
