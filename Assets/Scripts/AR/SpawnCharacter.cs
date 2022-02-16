using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpawnCharacter : MonoBehaviour
{
    private ARPlaneManager _arPlaneManager;

    [FormerlySerializedAs("_spawnedCharacter")] public GameObject spawnedCharacter;
    public Button stopTalkingButton;

    private void Start()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_arPlaneManager.trackables.count != 0)
        {
            //GameObject.Find("Character_Manager").GetComponent<>();
        }
    }

    public void Spawn(GameObject character, Vector3 pos)
    {
        if(spawnedCharacter == null)
        {
            spawnedCharacter = Instantiate(character, pos, Quaternion.identity);
            
            stopTalkingButton.onClick.AddListener(spawnedCharacter.GetComponent<Move>().StopSpeaking);
        }
        
    }
}
