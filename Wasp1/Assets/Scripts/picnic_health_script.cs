using UnityEngine;
using System.Collections;

public class picnic_health_script : MonoBehaviour {

	public int picnicHealthCurrent;
	public int picnicPercentage;
	public int picnicHealthTotal;
	public GameObject spouse;
	public GameObject food;
	public GameObject _GM;
	public GameObject hb;
	public float healthBarWidth,healthBarSize;

	public float timeEaten;
	public float elapsedTime;

	// Use this for initialization
	void Start () {
		timeEaten =  Time.time-100;
		healthBarSize=2;
		picnicHealthCurrent = 200;
		picnicHealthTotal = picnicHealthCurrent;
	}
	
	// Update is called once per frame
	void Update () {
		drawHealthBar();

		float healthPercent = getPercentage();
		healthBarWidth = healthPercent*healthBarSize;
		hb.transform.Find("red").guiTexture.pixelInset=new Rect(0,0,100*healthBarSize,5);
		hb.transform.Find("green").guiTexture.pixelInset=new Rect(0,0,healthBarWidth,5);
	}

	public void damagePicnicIntegrity(int damage){
		if((picnicHealthCurrent-damage)>=0){
			timeEaten =  Time.time;
			picnicHealthCurrent = picnicHealthCurrent - damage;

			if(picnicHealthCurrent <= 0){
				_GM.GetComponent<Setup>().failureCondition=true;
			}
		}
	}

	public float getPercentage(){
		float value = (100f/picnicHealthTotal)*picnicHealthCurrent;
//		float x = (100f/picnicHealthTotal)*picnicHealthCurrent;
//		Debug.Log (x);
//		Debug.Log (picnicHealthTotal);
//		Debug.Log (picnicHealthCurrent);
		return value;
	}

	void drawHealthBar(){
		elapsedTime =  Time.time - timeEaten;
		if(elapsedTime<1.5f){
			hb.active=true;
		}else{
			hb.active=false;
		}
	}
}
