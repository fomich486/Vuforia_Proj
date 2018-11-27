using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGame_Spawner : MonoBehaviour {
	//calculation info
	Initial init;
	//use to set direction;
	Vector3 startDirection;
	//use for speed calculations
	float distance;
	float nextSpawnTime;
	[SerializeField]
	[Range(0.5f,2f)]
	float timeBetweenObjSpawn;
	[SerializeField]
	[Range(0f,40f)]
	float speed;
	[SerializeField]
	[Range(0.05f,0.25f)]
	float persent;

	[SerializeField]
	Transform [] figures;
	List<GameObject> list_SpawnedObj;
	void OnEnable()
	{
		list_SpawnedObj = new List<GameObject> ();
		init = GetComponent<Initial> ();
		nextSpawnTime = Time.time;
	}
	void StartDirectionCalculation()
	{
		startDirection = init.tf_SunTarg.position - init.tf_SomethingTarg.position;
		distance =  startDirection.magnitude;
		startDirection =  startDirection.normalized;
	}
		
	void Update()
	{
		ObjCorrect();
		StartDirectionCalculation ();
		if (Time.time > nextSpawnTime) {
			spawnObj ();
			nextSpawnTime = Time.time + timeBetweenObjSpawn;
		}
		//Increase speed checker (LIST require)
	}

	void ObjCorrect()
	{
		foreach (GameObject gam in list_SpawnedObj) {
			//Velocity Correct
			Vector3 newDirection = (init.tf_SunTarg.position - gam.transform.position).normalized;
			gam.GetComponent<Rigidbody> ().velocity =  newDirection * speed;
			//SizeCorrect
			Vector3 newSize = Vector3.one * persent * distance;
			gam.transform.localScale = newSize;
		}
	}

	void ListControl(GameObject gam)
	{
		list_SpawnedObj.Remove(gam);
	}

	public void ClearObjectsList()
	{
		foreach (GameObject gam in list_SpawnedObj) {
			gam.GetComponent<figureScript> ().Die ();
		}
		list_SpawnedObj.Clear ();
	}

	void spawnObj (){
		int rnd = Random.Range (0, figures.Length);
		Vector3 pos_start = init.tf_SomethingTarg.position;
		GameObject gam = Instantiate (figures [rnd].gameObject, pos_start, Quaternion.identity) as GameObject;
		gam.GetComponent<figureScript> ().spawner_listControl.AddListener (ListControl); 
		gam.GetComponent<Rigidbody> ().velocity = startDirection * speed;
		Vector3 newSize = Vector3.one * persent * distance;
		gam.transform.localScale = newSize;
		list_SpawnedObj.Add (gam);
	}
}
