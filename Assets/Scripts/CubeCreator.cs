using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCreator : MonoBehaviour
{
    public Image[] img = new Image[3];
    public Text[] dim = new Text[3];
    private int selectedDim = 0;

    private Color selectedColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
    private Color deselectedColor = Color.white;

    private Cube[,,] cubes = new Cube[9, 9, 9];
    public Cube cubePrefab;

    private CameraPositioner cameraPositioner;

    void Start()
    {
        for (int i = 0; i < img.Length; i++)
            img[i].color = deselectedColor;
        img[selectedDim].color = selectedColor;

        for (int i = 0; i < dim.Length; i++)
            dim[i].text = "1";

        cameraPositioner = GetComponent<CameraPositioner>();
        ChangeSize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && selectedDim != 2)
        {
            img[selectedDim].color = deselectedColor;
            selectedDim++;
            img[selectedDim].color = selectedColor;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectedDim != 0)
        {
            img[selectedDim].color = deselectedColor;
            selectedDim--;
            img[selectedDim].color = selectedColor;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && int.Parse(dim[selectedDim].text) != 9)
        {
            dim[selectedDim].text = (int.Parse(dim[selectedDim].text) + 1).ToString();
            ChangeSize();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && int.Parse(dim[selectedDim].text) != 1)
        {
            dim[selectedDim].text = (int.Parse(dim[selectedDim].text) - 1).ToString();
            ChangeSize();
        }
    }

    void ChangeSize()
    {
        for (int d1 = 1; d1 <= 9; d1++)
        {
            for (int d2 = 1; d2 <= 9; d2++)
            {
                for (int d3 = 1; d3 <= 9; d3++)
                {
                    if (d1 <= int.Parse(dim[0].text) && d2 <= int.Parse(dim[1].text) && d3 <= int.Parse(dim[2].text))
                    {
                        // Blok powinien istniec

                        if (cubes[d1 - 1, d2 - 1, d3 - 1] == null)
                        {
                            cubes[d1 - 1, d2 - 1, d3 - 1] = Instantiate(cubePrefab, new Vector3(d1, d2, d3), new Quaternion());
                        }
                    } else
                    {
                        // Blok nie powinien istniec
                        if (cubes[d1 - 1, d2 - 1, d3 - 1] != null)
                        {
                            print("destroy");
                            Destroy(cubes[d1 - 1, d2 - 1, d3 - 1].gameObject);
                        }
                    }
                }
            }
        }

        cameraPositioner.UpdateCameras((float.Parse(dim[0].text) / 2) + 0.5f, 
                                       (float.Parse(dim[1].text) / 2) + 0.5f, 
                                       (float.Parse(dim[2].text) / 2) + 0.5f);
    }
}
