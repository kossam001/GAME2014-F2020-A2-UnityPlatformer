/* CrumblyPlatform.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Vanishing platform tiles behaviour.
 * 
 * 2020-11-23: Added this script.  Certain tiles will disappear when stepped on.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrumblyPlatform : MonoBehaviour
{
    public Tilemap tilemap;
    public bool isCollapsing = false;
    private List<Vector3Int> collapsingTiles;
    public float collapsingPeriod = 1;
    public float resetDelay = 3;

    // Start is called before the first frame update
    void Start()
    {
        collapsingTiles = new List<Vector3Int>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            !other.collider.isTrigger)
        {
            isCollapsing = true;
            StartCoroutine(CollapseTile(other));
        }
    }

    IEnumerator CollapseTile(Collision2D other)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(other.GetContact(0).point);
        TileBase tile = tilemap.GetTile(tilePosition);

        if (collapsingTiles.Contains(tilePosition))
        {
            yield break;
        }

        collapsingTiles.Add(tilePosition);
        yield return new WaitForSeconds(collapsingPeriod);

        tilemap.SetTile(tilePosition, null);
        StartCoroutine(ResetTile(tile, tilePosition));
    }

    IEnumerator ResetTile(TileBase tile, Vector3Int position)
    {
        yield return new WaitForSeconds(resetDelay);
        tilemap.SetTile(position, tile);
        isCollapsing = false;
        collapsingTiles.Remove(position);
    }
}
