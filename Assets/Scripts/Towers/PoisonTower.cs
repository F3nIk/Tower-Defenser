using UnityEngine;

public class PoisonTower : Tower
{
	private void Start()
	{
		debuff = new Poison();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	public override void Upgrade()
	{
		base.Upgrade();
		debuff = new Poison(level);
	}
}
