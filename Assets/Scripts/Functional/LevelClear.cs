/* LevelClear.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-30
 * 
 * Uses a plane to transition player to the next level.
 * 
 * 2020-11-30: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
