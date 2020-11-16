using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour
{
    public Transform lineEndpoint;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void LookForPlatformEdge()
    {
        Physics2D.Linecast(transform.position, lineEndpoint.position, layerMask);
    }
}
