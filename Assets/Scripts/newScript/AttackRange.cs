using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class AttackRange : Attack {
    [Tooltip("远程攻击的移动速度")]
    [SerializeField] float speed;
    [Tooltip("过一段时间销毁")]
    [SerializeField] float destoryLater;
    Collider collider;
    Rigidbody rigidbody;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        if (rigidbody.velocity != Vector3.zero)
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        else
            rigidbody.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
