using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    //PUBLIC
    public static SceneLoadingManager _SCENE_MANAGER; //SINGELTON

    private float coins = 10.0f;
    private TextMeshProUGUI text;

    private bool canChangeScene = true;

    private void Awake()
    {
        if (_SCENE_MANAGER != null && _SCENE_MANAGER != this)
        {
            Destroy(this.gameObject); //Destruir si ya existe uno
        }
        else
        {
            //Dont destroy on load para cambiar de escenas
            _SCENE_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        Debug.Log("Update: "+this.coins);
        if (this.coins < 1)
        {
            Win();
            canChangeScene = false;
        }
    }

    private void Win()
    {
        if (canChangeScene)
            SceneManager.LoadScene("Win");
    }
    public void Lost()
    {
        if (canChangeScene)
            SceneManager.LoadScene("Lost");
    }
    public void Play()
    {
        Debug.Log("AAas");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        this.coins = 10f;
    }
    public void MainMenu()
    {
        canChangeScene = true;
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void coinCollected(GameObject textGameObject)
    {
        text = textGameObject.GetComponent<TextMeshProUGUI>();
        this.coins = this.coins - 1;
        text.text = this.coins.ToString();
    }
}
