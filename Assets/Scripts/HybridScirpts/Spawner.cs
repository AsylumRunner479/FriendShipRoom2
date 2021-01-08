
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;


public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh;
    [SerializeField] private Material unitMaterial;
    [SerializeField] private GameObject[] gameObjectPrefab;
    [SerializeField] int xSize = 10;
    [SerializeField] int ySize = 10;
    private int number;
    private int height;
    private bool building;
    [Range(0.1f, 20f)]
    [SerializeField] float spacing = 1f;
    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager entityManager;
    public int buildingHeight; 
    // Start is called before the first frame update
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        
            entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab[0], settings);
        
        
        InstantiateEntityGrid(xSize, ySize, spacing);
    }
    public void Randomnumber()
    {
        if (building == false)
        {
            float tempnumber = UnityEngine.Random.Range(0, gameObjectPrefab.Length - 1);
            number = Mathf.FloorToInt(tempnumber);
        }
        else
        {
            float tempnumber = UnityEngine.Random.Range(0, (gameObjectPrefab.Length - 1) * 2);
            if (tempnumber >= gameObjectPrefab.Length - 1)
            {
                tempnumber = gameObjectPrefab.Length - 1;
                building = false;
            }
            number = Mathf.FloorToInt(tempnumber);
        }
        
    }
    private void InstantiateEntity(float3 position)
    {
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab[number], settings);
        Entity myEntity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(myEntity, new Translation
        {
            Value = position
        });
            

    }
    private void InstantiateEntityGrid(int dimX, int dimY,  float spacing = 1f)
    {
        for (int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                
                Randomnumber();
                

                InstantiateEntity(new float3(i * spacing, 0, j * spacing));
                while (number <= buildingHeight)
                {
                    Randomnumber();
                    building = true;
                    InstantiateEntity(new float3(i * spacing, height * 5f, j * spacing));
                    height += 1;
                }
                building = false;
                height = 0;
            }
        }
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
        //typeof
        Entity myentity = entityManager.CreateEntity(
            archetype
            );
        entityManager.AddComponentData(myentity, new Translation { Value = new float3(2f, 0f, 4f)});
        entityManager.AddSharedComponentData(myentity, new RenderMesh { mesh = unitMesh, material = unitMaterial });
        
    }
   
}
