using UnityEngine;

namespace DotsClassicTest.Spawner
{
    public class ResourceSpawner:ISpawner
    {
        public T Spawn<T>(string path, Vector3 position = default, Quaternion rotation = default,
            Transform parent = null)
            where T : Component
        {
            var prefab = Resources.Load<T>(path);
            var component = GameObject.Instantiate(prefab, position, rotation, parent);
            return component;
        }
    }
}