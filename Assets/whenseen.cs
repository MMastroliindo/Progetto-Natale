using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class whenseen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            RaycastHit hit1;
            FindObjectsByType<MeshCollider>(FindObjectsSortMode.InstanceID).ToList().Find(o => Physics.Raycast(o.transform.position, transform.TransformDirection(Vector3.back), out hit1, Mathf.Infinity, layerMask)).GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
        }
    }
}
