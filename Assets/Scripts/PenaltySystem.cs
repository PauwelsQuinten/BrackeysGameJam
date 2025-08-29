using System;
using System.Collections;
using UnityEngine;

public class PenaltySystem : MonoBehaviour
{
    [SerializeField]
    private float _gracePeriod;
    [SerializeField]
    private GameEvent _penaltiesIncreased;
    [SerializeField]
    private GameEvent _gameOver;

    private bool _canTakePenalty = true;
    private int _penalties = 0;
    public void TakePenalty(Component sender, object obj)
    {
        if (!_canTakePenalty) return;
        _canTakePenalty = false;
        _penalties++;
        _penaltiesIncreased.Raise(this, _penalties);
        StartCoroutine(ResetCanTakePenalty());
        if(_penalties >= 3) _gameOver.Raise(this, EventArgs.Empty);
    }

    public void ResetPenalties(Component sender, object obj)
    {
        _penalties = 0;
        _penaltiesIncreased.Raise(this, _penalties);
        _canTakePenalty = true;
    }

    private IEnumerator ResetCanTakePenalty()
    {
        yield return new WaitForSeconds(_gracePeriod);
        _canTakePenalty = true;
    }
}
