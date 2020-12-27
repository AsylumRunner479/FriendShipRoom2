
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh;
    [SerializeField] private Material unitMaterial;
    // Start is called before the first frame update
    void Start()
    {
        MakeEntity(); 
    }
    private void MakeEntity()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype archetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );
        Entity myentity = entityManager.CreateEntity(
            archetype
            );
        entityManager.AddComponentData(myentity, new Translation { Value = new float3(2f, 0f, 4f)});
        entityManager.AddSharedComponentData(myentity, new RenderMesh { mesh = unitMesh, material = unitMaterial });
        
    }
   
}
