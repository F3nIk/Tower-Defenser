using UnityEngine;

public class IceTower : Tower
{
	//Class for addictional functional
	private void Start()
	{
		debuff = new Ice();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	public override void Upgrade()
	{
		base.Upgrade();
		debuff = new Ice(level);
	}
}
	

