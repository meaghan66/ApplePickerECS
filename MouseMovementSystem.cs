using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MouseMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MoveWithMouse>>())
        {
            var pos = Input.mousePosition;
            pos.z -= Camera.main.transform.position.z;
            var worldPos = Camera.main.ScreenToWorldPoint(pos);

            // Limit movement to X-axis
            worldPos.y = transform.ValueRO.Position.y;
            worldPos.z = transform.ValueRO.Position.z;

            // Update the position of the entity using the LocalTransform component.
            transform.ValueRW.Position = worldPos;
        }
    }
}
