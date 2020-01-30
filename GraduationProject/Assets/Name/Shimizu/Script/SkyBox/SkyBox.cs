//=======================================================================================
//! @file   SkyBox.cs
//! @brief  SkyBoxの回転
//! @author 田中歩夢
//! @date   01月29日
//! @note   ない
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    //回転スピード
    [SerializeField]
    [Range(0.01f, 0.9f)]
    private float rotateSpeed;

    // SkyBoxのMaterial
    [SerializeField]
    private Material sky;

    private float rotationRepeatValue;

    void Update()
    {

        rotationRepeatValue = Mathf.Repeat(sky.GetFloat("_Rotation") + rotateSpeed, 360f);

        sky.SetFloat("_Rotation", rotationRepeatValue);
    }
}
