using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonSceneManager : MonoBehaviour
{
    static SingletonSceneManager instance;

    [SerializeField] private string currentScene;
    [SerializeField] private string redScene, blueScene, greenScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadedScene; Debug.Log("ENABLED!!!!!!");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadedScene; Debug.Log("DISABLED!!!!!!");
    }

    void LoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        currentScene = scene.name;
    }

    void LoadMenuScene()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadSceneAsync("MenuScene");
        }
    }

    void LoadCurrentScene()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            switch (currentScene)
            {
                case "RedScene":
                    Debug.Log("RED SCENE!!!!!!!!!!");
                    SceneManager.sceneLoaded -= LoadedScene;
                    //UnloadScene();
                    LoadRedScene();
                    SceneManager.sceneLoaded += LoadedScene;
                    break;
                case "BlueScene":
                    Debug.Log("BLUE SCENE!!!!!!!!!!");
                    SceneManager.sceneLoaded += LoadedScene;
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void LoadRedScene()
    {
        SceneManager.LoadSceneAsync(redScene);
    }

    void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        LoadCurrentScene();
    }
}
