using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    //PUBLIC
    public static SceneManager _SCENE_MANAGER; //SINGELTON

    [SerializeField]
    private Canvas countCoins;

    private float coins = 5.0f;

    private void Awake()
    {
        if (_SCENE_MANAGER != null && _SCENE_MANAGER != this)
        {
            Destroy(_SCENE_MANAGER); //Destruir si ya existe uno
        }
        else
        {
            //Dont destroy on load para cambiar de escenas
            _SCENE_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    public void coinCollected()
    {
        
    }
}
