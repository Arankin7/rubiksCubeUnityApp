using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>() {};

    private readonly List<string> allMoves = new List<string>()
    {
        "U", "D", "L", "R", "F", "B",
        "U'", "D'", "L'", "R'", "F'", "B'",
        "U2", "D2", "L2", "R2", "F2", "B2",
    };

    private CubeState cubeState;
    private ReadCube readCube;

    [SerializeField]
    Button[] rotateButtons;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
        AddListeners();
        //rotateButtons[0].onClick.AddListener(delegate { ButtonMoves(rotateButtons[0].gameObject.name); });
    }

    // Update is called once per frame
    void Update()
    {
        if(moveList.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            // do the move at the first index
            DoMove(moveList[0]);
            // remove the move at the first index
            moveList.Remove(moveList[0]);
        }

    }

    private void AddListeners()
    {
        rotateButtons[0].onClick.AddListener(delegate { ButtonMoves(rotateButtons[0].gameObject.name); });
        rotateButtons[1].onClick.AddListener(delegate { ButtonMoves(rotateButtons[1].gameObject.name); });
        rotateButtons[2].onClick.AddListener(delegate { ButtonMoves(rotateButtons[2].gameObject.name); });
        rotateButtons[3].onClick.AddListener(delegate { ButtonMoves(rotateButtons[3].gameObject.name); });
        rotateButtons[4].onClick.AddListener(delegate { ButtonMoves(rotateButtons[4].gameObject.name); });
        rotateButtons[5].onClick.AddListener(delegate { ButtonMoves(rotateButtons[5].gameObject.name); });
        rotateButtons[6].onClick.AddListener(delegate { ButtonMoves(rotateButtons[6].gameObject.name); });
        rotateButtons[7].onClick.AddListener(delegate { ButtonMoves(rotateButtons[7].gameObject.name); });
        rotateButtons[8].onClick.AddListener(delegate { ButtonMoves(rotateButtons[8].gameObject.name); });
        rotateButtons[9].onClick.AddListener(delegate { ButtonMoves(rotateButtons[9].gameObject.name); });
        rotateButtons[10].onClick.AddListener(delegate { ButtonMoves(rotateButtons[10].gameObject.name); });
        rotateButtons[11].onClick.AddListener(delegate { ButtonMoves(rotateButtons[11].gameObject.name); });
    }
    public void ShuffleCube()
    {
        List<string> moves = new List<string>();

        int shuffleLength = Random.Range(10, 10);

        for(int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);

            moves.Add(allMoves[randomMove]);
        }

        moveList = moves;


    }

    public void ButtonMoves(string buttonName)
    {
        List<string> moves = new List<string>();

        moves.Add(buttonName);

        moveList = moves;
    }

    void DoMove(string move)
    {
        readCube.ReadState();

        CubeState.autoRotating = true;

        if(move == "U")
        {
            RotateSide(cubeState.up, -90);
        }
        if (move == "U'")
        {
            RotateSide(cubeState.up, 90);
        }
        if (move == "U2")
        {
            RotateSide(cubeState.up, -180);
        }

        if (move == "D")
        {
            RotateSide(cubeState.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(cubeState.down, 90);
        }
        if (move == "D2")
        {
            RotateSide(cubeState.down, -180);
        }

        if (move == "L")
        {
            RotateSide(cubeState.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(cubeState.left, 90);
        }
        if (move == "L2")
        {
            RotateSide(cubeState.left, -180);
        }

        if (move == "R")
        {
            RotateSide(cubeState.right, -90);
        }
        if (move == "R'")
        {
            RotateSide(cubeState.right, 90);
        }
        if (move == "R2")
        {
            RotateSide(cubeState.right, -180);
        }

        if (move == "F")
        {
            RotateSide(cubeState.front, -90);
        }
        if (move == "F'")
        {
            RotateSide(cubeState.front, 90);
        }
        if (move == "F2")
        {
            RotateSide(cubeState.front, -180);
        }

        if (move == "B")
        {
            RotateSide(cubeState.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(cubeState.back, 90);
        }
        if (move == "B2")
        {
            RotateSide(cubeState.back, -180);
        }
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }
}
