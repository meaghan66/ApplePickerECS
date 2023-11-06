using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public partial struct AppleCollision : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var entityManager = state.EntityManager;

        foreach (var (appleTransform, apple) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Apple>>())
        {
            foreach (var (basketTransform, basket) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<BasketProperties>>())
            {
                float3 applePosition = appleTransform.ValueRO.Position;
                float3 basketPosition = basketTransform.ValueRO.Position;
                float leftEdge = basket.ValueRO.LeftEdge;
                float rightEdge = basket.ValueRO.RightEdge;

                if (IsCollision(applePosition, basketPosition, leftEdge, rightEdge))
                {
                    // Schedule the destruction of the apple entity using the EntityCommandBuffer
                    //commandBuffer.DestroyEntity(apple.ValueRO.Prefab);

                    // You can add scoring logic or other game-related logic here
                    Debug.Log("BAM!");
                }
            }
        }

    }

    private bool IsCollision(float3 applePos, float3 basketPos, float leftEdge, float rightEdge)
    {
        Debug.Log("Apple: " + applePos.x);
        Debug.Log("Basket: " + basketPos.x);
        // Check if the apple is within the basket's left and right edges
        return applePos.x >= basketPos.x + leftEdge && applePos.x <= basketPos.x + rightEdge;
    }
}
