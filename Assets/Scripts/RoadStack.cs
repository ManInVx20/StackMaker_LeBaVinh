using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadStack : Stack
{
    public override void Setup(Player player)
    {
        gameObject.SetActive(true);

        base.Setup(player);
    }

    public override void Activate(Player player)
    {
        gameObject.SetActive(false);

        base.Activate(player);
    }
}
