using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private int Stage = 0;
	private int[] Scores = new int[10]{0,0,0,0,0,0,0,0,0,0};//position 0 being the highest
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ScoreUpdate(int NewScore){
		for(int i = 0; i<Scores.Length; i++){
			if(NewScore > Scores[i]){//The new Score is greater than the old one
				for(int l=i; l < Scores.Length; l++){//Take the old scores
					Scores[l+1] = Scores[l];//the one after it is equal to the one before
				}
				Scores[i] = NewScore;//Whatever it was at i is now the new score
				break;//Break out of loop
			}
		}
	}


	private void setGame(){
	
	}



}
