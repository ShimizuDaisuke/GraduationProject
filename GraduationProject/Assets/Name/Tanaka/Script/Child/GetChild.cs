//=======================================================================================
//! @file   GetChild.cs
//! @brief  子供を取得の処理
//! @author 田中歩夢
//! @date   01月17日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 子供を取得のクラス
public class GetChild : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject cameraDirector = GameObject.Find("Director/CameraDirector");

        ObjStateByCameraMove2D3D objCamera2D3D = cameraDirector.GetComponent<ObjStateByCameraMove2D3D>();
        
        foreach (Transform child in transform)
        {
            objCamera2D3D.AddObjByOneAxitMove(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
