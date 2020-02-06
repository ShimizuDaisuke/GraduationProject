// -----------------------------------------------------------------------------------------
//! @file       PlayerPosByCamera2D3D.cs
//!
//! @brief      2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.30
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosByCamera2D3D : MonoBehaviour
{
    // 3Dカメラから2Dカメラに移動する場合のプレイヤーのZ位置
    [SerializeField]
    private float ZPos_CameraFrom3DTo2D = 0.0f;

    // 2Dカメラから3Dカメラに移動する場合のプレイヤーのZ位置
    [SerializeField]
    private float ZPos_CameraFrom2DTo3D = 0.0f;

    // カメラの監督
    [SerializeField]
    private GameObject CameraDirectorObj = default;

    // プレイヤー
    private GameObject PlayerObj;

    // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーに当たった地面以外のオブジェクト
    private GameObject ObjHitNoGround;

    // 2Dカメラ⇔3Dカメラへ移動する前にプレイヤーがいた位置
    private Vector3 OncePos;

    // 2Dカメラ⇔3Dカメラへ移動中にいるプレイヤーの位置
    private Vector3 NowPos;

    // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが地面以外のオブジェクトに当たったか
    private bool IsHitNoGroundObj;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // 3Dカメラから2Dカメラに移動する場合のプレイヤーのZ位置が設定されていなかった場合
        if(ZPos_CameraFrom3DTo2D != 0.0f)
        {
            //　現在プレイヤーの位置にする
            ZPos_CameraFrom3DTo2D = PlayerObj.transform.position.z;
        }

        // 2Dカメラから3Dカメラに移動する場合のプレイヤーのZ位置が設定されていなかった場合
        if(ZPos_CameraFrom2DTo3D!=0.0f)
        {
            //　現在プレイヤーの位置にする
            ZPos_CameraFrom2DTo3D = PlayerObj.transform.position.z;
        }

    }

    /// <summary>
    /// 2Dカメラ⇔3Dカメラへ移動する際にいるプレイヤーの位置を決める
    /// </summary>
    /// <param name="IsCamera3D">最終的に3Dカメラになるのか</param>
    /// <param name="isinitialpos">最終的にプレイヤーが元の位置に戻るか</param>
    public void CreatePlayerPosByCameraMove2D3D(bool IsCamera3D, bool isinitialpos)
    {
        //  最終的にプレイヤーを元の位置に戻す場合
        if(isinitialpos)
        {
            // プレイヤーが以前にいた位置に戻す
            transform.position = OncePos;
        }
        else
        // 2Dカメラから3Dカメラに移動する場合
        if (IsCamera3D == true)
        {
            // プレイヤーの位置を決める
            transform.position = new Vector3(OncePos.x, OncePos.y, ZPos_CameraFrom2DTo3D);
        }
        else
        // 3Dカメラから2Dカメラに移動する場合
        {
            // プレイヤーの位置を決める
            transform.position = new Vector3(OncePos.x, OncePos.y, ZPos_CameraFrom3DTo2D);
        }
    }

    /// <summary>
    /// 2Dカメラ⇔3Dカメラへ移動する際にいるプレイヤーの位置を決める
    /// </summary>
    /// <param name="pos">プレイヤーの位置</param>
    public void CreatePlayerPosByCameraMove2D3D(Vector3 pos)
    {
        // 2Dカメラ⇔3Dカメラへ移動する際にいるプレイヤーの位置を決める
        transform.position = pos;
    }

    // ===============================================================================================

#if false
    /// <summary>
    /// 自作関数による当たり判定
    /// </summary>
    public bool HitNoArea2DCamera()
    {
        // 2Dカメラ⇔3Dカメラに移動している かつ 最終的に2Dカメラにする場合
        if ((CameraDirectorObj.GetComponent<CameraDirectorObj>().IsMove2D3DCameraPos)&&
            !(CameraDirectorObj.GetComponent<CameraDirectorObj>().IsAppearCamera3D))
        {

            // レイをプレイヤーを中心にZ向きに前後2つ作成する
            Ray[] ray_playerup = { new Ray(PlayerObj.transform.position, Vector3.forward),  // Z軸に沿って前向き
                                   new Ray(PlayerObj.transform.position, Vector3.back) };   // Z軸に沿って後向き

            // プレイヤーから飛ばしたレイに当たったオブジェクトの入れ物
            RaycastHit hit;

            // Z軸に沿って前後にあるレイ
            foreach (Ray ray in ray_playerup)
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * RayDistance, Color.red, 300);


                // プレイヤーからレイを飛ばして何からのオブジェクトに衝突した場合
                if (Physics.Raycast(ray, out hit, RayDistance))
                {
                    // 当たったオブジェクトが  カメラ2Dの時にプレイヤーが入ってはいけない領域の場合
                    if (hit.transform.gameObject.tag == "Camera2DNoArea")
                    {
                        // 「プレイヤーが地面以外のオブジェクトに当たった」とする
                        IsHitNoGroundObj = true;

                        // プレイヤーに当たったオブジェクトを記憶する
                        ObjHitNoGround = hit.transform.gameObject;


                        // プレイヤーが入ってはいけない領域に当たった
                        return true;
                    }

                }

 
            }
        }

        // プレイヤーが入ってはいけない領域に当たっていない
        return false;
    }

