using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Animator Anim;
	int Health = 1000;						//Health hits 0 = lose
	int PlayerPosition = 0;
	int[] Hissatsu = new int[2]{100,0};	//Max and starting amount
	bool Grounded = true;
	bool Guard = false;
	bool Slashing = false;
	bool TouchingBuilding = false;
	public Game StageNumber;
	private BuildingScript TheBuilding;


	// Use this for initialization
	void Start () {
		Anim = GetComponent<Animator> ();
		StartCoroutine(Controls ());

	}
	
	// Update is called once per frame
	void Update () {
			
	}


	IEnumerator Controls(){
		while(Health>0){
//		if (Input.GetButton ("Left")) {
//			Debug.Log("Pushed Left");
//		} else if (Input.GetButton ("Right")) {
//			Debug.Log("Pushed Right");
//		} else if (Input.GetButton ("Jump")) {
//			Debug.Log("Pushed Jump");
//		} else if (Input.GetButton ("Guard")) {
//			Debug.Log("Pushed Guard");
//		} else if (Input.GetButton ("Special")) {
//			Debug.Log("Pushed Special");
//		} else if (Input.GetButton ("Attack")) {
//			Debug.Log("Pushed Attack");
//		}

		if (Input.GetAxis ("Horizontal") != 0 && Grounded==true) {
		//	StageNumber.MoveMe(Input.GetAxis("Horizontal"));
				float banana = Input.GetAxis("Horizontal")*1;
				Anim.SetInteger("Actions", 1);
				yield return new WaitForSeconds(0.25F);
				if(banana<0 && Grounded == true){//Move Left
					PlayerPosition--;
					if(PlayerPosition<-1){
						PlayerPosition= -1;
					}
					Vector3 Target = StageNumber.MoveMe(PlayerPosition);
					while(transform.position.x >= Target.x+0.5F){
						transform.position = Vector3.Lerp(transform.position,Target,0.1F);
						yield return new WaitForSeconds(0.0F);
					}
				}else if(banana>0 && Grounded == true){//Move Right
					PlayerPosition++;
					if(PlayerPosition > 1){
						PlayerPosition=1;
					}
					Vector3 Target = StageNumber.MoveMe(PlayerPosition);
					while(transform.position.x <= Target.x-0.5F){
						transform.position = Vector3.Lerp(transform.position,Target,0.15F);
						yield return new WaitForSeconds(0.0F);
					}
				Anim.SetInteger("Actions", 0);
				}
			}else if (Input.GetAxis ("Vertical") != 0) {
				//	StageNumber.MoveMe(Input.GetAxis("Horizontal"));
				float banana = Input.GetAxis("Vertical")*1;

				if(banana>0){//Jump Up
					if (Grounded == true){
					Anim.SetInteger("Actions", 4);
					yield return new WaitForSeconds(0.1F);
					Anim.SetInteger("Actions", 0);
					StartCoroutine(JumpUp());
					}
				}else if(banana<0){//Guard
					Anim.SetInteger("Actions", 3);
					Guard = true;
					print("Guard is true");
					yield return new WaitForSeconds(0F);
					Guard = false;
				
				}
			}else if(Input.GetButtonDown ("Slash")){//Attack
				//Debug.Log("I SLASHED!");
				Anim.SetInteger("Actions", 2);
				Slashing = true;
				yield return new WaitForSeconds(0.1F);
				Anim.SetInteger("Actions", 0);
				Slashing = false;
			}else if(Input.GetButtonDown ("Special")){//Hissatsu or Special
				Anim.SetInteger("Actions", -1);
				yield return new WaitForSeconds(0.1F);
				Anim.SetInteger("Actions", 0);

			}
			yield return new WaitForSeconds(0.01F);
			if(Grounded == true){
				Anim.SetInteger("Actions", 0);
			}else if(Grounded == false){
				Anim.SetInteger("Actions", 9);
			}

		}
	}

	IEnumerator JumpUp(){
		float t = 1;
		while(t<=3){//transform.position.y <= 50
			Grounded = false;
			t+=0.01f;
		//	print (t);

			if(transform.position.y >= TheBuilding.transform.position.y -4 && TouchingBuilding == false){
				TouchingBuilding = true;
			}else if(GetGuard() == true && transform.position.y >= TheBuilding.transform.position.y-5){
				print ("Tits Mcgee");
				transform.position = new Vector3(transform.position.x, TheBuilding.transform.position.y-5);
				TouchingBuilding = false;
				break;
			}else if(TouchingBuilding == true){
				transform.position = new Vector3(transform.position.x, TheBuilding.transform.position.y-4);
			}else if(transform.position.y <= TheBuilding.transform.position.y-4){
				transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x,transform.position.y +5, 0), 0.1F/t);
			}
			yield return new WaitForSeconds(0.0F);
			if(TheBuilding == null){
				NotTouching();
			}
		}
		

		while (Grounded == false) {	

			if(transform.position.y <= -4){
				Anim.SetTrigger("OnGround");
				Grounded = true;
				//break;

			}else if(transform.position.y > -4){
				if(TouchingBuilding == false){
					transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, transform.position.y - 5, 0), 0.1F/t);
					yield return new WaitForSeconds(0.0F);
					t-=0.01f;
				}else if(TouchingBuilding == true){
					transform.position = new Vector3(transform.position.x, TheBuilding.transform.position.y-4, transform.position.y);
					yield return new WaitForSeconds(0.0F);
					if(transform.position.y >= TheBuilding.transform.position.y-3 && GetGuard() == true){
						TouchingBuilding = false;
					}
				}
			}
		}
	}

	public float GetPositionX(){
		float Tits = transform.position.x;
		return Tits;
	}
	public float GetPositionY(){
		float Tits = transform.position.y;
		return Tits;
	}

	public bool GetGuard(){
		return Guard;
	}

	public int GetHP(){
		return Health;
	}

	public void ReduceHP(){
		Health--;
		if (Health == 0) {
			print("I'm Finished");
		}
	}

	public bool isSlashing(){
		return Slashing;
	}

	public void SetBuilding(BuildingScript DasBuiliding){
		TheBuilding = DasBuiliding;
	}

	public void NotTouching(){
		print ("I am not touching the building");
		TouchingBuilding = false;
		transform.position = new Vector3 (transform.position.x, transform.position.y-0.5f, transform.position.z);
	}
}
