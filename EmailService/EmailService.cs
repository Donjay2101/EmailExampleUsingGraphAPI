using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailService
    {

        private GraphServiceClient GetProvider()
        {
            var clientId = "ClientID";
            var clientSecret = "ClientSecret";
            var tenantID = "TenantID";
            List<string> scopes = new List<string>() { "https://graph.microsoft.com/.default" } ;
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(clientId)
            .WithTenantId(tenantID)
            .WithClientSecret(clientSecret)
            .Build();

            GraphServiceClient graphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) => {
                    // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
                    var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                    // Add the access token in the Authorization header of the API
                    requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                })
            );
            return graphServiceClient;
        }

        public async Task SendEmail()
        {
            var client = GetProvider();
            var message = new Message
            {
                Subject = "Test",
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = "Email Content"
                },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "deepak.mehta@petronas.com"
                        }
                    }
                },
                CcRecipients = new List<Recipient>()
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "deepak.mehta@petronas.com"
                        }
                    }
                }
            };
            //await client.Me.SendMail(message, false).Request().PostAsync();
            await client.Users["a-abhijit@pethlab.com"].SendMail(message, false).Request().PostAsync();

        }
    }
}
