using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollChunks : MonoBehaviour {

    public GameObject Chunk;
    public float GridSize = 5;

    GameObject Floor;
    Vector3 Size;

    Vector3 Position = Vector3.zero;

	// Use this for initialization
	void Start () {
        Floor = Chunk.transform.GetChild(0).gameObject; // Zeroeth child should be the floor
        Size = Floor.GetComponent<Renderer>().bounds.size; // Find the size of the floor

        // Create a grid of Chunks
        for (int z = 0; z < GridSize; z++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                Vector3 Position = Vector3.Scale(new Vector3(x-GridSize/2, 0, z-GridSize/2), Size);
                GameObject NewChunk = Instantiate(Chunk, Position, Quaternion.identity);
                NewChunk.transform.SetParent(gameObject.transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Temporary code for movement, just to test
        Vector3 Translation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * -.1f;

        // Loop movement
        // x
        if (Position.x + Translation.x > Size.x)
        {
            Translation.x -= Size.x;
        }
        else if (Position.x + Translation.x < 0.0f)
        {
            Translation.x += Size.x;
        }
        // z
        if (Position.z + Translation.z > Size.z)
        {
            Translation.z -= Size.z;
        }
        else if (Position.z + Translation.z < 0.0f)
        {
            Translation.z += Size.z;
        }

        // Add translation to general position and the transforms of all chunks
        Position += Translation;
        foreach (Transform child in gameObject.transform)
        {
            child.Translate(Translation);
        }
    }
}
