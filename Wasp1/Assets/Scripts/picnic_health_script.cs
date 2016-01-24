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
	public GameObject rug;
	public GameObject mainCam;
	public GameObject AnchorLeft;
	public GameObject AnchorRight;
	public float healthBarWidth,healthBarSize;
	public SpriteRenderer SR;
	public float xOffset;
	public float yOffset;
	public GameObject eatSound;

	public float timeEaten;
	public float elapsedTime;

	// Use this for initialization
	void Start () {
		timeEaten =  Time.time-100;
		picnicHealthCurrent = 200;
		picnicHealthTotal = picnicHealthCurrent;

		//how many pixels wide the health bar will need to be drawn
		healthBarSize = Camera.main.WorldToScreenPoint(AnchorRight.transform.position).x - Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).x;

		//move health bar to center of it's host, then offset it's position
		hb.transform.position=Camera.main.WorldToViewportPoint(transform.position);

		//xy position to draw player healthbar (it will extend to the right from this point)
		xOffset = Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).x - Camera.main.WorldToScreenPoint(rug.transform.position).x;
		yOffset = Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).y - Camera.main.WorldToScreenPoint(rug.transform.position).y;
	}
	
	// Update is called once per frame
	void Update () {
		drawHealthBar();
		float healthPercent = getPercentage();
		healthBarWidth = healthPercent*healthBarSize/100;
		hb.transform.Find("red").guiTexture.pixelInset=new Rect(xOffset,yOffset,healthBarSize,5);
		hb.transform.Find("green").guiTexture.pixelInset=new Rect(xOffset,yOffset,healthBarWidth,5);
	}

	public void damagePicnicIntegrity(int damage){
		timeEaten =  Time.time;
		if((picnicHealthCurrent-damage)>=0){
			picnicHealthCurrent = picnicHealthCurrent - damage;
		}else{
			picnicHealthCurrent=0;
			if(!_GM.GetComponent<Setup>().getFailureCondition()){	//might already have failure condition due to player death
				_GM.GetComponent<Setup>().setFailureCondition(true);
			}
		}
	}

	public float getPercentage(){
		float value = (100f/picnicHealthTotal)*picnicHealthCurrent;
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

	public void forceDrawHealthBar(){
		timeEaten = Time.time;
	}

	public IEnumerator heal(int healValue){
		timeEaten =  Time.time;	//Will cause healthbar to render
		yield return new WaitForSeconds(0.3f);	//Delay will allow health increase to be apparent on the healthbar
		eatSound.GetComponent<playSound>().play();
		if((picnicHealthCurrent+healValue)<picnicHealthTotal){
			picnicHealthCurrent = picnicHealthCurrent+healValue;
		}else{
			picnicHealthCurrent = picnicHealthTotal;
		}
	}
}
