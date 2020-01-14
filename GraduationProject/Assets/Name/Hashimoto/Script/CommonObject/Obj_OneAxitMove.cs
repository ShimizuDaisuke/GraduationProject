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
    private List<Vector3> InitialPosition = new List<Vector3>();

    // [このオブジェクト内でそれぞれの子オブジェクト]が位置を統一させるか
    // true  : 位置がばらついている[このオブジェクトの子]が位置を統一させる
    // false : このオブジェクトの子がいない or このオブジェクトの親のみ位置を統一させる
    [SerializeField]
    private bool IsChildrenMoveOneAxit = false;

    // 子のオブジェクト (IsChildrenMoveOneAxitがtrueの場合のみ使う)
    private List<GameObject> TmpChildren = new List<GameObject>();

    // 親のオブジェクトのみ動かす場合(IsChildrenMoveOneAxitがfalseの場合)の番号
    private int ParentNum = 0;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Awake()
    {
        // それぞれの子オブジェクト]が位置を統一させる場合
        if(IsChildrenMoveOneAxit == true)
        {
            // それぞれの子のオブジェクトを呼ぶ
            foreach(Transform child in transform)
            {
                // 添付用の子のオブジェクトを保存する
                TmpChildren.Add(child.gameObject);

                // カメラが3D⇒2Dへ切り替える前の初期位置を、そのオブジェクトが非表示される前に記憶させる
                InitialPosition.Add(child.localPosition);
            }
        }
        else
        //  親のオブジェクトのみ動かす場合
        {
            // カメラが3D⇒2Dへ切り替える前の初期位置を、そのオブジェクトが非表示される前に記憶させる
            InitialPosition.Add(transform.position);
        }
    }

    /// <summary>
    /// 3D⇒2D:1つの軸を中心に、それぞれのオブジェクトの位置を統一させる
    /// 2D⇒3D:元にいた位置に戻る
    /// </summary>
    /// <param name="IsCamera3D">最終的に3Dカメラになるのか</param>
    public void MoveOneAxit(bool IsCamera3D)
    {
        // それぞれの子オブジェクト]が位置を統一させる場合
        if (IsChildrenMoveOneAxit == true)
        {
            // 現在の子の番号
            int nowchildnum = 0;

            // 添付用の子の番号
            int tmpchildnum = 0;

            // 添付用の子のオブジェクトからそれぞれの子のオブジェクトを呼ぶ
            foreach (GameObject child in TmpChildren)
            {
                // 現在の子が最大数より越えた場合、これ以上処理をしない


                // 指定された子のオブジェクトが無い場合
                if(child == null)
                {
                    // 次の子を注目する
                    nowchildnum++;
                    // 処理を飛ばす
                    continue;
                }


                // それぞれの子のオブジェクトの位置を動かす
                child.transform.localPosition = MoveOneAxitMain(InitialPosition[nowchildnum], IsCamera3D);

                // 次の子を注目する
                nowchildnum++;
            }

            // 親の位置を元の位置にリセットする
            transform.position = Vector3.zero;

        }
       
        //  親のオブジェクトのみ動かす場合
        {
            // 親のみ動かす
            transform.position = MoveOneAxitMain(InitialPosition[ParentNum], IsCamera3D);
        }
    }

    /// <summary>
    /// 3D⇒2D:1つの軸を中心に、それぞれのオブジェクトの位置を統一させる(メイン)
    /// 2D⇒3D:元にいた位置に戻る
    /// </summary>
    /// <param name="InitialPosition">初期位置</param>
    /// <param name="IsCamera3D">最終的に3Dカメラになるのか</param>
    /// <returns>指定されたオブジェクトの位置</returns>
    private Vector3 MoveOneAxitMain(Vector3 InitialPosition,bool IsCamera3D)
    {
        // カメラが2D⇒3Dへ切り替える場合
        if (IsCamera3D == true)
        {
            // 元にいた位置に戻す
            return InitialPosition;
        }
        else
        // カメラが3D⇒2Dへ切り替える場合
        {
            // 今後オブジェクトを動かす位置を決める
            Vector3 baseposition = (OneAxit == Axit.X) ? new Vector3(Position_MoveOneAxit, InitialPosition.y, InitialPosition.z) :  // 統一させる軸:X
                                   (OneAxit == Axit.Y) ? new Vector3(InitialPosition.x, Position_MoveOneAxit, InitialPosition.z) :  // 統一させる軸:Y
                                                         new Vector3(InitialPosition.x, InitialPosition.y, Position_MoveOneAxit);  //  統一させる軸:Z

            // とある軸を中心に動かす
            return transform.position = baseposition;
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

    //  3D⇒2Dにカメラを切り替えるときに、統一させる軸
    public Axit TidyOneAxit { get { return OneAxit; } private set { OneAxit = value; } }

}
