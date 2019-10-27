using UnityEngine;

/// <summary>
/// Created by Vernon
/// 2018.05.28
/// </summary>
public interface IAircraftState {

    void Do();
    void Activate();
    void Crash();
    void Respawn();
    void EndGame();
}
