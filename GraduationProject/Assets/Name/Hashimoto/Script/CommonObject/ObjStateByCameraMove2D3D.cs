// -----------------------------------------------------------------------------------------
//! @file       ObjStateByCameraMove2D3D.cs
//!
//! @brief      カメラが2D⇔3Dへ切り替えるまえに、オブジェクトの表示や位置を変える処理
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.26
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjStateByCameraMove2D3D : MonoBehaviour
{
    // 2Dカメラのみ表示されるオブジェクト
    [SerializeField]
    private GameObject[] ObjBy2DCamera = default;

    // 3Dカメラのみ表示されるオブジェクト
    [SerializeField]
    private GameObject[] ObjBy3DCamera = default;

    // カメラが3D→2Dへ切り替えたときに、一つの軸を中心に揃えるオブジェクト
    [SerializeField]
    private GameObject[] ObjByOneAxitMove = default;

    // カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域
    [SerializeField]
    private GameObject[] ObjByCamera2DNoArea = default;


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
            // そのオブジェクトがない場合、処理を飛ばす
            if (obj2d == null) continue;

            obj2d.SetActive(true);
        }

        // 3Dカメラのみ表示されるオブジェクトを非表示させる
        foreach (GameObject obj3d in ObjBy3DCamera)
        {
            // そのオブジェクトがない場合、処理を飛ばす
            if (obj3d == null) continue;

            obj3d.SetActive(false);
        }

        // とある軸を中心にそろえる
        foreach(GameObject objmoveoneaxit in ObjByOneAxitMove)
        {
            // そのオブジェクトがない場合、処理を飛ばす
            if (objmoveoneaxit == null) continue;

            // [3D⇒2Dにカメラを切り替えるときに、とある軸を中心にそれぞれのオブジェクトの位置を統一させる位置] のスクリプトを呼ぶ
            Obj_OneAxitMove script_Obj_OneAxitMove = objmoveoneaxit.GetComponent<Obj_OneAxitMove>();

            // とある軸を中心に位置を揃える
            script_Obj_OneAxitMove.MoveOneAxit(false);

            // 「プレイヤーの位置によって表示非表示させるオブジェクトによる処理」のスクリプトを呼ぶ
            Obj_AppearDisPlayerPos script_Obj_AppearDisPlayerPos = objmoveoneaxit.GetComponent<Obj_AppearDisPlayerPos>();

            // そのスクリプトが存在する場合
            if (script_Obj_AppearDisPlayerPos != null)
            {
                // プレイヤーの位置によって、とあるオブジェクトを表示や非表示させる
                script_Obj_AppearDisPlayerPos.DecideAppearDisAppearObjByPlayerPos(false);

            }

        }

        // カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域を配置する
        ChangeActiveObjByCamera2DNoArea(true);
    }

    /// <summary>
    ///  3Dカメラのみ表示されるオブジェクトを表示する
    /// </summary>
    private void AppearObjBy3DCamera()
    {
        // 3Dカメラのみ表示されるオブジェクトを表示させる
        foreach (GameObject obj3d in ObjBy3DCamera)
        {
            // そのオブジェクトがない場合、処理を飛ばす
            if (obj3d == null) continue;

            obj3d.SetActive(true);
        }

        //  2Dカメラのみ表示されるオブジェクトを表示させる
        foreach (GameObject obj2d in ObjBy2DCamera)
        {
            // そのオブジェクトがない場合、処理を飛ばす
            if (obj2d == null) continue;

            obj2d.SetActive(false);
        }

        // とある軸を中心にそろえる
        foreach (GameObject objmoveoneaxit in ObjByOneAxitMove)
        {
            // そのオブジェクトがない場合、処理を飛ばす
            if (objmoveoneaxit == null) continue;

            // [3D⇒2Dにカメラを切り替えるときに、とある軸を中心にそれぞれのオブジェクトの位置を統一させる位置] のスクリプトを呼ぶ
            Obj_OneAxitMove script_Obj_OneAxitMove = objmoveoneaxit.GetComponent<Obj_OneAxitMove>();

            // とある軸を中心に揃えた位置を元に戻す
            script_Obj_OneAxitMove.MoveOneAxit(true);

            // 「プレイヤーの位置によって表示非表示させるオブジェクトによる処理」のスクリプトを呼ぶ
            Obj_AppearDisPlayerPos script_Obj_AppearDisPlayerPos = objmoveoneaxit.GetComponent<Obj_AppearDisPlayerPos>();

            // そのスクリプトが存在する場合
            if(script_Obj_AppearDisPlayerPos != null)
            {
                // プレイヤーの位置によって、とあるオブジェクトを表示や非表示させる
                script_Obj_AppearDisPlayerPos.DecideAppearDisAppearObjByPlayerPos(true);

            }
        }

        // カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域を配置しない
        ChangeActiveObjByCamera2DNoArea(false);
    }

    /// <summary>
    /// カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域を表示、もしくは非表示させる
    /// </summary>
    /// <param name="isactive">オブジェクトを表示させるか(true:表示, false:非表示)</param>
    public void ChangeActiveObjByCamera2DNoArea(bool isactive)
    {
        // カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域を表示、もしくは非表示させる
        foreach (GameObject objcamera2dnoarea in ObjByCamera2DNoArea)
        {
            // そのオブジェクトがない場合、処理を飛ばす
            if (objcamera2dnoarea == null) continue;

            // そのオブジェクトが軸中心に動かすオブジェクトの場合、処理を飛ばす
            if (objcamera2dnoarea.GetComponent<Obj_OneAxitMove>() != null) continue;

            objcamera2dnoarea.SetActive(isactive);
        }
    }

    // ------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// [2Dカメラのみ表示されるオブジェクト]に追加する
    /// </summary>
    /// <param name="obj">追加したいオブジェクト</param>
    public void AddObjBy2DCamera(GameObject obj)
    {
        // 現在の領域
        int size = ObjBy2DCamera.Length;

        // 新たにオブジェクトを追加できるように領域を確保
        ObjBy2DCamera = new GameObject[size + 1];

        // 追加する
        ObjBy2DCamera[size] = obj;
    }

    /// <summary>
    ///  [3Dカメラのみ表示されるオブジェクト]に追加する
    /// </summary>
    /// <param name="obj">追加したいオブジェクト</param>
    public void AddObjBy3DCamera(GameObject obj)
    {
        // 現在の領域
        int size = ObjBy3DCamera.Length;

        // 新たにオブジェクトを追加できるように領域を確保
        ObjBy3DCamera = new GameObject[size + 1];

        // 追加する
        ObjBy3DCamera[size] = obj;
    }

    /// <summary>
    /// [カメラが3D→2Dへ切り替えたときに、一つの軸を中心に揃えるオブジェクト]に追加する
    /// </summary>
    /// <param name="obj">追加したいオブジェクト</param>
    public void AddObjByOneAxitMove(GameObject obj)
    {
        // 現在の領域
        int size = ObjByOneAxitMove.Length;

        // 新たにオブジェクトを追加できるように領域を確保
        ObjByOneAxitMove = new GameObject[size + 1];

        // 追加する
        ObjByOneAxitMove[size] = obj;
    }

    /// <summary>
    /// [カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域]に追加する
    /// </summary>
    /// <param name="obj">追加したいオブジェクト</param>
    public void AddObjByCamera2DNoArea(GameObject obj)
    {
        // 現在の領域
        int size = ObjByCamera2DNoArea.Length;

        // 新たにオブジェクトを追加できるように領域を確保
        ObjByCamera2DNoArea = new GameObject[size + 1];

        // 追加する
        ObjByCamera2DNoArea[size] = obj;
    }
}
