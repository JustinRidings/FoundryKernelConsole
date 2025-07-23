using Microsoft.AI.Foundry.Local;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ClientModel;

namespace FoundryConsole;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Foundry Local Initializations
        var modelAlias = "phi-4-mini";

        // Note: If the model isn't already on-box, it will download and may take a few minutes for this step.
        var manager = await FoundryLocalManager.StartModelAsync(aliasOrModelId: modelAlias);
        var model = await manager.GetModelInfoAsync(aliasOrModelId: modelAlias);

        // "Negative Space" programming
        if (manager == null || model == null)
        {
            throw new ArgumentNullException("Trouble initializing model");
        }

        var localEndpoint = manager.Endpoint; // http://localhost:NNNNN
        var localApiKey = manager.ApiKey; // "OPEN AI APIKEY"

        // Semantic Kernel Initializations
        var builder = Kernel.CreateBuilder();

        // Add OpenAI service
        builder.Services.AddOpenAIChatCompletion(
            modelId: model.ModelId,
            endpoint: localEndpoint,
            apiKey: localApiKey
        );

        builder.Plugins.AddFromType<DateTimeUtils>("date_time_utils");

        var kernel = builder.Build();
        Console.WriteLine("------------------------------");
        Console.WriteLine("FoundryKernelConsole");
        Console.WriteLine("------------------------------");
        Console.WriteLine();
        Console.WriteLine("You are now chatting with an LLM.\n");

        ChatHistory chatHistory = new();
        IChatCompletionService chatService = kernel.Services.GetRequiredService<IChatCompletionService>();

        chatHistory.AddSystemMessage("You are an AI assistant chatting via a CLI. Provide brief and concise responses.");

        while (true)
        {
            Console.Write($"[{DateTime.Now.ToString("hh:mm:ss")}] YOU> ");
            var input = Console.ReadLine();

            if (input == "--exit")
            {
                break;
            }
            if (input != null)
            {
                chatHistory.AddUserMessage(input);
                Console.Write($"[{DateTime.Now.ToString("hh:mm:ss")}] AI> ");
                
                OpenAIPromptExecutionSettings settings = new()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(options: new() { AllowConcurrentInvocation = true, AllowParallelCalls = true }),
                };

                var result = chatService.GetStreamingChatMessageContentsAsync(chatHistory, executionSettings: settings, kernel: kernel);

                // Stream the result.
                await foreach (var chunk in result)
                {
                    Console.Write(chunk);
                }
                Console.WriteLine();
            }
        }


    }
}
