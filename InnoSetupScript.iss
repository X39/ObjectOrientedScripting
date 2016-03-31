; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "OOS"
#define MyAppVersion "0.8.0-ALPHA"
#define MyAppPublisher "X39"
#define MyAppURL "http://x39.io/?page=projects&project=ObjectOrientedScripting"
#define MyAppExeName "WrapperUI.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{52A7EACD-C1EA-4328-B463-91F6AA9467F9}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]    
Source: "D:\GitHub\ObjectOrientedScripting\ObjectOrientedScripting\Wrapper\bin\Release\Wrapper.exe"; DestDir: "{app}"; Flags: ignoreversion    
Source: "D:\GitHub\ObjectOrientedScripting\ObjectOrientedScripting\WrapperUI\bin\Release\WrapperUI.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitHub\ObjectOrientedScripting\KnownIssues.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitHub\ObjectOrientedScripting\CompilerDlls\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "D:\GitHub\ObjectOrientedScripting\ObjectOrientedScripting\Wrapper\bin\Release\Logger.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitHub\ObjectOrientedScripting\stdLibrary\*"; DestDir: "{app}\stdLibrary\"; Flags: ignoreversion recursesubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[Code]
var
  ChangelogPage: TOutputMsgMemoWizardPage;

procedure InitializeWizard;
begin
  ChangelogPage := CreateOutputMsgMemoPage(wpWelcome, 'Changelog', 'The change history', 'You might want to check this out from time to time :)',
  'Version 0.8.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- stdLib:    Added interface ::std::ISerializable                          ' + AnsiChar(#10) +
  '    |- stdLib:    Added ::std::Vehicle(string, vec3) constructor                ' + AnsiChar(#10) +
  '    |v- stdLib:   Added `using ::std::Tupel`                                    ' + AnsiChar(#10) +
  '    ||-            Added Tupel2<T1, T2>                                         ' + AnsiChar(#10) +
  '    ||-            Added Tupel3<T1, T2, T3>                                     ' + AnsiChar(#10) +
  '    ||-            Added Tupel4<T1, T2, T3, T4>                                 ' + AnsiChar(#10) +
  '    |\-            Added Tupel5<T1, T2, T3, T4, T5>                             ' + AnsiChar(#10) +
  '    |- Compiler:  ReEnabled (& implemented) interfaces                          ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed TryCatch variable is not getting printed                ' + AnsiChar(#10) +
  '    |- Compiler:  Changed how virtual functions are adressed                    ' + AnsiChar(#10) +
  '    |- Compiler:  Changed where object meta data is located                     ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed issue in switch                                         ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed NPE in Idents on LNK errors                             ' + AnsiChar(#10) +
  '    \v- Compiler: Only one PreInit function is allowed now                      ' + AnsiChar(#10) +
  '     \-            Added new Linker exception LNK0060                           ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.7.4-ALPHA                                                             ' + AnsiChar(#10) +
  '    |v- Compiler: Fixed error in PreProcessor preventing some macros to work    ' + AnsiChar(#10) +
  '    |v- Compiler: Fixed define compiler flag                                    ' + AnsiChar(#10) +
  '    |v- Compiler: Added internal defines:                                       ' + AnsiChar(#10) +
  '    ||-            __VERSION <-- Contains current compiler version              ' + AnsiChar(#10) +
  '    ||-            __NEXCEPT(MSG) <-- Wrapper for Exception Object              ' + AnsiChar(#10) +
  '    |\                                Only working in Native Functions          ' + AnsiChar(#10) +
  '    |- Compiler:  `async` attribute functions now have to be of type script     ' + AnsiChar(#10) +
  '    |- Compiler:  Added `inline` attribute to functions                         ' + AnsiChar(#10) +
  '    |             static inline [ async ]                                       ' + AnsiChar(#10) +
  '    |                    <TYPE> <IDENT> ( <ARGLIST> ) [ throwing ] {...}        ' + AnsiChar(#10) +
  '    |- Compiler:  Added `throwing` attribute to functions                       ' + AnsiChar(#10) +
  '    |             throwing is required when using the "throw" operation.        ' + AnsiChar(#10) +
  '    |             functions that call such a function but do not catch the      ' + AnsiChar(#10) +
  '    |             potential exception will generate a warning                   ' + AnsiChar(#10) +
  '    |             Syntax:                                                       ' + AnsiChar(#10) +
  '    |             static [ inline | external ] [ async ] [ virtual ]            ' + AnsiChar(#10) +
  '    |                    <TYPE> <IDENT> ( <ARGLIST> ) throwing {...}            ' + AnsiChar(#10) +
  '    |v- Compiler: Added internal object Exception (extendable for custom)       ' + AnsiChar(#10) +
  '    ||-            catch now has to use Exception type                          ' + AnsiChar(#10) +
  '    |\-            throw now has to use Exception type                          ' + AnsiChar(#10) +
  '    |v- Compiler: updated vec3 (thx to Zeven90`s PR #48)                        ' + AnsiChar(#10) +
  '    ||-            new constructor `vec3(array<scalar>)`                        ' + AnsiChar(#10) +
  '    ||-            new member function `vec3 add(vec3 other)`                   ' + AnsiChar(#10) +
  '    ||-            new member function `vec3 diff(vec3 other)`                  ' + AnsiChar(#10) +
  '    ||-            new member function `vec3 dot(vec3 other)`                   ' + AnsiChar(#10) +
  '    ||-            new member function `vec3 cross(vec3 other)`                 ' + AnsiChar(#10) +
  '    ||-            new member function `scalar cos(vec3 other)`                 ' + AnsiChar(#10) +
  '    ||-            new member function `scalar magnitude()`                     ' + AnsiChar(#10) +
  '    ||-            new member function `scalar magnitudeSqr()`                  ' + AnsiChar(#10) +
  '    ||-            new member function `vec3 multiply(scalar n)`                ' + AnsiChar(#10) +
  '    ||-            new member function `scalar distance(vec3 other)`            ' + AnsiChar(#10) +
  '    ||-            new member function `scalar distanceSqr(vec3 other)`         ' + AnsiChar(#10) +
  '    |\-            new member function `vec3 normalized()`                      ' + AnsiChar(#10) +
  '    |- stdLib:    Updated ::std::Marker to use vec3                             ' + AnsiChar(#10) +
  '    |- stdLib:    Updated ::std::base::VehicleBase to use vec3                  ' + AnsiChar(#10) +
  '    |v- stdLib:   ::std::base::VehicleBase (thx to Zeven90`s PR #48)            ' + AnsiChar(#10) +
  '    |\-            Fixed error in `::std::Config getConfigEntry()`              ' + AnsiChar(#10) +
  '    |v- stdLib:   ::std::Man (thx to Zeven90`s PR #47)                          ' + AnsiChar(#10) +
  '    ||-            Added enum ::std::Man::VisionMode                            ' + AnsiChar(#10) +
  '    ||-            Fixed locality on `void disableAI(AiSection)` (throws now)   ' + AnsiChar(#10) +
  '    ||-            Fixed locality on `void enableAI(AiSection)` (throws now)    ' + AnsiChar(#10) +
  '    |\-            Updated enum ::std::Man::AiSection to 1.56                   ' + AnsiChar(#10) +
  '    \v- stdLib:   ::std::Group (thx to Zeven90`s PR #47)                        ' + AnsiChar(#10) +
  '     |             Fixed locality on `void delete()` (throws now)               ' + AnsiChar(#10) +
  '     |             Added `void join()`                                          ' + AnsiChar(#10) +
  '     \             Added `::std::Man getLeader()`                               ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.7.3-ALPHA                                                             ' + AnsiChar(#10) +
  '    |v- Compiler: Added `deref <ident>` operation, returns SQF name of given    ' + AnsiChar(#10) +
  '    ||            Non-Native function (virtual functions are not allowed too)   ' + AnsiChar(#10) +
  '    ||            or of given static variable                                   ' + AnsiChar(#10) +
  '    ||-           Added new Linker exception LNK0052                            ' + AnsiChar(#10) +
  '    ||            "Invalid Operation, Native functions are not derefable"       ' + AnsiChar(#10) +
  '    ||-           Added new Linker exception LNK0053                            ' + AnsiChar(#10) +
  '    ||            "Invalid Operation, Virtual Functions are not derefable"      ' + AnsiChar(#10) +
  '    ||-           Added new Linker exception LNK0054                            ' + AnsiChar(#10) +
  '    |\            "Invalid Operation, Non-Static Variables are not derefable"   ' + AnsiChar(#10) +
  '    ||-           Added new Linker exception LNK0055                            ' + AnsiChar(#10) +
  '    |\            "Invalid Operation, using this in static functions"           ' + AnsiChar(#10) +
  '    |v- Compiler: rewrote Expression code & EBNF                                ' + AnsiChar(#10) +
  '    ||v- Compiler: Expressions get parsed according to following precedence     ' + AnsiChar(#10) +
  '    |||-           First:  "+" | "-" | "*" | "/"                                ' + AnsiChar(#10) +
  '    |||-           Second: ">=" | "<=" | "==" | "<" | ">"                       ' + AnsiChar(#10) +
  '    ||\-           Third:  "&&" | "||"                                          ' + AnsiChar(#10) +
  '    ||-           EXPOP `==` is now using isEqualTo                             ' + AnsiChar(#10) +
  '    ||-           removed EXPOP `===`                                           ' + AnsiChar(#10) +
  '    ||-           removed EXPOP `&`                                             ' + AnsiChar(#10) +
  '    |\-           removed EXPOP `|`                                             ' + AnsiChar(#10) +
  '    |- Compiler:  added `bool array<T>::contains(T)` function                   ' + AnsiChar(#10) +
  '    |v- stdLib:   Added missing functions to ::std::Marker                      ' + AnsiChar(#10) +
  '    ||-            string getText()                                             ' + AnsiChar(#10) +
  '    |\-            void setText(string)                                         ' + AnsiChar(#10) +
  '    |- stdLib:    Fixed ::std::Man constructor not returning objects            ' + AnsiChar(#10) +
  '    |v- stdLib:   Altered how ::std::Group is working                           ' + AnsiChar(#10) +
  '    ||-            Removed ::std::Group::createGroup*() functions               ' + AnsiChar(#10) +
  '    ||v- stdLib:   Added ::std::Side class                                      ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asEast()                       ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asWest()                       ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asResistance()                 ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asCivilian()                   ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asLogic()                      ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asEnemy()                      ' + AnsiChar(#10) +
  '    |||-            Member Function: ::std::Side asFriendly()                   ' + AnsiChar(#10) +
  '    ||\-            Member Function: ::std::Side asUnknown()                    ' + AnsiChar(#10) +
  '    |\-            Added constructor ::std::Group::Group(::std::Side)           ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed exception when LNK0012 happens                          ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed external functions had to return                        ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed variables without value get printed in function         ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed Fixed static functions throw LNK0051                    ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed encapsulation call check (on private/protected)         ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed for loop arg1 variable not private in parents scope     ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed private functions                                       ' + AnsiChar(#10) +
  '    \- Compiler:  Fixed "_" in fnc name fucks up config.cpp                     ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.7.2-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Wrapper:   fixed Project.writeToFile(string) wrote buildfolder to        ' + AnsiChar(#10) +
  '    |             srcfolder                                                     ' + AnsiChar(#10) +
  '    |- WrapperUI: fixed saving exception which prevented saving changes         ' + AnsiChar(#10) +
  '    |- WrapperUI: fixed set buttons not getting disabled when loading files     ' + AnsiChar(#10) +
  '    |- Compiler:  fixed issue where assignment type is not chosen correctly     ' + AnsiChar(#10) +
  '    |- Compiler:  fixed member variables are "directly assigned"                ' + AnsiChar(#10) +
  '    |- Compiler:  fixed member variables w/o def val being initialized using nil' + AnsiChar(#10) +
  '    |- Compiler:  fixed missing comma printing on params for multi-arg fncs     ' + AnsiChar(#10) +
  '    |- Compiler:  fixed static native functions args get messed up with printout' + AnsiChar(#10) +
  '    |- Compiler:  fixed native functions wrappers wrong select index            ' + AnsiChar(#10) +
  '    |- Compiler:  fixed void function wrappers tried to return something        ' + AnsiChar(#10) +
  '    |- Compiler:  added internal script object (currently unused)               ' + AnsiChar(#10) +
  '    |- Compiler:  added internal floor(<scalar>) function                       ' + AnsiChar(#10) +
  '    |- Compiler:  added LNK0051 exception for variable defined twice in class   ' + AnsiChar(#10) +
  '    |-            and function                                                  ' + AnsiChar(#10) +
  '    |v- stdLib:   Updated ::std::Marker functions                               ' + AnsiChar(#10) +
  '    ||- stdLib:   new enum: Shape                                               ' + AnsiChar(#10) +
  '    ||- stdLib:   new function: void setType(string)                            ' + AnsiChar(#10) +
  '    ||- stdLib:   new function: string getType()                                ' + AnsiChar(#10) +
  '    ||- stdLib:   new function: void setShape(Shape)                            ' + AnsiChar(#10) +
  '    |\- stdLib:   new function: Shape getType()                                 ' + AnsiChar(#10) +
  '    |- stdLib:    Added delete() function to ::std::Marker                      ' + AnsiChar(#10) +
  '    |- stdLib:    Added deleteVehicle() function to ::std::base::VehicleBase    ' + AnsiChar(#10) +
  '    |- WrapperUI: WrapperUI will inform you about unsaved changes you have      ' + AnsiChar(#10) +
  '    |             made to current file upon load/close                          ' + AnsiChar(#10) +
  '    |- WrapperUI: Implemented Ressources                                        ' + AnsiChar(#10) +
  '    \- Compiler:  Implemented Ressources                                        ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  '    |- Compiler:  fixed member functions without arguments lacked params command' + AnsiChar(#10) +
  '    |- Compiler:  fixed callWrapper `___tmp___ = ___tmp___` printout            ' + AnsiChar(#10) +
  '    |- Compiler:  fixed breakout missed left args brackets                      ' + AnsiChar(#10) +
  '    |- Compiler:  fixed invalid LNKxxxx exception when function is not existing ' + AnsiChar(#10) +
  '    |- Compiler:  fixed ident call wrapper used ___tmp___ AND _tmp              ' + AnsiChar(#10) +
  '    |- Compiler:  fixed some other stuff (meh ... do not ask :) tiny things)    ' + AnsiChar(#10) +
  '    |- Compiler:  fixed loops not printing scopeName instruction                ' + AnsiChar(#10) +
  '    |- Compiler:  "using" files did not got preprocessed                        ' + AnsiChar(#10) +
  '    |- Compiler:  fixed third for argument is printed at the very begining      ' + AnsiChar(#10) +
  '    |- Compiler:  fixed native function calls do not process last argument      ' + AnsiChar(#10) +
  '    |- Compiler:  fixed passing <IDENT>.<IDENT> for a native function (and some ' + AnsiChar(#10) +
  '    |             other things too ... kinda hard to explain ^^)                ' + AnsiChar(#10) +
  '    |- Compiler:  fixed objects require auto keyword inside of code             ' + AnsiChar(#10) +
  '    |- Compiler:  added static native functions                                 ' + AnsiChar(#10) +
  '    |- Compiler:  added external function references using the external keyword ' + AnsiChar(#10) +
  '    |             static external [ async ] <TYPE> <IDENT> ( <ARGLIST> );       ' + AnsiChar(#10) +
  '    |- Compiler:  added foreach operation                                       ' + AnsiChar(#10) +
  '    |             foreach( <VARTYPE> <IDENT> in <IDENT> )                       ' + AnsiChar(#10) +
  '    |- Compiler:  added vec3 object to OOS                                      ' + AnsiChar(#10) +
  '    |v- Compiler: added native functions to OOS                                 ' + AnsiChar(#10) +
  '    ||-           isServer()                                                    ' + AnsiChar(#10) +
  '    ||-           sleep(scalar)                                                 ' + AnsiChar(#10) +
  '    |\-           isDedicated()                                                 ' + AnsiChar(#10) +
  '    |- stdLib:    added ::std::getPlayer static function to ::std::Man          ' + AnsiChar(#10) +
  '    |- stdLib:    added AiSection enum to ::std::Man                            ' + AnsiChar(#10) +
  '    |- stdLib:    added enableAI function to ::std::Man                         ' + AnsiChar(#10) +
  '    |- stdLib:    added disableAI function to ::std::Man                        ' + AnsiChar(#10) +
  '    |- stdLib:    fixed invalid return type in getObject of ::std::Context      ' + AnsiChar(#10) +
  '    |- stdLib:    fixed script issue on ::std::VehicleBase::setDamage(scalar)   ' + AnsiChar(#10) +
  '    |- stdLib:    fixed invalid string argument on ::std::Config::count()       ' + AnsiChar(#10) +
  '    |- stdLib:    fixed potential script issue in all native objects            ' + AnsiChar(#10) +
  '    \- stdLib:    changed function arglists of ::std::Context                   ' + AnsiChar(#10) +   
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.7.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  fixed objects added using the using instruction where not     ' + AnsiChar(#10) +
  '    |             touched by the PreProcessor                                   ' + AnsiChar(#10) +
  '    |- Compiler:  printout syntax got altered slightly (missing tabs and invalid' + AnsiChar(#10) +
  '    |             new lines)                                                    ' + AnsiChar(#10) +
  '    |- Compiler:  fixed for required all params or it would throw a NPE         ' + AnsiChar(#10) +
  '    |- Compiler:  fixed NPE when assigning variables in other namespaces        ' + AnsiChar(#10) +
  '    |- Compiler:  fixed StackOverflow case with this variable                   ' + AnsiChar(#10) +
  '    |- Compiler:  using directive threw out folder path instead of file path    ' + AnsiChar(#10) +
  '    |- Compiler:  using now is generalized (no difference between local and std ' + AnsiChar(#10) +
  '    |             includes) thus syntax changed:                                ' + AnsiChar(#10) +
  '    |             using ::foo::bar                                              ' + AnsiChar(#10) +
  '    |             instead of                                                    ' + AnsiChar(#10) +
  '    |             using "::foo::bar" or using <::foo::bar>                      ' + AnsiChar(#10) +
  '    |- Compiler:  fixed invalid encapsulation on object function "toString"     ' + AnsiChar(#10) +
  '    |- Compiler:  Changed cast operator from %...% to <...>                     ' + AnsiChar(#10) +
  '    |- Compiler:  fixed templates only could use native types                   ' + AnsiChar(#10) +
  '    |- Compiler:  added internal class "array"                                  ' + AnsiChar(#10) +
  '    |- Compiler:  added internal class "string"                                 ' + AnsiChar(#10) +
  '    |- Compiler:  Parser is more solid now (side effect thx to EBNF changes)    ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed CfgFunctions class is never written out regardless of   ' + AnsiChar(#10) +
  '    |             the NFNC flag                                                 ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed invalid distinction between variable and function when  ' + AnsiChar(#10) +
  '    |             using a class variable with an object as type                 ' + AnsiChar(#10) +
  '    |- Compiler:  class & native syntax now supports flags                      ' + AnsiChar(#10) +
  '    |             (not important for generic oos users)                         ' + AnsiChar(#10) +
  '    |             `class <IDENT> flags <FLAG1> <FLAG2> <FLAGN>`                 ' + AnsiChar(#10) +
  '    |             Flags which got introduced:                                   ' + AnsiChar(#10) +
  '    |             - disableConstructor                                          ' + AnsiChar(#10) +
  '    |             - noObjectExtends                                             ' + AnsiChar(#10) +
  '    |             - virtualFunctionsOnly (not available in native)              ' + AnsiChar(#10) +
  '    |- Compiler:  Introduced enum`s. They got following syntax:                 ' + AnsiChar(#10) +
  '    |             enum { <IDENT1> [ = <VALUE2> ], <IDENTN> [ = <VALUEN> ] }     ' + AnsiChar(#10) +
  '    |- Compiler:  SQF instruction now supports forcedType via "as". Example:    ' + AnsiChar(#10) +
  '    |             `SQF allPlayers as ::std::Array<::std::Men>`                  ' + AnsiChar(#10) +
  '    |- stdLib:    added ::std::UI::Display object                               ' + AnsiChar(#10) +
  '    |- stdLib:    added ::std::UI::Control object                               ' + AnsiChar(#10) +
  '    |- stdLib:    added ::std::Marker object                                    ' + AnsiChar(#10) +
  '    |- stdLib:    fixed invalid typing in ::std::base::VehicleBase object       ' + AnsiChar(#10) +
  '    |- stdLib:    removed ::std::Array object (moved to internal classes)       ' + AnsiChar(#10) +
  '    |- stdLib:    removed ::std::String object (moved to internal classes)      ' + AnsiChar(#10) +
  '    |- stdLib:    added get-/setObject function to ::std::Context               ' + AnsiChar(#10) +
  '    \- .oosproj:  Added <srcfolder> attribute to set the source folder          ' + AnsiChar(#10) +      
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.6.2-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  fixed various minor issues                                    ' + AnsiChar(#10) +
  '    |- Compiler:  removed multi-base classes                                    ' + AnsiChar(#10) +
  '    |- Compiler:  improved object structure (==> less overhead)                 ' + AnsiChar(#10) +
  '    |- Compiler:  fixed overloaded functions overwriting themself               ' + AnsiChar(#10) +
  '    |- Compiler:  fixed different case same name functions overwriting themself ' + AnsiChar(#10) +
  '    |- Compiler:  temporary disabled interfaces as function argument            ' + AnsiChar(#10) +
  '    |- Compiler:  fixedasync keyword made functions being callen instead of     ' + AnsiChar(#10) +
  '    |             spawned (and vice versa)                                      ' + AnsiChar(#10) +
  '    |- Compiler:  unlocked the native "object" object (actually thats a speciall' + AnsiChar(#10) +
  '    \             internal class now, might happen with string too soon)        ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.6.1-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  fixed InstanceOf printout                                     ' + AnsiChar(#10) +
  '    |- Compiler:  Introduced "using" operation, replaces #include               ' + AnsiChar(#10) +
  '    |- Compiler:  #include now wont "include" the file anymore and instead just ' + AnsiChar(#10) +
  '    |             checks the PreProcessor directives in given file              ' + AnsiChar(#10) +
  '    |- Compiler:  Added "extends" keyword to native classes --> you now can     ' + AnsiChar(#10) +
  '    |             extend native classes                                         ' + AnsiChar(#10) +
  '    |- Compiler:  Added code for the "async" keyword on functions (sorry)       ' + AnsiChar(#10) +
  '    |- Compiler:  "PRINTMODE" flag modes have changed:                          ' + AnsiChar(#10) +
  '    |             Possible modes are: NONE, 0, NEEDED, 1, ALL, 2                ' + AnsiChar(#10) +
  '    |- Compiler:  Renamed all stdLibrary objects to have first char uppercase   ' + AnsiChar(#10) +
  '    |- Compiler:  Added "VehicleBase" object to stdLibrary (not intended to be  ' + AnsiChar(#10) +
  '    |             created via new, will throw an error when you attempt)        ' + AnsiChar(#10) +
  '    |- Compiler:  Added "Man" object to stdLibrary, represents all CAManBase    ' + AnsiChar(#10) +
  '    |             ArmA objects                                                  ' + AnsiChar(#10) +
  '    \- WrapperUI: Introduced WrapperUI.exe, a UI for OOS <3 (to be improved)    ' + AnsiChar(#10) +    
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.6.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  Rewrote entire Linker & Writer                                ' + AnsiChar(#10) +
  '    |- Compiler:  Added !syntax! to call base constructors via following:       ' + AnsiChar(#10) +
  '    |             bar(arg1, arg2, argn) : foo(arg1, "foobar", argn) {...}       ' + AnsiChar(#10) +
  '    |- Compiler:  During Linking, functions will now check if they always return' + AnsiChar(#10) +
  '    |             (only exception for this are VOID functions)                  ' + AnsiChar(#10) +
  '    |- Compiler:  Added "PRINTMODE=<MODE>" Flag                                 ' + AnsiChar(#10) +
  '    |             Possible modes are: NONE, 0, NEEDED, 1, PARTIAL, 2, ALL, 3    ' + AnsiChar(#10) +
  '    |- Compiler:  Added fast assign operators: ++, --, +=, -=, *=, /=           ' + AnsiChar(#10) +
  '    |- Compiler:  Reorganized how overwrite should be used (see INFO1)          ' + AnsiChar(#10) +
  '    |- Compiler:  Added "async" keyword for functions (see INFO1)               ' + AnsiChar(#10) +
  '    |             async functions have to have the return type void!            ' + AnsiChar(#10) +
  '    |- INFO1:     Functions syntax got altered:                                 ' + AnsiChar(#10) +
  '    \             <encapsulation> [async] [override] <type> <name> ( <argList> )' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.5.3-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed invalid type cast with template objects                 ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed bug in PreProcessor which caused invalid output if a    ' + AnsiChar(#10) +
  '    |             line had whitespace characters after its last valid character ' + AnsiChar(#10) +
  '    |- Compiler:  SQF command arguments where not separated by commas           ' + AnsiChar(#10) +
  '    |- Compiler:  Added "scalar length()" function to ::std::array              ' + AnsiChar(#10) +
  '    |- Compiler:  Added "config" object to stdLibrary                           ' + AnsiChar(#10) +
  '    |- Compiler:  Added "namespace" object to stdLibrary                        ' + AnsiChar(#10) +
  '    |- Compiler:  Added "vehicle" object to stdLibrary                          ' + AnsiChar(#10) +
  '    |- Compiler:  PreProcessor allowed multiple includes of the same file ref.  ' + AnsiChar(#10) +
  '    |- Compiler:  Parent classes/interfaces now need to be adressed with        ' + AnsiChar(#10) +
  '    |             extends (for classes) or implements (for interfaces)          ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.5.2-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed output folder is not getting generated if not existing  ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed typeless functions are all recognized as constructor    ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed classes are lacking a cfgConfig layer                   ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed namespace static variables lacked semicolon in EBNF     ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed object functions not getting object parameter passed    ' + AnsiChar(#10) +
  '    |- Compiler:  Added output folder cleanup                                   ' + AnsiChar(#10) +
  '    |- Compiler:  Flag /NOCLEANUP which prevents output folder cleanup          ' + AnsiChar(#10) +
  '    |- Compiler:  PreProcessor now supports stdLibrary #include (using < >)     ' + AnsiChar(#10) +
  '    |- Compiler:  Added additional keyword "boolean" for bool types             ' + AnsiChar(#10) +
  '    |- Compiler:  New CompileErrors: C0049                                      ' + AnsiChar(#10) +
  '    |- Compiler:  Added NativeClasses                                           ' + AnsiChar(#10) +
  '    \- Compiler:  Added array object to stdLib                                  ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.5.1-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Wrapper:   Fixed naming of -gen param (poject.oosproj instead of         ' + AnsiChar(#10) +
  '    |             project.oosproj)                                              ' + AnsiChar(#10) +
  '    |- Wrapper:   Fixed "URI-Format not supported" message when not forcing     ' + AnsiChar(#10) +
  '    |             a DLL (dll lookup now works as expected -.-*)                 ' + AnsiChar(#10) +
  '    \- Compiler:  Fixed functions getting invalidly recognized as duplicate     ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.5.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Wrapper:   Fixed -gen is not working if file is not existing             ' + AnsiChar(#10) +
  '    |             (also if file was existing ... but expected error then)       ' + AnsiChar(#10) +
  '    |- Compiler:  Flag /DEFINE="#whatever(arg) dosomething with arg"            ' + AnsiChar(#10) +
  '    |- Compiler:  Flag /THISVAR="_thisvarname_"                                 ' + AnsiChar(#10) +
  '    |- Compiler:  PreProcessor replaced non-keywords when they just contained a ' + AnsiChar(#10) +
  '    |             part of the keyword (EG. keyword was FOO, FOOBAR would have   ' + AnsiChar(#10) +
  '    |             been replaced with <CONTENT>BAR)                              ' + AnsiChar(#10) +
  '    |- Compiler:  PreProcessor now supports "merge" operator ##                 ' + AnsiChar(#10) +
  '    |             #define FOO(BAR) BAR##FOOBAR                                  ' + AnsiChar(#10) +
  '    |             FOO(test) => testFOOBAR                                       ' + AnsiChar(#10) +
  '    \v- Compiler: GEN2 Implementation                                           ' + AnsiChar(#10) +
  '     |-           New Syntax                                                    ' + AnsiChar(#10) +
  '     |-           New SQF ObjectStructure                                       ' + AnsiChar(#10) +
  '     |-           Type Restriction (with that all stuff that is connected to it)' + AnsiChar(#10) +
  '     |-           Interfaces (and with them virtual functions)                  ' + AnsiChar(#10) +
  '     |-           "Linker" issues with proper issue IDs                         ' + AnsiChar(#10) +
  '     |            (currently room for 4 digits (so 0001 - 9999))                ' + AnsiChar(#10) +
  '     \-           No unneeded overhead anymore                                  ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.4.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Wrapper:   Now returns -1 if was not successfully                        ' + AnsiChar(#10) +
  '    |- Wrapper:   Added "setFlags(string[])" function to ICompiler interface    ' + AnsiChar(#10) +
  '    |- Wrapper:   Fixed compilerDLL search location                             ' + AnsiChar(#10) +
  '    |             Working dir (applicationside) was checked                     ' + AnsiChar(#10) +
  '    |             and not executable dir                                        ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed naming of functions in output config file               ' + AnsiChar(#10) +
  '    |             being incorrect                                               ' + AnsiChar(#10) +
  '    |- Compiler:  Added flag /CLFN with STRING value ("/CLFN=blabla.cfg")       ' + AnsiChar(#10) +
  '    |             Sets how the output config will be named                      ' + AnsiChar(#10) +
  '    |- Compiler:  Added flag /NFNC                                              ' + AnsiChar(#10) +
  '    \             Removes the CfgFunctions class from the config file           ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.3.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |- Compiler:  changed block native code from:                               ' + AnsiChar(#10) +
  '    |                 native <instructions> endnative                           ' + AnsiChar(#10) +
  '    |             to:                                                           ' + AnsiChar(#10) +
  '    |                 startnative <instructions> endnative                      ' + AnsiChar(#10) +
  '    |- Compiler:  Added "native(<instructions>)" specially for expressions      ' + AnsiChar(#10) +
  '    |             (will be merged at some point with the block native again)    ' + AnsiChar(#10) +
  '    |- Compiler:  Added SQF Call instruction:                                   ' + AnsiChar(#10) +
  '    |                SQF [ (>arg1>, <argN>) ] <instruction> [ (>arg1>, <argN>) ]' + AnsiChar(#10) +
  '    |- Compiler:  Added missing detection for                                   ' + AnsiChar(#10) +
  '    |             unsigned integer/double values in VALUE                       ' + AnsiChar(#10) +
  '    |- Compiler:  Added missing detection for                                   ' + AnsiChar(#10) +
  '    |             >, >=, <, <= operations in EXPRESSION                         ' + AnsiChar(#10) +
  '    |- Compiler:  Added missing LOCALVARIABLE alternative for FORLOOP           ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed FORLOOP                                                 ' + AnsiChar(#10) +
  '    \- Compiler:  PrettyPrint sqf output improved                               ' + AnsiChar(#10) +
  '                                                                                ' + AnsiChar(#10) +
  'Version 0.2.0-ALPHA                                                             ' + AnsiChar(#10) +
  '    |v- Wrapper:  New Parameters                                                ' + AnsiChar(#10) +
  '    ||-           "sc=<FILE>"    Used to check the syntax of some document      ' + AnsiChar(#10) +
  '    ||-           "dll=<FILE>"   Forces given dll (ignores project settings)    ' + AnsiChar(#10) +
  '    |\-           "log[=<FILE>]" Enables LogToFile                              ' + AnsiChar(#10) +
  '    |                            (with optional file parameter)                 ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed TryCatch                                                ' + AnsiChar(#10) +
  '    |- Compiler:  Fixed Expressions                                             ' + AnsiChar(#10) +
  '    |- Compiler:  Implemented class inheritance                                 ' + AnsiChar(#10) +
  '    |- Compiler:  Implemented public/private encapsulation                      ' + AnsiChar(#10) +
  '    |- Compiler:  when parsing error was found the objectTree                   ' + AnsiChar(#10) +
  '    |             wont get written out anymore                                  ' + AnsiChar(#10) +
  '    |- Wrapper:   Fixed ArgumentDetection (foo=bar was not detected)            ' + AnsiChar(#10) +
  '    \- Logger:    Disabled logToFile per default                                '
  );
end;