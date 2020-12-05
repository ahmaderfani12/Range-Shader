using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] Vector3 m_startPosition;
    [SerializeField] GameObject m_item;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          var clone =  GameObject.FindObjectOfType<Item>();
            if (clone == null)
            {
                Instantiate(m_item, m_startPosition, m_item.transform.rotation);
            }
        }
    }
}
