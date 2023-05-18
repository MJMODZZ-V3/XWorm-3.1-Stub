Imports System
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices

Namespace Stub
	' Token: 0x0200000F RID: 15
	Public NotInheritable Module ClipboardFunc
		' Token: 0x0600005D RID: 93 RVA: 0x00005C0C File Offset: 0x00003E0C
		Public Function GetText() As String
			Dim ReturnValue As String = String.Empty
			Dim thread As Thread = New Thread(Sub()
				ReturnValue = Clipboard.GetText()
			End Sub)
			thread.SetApartmentState(ApartmentState.STA)
			thread.Start()
			thread.Join()
			Return ReturnValue
		End Function

		' Token: 0x0600005E RID: 94 RVA: 0x00005C5C File Offset: 0x00003E5C
		Public Sub SetText(txt As String)
			Dim thread As Thread = New Thread(Sub()
				Clipboard.SetText(txt)
			End Sub)
			thread.SetApartmentState(ApartmentState.STA)
			thread.Start()
			thread.Join()
		End Sub
	End Module
End Namespace
