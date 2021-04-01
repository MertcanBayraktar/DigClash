using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
	#region Singleton
	public static Player instance;
	private void Awake()
	{
		if (instance != null)
			Debug.Log("Birden fazla player instance");
		instance = this;
	}
	#endregion
	public List<GameObject> friendlyList;
	public List<GameObject> enemyList;
	public bool runToEnemy = false;
	public float smoothTime = 0.4f;
	public Vector3 cameraOffSet;
	private Camera _cameraToFollow;
	private Vector3 _refVector;
	void Start()
	{
		_cameraToFollow = Camera.main;
	}
	void FixedUpdate()
	{
		_cameraToFollow.transform.position = Vector3.SmoothDamp(_cameraToFollow.transform.position, transform.position + cameraOffSet, ref _refVector, smoothTime);
	}
	void Update()
	{
		if (GameMaster.instance._gameState == GameMaster.gameState.play)
		{
			if (runToEnemy)
			{
				friendlyList[0].transform.position = Vector3.MoveTowards(friendlyList[0].transform.position, enemyList[0].transform.position, 5f * Time.deltaTime);
			}
		}
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "FinishLine")
		{
			GameMaster.instance._gameState = GameMaster.gameState.end;
			this.GetComponent<NavMeshAgent>().speed = 0;
		}
	}
}
