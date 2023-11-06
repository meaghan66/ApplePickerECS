using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

class AppleAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
}

class AppleBaker : Baker<AppleAuthoring>
{
    public override void Bake(AppleAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        var spawnerPosition = authoring.transform.position;
        AddComponent(entity, new Apple
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            SpawnPosition = spawnerPosition,
            NextSpawnTime = 0.0f,
            SpawnRate = authoring.SpawnRate
        });
    }

}