using UnityEngine;
using System.Collections;

public class script_victoryMessageFadeIn : MonoBehaviour {

	bool fadeInTriggered;
	SpriteRenderer rend;
	public GameObject GM;

	void Start () {
		fadeInTriggered = false;
		rend=this.GetComponent<SpriteRenderer>();
	}

	void Update(){
		if(GM.GetComponent<Setup>().getVictoryCondition()){
			if(!fadeInTriggered){
				fadeInTriggered=true;
				StartCoroutine(fadeIn());
			}
		}
	}

	IEnumerator fadeIn()
	{
		yield return new WaitForSeconds(3.0f);
		for (float i = 0; i <= 255; i++) {
			rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, i/255*3);
			yield return new WaitForSeconds(0.0f);
		}
	}
}
