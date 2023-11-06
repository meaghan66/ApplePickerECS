using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using System.Diagnostics;
using Unity.Collections;
using UnityEngine;
using Unity.Mathematics;

[BurstCompile]

public partial struct BasketSpawn : ISystem
{
    void Start(ref SystemState state)
    {
        foreach (var (transform, basket) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketProperties>>())
        {
            for(int i = 0; i < basket.ValueRO.NumBaskets; i++)
            {
                float3 pos = basket.ValueRW.Position * basket.ValueRO.Spacing;
                Entity newEntity = state.EntityManager.Instantiate(basket.ValueRO.Prefab);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
            }
        }

    }


}
