using UnityEngine;
using System.Collections;

public class TouchDispatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		return;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) {
			GameObject touchObject = GetTouchObject(touch);

			if (touchObject != null) {
				touchObject.SendMessage ("OnTouchEvent", touch, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

 	GameObject GetTouchObject (Touch touch) {
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
		RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
	
		if (hit.collider != null) {
			return hit.collider.gameObject;
		}
		else {
			return null;
		}
	}

}
