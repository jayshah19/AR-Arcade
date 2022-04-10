using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// The scene controller.
/// </summary>

public class SceneController : MonoBehaviour
{

    /// <summary>
    /// Changes the scene.
    /// </summary>
    /// <param name="SceneName">The scene name.</param>
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
