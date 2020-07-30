using UnityEngine;

/// <summary>
/// Alows to buy towers in empty nodes
/// </summary>
public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;

	[HideInInspector]
	public GameObject currentNode;
	[SerializeField]
	private GameObject[] Towers;
	[SerializeField]
	private Transform map;

	private BuildManager()
	{
		instance = this;
	}

	public Node Node
	{
		get => default;
		set
		{
		}
	}

	public Tower Tower
	{
		get => default;
		set
		{
		}
	}

	public void BuildTower(int id)
	{
		var Tower = Towers[id];

		if(Tower.TryGetComponent<Tower>(out var tower) && currentNode.TryGetComponent<Node>(out var node))
		{
			if (tower.MoneyCost <= PlayerManager.instance.money && node.IsAvailable)
			{
				var newTower = Instantiate(Towers[id]);
				newTower.transform.position = currentNode.transform.position;

				PlayerManager.instance.AddMoney(-tower.MoneyCost);
				PlayerManager.instance.Towers.Add(newTower);

				if (newTower.TryGetComponent<Tower>(out var newtower))
				{
					newtower.CurrentNode = node;
					newtower.CurrentNode.IsAvailable = false;
				}

				
				currentNode = null;
				GUIManager.instance.CloseTowerShop();
			}
			else
			{
				GUIManager.instance.NewWarningMessage("Not enough money!");
				return;
			}

		}

		
	}
	public void LoadTower(int id,int level, Vector3 position, string nodeName)
	{
		var newTower = Instantiate(Towers[id], position,Quaternion.identity);
		PlayerManager.instance.Towers.Add(newTower);

		if (newTower.TryGetComponent<Tower>(out var newtower))
		{
			if (map.transform.Find(nodeName).TryGetComponent<Node>(out var node))
			{
				newtower.level = level;
				newtower.LoadUpgrade();
				newtower.CurrentNode = node;
				newtower.CurrentNode.IsAvailable = false;
			}

		}

		currentNode = null;
	}
	
}
