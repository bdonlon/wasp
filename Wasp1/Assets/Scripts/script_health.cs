using UnityEngine;
using System.Collections;

public class script_health : MonoBehaviour {

	public float hp;
	public float maxHp;
	public float healthBarWidth,healthBarSize;
	public int healthBarHeight;
	public GameObject healthBar;
	public GameObject hb;
	public GameObject player;
	public GameObject AnchorLeft;
	public GameObject AnchorRight;
	public float xOffset;
	public float yOffset;

	// Use this for initialization
	void Start (){
		healthBarWidth=1;
		hb = Instantiate(healthBar,transform.position,transform.rotation) as GameObject;
		hb.SetActive(false);
		hb.transform.parent = transform;
		player = GameObject.Find("player");

		//how many pixels wide the health bar will need to be drawn
		healthBarSize = Camera.main.WorldToScreenPoint(AnchorRight.transform.position).x - Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).x;
	}
	
	// Update is called once per frame
	void Update () {
		hp=player.GetComponent<playerMovement>().health;

		//move health bar to center of it's host, then offset it's position
		hb.transform.position=Camera.main.WorldToViewportPoint(transform.position);

		//xy position to draw player healthbar (it will extend to the right from this point)
		xOffset = Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).x - Camera.main.WorldToScreenPoint(player.transform.position).x;
		yOffset = Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).y - Camera.main.WorldToScreenPoint(player.transform.position).y;

		float healthPercent = hp/maxHp*100;
		if(healthPercent<0){
			healthPercent=0;
		}
		if(healthPercent>100){
			healthPercent=100;
		}
		healthBarWidth = healthPercent*healthBarSize/100;
		hb.transform.Find("red").guiTexture.pixelInset=new Rect(xOffset,yOffset,healthBarSize,5);
		hb.transform.Find("green").guiTexture.pixelInset=new Rect(xOffset,yOffset,healthBarWidth,5);
	}
}
