using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	public bool isFollow = true;
	public Vector3 initialPosition;
	private  float animationTime = 1.4f;
	public GoToGameOverScene restarCtrl;

	void Start ()
	{
		//initialPosition = transform.position;
		offset = transform.position - player.transform.position;
	}

	void LateUpdate ()
	{
		if (isFollow) {
			transform.position = new Vector3(transform.position.x,transform.position.y,player.transform.position.z + offset.z);
		}
	}

	public Vector3 GetInitialPosition(){

		return initialPosition;
	}

	public void Dezoom(){

		StartCoroutine (DezoomAnimation ());
	}

	IEnumerator DezoomAnimation(){
		isFollow = false;
		float count = 0f;
		Vector3 startPosition = transform.position;
		Vector3 endPosition = new Vector3(transform.position.x,transform.position.y + 4f ,transform.position.z - 15f);
		while(count < animationTime){
			count += Time.deltaTime / animationTime;
			transform.position = Vector3.Lerp (startPosition, endPosition, count);
			yield return new WaitForFixedUpdate();
		}
		isFollow = true;
		transform.position = initialPosition;
		restarCtrl.restartGame ();

	}
}