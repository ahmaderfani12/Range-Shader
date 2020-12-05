using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    [SerializeField] Material m_mat;
    private bool started = false;
    private float alpha;

    private void Start()
    {
        m_mat = this.GetComponent<MeshRenderer>().material;
        alpha = m_mat.GetFloat("_alpha");
    }
    private void LateUpdate()
    {
        MouseJobs();
        RotationJobs();
    }

    private void RotationJobs()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.RotateAroundLocal(Vector3.up, 2 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAroundLocal(Vector3.up, -2 * Time.deltaTime);
        }
    }

    private void MouseJobs()
    {
        if (Input.GetMouseButtonDown(0) && !started)
        {
            started = true;
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                transform.localPosition = hit.point;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (started)
            {
                m_mat.SetFloat("_ring", 0);
                Sequence seq = DOTween.Sequence();
                seq.Join(DOTween.To(IncreaseCircleSize, 0, 1, 0.7f));
                seq.Join(DOTween.To(DecreaseAlpha, 0, 1, 0.7f));
                seq.AppendCallback(() => {
                    this.transform.GetChild(0).transform.parent = null;
                    Destroy(this.gameObject);
                });
               
            }
        }
    }
    private void IncreaseCircleSize(float step)
    {
        m_mat.SetFloat("_circlePower", Mathf.Lerp(0, 0.47f, step));
    }

    private void DecreaseAlpha(float step)
    {
        m_mat.SetFloat("_alpha", Mathf.Lerp(alpha, 0, step));

    }
}
