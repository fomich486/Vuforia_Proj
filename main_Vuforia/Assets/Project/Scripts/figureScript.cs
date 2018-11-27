using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ListEvent : UnityEvent<GameObject>{}

public class figureScript : MonoBehaviour {

	public ListEvent spawner_listControl;
	Renderer rend;
	float val_rotSpeed = 90f;
	Vector3 rotateAxis;
	[SerializeField]
	ParticleSystem ps_DieEffect;
	// Use this for initialization
	void Start () {
		RandomRotationDir ();
		RandomStartColor ();
	}

	void RandomRotationDir()
	{
		rotateAxis = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		rotateAxis = rotateAxis.normalized;
	}
	void RandomStartColor()
	{
		Color col = new Color (Random.Range(0.2f,1f),Random.Range(0.2f,1f),Random.Range(0.2f,1f));
		rend = GetComponent<Renderer> ();
		rend.material.color = col; 
		ParticleSystem.MainModule psMain = ps_DieEffect.main;
		psMain.startColor = col;
		ps_DieEffect.gameObject.transform.localScale = transform.localScale;
	}
	void Update () {
		transform.Rotate (rotateAxis*val_rotSpeed*Time.deltaTime);
	}

	public void Die()
	{
		if (spawner_listControl != null) {
			spawner_listControl.Invoke(gameObject);
		}
		Destroy (Instantiate (ps_DieEffect.gameObject, transform.position, Quaternion.identity), ps_DieEffect.main.duration);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Sun") {
			Die ();
		}
	}
		
}
