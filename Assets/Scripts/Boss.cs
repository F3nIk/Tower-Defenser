using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
	protected override void Death()
	{
		base.Death();
		PlayerManager.instance.BossKilled();
		WavesManager.instance.WaveEnded();
	}
}
