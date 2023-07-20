using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class switchsprit:MonoBehaviour{
	//switchs between charters
	// a twice = book
	// l twice = bucket
	// k twice = bomb
	public GameObject book;
	public GameObject bomb;
	public GameObject bucket;


	void Start(){
		book.GetComponent<TopDownRun>().enabled =true;
		book.GetComponent<followCharacter>().enabled=false;

		bomb.GetComponent<followCharacter>().enabled=true;
		bomb.GetComponent<TopDownRun>().enabled=false;

		bucket.GetComponent<followCharacter>().enabled=true;
		bucket.GetComponent<TopDownRun>().enabled=false;

	}

	void Update(){
		if(Input.GetKeyDown ("a") ){
			book.GetComponent<TopDownRun>().enabled =true;
			book.GetComponent<followCharacter>().enabled=false;

			bomb.GetComponent<followCharacter>().enabled=true;
			bomb.GetComponent<TopDownRun>().enabled=false;

			bucket.GetComponent<followCharacter>().enabled=true;
			bucket.GetComponent<TopDownRun>().enabled=false;
			} 
	else if(Input.GetKeyDown("k") ){
			book.GetComponent<TopDownRun>().enabled =false;
			book.GetComponent<followCharacter>().enabled=true;

			bomb.GetComponent<followCharacter>().enabled=false;
			bomb.GetComponent<TopDownRun>().enabled=true;

			bucket.GetComponent<followCharacter>().enabled=true;
			bucket.GetComponent<TopDownRun>().enabled=false;
			} 
	else if(Input.GetKeyDown("l") ){
			book.GetComponent<TopDownRun>().enabled =false;
			book.GetComponent<followCharacter>().enabled=true;

			bomb.GetComponent<followCharacter>().enabled=true;
			bomb.GetComponent<TopDownRun>().enabled=false;

			bucket.GetComponent<followCharacter>().enabled=false;
			bucket.GetComponent<TopDownRun>().enabled=true;
	}
	}
}