using UnityEngine;

namespace DotsClassicTest.Spawner
{
    public interface ISpawner
    {
        public T Spawn<T>(string path, Vector3 position = default, Quaternion rotation = default,
            Transform parent = null)
            where T : Component;
    }
}