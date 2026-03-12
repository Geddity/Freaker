using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropScreen : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform cropArea;
    private static CropScreen use;
    public Camera cam;
    public Button cropButton;
    public TextMeshProUGUI watermark;

    private void Awake()
    {
        use = this;
    }

    public static void Create(string path)
    {
        use.Crop(path);
    }

    void Crop(string path)
    {
        StartCoroutine(SeveCrop(path));
    }

    Texture2D AddWatermark(Texture2D background, TextMeshProUGUI add)
    {
        add = watermark;
        return background;
    }

    IEnumerator SeveCrop(string path)
    {
        cropArea.GetComponentInChildren<Image>().enabled = false; // отключаем область снимка
        watermark.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        int width = (int)(cropArea.rect.width * canvas.scaleFactor);
        int height = (int)(cropArea.rect.height * canvas.scaleFactor);
        int x = (int)(cropArea.anchoredPosition.x * canvas.scaleFactor);
        int y = (int)(cropArea.anchoredPosition.y * canvas.scaleFactor);
        Texture2D croppedTexture = new Texture2D(width, height); // создаем новую текстуру
        Texture2D originalTexture = ScreenCapture.CaptureScreenshotAsTexture(); // берем текстуру экрана

        if (x + width <= originalTexture.width && y + height <= originalTexture.height && cam.GetComponent<CreationCameraScript>().zoomedIn) // проверяем что область снимка не выходит за границы экрана
        {
            croppedTexture.SetPixels(originalTexture.GetPixels(x, y, width, height)); // копируем пиксели в новую текстуру
            croppedTexture = AddWatermark(croppedTexture, watermark);
            croppedTexture.Apply();
            
            byte[] bytes = croppedTexture.EncodeToPNG(); // конвертируем
            File.WriteAllBytes(path, bytes); // сохраняем в файл
            Debug.Log("Создан скриншот: " + path);
        }

        Destroy(croppedTexture); // удаляем текстуры
        Destroy(originalTexture);
        cropArea.GetComponentInChildren<Image>().enabled = true; // возвращаем область
        watermark.gameObject.SetActive(false);
    }

    void CreateDirectory()
    {
        try
        {
            if (!Directory.Exists("Freakshots"))
            {
                Directory.CreateDirectory("Freakshots");
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void CropButton()
    {
        CreateDirectory();

        Create("Freakshots" + "/" + System.DateTime.Now.ToFileTime() + ".png");
    }

    private void Update()
    {
        if (cam.GetComponent<CreationCameraScript>().zoomedIn)
        {
            cropButton.interactable = true;
        }
        else
        {
            cropButton.interactable = false;
        }
    }
}
