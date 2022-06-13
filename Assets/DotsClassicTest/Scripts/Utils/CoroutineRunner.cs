using System.Collections;
using UnityEngine;

namespace DotsClassicTest.Utils
{
    public class CoroutineRunner:MonoBehaviour,ICoroutineRunner
    {
        Coroutine ICoroutineRunner.StartCoroutine(IEnumerator iEnumerator)
        {
            return StartCoroutine(iEnumerator);
        }

        void ICoroutineRunner.StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }
}