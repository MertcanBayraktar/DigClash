using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	bool collidedWithPlayer = false;
	public GameObject explosionParticle;
	void Update()
	{
		if (GameMaster.instance._gameState == GameMaster.gameState.play)
		{
			if (collidedWithPlayer && Player.instance.friendlyList.Count >= 1)
			{
				this.transform.position = Vector3.MoveTowards(this.transform.position, Player.instance.friendlyList[0].transform.position, 5f * Time.deltaTime);
				if (transform.position == Player.instance.friendlyList[0].transform.position)
				{
					Player.instance.runToEnemy = false;
					GameObject go = Instantiate(explosionParticle, this.transform.position, this.transform.rotation);
					Destroy(go, 1f);
					Destroy(this.gameObject);
					Player.instance.enemyList.Remove(this.gameObject);
					Destroy(Player.instance.friendlyList[0]);
					Player.instance.friendlyList.Remove(Player.instance.friendlyList[0]);

				}
			}
			else if (collidedWithPlayer && Player.instance.friendlyList.Count == 0)
				Debug.Log("Game Over");
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			Player.instance.runToEnemy = true;
			collidedWithPlayer = true;
			if(!Player.instance.enemyList.Contains(this.gameObject))
				Player.instance.enemyList.Add(this.gameObject);
		}		
	}
	private void OnDestroy()
	{
		Player.instance.enemyList.Remove(this.gameObject);	
	}
}
