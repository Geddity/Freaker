using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //public string sceneName;
    //public void ChangeScene()
    //{
    //    SceneManager.LoadScene(sceneName);
    //}


    //tolko eto potom
    public void LoadScene(string mapName)
    {
        SceneManager.LoadScene(mapName, LoadSceneMode.Single);
        SceneManager.LoadScene("PlayerStuff", LoadSceneMode.Additive);

        //SceneManager.UnloadSceneAsync("Creation_Arena");
        //SceneView.RepaintAll();
    }
}
