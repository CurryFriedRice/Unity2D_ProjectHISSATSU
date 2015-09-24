using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour {

	public float Ground = -3;
	protected Player PlayerUnit;
	public BuildingLayer[] Building = new BuildingLayer[5];
	//public BuildingLayer[] BuildingLayers;
	//Use this for initialization

	void Start () {
		StartCoroutine (Falling ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPlayer(Player NewUnit){
		PlayerUnit = NewUnit;
		for (int i = 0; i<Building.Length; i++) {
			if(Building[i]!=null){
				Building[i].SetPlayer(NewUnit);
			}
		}
	}

	IEnumerator Falling(){
		float t = 1;
		while(PlayerUnit.GetHP() >0){
			float playerPos = PlayerUnit.GetPositionY ();
			float here = transform.position.y;
			float there =  transform.position.y + 10; 
			if(transform.position.y <= playerPos+5 && PlayerUnit.GetGuard() == true){
				t = 5;
				while (t>1){
					t -= 0.1F;
					transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x,there,0),0.01F*t);
					yield return new WaitForSeconds(0.0F);
				}
			}else if (transform.position.y > Ground) {
				transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x,-5,0),0.01F*t);
				t += 0.01F;
				//print (t);
				if(t>5){
					t=5F;
				}
			}else if(transform.position.y < Ground ){
				//print("Yeowch");
				transform.position = new Vector3(0,Ground,0);
				PlayerUnit.ReduceHP();
			}


			//
			if(Building[0] == null){
				PlayerUnit.NotTouching();
				if(Building[1] != null){
				for(int i = 1;i<Building.Length; i++){
					if(Building[i] == null){
					
					}
					Building[i].transform.position = new Vector3(Building[i].transform.position.x,
					                                             Building[i].transform.position.y-4,
					                                             Building[i].transform.position.z);
					Building[i-1] = Building[i];
					

					
					if(Building[i+1] == null){
						Building[i] = null;
						break;
					}
					//transform = Building[i].transform;
					
				}
				transform.position = new Vector3(transform.position.x,transform.position.y+4,transform.position.z);
				}
				if(Building[0] == null){
					print("building Destroyed");
					Destroy (this.gameObject);
				}	
			}
			//

		yield return new WaitForSeconds(0.0F);
		}
	}


	public BuildingLayer GetLayerZero(){
	
		return Building[0];
	}
}
