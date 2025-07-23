# 🧠 FoundryConsole App Documentation

## 📌 Overview
**FoundryConsole** is a simple C# console application that integrates **Microsoft.AI.Foundry.Local** with **Semantic Kernel** to enable local LLM (Large Language Model) chat capabilities. It initializes a local model, configures Semantic Kernel with OpenAI-compatible settings, and provides a streaming chat interface.

---

## 🚀 Features
- Initializes and hosts a local LLM using Foundry Local.
- Configures Semantic Kernel with the local model endpoint.
- Provides a real-time streaming chat interface.
- Gracefully handles model initialization failures.
- Supports exit command (`--exit`) to terminate the session.

---

## 🧱 Dependencies
Make sure the following NuGet packages are installed:
- `Microsoft.AI.Foundry.Local`
- `Microsoft.SemanticKernel`
- `Microsoft.SemanticKernel.Connectors.OpenAI`
- `System.ClientModel`

---

## 🛠️ Initialization Flow

### 1. **Start and Load Local Model**
```csharp
var manager = await FoundryLocalManager.StartModelAsync(aliasOrModelId: "phi-4-mini");
var model = await manager.GetModelInfoAsync(aliasOrModelId: "phi-4-mini");
```
- Downloads and starts the model if not already available.
- Retrieves model metadata and endpoint info.

### 2. **Validation**
```csharp
if (manager == null || model == null)
{
    throw new ArgumentNullException("Trouble initializing model");
}
```
- Ensures model and manager are properly initialized.

### 3. **Configure Semantic Kernel**
```csharp
builder.Services.AddOpenAIChatCompletion(
    modelId: model.ModelId,
    endpoint: manager.Endpoint,
    apiKey: manager.ApiKey
);
```
- Adds OpenAI-compatible chat completion service using local model.

---

## 💬 Chat Loop

### Prompt Input
```csharp
Console.Write($"[{DateTime.Now:hh:mm:ss}] YOU> ");
var input = Console.ReadLine();
```

### Exit Condition
```csharp
if (input == "--exit") break;
```

### Streaming Response
```csharp
var result = kernel.InvokePromptStreamingAsync(input);
await foreach (var chunk in result)
{
    Console.Write(chunk);
}
```
- Streams the LLM response in real time for a smooth console experience.

---

## 📎 Notes
- **Model Alias**: `"phi-4-mini"` is used as the default alias. You can change this to any supported local model.
- **Local Endpoint**: Typically resolves to `http://localhost:NNNNN`.
- **API Key**: A placeholder key used for local OpenAI-compatible authentication.

---

## 🧪 Example Session
```
------------------------------
FoundryKernelConsole
------------------------------

You are now chatting with an LLM.

[03:45:12] YOU> What's the capital of France?
[03:45:12] AI> The capital of France is Paris.
```

---

## 📤 Exit
To terminate the chat session, type:
```
--exit
```
