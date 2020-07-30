using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public int Money;
	public int Wave;
	public int Hearts;

	public long totalEnemiesKilled;
	public long totalBossesKilled;
	public long totalGoldEarned;
	public int highestWave;

	public int[] id;
	public int[] level;
	public float[,] pos;
	
	public string[] nodeName;

	public PlayerData()
	{
		totalEnemiesKilled = PlayerManager.instance.totalEnemiesKilled;
		totalBossesKilled = PlayerManager.instance.totalBossesKilled;
		totalGoldEarned = PlayerManager.instance.totalGoldEarned;
		highestWave = PlayerManager.instance.highestWave;

		if (WavesManager.instance.CountOfHeart <= 0)
		{
			Money = 250;
			Wave = 0;
			Hearts = 3;
		}
		else
		{
			Money = PlayerManager.instance.money;
			Wave = WavesManager.instance.CountOfWave;
			Hearts = WavesManager.instance.CountOfHeart;



			int count = PlayerManager.instance.Towers.Count;

			pos = new float[count, 3];
			level = new int[count];
			id = new int[count];
			nodeName = new string[count];

			for (int i = 0; i < PlayerManager.instance.Towers.Count; i++)
			{
				if (PlayerManager.instance.Towers[i].TryGetComponent<Tower>(out var tower))
				{
					pos[i, 0] = tower.transform.position.x;
					pos[i, 1] = tower.transform.position.y;
					pos[i, 2] = tower.transform.position.z;

					level[i] = tower.level;
					id[i] = tower.ID;

					nodeName[i] = tower.CurrentNode.gameObject.name;
				}
			}
		}
	}
}

