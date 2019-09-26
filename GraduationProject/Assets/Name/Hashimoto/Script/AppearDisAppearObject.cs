// -----------------------------------------------------------------------------------------
//! @file       AppearDisAppearObject.cs
//!
//! @brief      「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクト
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.26
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearDisAppearObject : MonoBehaviour
{
    // 2Dカメラのみ表示されるオブジェクト
    [SerializeField]
    private GameObject[] ObjBy2DCamera;

    // 3Dカメラのみ表示されるオブジェクト
    [SerializeField]
    private GameObject[] ObjBy3DCamera;


    /// <summary>
    /// 2Dや3Dカメラのみ表示されるオブジェクトを表示非表示させる
    /// </summary>
    /// <param name="is3dcamera">現在3Dカメラを使用しているか(false：2Dカメラで表示している / true：3Dカメラで表示している)</param>
    public void ChangeObjByCamera(bool is3dcamera)
    {
        // 現在3Dカメラを使用している場合
        if (is3dcamera == true)
        {
            //  3Dカメラのみ表示されるオブジェクトを表示する
            AppearObjBy3DCamera();
        }
        else
        // 現在2Dカメラを使用している場合
        {
            // 2Dカメラのみ表示されるオブジェクトを表示する
            AppearObjBy2DCamera();
        }
    }


    /// <summary>
    /// 2Dカメラのみ表示されるオブジェクトを表示する
    /// </summary>
    private void AppearObjBy2DCamera()
    {
        //  2Dカメラのみ表示されるオブジェクトを表示させる
        foreach (GameObject obj2d in ObjBy2DCamera)
        {
            obj2d.SetActive(true);
        }

        // 3Dカメラのみ表示されるオブジェクトを非表示させる
        foreach (GameObject obj3d in ObjBy3DCamera)
        {
            obj3d.SetActive(false);
        }
    }

    /// <summary>
    ///  3Dカメラのみ表示されるオブジェクトを表示する
    /// </summary>
    private void AppearObjBy3DCamera()
    {
        // 3Dカメラのみ表示されるオブジェクトを表示させる
        foreach (GameObject obj3d in ObjBy3DCamera)
        {
            obj3d.SetActive(true);
        }

        //  2Dカメラのみ表示されるオブジェクトを表示させる
        foreach (GameObject obj2d in ObjBy2DCamera)
        {
            obj2d.SetActive(false);
        }
    }



}
