using System.Collections;
using UnityEngine;

namespace DotsClassicTest.Utils
{
    public static class CoroutineUtils
    {
        public static IEnumerator MoveItem(Transform transform, Vector3 startPos, Vector3 endPos, float duration)
        {
            var time = 0f;
            while (time < 1)
            {
                time += Time.deltaTime/duration;
                transform.position = Vector3.Lerp(startPos, endPos, time);
                yield return null;
            }

            transform.position = endPos;
        }
    }
}