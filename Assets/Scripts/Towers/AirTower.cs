using UnityEngine;

public class AirTower : Tower
{
	private void Start()
	{
		debuff = null;
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	public override void Upgrade()
	{
		base.Upgrade();
		debuff = null;
	}
}
