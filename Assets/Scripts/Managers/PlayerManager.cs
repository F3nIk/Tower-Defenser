using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains all player data and methods.
/// </summary>
public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance;

	private PlayerManager()
	{
		instance = this;
	}

	[HideInInspector]
	public int money { private set; get; } = 250;
	[HideInInspector]
	public List<GameObject> Towers;

	public long totalEnemiesKilled { get; private set; } = 0;
	public long totalBossesKilled { get; private set; } = 0;
	public long totalGoldEarned { get; private set; } = 0;
	public int highestWave { get; private set; } = 0;

	private void Awake()
	{
		GUIManager.instance.MoneyLabelUpdate(money);
	}

	private void Start()
	{
		if (SaveSystem.IsLoading)
			SaveSystem.Load();
	}

	public void AddMoney(int money)
	{
		this.money += money;
		if (money > 0) GoldEarned(money);
		GUIManager.instance.MoneyLabelUpdate(this.money);
	}

	public void EnemyKilled()
	{
		totalEnemiesKilled++;
	}

	public void BossKilled()
	{
		totalBossesKilled++;
	}

	public void GoldEarned(int value)
	{
		totalGoldEarned += value;
	}

	public void HighestWave(int wave)
	{
		highestWave = wave - 1;
	}

	public void GetStatistics(out long enemies, out long bosses, out long gold, out int wave)
	{
		enemies = totalEnemiesKilled;
		bosses = totalBossesKilled;
		gold = totalGoldEarned;
		wave = highestWave;
	}

	private void OnApplicationQuit()
	{
		SaveSystem.Save();
		Application.Quit();
	}
	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			SaveSystem.Save();
		}
	}

	public void LoadData(PlayerData data)
	{
		money = data.Money;
		WavesManager.instance.CountOfWave = data.Wave;
		WavesManager.instance.CountOfHeart = data.Hearts;

		totalEnemiesKilled = data.totalEnemiesKilled;
		totalBossesKilled = data.totalBossesKilled;
		totalGoldEarned = data.totalGoldEarned;
		highestWave = data.highestWave;

		for (int i = 0; i < data.level.Length; i++)
		{
			int id = data.id[i];
			int level = data.level[i];
			Vector3 pos = new Vector3(data.pos[i, 0], data.pos[i, 1], data.pos[i, 2]);
			string nodeName = data.nodeName[i];

			BuildManager.instance.LoadTower(id, level, pos, nodeName);
		}

		GUIManager.instance.MoneyLabelUpdate(money);
	}
}
