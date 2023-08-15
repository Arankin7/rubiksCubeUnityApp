using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    CubeState cubeState;

    [SerializeField]
    public Transform up, down, left, right, front, back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        cubeState = FindObjectOfType<CubeState>();

        UpdateCubeMap(cubeState.front, front);
        UpdateCubeMap(cubeState.back, back);
        UpdateCubeMap(cubeState.up, up);
        UpdateCubeMap(cubeState.down, down);
        UpdateCubeMap(cubeState.left, left);
        UpdateCubeMap(cubeState.right, right);
    }

    void UpdateCubeMap(List<GameObject> face, Transform side)
    {
        int i = 0;

        foreach(Transform map in side)
        {
            if(face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1, 0.55f, 0);
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            i++;
        }
    }
}
