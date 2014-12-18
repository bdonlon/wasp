using UnityEngine;
using System.Collections;

public class script_controls_graphic : MonoBehaviour {

	public GameObject graphic_move;
	public GameObject graphic_swing;
	SpriteRenderer rend1;
	SpriteRenderer rend2;

	// Use this for initialization
	void Start () {
		//StartCoroutine(moveGraphics());
		rend1=graphic_move.GetComponent<SpriteRenderer>();
		rend2=graphic_swing.GetComponent<SpriteRenderer>();
		StartCoroutine(fadeOut());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator fadeOut()
	{
		for (float i = 255; i >= 0; i--) {
			rend1.color = new Color(rend1.color.r, rend1.color.g, rend1.color.b, i/255*3);
			rend2.color = new Color(rend2.color.r, rend2.color.g, rend2.color.b, i/255*3);
			yield return new WaitForSeconds(0.0f);
		}

		Destroy(this.gameObject);
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
