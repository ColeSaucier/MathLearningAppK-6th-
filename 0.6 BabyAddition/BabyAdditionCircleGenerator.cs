using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BabyAdditionCircleGenerator : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public int minObjects = 1;
    public int maxObjects = 10;
    public float spacing = 1.0f;
    public Vector3 initialSpawnPosition = new Vector3(-1, 0, 0);
    public TextMeshPro equation;
    public TextMeshPro sum;
    public int sumObjects;

    private bool isDragging;
    private Vector3 lastMousePosition;
    private Vector3 offset;
    public GameObject subparentObject2;

    public bool collided = false;
    public bool undoCombination;

    private bool isDragging2;
    public Vector3 initialSpawnPosition2 = new Vector3(1, 0, 0);
    public GameObject subparentObject;

    public Vector3 combinedSpawnPosition = new Vector3(0, 0, 0);
    public GameObject combinedObject;

    public AnimationClip shakeAnimationClip;
    public GameObject parentObject;
    public GameObject parentObject2;

    void Start()
    {
        //Animation parents
        parentObject = new GameObject("parentObject");
        parentObject2 = new GameObject("GeneratedPrefabs2");
        
        Animation animation = parentObject.AddComponent<Animation>();
        Animation animation2 = parentObject2.AddComponent<Animation>();

        if (shakeAnimationClip != null)
        {
            animation.clip = shakeAnimationClip;
            animation.AddClip(animation.clip, "ShakeSmallCircle");
            animation.Play("ShakeSmallCircle");
            animation2.clip = shakeAnimationClip;
            animation2.AddClip(animation.clip, "ShakeSmallCircle");
            animation2.Play("ShakeSmallCircle");
            //Debug.LogError("Animation is playing: " + animation.isPlaying); 
        }
        else
        {
            Debug.LogError("Animation clip 'ShakeSmallCircle' not found or not assigned!");
        }

        // Create an empty parent object to hold the generated prefabs
        subparentObject = new GameObject("GeneratedPrefabs1");
        subparentObject.transform.SetParent(parentObject.transform);
        subparentObject2 = new GameObject("GeneratedPrefabs2");
        subparentObject2.transform.SetParent(parentObject2.transform);
        combinedObject = new GameObject("parentObject2");

        // Generate a random number of prefabs
        int numberOfObjects = Random.Range(minObjects, maxObjects + 1);
        int numberOfObjects2 = Random.Range(minObjects, maxObjects + 1);
        sumObjects = numberOfObjects + numberOfObjects2;
        equation.text = $"{numberOfObjects}+{numberOfObjects2}";

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Generate a random position with variable spacing
            Vector3 spawnPosition = initialSpawnPosition + new Vector3(0, spacing, 0) * i;

            // Instantiate the prefab at the generated position
            GameObject newPrefab = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

            // Set the parent of the instantiated prefab to the parentObject
            newPrefab.transform.parent = subparentObject.transform;
        }

        Vector3 firstCirclePosition = subparentObject.transform.GetChild(0).transform.position;
        // Get the last child object's position
        Vector3 lastCirclePosition = subparentObject.transform.GetChild(numberOfObjects - 1).transform.position;
        Vector3 delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        BoxCollider collider = subparentObject.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition;
        collider.size = new Vector3(0.5f, numberOfObjects * spacing, 1f);

        for (int i = 0; i < numberOfObjects2; i++)
        {
            // Generate a random position with variable spacing
            Vector3 spawnPosition = initialSpawnPosition2 + new Vector3(0, spacing, 0) * i;

            // Instantiate the prefab at the generated position
            GameObject newPrefab = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

            // Set the parent of the instantiated prefab to the parentObject
            newPrefab.transform.parent = subparentObject2.transform;
        }

        firstCirclePosition = subparentObject2.transform.GetChild(0).transform.position;
        // Get the last child object's position
        lastCirclePosition = subparentObject2.transform.GetChild(numberOfObjects2 - 1).transform.position;
        delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        collider = subparentObject2.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition;
        collider.size = new Vector3(0.5f, numberOfObjects2 * spacing, 1f);

        for (int i = 0; i < sumObjects; i++)
        {
            if (i < 10)
            {
                // Generate a random position with variable spacing
                Vector3 spawnPosition = combinedSpawnPosition + new Vector3(0, spacing, 0) * i;
                // Instantiate the prefab at the generated position
                GameObject newPrefab = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                // Set the parent of the instantiated prefab to the parentObject
                newPrefab.transform.parent = combinedObject.transform;
            }
            else
            {
                // Generate a random position with variable spacing
                Vector3 spawnPosition = combinedSpawnPosition + new Vector3(spacing, spacing * (i-10), 0);
                // Instantiate the prefab at the generated position
                GameObject newPrefab = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                // Set the parent of the instantiated prefab to the parentObject
                newPrefab.transform.parent = combinedObject.transform;
            }
        }

        firstCirclePosition = combinedObject.transform.GetChild(0).transform.position;
        
        // Get the last child object's position
        lastCirclePosition = combinedObject.transform.GetChild(sumObjects - 1).transform.position;
        Vector3 neededAdjustment = Vector3.zero;
        if (sumObjects < 10)
        {
            neededAdjustment = new Vector3(0, 0, 0);
        }
        else
        {
            float tenDifference = sumObjects % 10;
            tenDifference = 10 - tenDifference;
            neededAdjustment = new Vector3(0, spacing * tenDifference/2, 0);
        }
        delta2 = (lastCirclePosition - firstCirclePosition) / 2f;

        // Add a BoxCollider to the parent object and center it
        collider = combinedObject.AddComponent<BoxCollider>();
        collider.center = delta2 + firstCirclePosition + neededAdjustment;
        collider.size = new Vector3(0.5f, 10 * spacing, 1f);
        combinedObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == subparentObject.GetComponent<BoxCollider>())
            {
                isDragging = true;
                lastMousePosition = GetWorldMousePosition();
                offset = lastMousePosition - subparentObject.transform.position;
            }
            if (Physics.Raycast(ray, out hit) && hit.collider == subparentObject2.GetComponent<BoxCollider>())
            {
                isDragging2 = true;
                lastMousePosition = GetWorldMousePosition();
                offset = lastMousePosition - subparentObject2.transform.position;
            }
            if (Physics.Raycast(ray, out hit) && hit.collider == combinedObject.GetComponent<BoxCollider>())
            {
                undoCombination = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isDragging2 = false;
            // Check for overlap between circleGroup and circleGroup1 colliders
            if (subparentObject.GetComponent<BoxCollider>().bounds.Intersects(subparentObject2.GetComponent<BoxCollider>().bounds))
            {
                subparentObject.SetActive(false);
                subparentObject2.SetActive(false);
                combinedObject.SetActive(true);
                sum.text = sumObjects.ToString();
            }
            if (undoCombination)
            {
                undoCombination = false;
                subparentObject.SetActive(true);
                subparentObject2.SetActive(true);
                combinedObject.SetActive(false);
                sum.text = "";
            }
        }
        if (isDragging)
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 targetPosition = currentMousePosition - offset;
            subparentObject.transform.position = targetPosition;
            lastMousePosition = currentMousePosition;
        }
        if (isDragging2)
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 targetPosition = currentMousePosition - offset;
            subparentObject2.transform.position = targetPosition;
            lastMousePosition = currentMousePosition;
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
