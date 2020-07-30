using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that contains data about current wave and array of all type of enemies.
/// </summary>
public class WavesManager : MonoBehaviour
{
	public static WavesManager instance;

	private WavesManager()
	{
		instance = this;
	}

	[HideInInspector]
	public int CountOfWave = 0;
	public int CountOfHeart = 3;
	public void BreakHeart()
	{
		CountOfHeart--;
		GUIManager.instance.BreakHeart(CountOfHeart);

		Debug.Log("Life:" + CountOfHeart);

		if(CountOfHeart < 1)
		{
			Debug.Log("GameOver");
			GUIManager.instance.GameOver();
			Invoke("ReturnToMainMenu", 1f);
			Time.timeScale = 0.25f;
			

		}
	}

	[SerializeField]
	private GameObject[] Enemies;
	[SerializeField]
	private GameObject[] Bosses;
	[SerializeField]
	private Transform WavePoint;

	private bool IswaveReady = true;

	private void Start()
	{
		Time.timeScale = 1;	
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && IswaveReady) StartNewWave();
		if (IswaveReady) GUIManager.instance.ReadyNewWave();
	}

	private void StartNewWave()
	{
		IswaveReady = false;
		CountOfWave++;
		GUIManager.instance.AnnounceNewWave(CountOfWave);
		PlayerManager.instance.HighestWave(CountOfWave);

		int EnemyCount = CountOfWave + 5;

		float timeToBossComing = 0;

		for (int i = 0; i < EnemyCount; i++)
		{
			var count = Random.Range(2.5f, 7.5f);

			timeToBossComing += count / 2 + CountOfWave * 0.25f;

			if (i == EnemyCount - 1) Invoke("SpawnBoss", timeToBossComing);
			else Invoke("SpawnEnemy", count);
		}

	}

	private void SpawnEnemy()
	{
		var enemy = Instantiate(Enemies[GetRandomEnemy()], WavePoint.position, Quaternion.identity);
	}

	private void SpawnBoss()
	{
		GUIManager.instance.NewWarningMessage("Boss incoming!");
		Instantiate(Bosses[Random.Range(0, Bosses.Length)], WavePoint.position, Quaternion.identity);
	}

	private int GetRandomEnemy()
	{
		var chance = Random.Range(0.01f,1.01f);

		int id = 0;

		if (0 < chance && chance <= 0.4) id = 0;
		else if (0.4 < chance && chance <= 0.8) id = 1;
		else if (0.8 < chance && chance <= 0.9) id = 2;
		else if (0.9 < chance && chance <= 0.95) id = 3;
		else if (0.95 < chance) id = 4;

		if (id < Enemies.Length) return id;
		else return Enemies.Length-1;

	}

	public void WaveEnded()
	{
		IswaveReady = true;
	}

	private void ReturnToMainMenu()
	{
		SaveSystem.Save();

		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}

	public Enemy Enemy
	{
		get => default;
		set
		{
		}
	}

	public Boss Boss
	{
		get => default;
		set
		{
		}
	}

	public Waypoints Waypoints
	{
		get => default;
		set
		{
		}
	}
}
