using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface ITurrent
{

    Transform Target { get; set; }
    int Damage { get; set; }
    float Range { get; set; }
    float FireRate { get; set; }
    float FireCountdown { get; set; }
    List<GameObject> EnemiesInRange { get; set; }
    int BuyValue { get; set; }
    int SellValue { get; set; }

    void SetTarget(Transform target);
}

