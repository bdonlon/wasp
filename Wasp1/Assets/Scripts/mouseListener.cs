using UnityEngine;
using System.Collections;

public class mouseListener : MonoBehaviour {

	public GameObject swatter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			//swatter.GetComponent<swat>().enable();

			StartCoroutine(swatter.GetComponent<swatter_script>().swing());

			//swatter.GetComponent<swat>().disable();
		}
	}
}
