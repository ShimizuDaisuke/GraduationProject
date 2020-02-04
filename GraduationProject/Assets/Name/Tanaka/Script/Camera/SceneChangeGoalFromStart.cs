//=======================================================================================
//! @file   EventCameraGoalFromStart.cs
//! @brief  シーンが変わったときにスタートからゴールへのカメラ動きのフラグ？
//! @author 田中歩夢
//! @date   01月21日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//シーンが変わったときにスタートからゴールへのカメラ動きのフラグ？
public class SceneChangeGoalFromStart : MonoBehaviour
{
    //イベントDirectorオブジェクト
    private GameObject m_eventDirectorObj = null;
    //イベントDirector
    private EventDirector m_eventDirector = default;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_eventDirectorObj = GameObject.Find("Director/EventDirector");
        if (m_eventDirectorObj != null)
        {
            m_eventDirector = m_eventDirectorObj.GetComponent<EventDirector>();
        }

        if (m_eventDirector != null)
        {
            m_eventDirector.IsEventKIND = EventDirector.EventKIND.CAMERA_GOAL_FROM_START;
        }
    }
}
