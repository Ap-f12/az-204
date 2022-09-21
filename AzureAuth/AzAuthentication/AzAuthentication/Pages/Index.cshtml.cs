using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AzAuthentication.Pages
{
  
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition tokenAcquisition;
        public string blob ;
        public string accessToken;

        public IndexModel(ILogger<IndexModel> logger, ITokenAcquisition _tokenAcquisition)
        {
            _logger = logger;
            tokenAcquisition =_tokenAcquisition;
        }

        public async Task OnGet()
        {

            //var scope = new string[] { "https://storage.azure.com/user_impersonation" };

            //accessToken =  await tokenAcquisition.GetAccessTokenForUserAsync(scope);

            TokenAcquisitionTokenCredential token = new TokenAcquisitionTokenCredential(tokenAcquisition);
            var blobUri = new Uri("https://azauthstorage.blob.core.windows.net/newcontainer/testupload.html");
            var blobClient = new BlobClient(blobUri, token);
            var memoryStream = new MemoryStream();
            blobClient.DownloadTo(memoryStream);
            memoryStream.Position = 0;
            var streamReader = new StreamReader(memoryStream);
            blob = streamReader.ReadToEnd();


        }
    }
}