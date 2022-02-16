using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using Button = UnityEngine.UI.Button;

public class CharcterSelector : MonoBehaviour
{
    [SerializeField] private GameObject _bogolubovPrefab;
    [SerializeField] private GameObject _mecherikhovPrefab;
    [SerializeField] private GameObject _frankPrefab;
    [SerializeField] private GameObject _kurchatovPrefab;

    public GameObject _selectedCharacter;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectCharacter(string name)
    {
        if (name == "Bogolubov")
        {
            _selectedCharacter = _bogolubovPrefab;
        }

        if (name == "Mecherikhov")
        {
            _selectedCharacter = _mecherikhovPrefab;
        }

        if (name == "Frank")
        {
            _selectedCharacter = _frankPrefab;
        }

        if (name == "Kurchatov")
        {
            _selectedCharacter = _kurchatovPrefab;
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "AR_Scene")
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
        if (arSessionOrigin != null)
        {
            if(arSessionOrigin.GetComponent<ARPlaneManager>().trackables.count != 0)
                foreach (var plane in arSessionOrigin.GetComponent<ARPlaneManager>().trackables)
                {
                    arSessionOrigin.GetComponent<SpawnCharacter>().Spawn(_selectedCharacter, plane.center);
                }
        }
    }
}
