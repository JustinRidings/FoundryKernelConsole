# FoundryKernelConsole
#### A simple C# console application that leverages the Foundry Local SDK &amp; Semantic Kernel in order to interact with LLM's on your local machine.

Reference: https://github.com/microsoft/Foundry-Local/blob/75c7126a/sdk/cs/README.md#L6-L29

As of today, there is no public NuGet package available for the Foundry Local SDK. Once one is added, you can skip to Step 4.

Step 1) Clone the Foundry Local repo:

```bash
git clone https://github.com/microsoft/Foundry-Local.git
```

Step 2) Build the Foundry Local SDK to generate a local NuGet:

```bash
cd Foundry-Local\sdk\cs
dotnet build
```
Step 3) Add the local NuGet location as a Package Source in Visual Studio

Step 4) Install the Foundry Local SDK (locally built) NuGet package in your project:

```bash
dotnet add package Microsoft.AI.Foundry.Local
```

