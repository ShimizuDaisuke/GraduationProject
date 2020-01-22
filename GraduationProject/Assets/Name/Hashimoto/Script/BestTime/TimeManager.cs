//=======================================================================================
//! @file   TimeManager.cs
//!
//! @brief  時間によるマネージャー  
//!
//! @author 橋本奉武
//!
//! @date   1月7日
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間によるマネージャー
/// </summary>
public class TimeManager : MonoBehaviour
{
    // クリアするのに掛かった時間
    static private float time;

    // Start is called before the first frame update
    void Start()
    {
        // このオブジェクトを他のシーンへ遷移しても破棄しないように設定
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        // クリアするのに掛かった時間
        public float ClearTime { get { return time; } set { time = value; } }
}
