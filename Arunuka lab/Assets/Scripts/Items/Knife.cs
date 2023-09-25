using System.Collections.Generic;
using EzySlice;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the knife, cutting the collided objects.
/// </summary>
public class Knife : MonoBehaviour, IUsable, IPickable
{
    public UnityEvent OnUse { get; set; }

    public Transform cutPlane;
    public LayerMask layerMask;
    public Vector3 CutZoneKnife;
    public float explosionForce = 100f;
    public int maxCutsPerObject = 3;
    public bool tutorialMode;
    AudioManager audioManager;
    [SerializeField] private CuttingManager cuttingManager;

    [SerializeField] private UnityEvent onCut;

    [SerializeField] private UnityEvent onPickUp;

    [SerializeField] private UnityEvent onDrop;

    // Cache of how many cuts has the parent.
    // Note: The GetHasCode of GameObject is the InstanceId, so it's not expensive to leave it as the key.
    //
    private readonly Dictionary<GameObject, int> currentCutsInParent = new();

    private readonly Dictionary<GameObject, GameObject> parentsPerObject = new();

    public bool KeepWorldPosition => false;

    private readonly float movementSpeed = 1.0f;
    private Rigidbody m_Rigidbody;
    private bool isPickable = true;
    private bool isPickedUp;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called after the creation of the objects.
    /// </summary>
    private void Start()
    {
        audioManager = AudioManager.Instance;
        enabled = !tutorialMode;

        // Note: It needs to be referenced in the Start because 
        //       we don't want to change the execution priority just for the Awake methods.
        //
        cuttingManager = CuttingManager.Instance;
    }

    /// <summary>
    /// Called every game frame.
    /// </summary>
    private void Update()
    {
        if (!IsPickedUp())
            return;

        Vector3 localPosition = transform.localPosition;
        Vector2 lookDirection = InputManager.GetInstance().GetlookInput();
        float movementVelocity = lookDirection.x * movementSpeed * Time.deltaTime;
        float zPosition = localPosition.z + movementVelocity * -1; // Invert the movement.
        zPosition = Mathf.Clamp(zPosition, -0.25f, 0.25f);
        transform.localPosition = new Vector3(localPosition.x, localPosition.y, zPosition);

        if (InputManager.GetInstance().GetLeftMousePressed())
            Slice();
    }

