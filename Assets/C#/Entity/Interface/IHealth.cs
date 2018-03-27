using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth{
    void SetHealth(int health);
    void AddHealth(int add);
    int GetHealth();
}
