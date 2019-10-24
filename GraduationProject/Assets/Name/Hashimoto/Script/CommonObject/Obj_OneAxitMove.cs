// --------------------------------------------------------------------------------------------------------------
//! @file       Obj_OneAxitMove.cs
//!
//! @brief      3D⇒2Dにカメラを切り替えるときに、とある軸を中心にそれぞれのオブジェクトの位置を統一させる位置
//!
//! @author     橋本 奉武
//!
//! @date       2019.10.05
// ---------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_OneAxitMove : MonoBehaviour
{
    // < メモ >
    // ゲーム用のカメラによるオブジェクトの種類
    enum ObjKindByGameCamera
    {
        ERR = -1,       // 例外
        NORAML,         // 通常のオブジェクト
        APPEAR2DDIS3D,  // 2D画面のみ表示されるオブジェクト(リンク ありorなし)
        APPEAR3DDIS2D,  // 3D画面のみ表示されるオブジェクト(リンク ありorなし)
        MOVEONEAXIT,    // 1つの軸を中心に動くオブジェクト
        MAX,            // この列挙型の最大数
    }

    // とある軸(x,y,z軸)
    public enum Axit { X, Y, Z }

    // 3D⇒2Dにカメラを切り替えるときに、統一させる軸
    [SerializeField]
    private Axit OneAxit = default;

    // とある軸を中心に、それぞれのオブジェクトを統一させる位置
    [SerializeField]
    private float Position_MoveOneAxit = default;

    // カメラが3D⇒2Dへ切り替える前の初期位置
    private Vector3 InitialPosition;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Awake()
    {
        // カメラが3D⇒2Dへ切り替える前の初期位置を、そのオブジェクトが非表示される前に記憶させる
        InitialPosition = transform.position;
    }

    /// <summary>
    /// 3D⇒2D:1つの軸を中心に、それぞれのオブジェクトの位置を統一させる
    /// 2D⇒3D:元にいた位置に戻る
    /// </summary>
    /// <param name="IsCamera3D">最終的に3Dカメラになるのか</param>
    public void MoveOneAxit(bool IsCamera3D)
    {
        // カメラが2D⇒3Dへ切り替える場合
        if (IsCamera3D == true)
        {
            // 元にいた位置に戻す
            transform.position = InitialPosition;
        }
        else
        // カメラが3D⇒2Dへ切り替える場合
        {
            // 今後オブジェクトを動かす位置を決める
            Vector3 baseposition = (OneAxit == Axit.X) ? new Vector3(Position_MoveOneAxit, InitialPosition.y, InitialPosition.z) :  // 統一させる軸:X
                                   (OneAxit == Axit.Y) ? new Vector3(InitialPosition.x, Position_MoveOneAxit, InitialPosition.z) :  // 統一させる軸:Y
                                                         new Vector3(InitialPosition.x, InitialPosition.y, Position_MoveOneAxit);  //  統一させる軸:Z


            // とある軸を中心に動かす
            transform.position = baseposition;
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        //  3D⇒2Dにカメラを切り替えるときに、統一させる軸
        public Axit TidyOneAxit { get { return OneAxit; } private set { OneAxit = value; } }

}
