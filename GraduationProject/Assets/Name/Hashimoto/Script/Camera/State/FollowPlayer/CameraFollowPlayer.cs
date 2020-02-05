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
    // 「カメラがプレイヤーに追従するとき、カメラの高さを維持するか」による種類
    public enum Kind_IsKeepHeight
    {
        ERR = -1,       // 例外
        NONE,           // どのカメラの高さを維持しない
        ONLY2D,         // 2Dカメラのみ高さを維持する
        ONLY3D,         // 3Dカメラのみ高さを維持する
        STOP3D2D,       // 2Dも3Dもカメラのみ高さを維持する
        MAX             // この列挙型の最大数
    };

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

    // プレイヤー
    private GameObject Player;

    // 2Dカメラとプレイヤーの距離
    private Vector3 DirectionCamera2DPlayerPos;

    // 3Dカメラとプレイヤーの距離
    private Vector3 DirectionCamera3DPlayerPos;

    // 3Dカメラでプレイヤーが後ろ向きに進む場合、カメラの位置を手前に引くか
    private bool Is3DCameraPlayerBack = false;

    // 3Dカメラでプレイヤーが後ろ向きに進む場合、カメラの位置を手前に引く距離
    private float Direction_3DCameraPlayerBack = -2.5f;

    // カメラがプレイヤーに追従するのにかかる時間(/s) <カメラのブレを防止するため>
    private float FollowingTime = 5.0f;

    // 2Dカメラの向き
    private Vector3 RotationCamera2D;

    // 3Dカメラの向き
    private Vector3 RotationCamera3D;

    // プレイヤーが動いても、カメラが上下に揺れてないように制限をつける高さ
    private int CameraHeight_NoShake = 5;

    // カメラがプレイヤーに追従するとき、カメラの高さを維持するか
    private Kind_IsKeepHeight IsKeepHeight;

    // カメラがプレイヤーに時間を変えて追従するためにラップする処理に掛かる時間
    private float LerpTime = 0.0f;

    // ----------------------------------------------------------------------------------------

    // 加速度
    Rigidbody rigidbody;

    // カメラが前後に動かす加速度の目安
    float acceleration = 1.0f;

    // ----------------------------------------------------------------------------------------

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
        Vector3 playerpos =  new Vector3(Player.transform.position.x, (float)playerheight, Player.transform.position.z);

        // 2Dカメラとプレイヤーの距離を計る
        DirectionCamera2DPlayerPos = Camera2D.transform.position - playerpos;

        // 3Dカメラとプレイヤーの距離を計る
        DirectionCamera3DPlayerPos = Camera3D.transform.position - playerpos;

        //「Rigidbody」の初期化
        rigidbody = Player.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// カメラは、常に一定の距離でプレイヤーに追従する [メイン]
    /// </summary>
    public void FollowPlayer()
    {
        // 2Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera2D, DirectionCamera2DPlayerPos, RotationCamera2D,false);

        // 3Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera3D, DirectionCamera3DPlayerPos, RotationCamera3D,true);
    }

    /// <summary>
    /// カメラは、常に一定の距離でプレイヤーに追従する [詳しく]
    /// </summary>
    /// <param name="camera">カメラ</param>
    /// <param name="direction">カメラとプレイヤーの距離</param>
    /// <param name="rotation">カメラの向き</param>
    /// <param name="is3dcamera">3Dカメラなのか(false:2Dカメラ、true:3Dカメラ)</param>
    private void FollowPlayer(GameObject camera,Vector3 direction,Vector3 rotation,bool is3dcamera)
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

        // カメラの高さを維持する場合
        if ( (LerpTime == 0) &&
             ( (IsKeepHeight == Kind_IsKeepHeight.STOP3D2D)                    ||
               ( (IsKeepHeight == Kind_IsKeepHeight.ONLY2D) && !(is3dcamera) ) ||
               ( (IsKeepHeight == Kind_IsKeepHeight.ONLY3D) && (is3dcamera)  )    )
             )
        {
            // カメラが移動する前にカメラの高さを取得する
            float height = camera.transform.position.y;

            // 時間をかけて、カメラはプレイヤーに追従する <カメラのブレ防止>
            camera.transform.position = Vector3.Lerp(NowPos, NextPos, FollowingTime * Time.deltaTime);

            // カメラの高さのみ維持する
            camera.transform.position = new Vector3(camera.transform.position.x, height, camera.transform.position.z);

        }
        else
        {
            // 時間をかけて、カメラはプレイヤーに追従する <カメラのブレ防止>
            camera.transform.position = Vector3.Lerp(NowPos, NextPos, FollowingTime * Time.deltaTime);

            // カメラがプレイヤーに時間を変えて追従するためにラップする処理に掛かる時間を減らす
            if (LerpTime > 0)
            {
                LerpTime += -Time.deltaTime;

                // その時間の範囲
                if (LerpTime < 0) LerpTime = 0;
            }
        }



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

        // カメラがプレイヤーに時間を変えて追従するためにラップする処理に掛かる時間をリセットする
        LerpTime = 0.0f;

        // ----------------------------------------------------------------------------------------------------

        // 2Dカメラが次へ進む目的地の位置 
        Vector3 Camera2DNextPos = new Vector3(Player.transform.position.x + DirectionCamera2DPlayerPos.x,
                                              (float)playerheight         + DirectionCamera2DPlayerPos.y,
                                              Player.transform.position.z + DirectionCamera2DPlayerPos.z);

        // 2Dカメラのみ高さを維持する場合
        if((IsKeepHeight == Kind_IsKeepHeight.STOP3D2D) || (IsKeepHeight == Kind_IsKeepHeight.ONLY2D))
        {
            // 移動する前に、2Dカメラの高さを取得する
            float height = Camera2D.transform.position.y;

            // 2Dカメラはプレイヤーに時間かけずに追従する
            Camera2D.transform.position = Camera2DNextPos;

            // 2Dカメラの高さを維持する
            Camera2D.transform.position = new Vector3(Camera2D.transform.position.x,height, Camera2D.transform.position.z);
        }
        else
        {
            // 2Dカメラはプレイヤーに時間かけずに追従する
            Camera2D.transform.position = Camera2DNextPos;
        }

        // ----------------------------------------------------------------------------------------------------

        // 3Dカメラが次へ進む目的地の位置 
        Vector3 Camera3DNextPos = new Vector3(Player.transform.position.x + DirectionCamera3DPlayerPos.x,
                                               (float)playerheight        + DirectionCamera3DPlayerPos.y,
                                              Player.transform.position.z + DirectionCamera3DPlayerPos.z);
        
        // 3Dカメラのみ高さを維持する場合
        if ((IsKeepHeight == Kind_IsKeepHeight.STOP3D2D) || (IsKeepHeight == Kind_IsKeepHeight.ONLY3D))
        {
            // 移動する前に、3Dカメラの高さを取得する 
            float height = Camera2D.transform.position.y;

            // 3Dカメラはプレイヤーに時間かけずに追従する
            Camera3D.transform.position = Camera3DNextPos;

            // 3Dカメラのみ高さを維持する
            Camera3D.transform.position = new Vector3(Camera3D.transform.position.x, height, Camera3D.transform.position.z);
        }
        else
        {
            // 3Dカメラはプレイヤーに時間かけずに追従する
            Camera3D.transform.position = Camera3DNextPos;
        }
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
            // 3Dカメラとプレイヤーの距離を更新する
            DirectionCamera3DPlayerPos = camerapos - playerpos;
        }
        else
        // 2Dのカメラの場合
        {
            // 2Dカメラとプレイヤーの距離を更新する
            DirectionCamera2DPlayerPos = camerapos - playerpos;
        }
    }

    /// <summary>
    /// カメラがプレイヤーに追従するとき、カメラの高さを維持するか決める
    /// </summary>
    /// <param name="kind"></param>
    /// <param name="lerptime">ラップするのにかかる時間</param>
    public void DecideKeepCameraHeight(Kind_IsKeepHeight kind, float lerptime = 0.0f)
    {
        // カメラがプレイヤーに追従するとき、カメラの高さを変えるか 決める
        IsKeepHeight = kind;

        // カメラがプレイヤーに時間を変えて追従するためにラップする処理に掛かる時間
        if(lerptime > 0.0f) LerpTime = lerptime;

    }

    /// <summary>
    /// 「3Dカメラでプレイヤーが後ろ向きに進む場合、カメラの位置を手前に引く」か判断する
    /// </summary>
    /// <param name="vell">プレイヤーの速度</param>
    public void Judge3DCameraPlayerBack(Vector2 vell)
    {
        // 加速度が小さい場合、何もしない
        if (rigidbody.velocity.magnitude < acceleration) return;

        // Y軸方向に移動速度が小さい場合
        if (vell.y < 0)
        {
            // カメラの位置を手前に引くようにする
            Create3DCameraPlayerBack();
        }
        else
        // Y軸方向に移動速度が大きい場合
        if (vell.y > 0)
        {
            // リセットする
            Reset3DCameraPlayerBack();
        }
    }

    /// <summary>
    /// 「3Dカメラでプレイヤーが後ろ向きに進む場合、カメラの位置を手前に引く」ようにする
    /// </summary>
    private void Create3DCameraPlayerBack()
    {
        // 以前までカメラの位置が手前に引くようにしていなかった場合
        if(!Is3DCameraPlayerBack)
        {
            // カメラの位置を手前に引くようにする
            Is3DCameraPlayerBack = true;

            // 3Dカメラとプレイヤーの距離を更新する
            DirectionCamera3DPlayerPos = new Vector3(DirectionCamera3DPlayerPos.x + Direction_3DCameraPlayerBack,
                                                     DirectionCamera3DPlayerPos.y,
                                                     DirectionCamera3DPlayerPos.z);
        }
    }

    /// <summary>
    /// 「3Dカメラでプレイヤーが後ろ向きに進む場合、カメラの位置を手前に引かない」ようにリセットする
    /// </summary>
    public void Reset3DCameraPlayerBack()
    {
        // 3Dカメラでプレイヤーが後ろ向きに進む時に、カメラの位置が手前に引くようにした場合
        if (Is3DCameraPlayerBack)
        {
            // カメラの位置を手前に引かないようにリセットする
            Is3DCameraPlayerBack = false;

            // 3Dカメラとプレイヤーの距離をリセットする
            DirectionCamera3DPlayerPos = new Vector3(DirectionCamera3DPlayerPos.x - Direction_3DCameraPlayerBack,
                                                     DirectionCamera3DPlayerPos.y,
                                                     DirectionCamera3DPlayerPos.z);
        }
    }
}
