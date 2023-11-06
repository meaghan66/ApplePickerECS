using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BasketAuthoring : MonoBehaviour
{
    public GameObject basketPrefab;
    public float moveSpeed;
    public float leftEdge;
    public float rightEdge;
    public int numBaskets = 3;
    public float spacing = 2f;


    [ExecuteInEditMode]
    private void OnValidate()
    {
        // Ensure numBaskets is not negative
        numBaskets = Mathf.Max(0, numBaskets);
    }

    private class BasketBaker : Baker<BasketAuthoring>
    {
        public override void Bake(BasketAuthoring authoring)
        {

            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new BasketProperties
            {
                Prefab = GetEntity(authoring.basketPrefab, TransformUsageFlags.Dynamic),
                MoveSpeed = authoring.moveSpeed,
                LeftEdge = authoring.leftEdge,
                RightEdge = authoring.rightEdge,
                NumBaskets = authoring.numBaskets,
                Spacing = authoring.spacing
            };

            AddComponent(entity, propertiesComponent);

            var mouseMoveComponent = new MoveWithMouse
            {
                move = true
            };

            AddComponent(entity, mouseMoveComponent);
            
        }
    }
}

public struct BasketProperties : IComponentData
{
    public float MoveSpeed;
    public float LeftEdge;
    public float RightEdge;
    public float3 Position;
    public int NumBaskets;
    public float Spacing;
    public Entity Prefab;
}

public struct MoveWithMouse : IComponentData
{
    public bool move;
}
