using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;

namespace UploadDownloadFromBlob
{
    class AzureBlobSample
    {
        public void UploadFiles(
            IEnumerable<FileInfo> files,
            string connectionString,
            string container)
        {
            var containerClient = new BlobContainerClient(connectionString, container);

            Console.WriteLine("Uploading files to blob storage");

            foreach (var file in files)
            {
                try
                {
                    var blobClient = containerClient.GetBlobClient(file.Name);
                    using (var fileStream = File.OpenRead(file.FullName))
                    {
                        blobClient.Upload(fileStream);
                    }
                    Console.WriteLine($"{file.Name} uploaded");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        //SAAS Query token

        public async void DownloadFilesAsync(
            IEnumerable<FileInfo> files,
            string connectionString,
            string container)
        {
            var containerClient = new BlobContainerClient(connectionString, container);

            Console.WriteLine("Downloading files to blob storage");

            string FileName = "Wifi_April.pdf";
            var blobClient = containerClient.GetBlobClient(FileName);
            string path = "C:\\Users\\admin\\Desktop\\DownloadedFiles";

            using (var fileStream = File.OpenWrite(Path.Combine(path, "Wifi_April.pdf")))
            {
                blobClient.DownloadTo(fileStream);
            }
        }
    }
}
