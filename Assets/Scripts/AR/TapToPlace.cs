using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_ReferencePointManager;
    List<ARAnchor> m_ReferencePoint; 
    ARPlaneManager m_PlaneManager;

    private GameObject cube;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject _planePrefab;
    private bool _spawnedPlane = false;
    private GameObject _plane;

    //Remove all reference points created
    public void RemoveAllReferencePoints()
    {
        foreach (var referencePoint in m_ReferencePoint)
        {
            m_ReferencePointManager.RemoveAnchor(referencePoint);
        }
        m_ReferencePoint.Clear();
    }


    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_ReferencePointManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_ReferencePoint = new List<ARAnchor>();
    }


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
        if (arSessionOrigin != null && !_spawnedPlane)
        {
            if(arSessionOrigin.GetComponent<ARPlaneManager>().trackables.count != 0)
                foreach (var plane in arSessionOrigin.GetComponent<ARPlaneManager>().trackables)
                {
                    Instantiate(_planePrefab, plane.center, Quaternion.identity);
                    _spawnedPlane = true;
                }
        }
    }
}