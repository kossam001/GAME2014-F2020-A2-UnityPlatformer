/* LoadScene.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-11
 * 
 * Loads the scene with sceneName when onButtonClicked is called.
 * 
 * 2020-11-11: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public void onButtonClicked()
    {
        // Reset the score
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            //GameManager.Instance.ResetScore();
        }

        SceneManager.LoadScene(sceneName);
    }
}
