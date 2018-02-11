using System;
using Google.Cloud.Storage.V1;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

public class StorageService : IStorageService
{
    private readonly StorageClient _client;
    private readonly GoogleCloudSettings _googleCloudSettings;

    public StorageService(GoogleCloudSettings googleCloudSettings)
    {
        var json=JsonConvert.SerializeObject(googleCloudSettings);
        var credential = GoogleCredential.FromJson(json);
        _client = StorageClient.Create(credential);
        _googleCloudSettings = googleCloudSettings;
    }
    public async Task<string> UploadImageAndReturnUrlAsync(string base64Image, string name)
    {
        var bytes = Convert.FromBase64String(base64Image);
        var imageStream = new MemoryStream(bytes);
        string bucketName =_googleCloudSettings.bucket;
        var uploadedObj = await _client.UploadObjectAsync(bucketName, name+".png", "image/png", imageStream);
        return uploadedObj.MediaLink;
    }
}