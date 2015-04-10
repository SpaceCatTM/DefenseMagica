using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (Resources.Load ("Animations/Stage1_Player_10"));

		GetComponent<Animator> ().runtimeAnimatorController = Resources.Load ("Animations/Stage1_Player_10") as RuntimeAnimatorController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
