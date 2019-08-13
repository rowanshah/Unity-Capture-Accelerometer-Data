using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Accelerometer : MonoBehaviour
{
  private Vector3 defaultAcceleration;
   private string url = "Enter your URL";
    private string db = "Enter your database name";
    private string username = "Enter your username";
    private string password = "Enter your password";
    private string device = "Dev1_";
     private float waitTime = 10f;
    private float timer = 0.0f;

   void OnEnable () {
	defaultAcceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1*Input.acceleration.z);
  } 

   void Update() {
    timer += Time.deltaTime;
    if (timer > waitTime)
    {
	Vector3 acceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1*Input.acceleration.z);
	acceleration -= defaultAcceleration;
	StartCoroutine(PutData(acceleration));
    timer = 0.0f;
    }
  }

//Send to cloud
 private IEnumerator PutData(Vector3 data)
    {
        string dateTime = System.DateTime.Now.ToString("MMddyyyy-Hmmss");
        dateTime += device;
        string json = "{\"_id\":\"" + dateTime +"\", \"data\":\"" + data + "\"}";

        Debug.Log("Accelerometer Data Request made");
        // Request and wait for the desired page.

        var request = new UnityWebRequest(url + db, "POST");
        request.SetRequestHeader("Authorization", "Basic " + Base64Encode(username + ":" + password));

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();

        yield return null;

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
   }
 public string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}