using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeManager : MonoBehaviour
{
    // Prefab kostki, ktora bedzie tworzona na scenie
    public Cube cubePrefab;

    // Grafiki, teksty i wartosci tekstow sluzace do obslugi rozmiaru MegaKostki
    public Image[] images = new Image[3];
    public Image[] simControlImages = new Image[3];
    public Text[] texts = new Text[3];
    private int[] sizes = new int[3];
    // Obecny indeks ww tablic
    private int currentDimension = 0;

    // Kolory do kolorowania grafik z tablicy images
    private Color selectedColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
    private Color deselectedColor = Color.white;

    // Minimalny i maksymalny rozmiar jednego wymiaru MegaKostki
    public static int minSize = 1;
    public static int maxSize = 100;
    // MegaKostka z odwolaniami do pojedynczych kostek - voxeli
    private Cube[,,] megaCube;
    // Temperatury MegaKostki, w poprzednim cyklu i w obecnym
    private float[,,] prevTemp;
    private float[,,] currTemp;

    // Zmienne do wprowadzenia opoznienia w sytuacji przytrzymania klawisza przy zmienianu rozmiaru MegaKostki
    private int framesPressedUp = 0;
    private int framesPressedDown = 0;
    private int framesPressedDelay = 20;

    // Obiekt odpowiedzialny za odpowiednie pozycjonowanie kamer
    private CameraPositioner cameraPositioner;

    // Zmienna okreslajaca, czy symulacja ma trwac
    private bool isSimulationPaused = true;

    // Wspolczynnik wyrownania temperatur
    private float a = 1;

    // Realny rozmiar kostki
    private float cubeRealSize = 1;


    public void Start()
    {
        images[0].color = selectedColor;
        images[1].color = deselectedColor;
        images[2].color = deselectedColor;
        
        simControlImages[0].color = deselectedColor;
        simControlImages[1].color = deselectedColor;
        simControlImages[2].color = selectedColor;

        texts[0].text = "3";
        texts[1].text = "3";
        texts[2].text = "3";

        sizes[0] = 3;
        sizes[1] = 3;
        sizes[2] = 3;

        cameraPositioner = GetComponent<CameraPositioner>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentDimension != 2)
        {
            images[currentDimension].color = deselectedColor;
            currentDimension++;
            images[currentDimension].color = selectedColor;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDimension != 0)
        {
            images[currentDimension].color = deselectedColor;
            currentDimension--;
            images[currentDimension].color = selectedColor;
        }

        if (Input.GetKey(KeyCode.UpArrow) && sizes[currentDimension] != maxSize)
        {
            if (framesPressedUp == 0 || framesPressedUp > framesPressedDelay)
                texts[currentDimension].text = (++sizes[currentDimension]).ToString();

            framesPressedUp++;
        } else
        {
            framesPressedUp = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow) && sizes[currentDimension] != minSize )
        {
            if (framesPressedDown == 0 || framesPressedDown > framesPressedDelay)
                texts[currentDimension].text = (--sizes[currentDimension]).ToString();

            framesPressedDown++;
        } else
        {
            framesPressedDown = 0;
        }

        UpdateTemperatures();
    }

    private void UpdateTemperatures()
    {
        // Jesli symulacja nie jest zapauzowana, zaktualizuj temperatury
        if (!isSimulationPaused)
        {
            for (int x = 0; x < sizes[0]; x++)
                for (int y = 0; y < sizes[1]; y++)
                    for (int z = 0; z < sizes[2]; z++)
                    {
                        float sum = 0;
                        float amountOfNeighbours = 0;
                        if (x != 0)
                        {
                            sum += prevTemp[x - 1, y, z];
                            amountOfNeighbours++;
                        }
                        if (x < sizes[0] - 1)
                        {
                            sum += prevTemp[x + 1, y, z];
                            amountOfNeighbours++;
                        }

                        if (y != 0)
                        {
                            sum += prevTemp[x, y - 1, z];
                            amountOfNeighbours++;
                        }
                        if (y < sizes[1] - 1)
                        {
                            sum += prevTemp[x, y + 1, z];
                            amountOfNeighbours++;
                        }

                        if (z != 0)
                        {
                            sum += prevTemp[x, y, z - 1];
                            amountOfNeighbours++;
                        }
                        if (z < sizes[2] - 1)
                        {
                            sum += prevTemp[x, y, z + 1];
                            amountOfNeighbours++;
                        }


                        currTemp[x, y, z] = prevTemp[x, y, z] + ((sum - (amountOfNeighbours * prevTemp[x, y, z])) * 0.01f * a) / (cubeRealSize * cubeRealSize);
                        UpdateCubeColor(x, y, z);
                    }
            prevTemp = currTemp;
            currTemp = new float[sizes[0], sizes[1], sizes[2]];

            float suma = 0;
            for (int i = 0; i < sizes[0]; i++)
                for (int j = 0; j < sizes[1]; j++)
                    for (int k = 0; k < sizes[2]; k++)
                        suma += prevTemp[i, j, k];

            print("Suma temperatur: " + suma);
        }
    }

    public void CreateMegaCube()
    {
        if (megaCube != null)
        {
            for (int i = 0; i < megaCube.GetLength(0); i++)
                for (int j = 0; j < megaCube.GetLength(1); j++)
                    for (int k = 0; k < megaCube.GetLength(2); k++)
                        Destroy(megaCube[i, j, k].gameObject);
        }

        megaCube = new Cube[sizes[0], sizes[1], sizes[2]];
        prevTemp = new float[sizes[0], sizes[1], sizes[2]];
        currTemp = new float[sizes[0], sizes[1], sizes[2]];

        for (int i = 0; i < megaCube.GetLength(0); i++)
            for (int j = 0; j < megaCube.GetLength(1); j++)
                for (int k = 0; k < megaCube.GetLength(2); k++)
                {
                    megaCube[i, j, k] = Instantiate(cubePrefab, new Vector3(i, j, k), new Quaternion());
                }


        cameraPositioner.UpdateCameras(((float)sizes[0] / 2) - 0.5f,
                                       ((float)sizes[1] / 2) - 0.5f,
                                       ((float)sizes[2] / 2) - 0.5f);


    }

    public void PlayPressed()
    {
        isSimulationPaused = false;
        simControlImages[0].color = selectedColor;
        simControlImages[1].color = deselectedColor;
        simControlImages[2].color = deselectedColor;
    }
    public void PausePressed()
    {
        isSimulationPaused = true;
        simControlImages[0].color = deselectedColor;
        simControlImages[1].color = selectedColor;
        simControlImages[2].color = deselectedColor;
    }
    public void StopPressed()
    {
        // Reset symulacji
        isSimulationPaused = true;
        simControlImages[0].color = deselectedColor;
        simControlImages[1].color = deselectedColor;
        simControlImages[2].color = selectedColor;


    }
    public void AddHeat()
    {
        for (int i=0;i<sizes[0];i++)
            for (int j=0;j<sizes[1];j++)
            {
                currTemp[i, j, 0] = 1000;
                prevTemp[i, j, 0] = 1000;
                UpdateCubeColor(i, j, 0);
            }
    }

    private void UpdateCubeColor(int x, int y, int z)
    {
        megaCube[x, y, z].GetComponent<Renderer>().material.color = new Color(1, 1 - (currTemp[x, y, z] / 1000), 1 - (currTemp[x, y, z] / 1000), 0.1f + (0.0009f * currTemp[x, y, z]));
    }

}
