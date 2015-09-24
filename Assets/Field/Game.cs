using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	int Score = 0;
	int Combo = 0;
	public Player Hunk;
	public BuildingScript BuildingTest;
	BuildingScript BaconSauce;

	// Use this for initialization
	void Start () {
		BaconSauce = (BuildingScript)Instantiate(BuildingTest, new Vector3(0f,55f,0f),Quaternion.identity);
		BaconSauce.SetPlayer(Hunk);
		Hunk.SetBuilding (BaconSauce);
	}
	
	// Update is called once per frame
	void Update () {
		if (BaconSauce == null) {
			BaconSauce = (BuildingScript)Instantiate(BuildingTest, new Vector3(0f,55f,0f),Quaternion.identity);
			BaconSauce.SetPlayer(Hunk);
			Hunk.SetBuilding (BaconSauce);
		}
	}

	bool CheckPosition(){
		return true;
	}

	public Vector3 MoveMe(int Direction){
		if(Direction == -1){
			return new Vector3(-5,Hunk.GetPositionY());
		}else if(Direction == 0){
			return new Vector3(-0,Hunk.GetPositionY());
		}else if(Direction == 1){
			return new Vector3(5,Hunk.GetPositionY());
		}else{
			return new Vector3(0,0);
		}
	}

}
