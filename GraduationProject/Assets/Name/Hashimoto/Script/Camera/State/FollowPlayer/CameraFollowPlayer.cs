// -----------------------------------------------------------------------------------------
//! @file       CameraFollowPlayer.cs
//!
//! @brief      カメラがプレイヤーに追従する
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.28
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

    // カメラ２Dの高さを変える当たり判定クラス
    [SerializeField]
    private Camera2DHitHightFlag m_camera2DHitHightFlag = default;

    //カメラディレクター
    [SerializeField]
    private CameraDirector m_cameraDirector = default;

    //カメラの高さ
    [SerializeField]
    private float m_cameraHight = 0.0f;

    // 2Dカメラとプレイヤーの距離
    private Vector3 DirectionCamera2DPlayerPos;

    // 3Dカメラとプレイヤーの距離
    private Vector3 DirectionCamera3DPlayerPos;

    // プレイヤー
    private GameObject Player;

    // カメラがプレイヤーに追従するのにかかる時間(/s) <カメラのブレを防止するため>
    private float FollowingTime = 5.0f;

    // 2Dカメラの向き
    private Vector3 RotationCamera2D;

    // 3Dカメラの向き
    private Vector3 RotationCamera3D;

    // プレイヤーが動いても、カメラが上下に揺れてないように制限をつける高さ
    private int CameraHeight_NoShake = 5;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");

        // 2Dカメラの向きを取得する
        RotationCamera2D = Camera2D.transform.localEulerAngles;

        // 3Dカメラの向きを取得する
        RotationCamera3D = Camera3D.transform.localEulerAngles;
        
        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // プレイヤーが動いてもカメラが上下に揺れないように制限する
        playerheight = playerheight - (playerheight % CameraHeight_NoShake);

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
        FollowPlayer(Camera2D, DirectionCamera2DPlayerPos, RotationCamera2D);

        // 3Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera3D, DirectionCamera3DPlayerPos, RotationCamera3D);
    }

    /// <summary>
    /// カメラは、常に一定の距離でプレイヤーに追従する [詳しく]
    /// </summary>
    /// <param name="camera">カメラ</param>
    /// <param name="direction">カメラとプレイヤーの距離</param>
    /// <param name="rotation">カメラの向き</param>
    private void FollowPlayer(GameObject camera,Vector3 direction,Vector3 rotation)
    {
        // 現在のカメラの位置
        Vector3 NowPos = camera.transform.position;

        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // プレイヤーが動いてもカメラが上下に揺れないように制限する
        playerheight = playerheight - (playerheight % CameraHeight_NoShake);

        // カメラが次へ進む目的地の位置 
        Vector3 NextPos =  new Vector3(Player.transform.position.x + direction.x,
                                       direction.y + playerheight,
                                       Player.transform.position.z+direction.z);



        // 時間をかけて、カメラはプレイヤーに追従する <カメラのブレ防止>
        camera.transform.position = Vector3.Lerp(NowPos, NextPos, FollowingTime * Time.deltaTime);

        // カメラの向きを常にそろえる
        camera.transform.rotation = Quaternion.Euler(rotation);
    }

    /// <summary>
    /// カメラが2D⇔3D移動する場合、時間をかけずにプレイヤーに追従する
    /// </summary>
    public void FllowPlayerNoSlowy()
    {
        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // プレイヤーが動いてもカメラが上下に揺れないように制限する
        playerheight = playerheight - (playerheight % CameraHeight_NoShake);

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

    /// <summary>
    /// カメラの位置を変える
    /// </summary>
    /// <param name="camerapos">カメラの位置</param>
    /// <param name="is3dcamera">位置を変えたいカメラは3Dなのか(false→2Dカメラ, true→3Dカメラ)</param>
    public void ChangeCameraPos(Vector3 camerapos,bool is3dcamera)
    {
        // プレイヤーの高さを小数点切り捨てる(対策：酔い止め)
        int playerheight = Mathf.FloorToInt(Player.transform.position.y);

        // プレイヤーが動いてもカメラが上下に揺れないように制限する
        playerheight = playerheight - (playerheight % CameraHeight_NoShake);

        // プレイヤーの位置
        Vector3 playerpos = new Vector3(Player.transform.position.x, (float)playerheight, Player.transform.position.z);

        // 3Dのカメラの場合
        if (is3dcamera)
        {
            // 3Dカメラの位置を変える
            Camera3D.transform.position = camerapos;

            // 3Dカメラとプレイヤーの距離を更新する
            DirectionCamera3DPlayerPos = Camera3D.transform.position - playerpos;
        }
        else
        // 2Dのカメラの場合
        {
            // 2Dカメラの位置を変える
            Camera2D.transform.position = camerapos;

            // 2Dカメラとプレイヤーの距離を更新する
            DirectionCamera2DPlayerPos = Camera2D.transform.position - playerpos;
        }
    }
}
