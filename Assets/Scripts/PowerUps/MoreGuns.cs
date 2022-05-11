using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreGuns : PowerUps
{
    public override void effect()
    {
        base.effect();
        player.guns++;
    }
}
