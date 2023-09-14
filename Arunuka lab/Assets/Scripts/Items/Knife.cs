using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knife : MonoBehaviour, IUsable
{

    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }

    public Transform cutPlane;
    public LayerMask layerMask;
    //public Material crossMaterial;
    public Vector3 CutZoneKnife;
    public float explosionForce = 100f;

    // Limit Cuts
    private Dictionary<GameObject, int> cutsPerObject = new Dictionary<GameObject, int>();
    public int maxCutsPerObject = 3; // max slice cuts per object

    private Dictionary<GameObject, GameObject> parentsPerObject = new Dictionary<GameObject, GameObject>();

    public bool tutorialMode;
    [SerializeField]
    private CuttingManager cuttingManager;
    private void Start()
    {
        if (tutorialMode)
            enabled = false;


    }
    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }

    public void Slice()
    {
        /*
        Collider[] hits = Physics.OverlapBox(cutPlane.position, CutZoneKnife, cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            GameObject hitObject = hits[i].gameObject;

            if (!cutsPerObject.ContainsKey(hitObject))
                cutsPerObject[hitObject] = 0;

            int currentCutsForObject = cutsPerObject[hitObject];

            if (currentCutsForObject >= maxCutsPerObject)
            {
                Debug.Log("Se ha alcanzado el límite de cortes permitidos para este objeto.");
                continue; // Continúa al siguiente objeto si se alcanza el límite
            }

            SlicedHull hull = SliceObject(hitObject, crossMaterial);
            if (hull != null)
            {
                GameObject bottom = hull.CreateLowerHull(hitObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(hitObject, crossMaterial);

                AddHullComponents(bottom);
                AddHullComponents(top);
                Destroy(hitObject);

                cutsPerObject[bottom] = currentCutsForObject + 1; // Incrementa el contador de cortes para la mitad inferior
                cutsPerObject[top] = currentCutsForObject + 1; // Incrementa el contador de cortes para la mitad superior
            }
        }
        */

        Collider[] hits = Physics.OverlapBox(cutPlane.position, CutZoneKnife, cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            GameObject hitObject = hits[i].gameObject;


            if (!cutsPerObject.ContainsKey(hitObject))
                cutsPerObject[hitObject] = 0;

            int currentCutsForObject = cutsPerObject[hitObject];

            if (currentCutsForObject >= maxCutsPerObject)
            {
                Debug.Log("Se ha alcanzado el límite de cortes permitidos para este objeto.");
                cuttingManager.CheckCut();
                continue; // Continúa al siguiente objeto si se alcanza el límite
            }

            // Guarda las transformaciones locales antes del corte
            Vector3 originalPosition = hitObject.transform.localPosition;
            Quaternion originalRotation = hitObject.transform.localRotation;
            Vector3 originalScale = hitObject.transform.localScale;

            // Imprime las transformaciones locales antes del corte en la consola
            Debug.Log("Original Position: " + originalPosition);
            Debug.Log("Original Rotation: " + originalRotation);
            Debug.Log("Original Scale: " + originalScale);

            Material crossMaterial = hitObject.GetComponent<Food>().crossMaterial;

            SlicedHull hull = SliceObject(hitObject, crossMaterial);
            if (hull != null)
            {
                // Crea un nuevo GameObject vacío solo si no existe uno para el objeto que se está cortando
                GameObject parent;
                if (!parentsPerObject.ContainsKey(hitObject))
                {
                    parent = new GameObject();
                    parent.transform.position = Vector3.zero; // Crea el objeto padre en el punto (0, 0, 0)
                    parent.name = hitObject.name + " (Sliced)";
                    parentsPerObject[hitObject] = parent;
                    //Create a new list of slice parents
                    cuttingManager.AddSliceItem(parent); 

                }
                else
                {
                    parent = parentsPerObject[hitObject];
                }

                GameObject bottom = hull.CreateLowerHull(hitObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(hitObject, crossMaterial);

                bottom.transform.SetParent(parent.transform, true);
                top.transform.SetParent(parent.transform, true);


                AddHullComponents(bottom, hitObject.GetComponent<Food>().IngredientType, hitObject.GetComponent<Food>().crossMaterial);
                AddHullComponents(top, hitObject.GetComponent<Food>().IngredientType, hitObject.GetComponent<Food>().crossMaterial);
                Destroy(hitObject);

                cutsPerObject[bottom] = currentCutsForObject + 1; // Incrementa el contador de cortes para la mitad inferior
                cutsPerObject[top] = currentCutsForObject + 1; // Incrementa el contador de cortes para la mitad superior

                // Asigna el mismo objeto padre a las mitades cortadas
                parentsPerObject[bottom] = parent;
                parentsPerObject[top] = parent;

                // Restaura las transformaciones locales de las mitades cortadas
                bottom.transform.localPosition = originalPosition;
                bottom.transform.localRotation = originalRotation;
                bottom.transform.localScale = originalScale;

                top.transform.localPosition = originalPosition;
                top.transform.localRotation = originalRotation;
                top.transform.localScale = originalScale;

            }
        }


    }

    public void AddHullComponents(GameObject go, Ingredients vegetableType, Material CrossMaterial)
    {
        go.layer = 8;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        go.transform.rotation = Quaternion.identity;
        // Item Food
        Food food = go.AddComponent<Food>();
        food.SetIngredientType(vegetableType);
        food.SetCrossMaterial(CrossMaterial);

        rb.AddExplosionForce(explosionForce, go.transform.position, 20);

    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    private void OnDrawGizmos()
    {
        // Draw wireframe box representing the overlap area
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(cutPlane.position, cutPlane.rotation, new Vector3(1, 0.1f, 1));
        Gizmos.DrawWireCube(Vector3.zero, CutZoneKnife);

        // Perform the overlap check
        Collider[] hits = Physics.OverlapBox(cutPlane.position, CutZoneKnife, cutPlane.rotation, layerMask);

        // Draw spheres at the center of the overlapping colliders
        Gizmos.color = Color.red;
        foreach (var hit in hits)
        {
            Gizmos.DrawSphere(hit.bounds.center, 0.1f);
        }
    }



}
