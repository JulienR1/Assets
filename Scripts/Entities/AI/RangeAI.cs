using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAI : EntityAI
{
    public override bool FindPath(Entity target)
    {
        return false;
    }

    public override bool FollowPath()
    {
        return false;
    }

    public override void Attack(Entity target)
    {

    }

    public override void Flee(Entity target)
    {

    }

    public override void Dodge(Entity target)
    {

    }
}
