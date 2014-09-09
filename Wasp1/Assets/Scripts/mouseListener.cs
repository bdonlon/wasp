using UnityEngine;
using System.Collections;

public class mouseListener : MonoBehaviour {

	public GameObject swatter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.UpArrow))
		{
			//StartCoroutine(swatter.GetComponent<swatter_script>().swingKey("up"));
			swatter.GetComponent<swatter_script>().swingKey ("up");
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			swatter.GetComponent<swatter_script>().swingKey ("down");
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			swatter.GetComponent<swatter_script>().swingKey ("left");
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			swatter.GetComponent<swatter_script>().swingKey ("right");
		}

		if(Input.GetMouseButtonDown(1)){
			//swatter.GetComponent<swat>().enable();

			StartCoroutine(swatter.GetComponent<swatter_script>().swingMouse());

			//swatter.GetComponent<swat>().disable();
		}
	}
}
