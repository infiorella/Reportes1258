using Colegio1258.Services.Abstract;
using System.Net.Http.Headers;
using System.Text;

namespace Colegio1258.Services.Implementation
{
    public class Mailing : IMailing
    {
        private readonly string sendGridApiKey;

        public Mailing(IConfiguration configuration)
        {
            this.sendGridApiKey = configuration.GetValue<string>("SendGridApiKey");
        }

        public async void InfoAccountCreated(string email, string name, string lastname)
        {
            try
            {
                HttpClient httpClient = new();

                var dynamicData = new
                {
                    email,
                    name,
                    lastname
                };
                List<object> to = new()
                {
                    new { email }
                };
                List<object> personalizations = new()
                {
                    new {
                        to,
                        dynamic_template_data = dynamicData
                    }
                };

                var body = new
                {
                    template_id = "d-e30b3178084c485584e98a3d640626a0",
                    from = new
                    {
                        email = "vasquezani2@gmail.com"
                    },
                    personalizations,
                };

                var uri = new Uri("https://api.sendgrid.com/v3/mail/send");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sendGridApiKey);
                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                string textResult = await response.Content.ReadAsStringAsync();

                Console.WriteLine("---res mailing.---");
                Console.WriteLine(textResult);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }
    }
}
