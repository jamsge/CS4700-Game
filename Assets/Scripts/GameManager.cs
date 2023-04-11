using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;

    void Awake()
    {
        LoadSceneAdditively("InGameHUD");
    }

    void Start() {
    }

    private static void LoadSceneAdditively(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