#endif

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay(Collision collision)
    {
        // 2Dカメラ⇔3Dカメラに移動している場合  かつ カメラが3D→2Dへ切り替えるとき
        if ((CameraDirectorObj.GetComponent<CameraDirector>().IsMove2D3DCameraPos) && (CameraDirectorObj.GetComponent<CameraDirector>().IsAppearCamera3D == false))
        {
            // 当たったオブジェクトが カメラ2Dの時にプレイヤーが入ってはいけない領域の場合
            if (   ( (collision.gameObject.tag == "Camera2DNoArea") ||(collision.gameObject.GetComponent<Obj_OneAxitMove>() != null) ) 
                &&   (collision.gameObject.tag != "EraserDustCover") )
            {
                // 「プレイヤーが地面以外のオブジェクトに当たった」とする
                IsHitNoGroundObj = true;

                // プレイヤーに当たったオブジェクトを記憶する
                ObjHitNoGround = collision.gameObject;
            }
        }
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerStay(Collider collision)
    {
        // 2Dカメラ⇔3Dカメラに移動している場合  かつ カメラが3D→2Dへ切り替えるとき
        if ((CameraDirectorObj.GetComponent<CameraDirector>().IsMove2D3DCameraPos) && (CameraDirectorObj.GetComponent<CameraDirector>().IsAppearCamera3D == false))
        {
            // 当たったオブジェクトが カメラ2Dの時にプレイヤーが入ってはいけない領域の場合
            if (((collision.gameObject.tag == "Camera2DNoArea") || (collision.gameObject.GetComponent<Obj_OneAxitMove>() != null))
                && (collision.gameObject.tag != "EraserDustCover"))
            {
                // 「プレイヤーが地面以外のオブジェクトに当たった」とする
                IsHitNoGroundObj = true;

                // プレイヤーに当たったオブジェクトを記憶する
                ObjHitNoGround = collision.gameObject;
            }
        }
    }

    // ===============================================================================================

    /// <summary>
    /// 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが当たった地面以外のオブジェクトの透明度を変える
    /// ※「プレイヤーが当たった地面以外のオブジェクト」material:RenderingMode Opaque以外にする
    /// </summary>
    /// <param name="nowtransparence">現在のオブジェクトの透明度</param>
    /// <param name="changedtransparence">オブジェクトに反映させる透明度</param>
    public void MakeTransparencePlayerHitObjNoGround(ref float nowtransparence, float changedtransparence)
    {
        // マテリアルが存在していない場合、何もしない
        if (ObjHitNoGround.GetComponent<Renderer>() == null) return;

        // プレイヤーに当たった地面以外のオブジェクトの色を取得する
        Color color = ObjHitNoGround.GetComponent<Renderer>().material.color;

        // プレイヤーが地面に当たる前に現在のオブジェクトの透明度を記憶させる
        nowtransparence = color.a;

        // プレイヤーに当たった地面以外のオブジェクトの透明度を変える
        color.a = changedtransparence;

        // プレイヤーに当たった地面以外のオブジェクトの透明度を反映させる
        ObjHitNoGround.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが当たった地面以外のオブジェクトの透明度を変える
    /// ※「プレイヤーが当たった地面以外のオブジェクト」material:RenderingMode Opaque以外にする
    /// </summary>
    /// <param name="changedtransparence">オブジェクトに反映させる透明度</param>
    public void MakeTransparencePlayerHitObjNoGround(float changedtransparence)
    {

        // マテリアルが存在していない場合、何もしない
        if (ObjHitNoGround.GetComponent<Renderer>() == null) return;

        // プレイヤーに当たった地面以外のオブジェクトの色を取得する
        Color color = ObjHitNoGround.GetComponent<Renderer>().material.color;

        // プレイヤーに当たった地面以外のオブジェクトの透明度を変える
        color.a = changedtransparence;

        // プレイヤーに当たった地面以外のオブジェクトの透明度を反映させる
        ObjHitNoGround.GetComponent<Renderer>().material.color = color;
    }


    /// <summary>
    /// 取得・設定関数
    /// </summary>

    // 2Dカメラ⇔3Dカメラへ移動する前にプレイヤーがいた位置
    public Vector3 PlayerOncePos { get { return OncePos; } set { OncePos = value; } }

    // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが当たった地面以外のオブジェクト
    public GameObject PlayerHitObjNoGround { get { return ObjHitNoGround; } set { ObjHitNoGround = value; } }

    // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが地面以外のオブジェクトに当たったか
    public bool IsHitPlayerNoGroundObj { get { return IsHitNoGroundObj; } set { IsHitNoGroundObj = value; } }

        
}
