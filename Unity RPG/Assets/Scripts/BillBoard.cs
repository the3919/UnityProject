using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {
    public GameObject m_Camera;
    // Use this for initialization
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.down);
        // transform.LookAt(m_Camera.transform);
    }
}
 