    /// <inheritdoc />
    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }

    /// <inheritdoc />
    public void SetIsPickable(bool isPickable)
    {
        this.isPickable = isPickable;
    }

    /// <inheritdoc />
    public bool IsPickable()
    {
        return isPickable;
    }

    /// <inheritdoc />
    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    /// <inheritdoc />
    public GameObject PickUp(GameObject picker)
    {
        isPickedUp = true;
        m_Rigidbody.isKinematic = true;

        //TODO: Set an animation for this.
        //
        transform.SetLocalPositionAndRotation(
            new Vector3(-0.31099999f, 0.42f, -0.0340000018f),
            new Quaternion(0.114386953f, 0.710132778f, -0.0854734182f, 0.689435542f));

        onPickUp?.Invoke();
        audioManager.PlaySoundKnifeOut();
        return gameObject;
    }

    /// <inheritdoc />
    public void Drop()
    {
        isPickedUp = false;
        m_Rigidbody.isKinematic = false;

        // TODO: Add animation to leave knife in its correct position.
        //
        transform.SetLocalPositionAndRotation(
            new Vector3(-0.31099999f, 0.0170000009f, -0.474397004f),
            new Quaternion(-0.413232654f, 0.435098618f, -0.567699552f, 0.563600302f));

        onDrop?.Invoke();
        HoverCursor.Instance.OnExitHover();
    }

    /// <summary>
    /// Slices the objects that entered in contact with the knife collider.
    /// </summary>
    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, CutZoneKnife, cutPlane.rotation, layerMask);
        if (hits.Length <= 0)
            return;

        bool checkMeOut = true;

        foreach (Collider hit in hits)
        {
            GameObject hitObject = hit.gameObject;

            int currentHitsOnParent = 0;
            GameObject cutParent = GetCutParent(hitObject);
            if (cutParent != null)
                currentHitsOnParent = currentCutsInParent[cutParent];

            if (currentHitsOnParent < maxCutsPerObject)
                checkMeOut = false;
            else
                continue;

            // Guarda las transformaciones locales antes del corte.
            //
            Vector3 originalPosition = hitObject.transform.localPosition;
            Quaternion originalRotation = hitObject.transform.localRotation;
            Vector3 originalScale = hitObject.transform.localScale;

            // Imprime las transformaciones locales antes del corte en la consola
            //
            Debug.Log("Original Position: " + originalPosition);
            Debug.Log("Original Rotation: " + originalRotation);
            Debug.Log("Original Scale: " + originalScale);

            Material crossMaterial = hitObject.GetComponent<Food>().crossMaterial;
            SlicedHull hull = SliceObject(hitObject, crossMaterial);
            if (hull == null)
                continue;

            // Crea un nuevo GameObject vacío solo si no existe uno para el objeto que se está cortando.
            //
            if (cutParent == null)
            {
                cutParent = new GameObject();
                cutParent.transform.position = Vector3.zero;
                cutParent.name = hitObject.name + " (Sliced)";
                cuttingManager.AddSliceItem(cutParent);

                parentsPerObject[hitObject] = cutParent;
                currentCutsInParent[cutParent] = 0;
            }

            GameObject bottom = hull.CreateLowerHull(hitObject, crossMaterial);
            GameObject top = hull.CreateUpperHull(hitObject, crossMaterial);

            bottom.transform.SetParent(cutParent.transform, true);
            top.transform.SetParent(cutParent.transform, true);

            AddHullComponents(bottom, hitObject);
            AddHullComponents(top, hitObject);
            Destroy(hitObject);

            // Asigna el mismo objeto padre a las mitades cortadas.
            //
            parentsPerObject[top] = cutParent;
            parentsPerObject[bottom] = cutParent;

            // Restaura las transformaciones locales de las mitades cortadas.
            //
            bottom.transform.SetLocalPositionAndRotation(originalPosition, originalRotation);
            bottom.transform.localScale = originalScale;

            top.transform.SetLocalPositionAndRotation(originalPosition, originalRotation);
            top.transform.localScale = originalScale;

            // Adds cuts in parent counter.
            //
            currentCutsInParent[cutParent]++;
            onCut?.Invoke();
            audioManager.PlaySoundKnifeCut();
        }

        if (!checkMeOut)
            return;
        AudioManager.Instance.PlayActionDenied();
        Debug.Log("Se ha alcanzado el límite de cortes permitidos para este objeto.");
        cuttingManager.CheckCut();
    }

    public void AddHullComponents(GameObject go, GameObject parent)
    {
        go.layer = 8;
        var rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        var collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        go.transform.rotation = Quaternion.identity;

        // Item Food
        //
        var parentFood = parent.GetComponent<Food>();
        var food = go.AddComponent<Food>();
        food.SetIngredientType(parentFood.IngredientType);
        food.SetCrossMaterial(parentFood.crossMaterial);

        // IPickable
        //
        var parentPickable = parent.GetComponent<PickableObject>();
        var pickable = go.AddComponent<PickableObject>();
        pickable.KeepWorldPosition = parentPickable.KeepWorldPosition;

        rb.AddExplosionForce(explosionForce, go.transform.position, 20);
    }

    /// <summary>
    /// Try to slice the object, and returns the sliced object.
    /// </summary>
    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    /// <summary>
    /// Returns the parent of the cut object. If it does not have a parent (It's first time cutting it),
    /// then it returns NULL.
    /// </summary>
    private GameObject GetCutParent(GameObject objectToCut)
    {
        if (parentsPerObject.TryGetValue(objectToCut, out GameObject parent))
            return parent;

        return null;
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
        foreach (Collider hit in hits)
            Gizmos.DrawSphere(hit.bounds.center, 0.1f);
    }
}