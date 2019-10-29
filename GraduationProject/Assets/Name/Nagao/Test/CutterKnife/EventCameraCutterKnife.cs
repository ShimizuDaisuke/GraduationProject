///======================================================================================= 
///! @file       EventCameraCutterKnife 
///! @brief      カッターナイフのカメラの処理
///! @author     長尾昌輝
///! @date       2019/10/28
///======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCameraCutterKnife : CameraEventBase
{
    //カッターナイフのオブジェクト
    [SerializeField]
    private GameObject m_cutterKnifeObj;

    // プレイヤーとカメラの距離
    private Vector3 m_diration = new Vector3(-5.0f, 1.0f, 0.0f);

    //カメラ移動からモデル移動をさせる為の時間 
    private float ChangeTime = 0.0f;
    
    //カメラを動かす最大値
    [SerializeField]
    private float MaxTime = 0.0f;

    //モデルを動かす判定
    private bool m_moveflag = false;


    // Start is called before the first frame update
    void Start()
    {
        // 初期化する
        Initilaize();
    }

    void Update()
    {

        //時間を超えたら
        if (ChangeTime > MaxTime)
        {
            //モデルを動かす
            m_moveflag = true;

            //時間をリセット
            ChangeTime = 0.0f;
        }

    }

   
    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    public override void MoveCameraByEvent()
    {
        //モデルが動いてないなら時間を進める
        if (m_moveflag == false)
        {
            // 現在のカメラの位置
            Vector3 NowPos = Camera3D.transform.position;

            // カメラが次へ進む目的地の位置 
            Vector3 NextPos = Player.transform.position + m_diration;

            // カメラの位置をプレイヤーに追従させる
            Camera3D.transform.position = Vector3.Lerp(NowPos, NextPos,Time.deltaTime);

            //時間経過
            ChangeTime += Time.deltaTime;
        }
     

            //Player.transform.position + m_diration;

            //Camera3D.transform.RotateAround(m_seesawObj.transform.position,Vector3.up, 5.0f);
            //Camera3D.transform.rotation = Quaternion.RotateTowards(Camera3D.transform.rotation, Quaternion.Euler(/*m_seesawObj.transform.localEulerAngles + */new Vector3(0.0f,180.0f,0.0f)), 2.0f);
            //Debug.Log("fdasgafuyfa");
        }

    public bool MoveFlag { get { return m_moveflag; } set { m_moveflag = value; } }

}
