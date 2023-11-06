using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using System.Diagnostics;
using Unity.Collections;
using UnityEngine;
using Unity.Mathematics;

[BurstCompile]
public partial struct AppleSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, apple) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Apple>>())
        {
            apple.ValueRW.SpawnPosition = transform.ValueRO.Position;
            ProcessApple(ref state, apple);
        }
    }

    private void ProcessApple(ref SystemState state, RefRW<Apple> apple)
    {
        if (apple.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity newEntity = state.EntityManager.Instantiate(apple.ValueRO.Prefab);
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(apple.ValueRW.SpawnPosition));

            apple.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + apple.ValueRO.SpawnRate;

        }

    }

}