set CurDir=%~dp0
set LibName=%1
pdb2mdb %LibName%.dll
xcopy %LibName%.dll %CurDir%..\..\Framework\Assets\Plugins\ /y
xcopy %LibName%.dll.mdb %CurDir%..\..\Framework\Assets\Plugins\ /y