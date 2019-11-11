//=======================================================================================
//! @file   EventCameraScissorsCut.cs
//! @brief  ハサミの切る時のカメラの処理
//! @author 田中歩夢
//! @date   10月31日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ハサミの切る時のカメラの処理
public class EventCameraScissorsCut : CameraEventBase
{
    //ハサミの動きのクラス
    private ScissorsController m_scissorsCon = default;

    //ハサミのレイヤーマスク
    [SerializeField]
    private LayerMask m_layerMaskCut;

    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    /// <param name="camera3D">3D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    /// <param name="camera2D">2D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    public override void MoveCameraByEvent()
    {

        //レイの作成
        Ray ray = new Ray(Player.transform.position, Vector3.right);

        // Rayが衝突したコライダーの情報
        RaycastHit hit;
        //レイの飛ばす距離
        float distance = 1.0f;

        if(Physics.Raycast(ray, out hit,distance, m_layerMaskCut))
        {
            //刃の親を取得
            GameObject scissors = hit.collider.transform.parent.gameObject;
            
            //ハサミが回転しきったかどうかのフラグ
            bool rotationFinishFlag = scissors.GetComponent<ScissorsController>().RotationFinishFlag;
            
            //回転しきっていなかったら
            if(!rotationFinishFlag)
            {
                //カメラを近づける
                Camera3D.transform.position = Vector3.MoveTowards(Camera3D.transform.position, new Vector3(Player.transform.position.x - 3.0f,Camera3D.transform.position.y,Camera3D.transform.position.z), 0.01f);
            }
            else
            {
                //カメラを指定位置まで移動
                GameObject cameraMovePos = scissors.transform.Find("CameraMovePos").gameObject;
                Camera3D.transform.position = Vector3.MoveTowards(Camera3D.transform.position, cameraMovePos.transform.position, 0.1f);
            }
        }
    }
}
