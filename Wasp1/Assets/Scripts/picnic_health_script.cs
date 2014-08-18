using UnityEngine;
using System.Collections;

public class picnic_health_script : MonoBehaviour {

	public int picnicHealthCurrent;
	public int picnicPercentage;
	public int picnicHealthTotal;
	public GameObject spouse;
	public GameObject food;

	// Use this for initialization
	void Start () {
		picnicHealthCurrent = spouse.GetComponent<spouse_behavior>().getHealth()+food.GetComponent<food_behavior>().getHealth();
		picnicHealthTotal = picnicHealthCurrent;
	}
	
	// Update is called once per frame
//	void Update () {
//		picnicPercentage = 100 - picnicHealth
//	}

	public void damagePicnicIntegrity(int damage){
		picnicHealthCurrent = picnicHealthCurrent - damage;
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
