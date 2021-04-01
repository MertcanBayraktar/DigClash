using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameMaster : MonoBehaviour
{
	#region Singleton
	public static GameMaster instance;
	private void Awake()
	{
		if (instance != null)
			Debug.Log("birden fazla gamemaster instance");
		instance = this;
	}
	#endregion
	public enum gameState { start, play, end }
	public gameState _gameState;
	public TMP_Text countdownTimer;
	public GameObject moveEndPosition;
	bool countdownHasStarted = false;
	public GameObject player;
	public GameObject cameraEndPosition;
	public GameObject levelEndPanel;
	void Start()
	{
		_gameState = gameState.start;
	}
	void Update()
	{
		switch (_gameState)
		{
			case gameState.start:
				if (Input.GetMouseButtonDown(0))
				{
					if (!countdownHasStarted)
						StartCoroutine(Countdown(3));
				}
				break;
			case gameState.play:
				break;
			case gameState.end:
				Camera.main.transform.DOMove(cameraEndPosition.transform.position, 3f);
				Camera.main.transform.DORotate(new Vector3(0, 0, 0), 3f, RotateMode.Fast);
				if (Player.instance.friendlyList.Count >= 1)
				{
					for (int i = 0; i < Player.instance.friendlyList.Count; i++)
					{
						Player.instance.friendlyList[i].transform.position = Vector3.MoveTowards(Player.instance.friendlyList[i].transform.position,
							moveEndPosition.transform.position, 4f * Time.deltaTime);
						if (Player.instance.friendlyList[i].transform.position == moveEndPosition.transform.position)
						{
							if (Player.instance.friendlyList.Contains(Player.instance.friendlyList[i]))
							{
								Destroy(Player.instance.friendlyList[i]);
								Player.instance.friendlyList.Remove(Player.instance.friendlyList[i]);
							}
						}
					}
				}
				if (Player.instance.friendlyList.Count == 0)
					player.transform.position = Vector3.MoveTowards(player.transform.position, moveEndPosition.transform.position, 2.5f * Time.deltaTime);

				if (Vector3.Distance(player.transform.position, moveEndPosition.transform.position) < 2f)
					levelEndPanel.SetActive(true);


				break;
		}
	}
	IEnumerator Countdown(int seconds)
	{
		countdownHasStarted = true;
		for (int i = seconds; i > 0; i--)
		{
			countdownTimer.text = i.ToString();
			yield return new WaitForSeconds(1f);
		}
		_gameState = gameState.play;
		countdownTimer.enabled = false;
	}
	public void RestartLevel()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}
