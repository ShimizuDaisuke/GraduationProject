//=======================================================================================
//! @file   Camera2DHitHightFlag.cs
//! @brief  カメラ２Dの高さを変える当たり判定の処理
//! @author 田中歩夢、橋本奉武
//! @date   01月08日
//! @note   その当たり判定の幅は広くすること！ by 橋本
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名省略
using Kind_IsKeepCameraHeight = CameraFollowPlayer.Kind_IsKeepHeight;

// カメラ２Dの高さを変える当たり判定の処理
public class Camera2DHitHightFlag : MonoBehaviour
{
    //カメラがプレイヤーに追従する
    [SerializeField]
    private CameraFollowPlayer m_cameraFP = default;

    //２DカメラのY座標（当たってる時の）
    [SerializeField]
    private float m_cameraHitYPos = 0.0f;

    //3DカメラZ座標
    [SerializeField]
    private float m_camera3dHitYPos = 0.0f;

    //２Dカメラ
    [SerializeField]
    private GameObject m_camera2d = null;

    //３Dカメラ
    [SerializeField]
    private GameObject m_camera3d = null;

    // カメラがプレイヤーに追従するとき、カメラの高さを固定するか
    [SerializeField]
    private Kind_IsKeepCameraHeight IsKeepCameraHeight;

    // 現在、エリア内にプレイヤーが入っているか
    private bool m_hitNowHightFlag = false;

    // 過去、エリア内にプレイヤーが入ったか
    private bool m_hitOnceHightFlag = false;

    //プレイヤー
    private GameObject m_player = null;

    //2DカメラZ座標
    private float m_camera2DposZ = -14.0f;

    // プレイヤーとカメラの距離
    private Vector3 dir = Vector3.zero;

    // プレイヤーと3dカメラの距離
    private Vector3 dir3d = Vector3.zero;

    // とある時間
    private float time = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 現在と過去でフラグが異なっている場合 かつ 現在プレイヤーがエリア内にプレイヤーが入った場合
        if ((m_hitNowHightFlag != m_hitOnceHightFlag)&&(m_hitNowHightFlag == true))
        {
            // 現在のプレイヤーとカメラの距離を計算
            dir = m_camera2d.transform.position - m_player.transform.position;

            // 現在のプレイヤーと3dカメラの距離を計算
            dir3d = m_camera3d.transform.position - m_player.transform.position;

            // 指定された位置にカメラを動かす
            if(m_cameraHitYPos != 0)   m_cameraFP.ChangeCameraPos(new Vector3(m_player.transform.position.x, m_cameraHitYPos, m_camera2DposZ), false);                         // 2Dカメラ
            if(m_camera3dHitYPos != 0) m_cameraFP.ChangeCameraPos(new Vector3(m_camera3d.transform.position.x, m_camera3dHitYPos, m_camera3d.transform.position.z), true);     // 3Dカメラ

            // カメラの高さを維持させるか決める
            m_cameraFP.DecideKeepCameraHeight(IsKeepCameraHeight, time);

            // リセットする
            m_hitNowHightFlag = false;
        }

        // 常に過去にエリア内にプレイヤーが入ったか　更新する
        m_hitOnceHightFlag = m_hitNowHightFlag;
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //電気が当たったら
        if (collider.gameObject.tag == "Player")
        {
            // エリア内にプレイヤーが入った
            m_hitNowHightFlag = true;
        }
    }
}
