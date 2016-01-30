using UnityEngine;
using System.Collections;

public class script_GUI_wave_text : MonoBehaviour {
	
	public Sprite[] Numbers;
	public GameObject 	wave, X1,X10,X100, of, Y1,Y10,Y100,
	finalWaveFINAL, finalWaveWAVE,
	youReached,
	failureText;
	SpriteRenderer rendWave,rendX1,rendX10,rendX100,rendOf,rendY1,rendY10,rendY100,rendFWF,rendFWW,rendReached,rendFailureText;
	int currWave,maxWaves;
	Color colour;
	public GameObject _GM;
	public int first,second,third;
	public bool finalWaveSemaphore;
	
	// Use this for initialization
	void Start () {
		finalWaveSemaphore=true;
		rendWave=wave.GetComponent<SpriteRenderer>();
		rendX1=X1.GetComponent<SpriteRenderer>();
		rendX10=X10.GetComponent<SpriteRenderer>();
		rendX100=X100.GetComponent<SpriteRenderer>();
		rendOf=of.GetComponent<SpriteRenderer>();
		rendY1=Y1.GetComponent<SpriteRenderer>();
		rendY10=Y10.GetComponent<SpriteRenderer>();
		rendY100=Y100.GetComponent<SpriteRenderer>();
		rendFWF=finalWaveFINAL.GetComponent<SpriteRenderer>();
		rendFWW=finalWaveWAVE.GetComponent<SpriteRenderer>();
		rendReached=youReached.GetComponent<SpriteRenderer>();
		rendFailureText=failureText.GetComponent<SpriteRenderer>();
		
		if(_GM.GetComponent<Setup>().getEndless()){
			float distance = Mathf.Abs(wave.transform.position.x - X1.transform.position.x);
			Vector3 newPosition = new Vector3(0-distance/2,wave.transform.position.y,wave.transform.position.z);
			wave.transform.position = newPosition;
			newPosition = new Vector3(0+distance/2,X1.transform.position.y,X1.transform.position.z);
			X1.transform.position = newPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setWaveInfo(int curr, int max)
	{
		curr++; //Value starts at 0
		if(!_GM.GetComponent<Setup>().getEndless()){
			if(curr<max){
				wave.active=true;
				X1.active=true;
				of.active=true;
				Y1.active=true;
				setX(curr);
				setY(max);
			}else if(curr==max){
				finalWaveFINAL.active=true;
				finalWaveWAVE.active=true;
			}
		}else{
			wave.active=true;
			X1.active=true;
			setX(curr);
		}
		StartCoroutine(drawWave());
	}
	
	void deactiveateAll(){
		wave.active=false;
		X1.active=false;
		X10.active=false;
		X100.active=false;
		of.active=false;
		Y1.active=false;
		Y10.active=false;
		Y100.active=false;
		finalWaveFINAL.active=false;
		finalWaveWAVE.active=false;
	}
	
	void setX(int x){
		if(x<10){
			rendX1.sprite=Numbers[int.Parse(x.ToString().Substring(0, 1))];
		}else if(x>=10 && x<100){
			rendX1.sprite=Numbers[int.Parse(x.ToString().Substring(0, 1))];
			rendX10.sprite=Numbers[int.Parse(x.ToString().Substring(1, 1))];
			X10.active=true;
		}else if(x>=100 && x<999){
			rendX1.sprite=Numbers[int.Parse(x.ToString().Substring(0, 1))];
			rendX10.sprite=Numbers[int.Parse(x.ToString().Substring(1, 1))];
			rendX100.sprite=Numbers[int.Parse(x.ToString().Substring(2, 1))];
			X10.active=true;
			X100.active=true;
		}
	}
	
	void setY(int y){
		if(y<10){
			rendY1.sprite=Numbers[int.Parse(y.ToString().Substring(0, 1))];
		}else if(y>=10 && y<100){
			rendY1.sprite=Numbers[int.Parse(y.ToString().Substring(0, 1))];
			rendY10.sprite=Numbers[int.Parse(y.ToString().Substring(1, 1))];
		}else if(y>=100){
			rendY1.sprite=Numbers[int.Parse(y.ToString().Substring(0, 1))];
			rendY10.sprite=Numbers[int.Parse(y.ToString().Substring(1, 1))];
			rendY100.sprite=Numbers[int.Parse(y.ToString().Substring(2, 1))];
		}
	}

	public void drawFinalWave(){
		if(finalWaveSemaphore){
			StartCoroutine(drawWave());
		}
		finalWaveSemaphore=false;
	}
	
	IEnumerator drawWave(){
		if(_GM.GetComponent<Setup>().getFailureCondition())
		{
			yield return new WaitForSeconds(0.5f);
			
			for (float i = 90; i >= 0; i--) {	//Fade out
				colour = new Color(1, 1, 1, i/255*3);
				rendFailureText.color = colour;
				yield return new WaitForSeconds(0.0f);
			}
			youReached.active=true;
			wave.active=true;
			if(currWave<10){X1.active=true;}
			if(currWave>=10){X10.active=true;}
			if(currWave>=100){X100.active=true;}
		}

		for (float i = 0; i < 90; i++) {	//Fade in
			colour = new Color(1, 1, 1, i/255*3);
			rendWave.color = colour;
			rendX1.color = colour;
			rendX10.color = colour;
			rendX100.color = colour;
			rendOf.color = colour;
			rendY1.color = colour;
			rendY10.color = colour;
			rendY100.color = colour;
			rendFWF.color = colour;
			rendFWW.color = colour;
			rendReached.color = colour;
			yield return new WaitForSeconds(0.0f);
		}
		
		if(!_GM.GetComponent<Setup>().getFailureCondition())
		{
			yield return new WaitForSeconds(0.5f);
			
			for (float i = 90; i >= 0; i--) {	//Fade out
				colour = new Color(1, 1, 1, i/255*3);
				rendWave.color = colour;
				rendX1.color = colour;
				rendX10.color = colour;
				rendX100.color = colour;
				rendOf.color = colour;
				rendY1.color = colour;
				rendY10.color = colour;
				rendY100.color = colour;
				rendFWF.color = colour;
				rendFWW.color = colour;
				rendReached.color = colour;
				yield return new WaitForSeconds(0.0f);
			}
			
			deactiveateAll();
		}
	}
}
