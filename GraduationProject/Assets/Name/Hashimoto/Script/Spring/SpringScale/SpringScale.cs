//=======================================================================================
//! @file   SpringScale.cs
//!
//! @brief  ばねサイズ
//!
//! @author 橋本奉武
//!
//! @date   10月10日
//!
//! @note  <参考> ばね振り子の力学的エネルギー
//!               http://www.wakariyasui.sakura.ne.jp/p/mech/rikiene/banehuriko.html
//!               https://assets.clip-studio.com/ja-jp/detail?id=1302083
//!               http://nn-hokuson.hatenablog.com/entry/2017/05/23/204718
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpringScale : MonoBehaviour
{
#if false

    // ばね定数 k[N/m]
    [SerializeField]
    private float k = 1.0f;

    // 自然長から引っ張った長さ a[m]
    [SerializeField]
    private float a = 10.0f;

    // ばねの自然長 e[m]
    private float e = 0.0f;

    // 現在の自然長から引っ張られた長さ　x[m]
    private float x = 0.0f;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // ばねの自然長を取得
        e = transform.localScale.y;

        // 弾性エネルギー
        float U = (k * a * a) / 2.0f;

        // 実際にサイズを変える
        transform.localScale = new Vector3(transform.localScale.x,
                                                 U,
                                                 transform.localScale.z);

        // 現在の自然長から引っ張られた長さを調べる
        x = transform.localScale.y - e;


    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 弾性エネルギー
        float U = (k * x * x) / 2.0f;

        // 実際にサイズを変える
        transform.localScale = new Vector3(transform.localScale.x,
                                                 U,
                                                 transform.localScale.z);

        // 現在の自然長から引っ張られた長さを調べる
        x = transform.localScale.y - e;
    }
#endif
#if false

    // 目的地
    private Vector3 TargetPositon = Vector3.zero;

    // 加速度
    private Vector3 Acceleration = Vector3.zero;
    // 速度
    private Vector3 Vellocity = Vector3.zero;
    // 位置
    private Vector3 Position = Vector3.zero;

    // 初期サイズ
    private Vector3 InitializeSize = Vector3.zero;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 初期サイズを取得する
        InitializeSize = transform.localScale;

        // 現在の位置
        Position = transform.localScale - new Vector3(0.0f, 10.0f, 0.0f);
        // 目的地を決める
        TargetPositon = transform.localScale;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // Zキー押されたら現在の位置などリセットする
        if(Input.GetKeyDown(KeyCode.Z))
        {
            // 現在の位置
            Position = InitializeSize - new Vector3(0.0f, 10.0f, 0.0f);

            // 加速度や速度
            Acceleration = Vellocity = Vector3.zero;

            return;
        }


        // 目的地と現在の位置の距離
        Vector3 distance = TargetPositon - Position;

        // 距離によって加速度を変える
        Acceleration = distance * 0.1f;
        // 速度を増やす
        Vellocity += Acceleration;
        // 速度に空気抵抗を加える
        Vellocity *= 0.9f;
        // 位置を変える
        Position += Vellocity;

        // 実際に位置を変える
        transform.localScale = Position;
        // 実際に加速させる
        GetComponent<Rigidbody>().AddForce(Acceleration * 100.0f);

        // 左右に動かす
        //transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
    }

