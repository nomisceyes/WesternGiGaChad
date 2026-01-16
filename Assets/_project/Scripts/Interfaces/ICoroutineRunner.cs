using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    public Coroutine StartRoutine(IEnumerator coroutine);       
}