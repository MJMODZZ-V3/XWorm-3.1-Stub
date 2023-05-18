Imports System
Imports System.Security.Cryptography
Imports System.Text

Namespace Stub
	' Token: 0x02000012 RID: 18
	Public Class AlgorithmAES
		' Token: 0x06000066 RID: 102 RVA: 0x00005F90 File Offset: 0x00004190
		Public Shared Function Decrypt(input As String) As Object
			Dim rijndaelManaged As RijndaelManaged = New RijndaelManaged()
			Dim md5CryptoServiceProvider As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
			Dim array As Byte() = New Byte(31) {}
			Dim sourceArray As Byte() = md5CryptoServiceProvider.ComputeHash(AlgorithmAES.UTF8SB(Settings.Mutex))
			Array.Copy(sourceArray, 0, array, 0, 16)
			Array.Copy(sourceArray, 0, array, 15, 16)
			rijndaelManaged.Key = array
			rijndaelManaged.Mode = CipherMode.ECB
			Dim cryptoTransform As ICryptoTransform = rijndaelManaged.CreateDecryptor()
			Dim array2 As Byte() = Convert.FromBase64String(input)
			Return AlgorithmAES.UTF8BS(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length))
		End Function

		' Token: 0x06000067 RID: 103 RVA: 0x0000601C File Offset: 0x0000421C
		Private Shared Function UTF8SB(s As String) As Byte()
			Return Encoding.UTF8.GetBytes(s)
		End Function

		' Token: 0x06000068 RID: 104 RVA: 0x00006038 File Offset: 0x00004238
		Private Shared Function UTF8BS(b As Byte()) As String
			Return Encoding.UTF8.GetString(b)
		End Function
	End Class
End Namespace
