using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundryConsole;

public class DateTimeUtils
{
    public DateTimeUtils()
    {

    }

    [KernelFunction("get_current_time")]
    [Description("Gets the current time")]
    public async Task<string> GetCurrentTime()
    {
        await Task.CompletedTask;
        return DateTime.Now.ToString("hh:mm:ss");
    }

    [KernelFunction("get_current_date")]
    [Description("Gets the current date")]
    public async Task<string> GetCurrentDate()
    {
        await Task.CompletedTask;
        return DateTime.Now.ToString("dd:MM:yyyy");
    }
}