#endif

    // 1フレームに経過する時間
    [Range(0.0f, 0.1f),SerializeField]
    private float ScaleSpeedTime = 0.1f;

    // 初期サイズ(最大サイズ)
    private Vector3 InitializeSize = Vector3.zero;

    // 高さが最小の場合のサイズ
    [SerializeField]
    private Vector3 MiuSizeByHeight = new Vector3(15.0f, 3.0f, 15.0f);

    // 最大サイズと最小サイズの差
    private Vector3 DifferenceMaxMiuSize = Vector3.zero;

    // 現在拡大・縮小してから経過した時間(範囲:0≦X≦1)
    private float NowScaleTime = 0.0f;

    // <<< テスト >>> 拡大するか (1:拡大、0:何もしない、-1:縮小)
    private int IsBig = 0;

    
    [SerializeField]
    private Vector3 raydirec = Vector3.down;
    [SerializeField]
    private float raylength = 0.1f;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 初期サイズ(最大サイズ)を取得する
        InitializeSize = transform.localScale;

        // 最大サイズと最小サイズの差
        DifferenceMaxMiuSize = InitializeSize - MiuSizeByHeight;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // <<< テスト >>> ZキーとXキー同時に押されたら、無効にする
        if ( !( (Input.GetKey(KeyCode.Z)) && (Input.GetKey(KeyCode.X)) ) )
        {

            // Zキー押されたら縮小する
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // サイズを変える
                transform.localScale = new Vector3(transform.localScale.x, InitializeSize.y, transform.localScale.z);

                // 縮小する準備を行う
                IsBig = -1;

                // 現在の拡大・縮小する時間を変える
                NowScaleTime = 1.0f;

            }
            
            // Xキー押されたら拡大する
             if (Input.GetKeyDown(KeyCode.X))
            {
                // サイズを変える
                transform.localScale = new Vector3(transform.localScale.x, MiuSizeByHeight.y, transform.localScale.z);

                // 拡大する準備を行う
                IsBig = 1;

                // 現在の拡大・縮小する時間を変える
                NowScaleTime = 0.0f;
            } 

        }

        // ------------------------------------------------------------------------------------------------

        //　ばねから飛ばすレイを作成する 
        Ray ray = new Ray(transform.position, raydirec);

        // レイの長さ
        float distance = raylength;

        // レイに当たったオブジェクト入れる箱
        RaycastHit hit;

        // レイの可視化
        Debug.DrawLine(ray.origin, ray.origin + raydirec * distance, Color.red);

        // もしレイが他のオブジェクトに当たった場合
        if ( (Physics.Raycast(ray, out hit, distance)) && (IsBig == 0) )
        {
            // 縮小させる
            IsBig = -1;
        }


        // ------------------------------------------------------------------------------------------------

        // 拡大・縮小しない場合、処理を飛ばす
        if (IsBig == 0) return;

        // 拡大・縮小して経過した時間を計る ( 放物線 →  [ t(時間)の2乗 ] )
        NowScaleTime += ScaleSpeedTime * IsBig;

        // 拡大・縮小させる範囲
        NowScaleTime = (NowScaleTime > 1.0f) ? 1.0f : (NowScaleTime < 0.0f) ? 0.0f : NowScaleTime;

        // 実際に拡大・縮小させる
        transform.localScale = new Vector3(MiuSizeByHeight.x + NowScaleTime * NowScaleTime * DifferenceMaxMiuSize.x, 
                                           MiuSizeByHeight.y + NowScaleTime * NowScaleTime * DifferenceMaxMiuSize.y,
                                           MiuSizeByHeight.z + NowScaleTime * NowScaleTime * DifferenceMaxMiuSize.z);

        // 拡大・縮小されたサイズが範囲外の場合、範囲内に収める
        // プレイヤーの高さが範囲外に大きくなった場合
        if((NowScaleTime == 1.0f) && (IsBig == 1))
        {
            // 最大サイズにする
            transform.localScale = new Vector3(InitializeSize.x, InitializeSize.y, InitializeSize.z);
            
            // 拡大しないようにする
            IsBig = 0;

            // ジャンプする
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 1000.0f, 0.0f));
        }
        // プレイヤーの高さが範囲外に小さくなった場合
        if ((NowScaleTime == 0.0f) && (IsBig == -1))
        {
            // 最小サイズにする
            transform.localScale = new Vector3(MiuSizeByHeight.x, MiuSizeByHeight.y, MiuSizeByHeight.z);
            
            // 縮小しないようにする
            IsBig = 1;
        }

        // ------------------------------------------------------------------------------------------------


    }

}
