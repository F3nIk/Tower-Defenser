using UnityEngine;

public class FireTower : Tower
{
	private void Start()
	{
		debuff = new Fire();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	public override void Upgrade()
	{
		base.Upgrade();
		debuff = new Fire(level);
	}
}
