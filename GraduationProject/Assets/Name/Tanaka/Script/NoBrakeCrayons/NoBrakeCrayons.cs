//=======================================================================================
//! @file   NoBrakeCrayons.cs
//! @brief  壊れないクレヨンカメラに入れる処理の処理
//! @author 田中歩夢
//! @date   01月17日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//壊れないクレヨンカメラに入れる処理のクラス
public class NoBrakeCrayons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cameraDirector = GameObject.Find("Director/CameraDirector");

        ObjStateByCameraMove2D3D objCamera2D3D = cameraDirector.GetComponent<ObjStateByCameraMove2D3D>();

        objCamera2D3D.AddObjByOneAxitMove(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
