using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SingletonSceneManager : MonoBehaviour
{
    static SingletonSceneManager instance;

    [SerializeField] private string currentScene;
    [SerializeField] private string redScene, blueScene, greenScene; // Scenes to load from their names in the project.
    [Space]
    [SerializeField] private GameObject loadingBackground;
    [SerializeField] private Button redButton, blueButton, greenButton;

    private void Awake()
    {
        // Create a singleton instance of this object that can be used to load scenes across other scenes.
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

    // Gets the scene name to monitor which scene is currently loaded.
    void LoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        currentScene = scene.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetLoadingBackground();
        GetButtons();
        SetButtonOnClickListeners();
    }

    // Get the loading background.
    void GetLoadingBackground()
    {
        loadingBackground = GameObject.Find("LoadingBackground");
        loadingBackground.SetActive(false);
    }

    // Get the 3 buttons that loads the red, blue and green scenes.
    void GetButtons()
    {
        redButton = GameObject.Find("RedSceneButton").GetComponent<Button>();
        blueButton = GameObject.Find("BlueSceneButton").GetComponent<Button>();
        greenButton = GameObject.Find("GreenSceneButton").GetComponent<Button>();
    }

    // Add an onclick listener for the 3 buttons to load their respective scenes.
    void SetButtonOnClickListeners()
    {
        redButton.onClick.AddListener(() => LoadRedScene());
        blueButton.onClick.AddListener(() => LoadBlueScene());
        greenButton.onClick.AddListener(() => LoadGreenScene());
    }

    public void LoadRedScene()
    {
        loadingBackground.SetActive(true);
        Invoke("DelayRedSceneLoad", 1f); // Invoked to fake a 1 second loading time.
        SceneManager.sceneLoaded += LoadedScene;
    }

    public void LoadBlueScene()
    {
        loadingBackground.SetActive(true);
        Invoke("DelayBlueSceneLoad", 1f); // Invoked to fake a 1 second loading time.
        SceneManager.sceneLoaded += LoadedScene;
    }

    public void LoadGreenScene()
    {
        loadingBackground.SetActive(true);
        Invoke("DelayGreenSceneLoad", 1f); // Invoked to fake a 1 second loading time.
        SceneManager.sceneLoaded += LoadedScene;
    }

    void DelayRedSceneLoad()
    {
        LoadSceneByName(redScene);
    }

    void DelayBlueSceneLoad()
    {
        LoadSceneByName(blueScene);
    }

    void DelayGreenSceneLoad()
    {
        LoadSceneByName(greenScene);
    }

    // Update is called once per frame
    void Update()
    {
        LoadCurrentScene();
        LoadMenuScene();
        QuitApplication();
    }

    // Loads scene by the sceneName through the parameter.
    void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void LoadCurrentScene()
    {
        // Checks which scene is currently loaded and loads that loaded scene when F2 is pressed.
        if (Input.GetKeyDown(KeyCode.F2))
        {
            switch (currentScene)
            {
                case "RedScene":
                    SceneManager.sceneLoaded -= LoadedScene;
                    LoadSceneByName(redScene);
                    SceneManager.sceneLoaded += LoadedScene;
                    break;
                case "BlueScene":
                    SceneManager.sceneLoaded -= LoadedScene;
                    LoadSceneByName(blueScene);
                    SceneManager.sceneLoaded += LoadedScene;
                    break;
                case "GreenScene":
                    SceneManager.sceneLoaded -= LoadedScene;
                    LoadSceneByName(greenScene);
                    SceneManager.sceneLoaded += LoadedScene;
                    break;
            }
        }
    }

    // Find and reference back the loading background and buttons when going back to menu scene.
    void LoadMenuScene()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !currentScene.Equals("MenuScene"))
        {
            SceneManager.LoadScene("MenuScene");
            Invoke("GetLoadingBackground", 1f);
            Invoke("GetButtons", 1f);
            Invoke("SetButtonOnClickListeners", 1f);
        }
    }

    // Quits the application when escape key is pressed.
    void QuitApplication()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
