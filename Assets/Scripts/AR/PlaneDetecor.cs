using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetecor : MonoBehaviour
{
    public GameObject arPlaneManagerObject;
    public GameObject WaitWindow;

    private ARPlaneManager _arPlaneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _arPlaneManager = arPlaneManagerObject.GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_arPlaneManager.trackables.count != 0)
            WaitWindow.SetActive(false);
    }
}
