//这个是用来处理背景图层的小偏移的

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShift : MonoBehaviour
{
    [SerializeField] float ratio;
    // Update is called once per frame
    void Update()
    {
        Vector3 move3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * ratio;
        if (move3 != Vector3.zero)
        {
            transform.position -= move3;

        }
    }
}
