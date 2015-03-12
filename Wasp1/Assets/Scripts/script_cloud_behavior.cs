using UnityEngine;
using System.Collections;

public class script_cloud_behavior : MonoBehaviour {

	public GameObject clouds;
	public Vector3 cloudsForce;
	Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y > 0){
			rigidbody.AddForce(cloudsForce);
		}else{
			rigidbody.velocity = Vector3.zero;
		}
	}
}
