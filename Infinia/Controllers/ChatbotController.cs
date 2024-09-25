using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class ChatbotController : Controller
{
    private readonly HttpClient httpClient;

    public ChatbotController()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://chatbotniki.azurewebsites.net/");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] MessageModel messageModel)
    {
        if (messageModel == null || string.IsNullOrEmpty(messageModel.UserMessage))
        {
            return BadRequest("Invalid message");
        }

        var responseMessage = await GetChatbotResponse(messageModel.UserMessage);
        return Json(new { response = responseMessage });
    }

    private async Task<string> GetChatbotResponse(string userMessage)
    {
        var payload = new
        {
            message = userMessage
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("chat", content);

        response.EnsureSuccessStatusCode();


        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(jsonResponse);
            return result.response; 
        }

        return "Error: Unable to get response from chatbot.";
    }
}

public class MessageModel
{
    public string UserMessage { get; set; }
}
