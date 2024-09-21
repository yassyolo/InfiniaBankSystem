using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class ChatbotController : Controller
{
    private readonly HttpClient _httpClient;

    public ChatbotController(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        var requestUri = "https://azure00.openai.azure.com/"; 

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b0aa353afad74497a56a318512400391"); // Your API key

        var response = await _httpClient.PostAsync(requestUri, content);

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
