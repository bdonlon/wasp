using UnityEngine;
using System.Collections;

public class script_healthbar_fade : MonoBehaviour {

	public GameObject red;
	public GameObject green;
	GUITexture redTex;
	GUITexture greenTex;
	public GameObject _GM;
	private float alpha;

	// Use this for initialization
	void Start () {
		_GM = GameObject.Find("_GM");
		redTex = red.GetComponent<GUITexture>();
		greenTex = green.GetComponent<GUITexture>();
		alpha = greenTex.color.a*255;
	}
	
	// Update is called once per frame
	void Update () {
//		if(_GM.GetComponent<Setup>().getFailureCondition()){
//			StartCoroutine(fade());
//		}

		alpha = 1-_GM.GetComponent<Setup>().getSpecialAlpha();
		redTex.color = new Color(redTex.color.r,redTex.color.g,redTex.color.b,alpha);
		greenTex.color = new Color(greenTex.color.r,greenTex.color.g,greenTex.color.b,alpha);
	}

//	IEnumerator fade(){
//		for (float i = alpha; i >= 0; i--) {	//Fade clouds to full opaque
//			redTex.color = new Color(redTex.color.r,redTex.color.g,redTex.color.b,i/255);
//			greenTex.color = new Color(greenTex.color.r,greenTex.color.g,greenTex.color.b,i/255);
//			yield return new WaitForSeconds(0.0f);
//		}
//	}
}
