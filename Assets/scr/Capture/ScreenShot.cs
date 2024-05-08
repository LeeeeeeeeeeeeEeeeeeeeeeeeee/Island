using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public Canvas cameraCanvas;
    public AudioSource audioSoure;

    public Texture2D texture;

    public void CaptureModeOn()
    {

    }

    public void CaptureStart()
    {
        StartCoroutine(MakeScreenShot());
    }

    private IEnumerator MakeScreenShot()
    {
        audioSoure.Play();
        cameraCanvas.enabled = false;

        yield return new WaitForEndOfFrame();
        Capture();
        cameraCanvas.enabled = true;
    }

    public void Capture()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "BRUNCH-SCREENSHOT-" + timestamp + ".png";

#if UNITY_IPHONE || UNITY_ANDROID
        CaptureScreenForMobile(fileName);
#else
        CaptureScreenForPC(fileName);
#endif
    }

    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("~/Downloads/" + fileName);
    }

    private void CaptureScreenForMobile(string fileName)
    {
        texture = ScreenCapture.CaptureScreenshotAsTexture();

        // do something with texture
        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);
        if (permission == NativeGallery.Permission.Denied)
        {
            if (NativeGallery.CanOpenSettings())
            {
                NativeGallery.OpenSettings();
            }
        }

        string albumName = "BRUNCH";
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        // cleanup
        Object.Destroy(texture);
    }
}
