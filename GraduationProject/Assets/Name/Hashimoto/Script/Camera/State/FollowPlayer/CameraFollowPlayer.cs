// -----------------------------------------------------------------------------------------
//! @file       CameraFollowPlayer.cs
//!
//! @brief      カメラがプレイヤーに追従する
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.28
//!
//! @note       ※後でカメラ2D⇔3D移動する場合:時間をかけずにカメラを目的地へ動かす 解決策:カメラを動かす時間 + その場からプレイヤーを動かさない時間
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // 2Dカメラ
    [SerializeField] private GameObject Camera2D = default;

    // 3Dカメラ
    [SerializeField] private GameObject Camera3D = default;

    // 2Dカメラとプレイヤーの距離
    private Vector3 DirectionCamera2DPlayerPos;

    // 3Dカメラとプレイヤーの距離
    private Vector3 DirectionCamera3DPlayerPos;

    // プレイヤー
    private GameObject Player;

    // カメラがプレイヤーに追従するのにかかる時間(/s) <カメラのブレを防止するため>
    private float FollowingTime = 5.0f;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");

        // ---------------------------------------------------------------------

        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // プレイヤーの位置
        Vector3 playerpos = new Vector3(Player.transform.position.x, (float)playerheight, Player.transform.position.z);

        // 2Dカメラとプレイヤーの距離を計る
        DirectionCamera2DPlayerPos = Camera2D.transform.position - playerpos;

        // 3Dカメラとプレイヤーの距離を計る
        DirectionCamera3DPlayerPos = Camera3D.transform.position - playerpos;
    }

    /// <summary>
    /// カメラは、常に一定の距離でプレイヤーに追従する [メイン]
    /// </summary>
    public void FollowPlayer()
    {
        // 2Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera2D, DirectionCamera2DPlayerPos);

        // 3Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera3D, DirectionCamera3DPlayerPos);
    }

    /// <summary>
    /// カメラは、常に一定の距離でプレイヤーに追従する [詳しく]
    /// </summary>
    /// <param name="camera">カメラ</param>
    /// <param name="direction">カメラとプレイヤーの距離</param>
    private void FollowPlayer(GameObject camera,Vector3 direction)
    {
        // 現在のカメラの位置
        Vector3 NowPos = camera.transform.position;

        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // カメラが次へ進む目的地の位置 
        Vector3 NextPos =  new Vector3(Player.transform.position.x + direction.x,
                                       direction.y + playerheight,
                                       Player.transform.position.z+direction.z);

        // 時間をかけて、カメラはプレイヤーに追従する <カメラのブレ防止>
        camera.transform.position = Vector3.Lerp(NowPos, NextPos, FollowingTime * Time.deltaTime); 
    }

    /// <summary>
    /// カメラが2D⇔3D移動する場合、時間をかけずにプレイヤーに追従する
    /// </summary>
    public void FllowPlayerNoSlowy()
    {
        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // ----------------------------------------------------------------------------------------------------

        // 2Dカメラが次へ進む目的地の位置 
        Vector3 Camera2DNextPos = new Vector3(Player.transform.position.x + DirectionCamera2DPlayerPos.x,
                                              (float)playerheight         + DirectionCamera2DPlayerPos.y,
                                              Player.transform.position.z + DirectionCamera2DPlayerPos.z);

        // 2Dカメラはプレイヤーに時間かけずに追従する
        Camera2D.transform.position = Camera2DNextPos;

        // ----------------------------------------------------------------------------------------------------

        // 3Dカメラが次へ進む目的地の位置 
        Vector3 Camera3DNextPos = new Vector3(Player.transform.position.x + DirectionCamera3DPlayerPos.x,
                                               (float)playerheight        + DirectionCamera3DPlayerPos.y,
                                              Player.transform.position.z + DirectionCamera3DPlayerPos.z);


        // 3Dカメラはプレイヤーに時間かけずに追従する
        Camera3D.transform.position = Camera3DNextPos;
    }
}
