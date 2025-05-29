using Microsoft.AI.Foundry.Local;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ClientModel;

namespace FoundryConsole;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Foundry Local Initializations
        var modelAlias = "phi-4";
        // Note: If the model isn't already on-box, it will download and may take a few minutes for this step.
        var manager = await FoundryLocalManager.StartModelAsync(aliasOrModelId: modelAlias);
        var model = await manager.GetModelInfoAsync(aliasorModelId: modelAlias);

        // "Negative Space" programming
        if(manager == null || model == null)
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

        var kernel = builder.Build();

        var result = kernel.InvokePromptStreamingAsync("Tell me a joke about AI.");

        // Stream the result.
        await foreach (var chunk in result)
        {
            Console.Write(chunk);
        }
    }
}
