/* NextPage.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-09-26
 * 
 * Enables the next GameObject in a provided list while disabling the current GameObject.
 * 
 * 2020-09-26: Added script to demo the instruction screens.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
    public List<GameObject> pages;
    private int pageNum = 0;

    public void TurnPage()
    {
        pages[pageNum].SetActive(false);

        // Once the max page count is hit, go back to 0
        if (pageNum == pages.Count - 1)
        {
            pageNum = 0;
        }
        else
        {
            pageNum++;
        }

        pages[pageNum].SetActive(true);
    }
}
