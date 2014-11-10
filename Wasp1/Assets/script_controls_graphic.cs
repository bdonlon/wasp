using UnityEngine;
using System.Collections;

public class script_controls_graphic : MonoBehaviour {

	public GameObject graphic_move;
	public GameObject graphic_swing;

	// Use this for initialization
	void Start () {
		StartCoroutine(moveGraphics());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator moveGraphics(){
		yield return new WaitForSeconds(1.0f);
		Vector3 force = new Vector3(10,0,0);
		graphic_move.rigidbody2D.AddForce(-force*35);
		graphic_swing.rigidbody2D.AddForce(force*35);
		yield return new WaitForSeconds(3.0f);



		Destroy(this.gameObject);
	}
}
