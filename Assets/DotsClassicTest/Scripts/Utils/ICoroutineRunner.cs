using System.Collections;
using UnityEngine;

namespace DotsClassicTest.Utils
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator iEnumerator);
        public void StopCoroutine(Coroutine coroutine);
    }
}