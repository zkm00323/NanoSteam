using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanMove : IMove {
    void StartRun();
    void FinishRun();
}
