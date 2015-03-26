using UnityEngine;
using System.Collections;

public class GUI_picnic : MonoBehaviour {
	
	public GameObject picnic;
	public string healthString;
	public float healthInt;
	
	// Update is called once per frame
	void Update () {
		healthInt = picnic.GetComponent<picnic_health_script>().getPercentage();
		if(healthInt>0){
			healthString = "Picnic Integrity: "+healthInt.ToString()+"%";
		}else{
			healthString = "";
		}
		guiText.text = healthString;
	}
}
