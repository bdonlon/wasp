using UnityEngine;
using System.Collections;

public class picnic_health_script : MonoBehaviour {

	public int picnicHealthCurrent;
	public int picnicPercentage;
	public int picnicHealthTotal;
	public GameObject spouse;
	public GameObject food;
	public GameObject _GM;

	// Use this for initialization
	void Start () {
		picnicHealthCurrent = 200;
		picnicHealthTotal = picnicHealthCurrent;
	}
	
	// Update is called once per frame
//	void Update () {
//		picnicPercentage = 100 - picnicHealth
//	}

	public void damagePicnicIntegrity(int damage){
		if((picnicHealthCurrent-damage)>=0){
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
}
