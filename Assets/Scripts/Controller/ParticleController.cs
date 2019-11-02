using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	public bool isFollow = true;
	public Vector3 initialPosition;



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


}
