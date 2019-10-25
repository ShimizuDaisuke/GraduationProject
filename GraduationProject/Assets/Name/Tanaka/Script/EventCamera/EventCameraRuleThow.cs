//=======================================================================================
//! @file   EventCameraRuleStraight.cs
//! @brief  投げる前のカメラの処理
//! @author 田中歩夢
//! @date   10月25日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//投げる前のカメラの動きのクラス
public class EventCameraRuleThow : CameraEventBase
{
    //定規のオブジェクト
    [SerializeField]
    private GameObject m_seesawObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    /// <param name="camera3D">3D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    /// <param name="camera2D">2D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    public void MoveCameraByEvent(ref GameObject camera3D, ref GameObject camera2D)
    {
        camera3D.transform.rotation = Quaternion.RotateTowards(camera3D.transform.rotation, Quaternion.Euler(/*m_seesawObj.transform.localEulerAngles + */new Vector3(0.0f,180.0f,0.0f)), 2.0f);

    }
}
