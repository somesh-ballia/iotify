Dim fso, strFwkDir
Dim ServiceDir, GacDLL, OldServiceFile
On Error Resume Next

' Create filesystem and shell objects

set shell = CreateObject("WScript.shell")
set fso = CreateObject("Scripting.FileSystemObject")

'This removes the service regardless of location or status
strComputer = "."
Set objWMIService = GetObject("winmgmts:" _
    & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")

Set colListOfServices = objWMIService.ExecQuery _
    ("Select * from Win32_Service Where Name = 'KLA-Tencor Generic Email Engine'")

For Each objService in colListOfServices
    objService.StopService()
    objService.Delete()
Next
        
ServiceDir = shell.ExpandEnvironmentStrings("%ProgramFiles%\IotFy\SerialOverIP\SerialOverIPService.exe")
' Get the environment variable for the Windows installation and append the assumed .NET path

strFwkDir = shell.ExpandEnvironmentStrings("%WinDir%" & "\Microsoft.NET\Framework64\v4.0.30319\")

'  Install the service , params - keep the window hidden (0) , don't wait (false)

If fso.FileExists(strFwkDir & "InstallUtil.exe") then  
If fso.FileExists(ServiceDir) then 
   ' shell.currentdirectory =  strFwkDir
	shell.run chr(34) & strFwkDir & "InstallUtil.exe" & chr(34) & " " & chr(34) & ServiceDir & chr(34), 0, false
End If
	
End If

