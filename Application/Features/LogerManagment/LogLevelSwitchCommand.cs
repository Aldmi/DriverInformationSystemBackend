using Application.Common.LogServices;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.LogerManagment;

[ApiController]
[Route("[controller]/[action]")]
public class LogLevelSwitchController : ControllerBase
{
    private readonly LogLevelSwitchService _logLevelSwitcher;
    public LogLevelSwitchController(LogLevelSwitchService logLevelSwitcher)
    {
        _logLevelSwitcher = logLevelSwitcher;
    }
    
    [HttpGet]
    public IActionResult GetReport()
    {
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    [HttpPut]
    public IActionResult ConsoleDebug()
    {
        _logLevelSwitcher.SwitchLevel2ConsoleDebug();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }

    [HttpPut]
    public IActionResult ConsoleInfo()
    {
        _logLevelSwitcher.SwitchLevel2ConsoleInfo();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    [HttpPut]
    public IActionResult StructureLogingDebug()
    {
        _logLevelSwitcher.SwitchLevel2StructureLogingDebug();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    [HttpPut]
    public IActionResult StructureLogingInfo()
    {
        _logLevelSwitcher.SwitchLevel2StructureLogingInfo();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    
    [HttpPut]
    public IActionResult StructureLogingInfoAndConsoleInfo()
    {
        _logLevelSwitcher.SwitchLevel2StructureLogingInfoAndConsoleInfo();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    [HttpPut]
    public IActionResult StructureLogingInfoAndConsoleWarning()
    {
        _logLevelSwitcher.SwitchLevel2StructureLogingInfoAndConsoleWarning();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
        
        
    [HttpPut]
    public IActionResult StructureLogingWarningAndConsoleInfo()
    {
        _logLevelSwitcher.SwitchLevel2StructureLogingWarningAndConsoleInfo();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    
    [HttpPut]
    public IActionResult StructureLogingWarningAndConsoleWarning()
    {
        _logLevelSwitcher.SwitchLevel2StructureLogingWarningAndConsoleWarning();
        var resp = _logLevelSwitcher.GetLevelSwitcherReport();
        return Ok(resp);
    }
    
    

}
