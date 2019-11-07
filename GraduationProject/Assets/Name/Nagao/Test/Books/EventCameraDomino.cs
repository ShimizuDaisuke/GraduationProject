//======================================================================================= 
//! @file       EventCameraDomino 
//! @brief      本のドミノ倒しのカメラの処理
//! @author     長尾昌輝
//! @date       2019/10/28
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCameraDomino : CameraEventBase
{
    //定規のオブジェクト
    [SerializeField]
    private GameObject m_dominoObj;

    ////カメラの移動速度
    //[SerializeField]
    //private Vector3 m_movePos = new Vector3(0.0f,0.0f,0.0f);

    //カメラの角度
    [SerializeField]
    private Vector3 m_angle = new Vector3(0.0f, 0.0f, 0.0f);

    //カメラを動かす判定
    private bool m_moveflag = false;

    //弾の速さ
    [SerializeField]
    private float speed = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    public override void MoveCameraByEvent()
    {
        //イベント中なら
        if (m_moveflag == true)
        {
            // カメラの向きの角度の変更;
            Camera3D.transform.rotation = Quaternion.Euler(m_angle);

            // カメラの位置の変更
            Camera3D.transform.position = new Vector3(Camera3D.transform.position.x, Camera3D.transform.position.y, -8.0f);

            //3D時の移動方向
            var direction = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)) * Vector3.forward;

            //3Dカメラの移動
            Camera3D.transform.position += direction * speed * Time.deltaTime;

            //2Dカメラの移動
            Camera2D.transform.position += direction * speed * Time.deltaTime;
        }

    }

    //イベント判定を取得・設定
    public bool MoveFlag { get { return m_moveflag; } set { m_moveflag = value; } }
}
