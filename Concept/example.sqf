OOS_testObj_fnc_testFncStatic = {
	private["_arg1","_arg2"];
	scopeName "fnc";
	params ["_obj""_arg1","_arg2"];
	
	diag_log(str(_arg2));
	("") breakOut "fnc";
};
OOS_testObj_fnc____constructor___eol = {
	private "_obj";
	_obj = [
		["testVarPublic","testFncPublic"],
		["testVarPublic","testFncPublic"],
		[
			{throw "UNKNOWN FUNCTION";},
			nil,
			{
				private["_arg1","_arg2"];
				scopeName "fnc";
				params ["_obj","_arg1","_arg2"];
				diag_log(str(_arg1));

			}
		],
		["testObj", ["testObj"]]
	];
	private[];
	scopeName "fnc";
	params ["_obj"];
	
	(_obj select 2) set [((_obj select 1) find "testVarPublic") + 1, ("")];
	if(!(isNil {(oos_testObj_fnc_testVarStatic)})) then
	{
		testVarStatic = ("");
	};
	[_obj, ("arg1"), ("arg2")] call ((_obj select 2) select (((_obj select 1) find "testFncPublic") + 1));
	[ ("arg1"), ("arg2")] call oos_testObj_fnc_testFncStatic;
	_obj
};

OOS_fnc_returnTrue = {
	private[];
	scopeName "fnc";
	params ["_obj"];
	
	(true) breakOut "fnc";
}
OOS_fnc_preInit = {
	if(isNil"OOS_testObj_fnc_testVarStatic") then {missionNamespace setVariable["OOS_testObj_fnc_testVarStatic",nil];};
	
	private["_obj","_testing"];
	scopeName "fnc";
	params ["_obj"];
	
	diag_log("preInit");
	_obj = ([] call oos_fnc_testObj____constructor___eol);
	[_obj, ("preInitArg1"), ("preInitArg2")] call ((_obj select 2) select (((_obj select 0) find "testFncPublic") + 1));
	[ ("preInitArg1"), ("preInitArg2")] call oos_testObj_fnc_testFncStatic;
	_testing = (0);
	_test = (0);
	while {(_test) < ((10))} do
	{
		scopeName "breakable";
		diag_log("test");
		_test = _test - 1;
	};
	switch ((alive(player))) do
	{
		case true: {
			try
			{
			}
			catch
			{
				test = _exception;
				throw ("foobar");
				diag_log(test);
			};
		};
		default {
			if(([] call oos_fnc_returnTrue)) then
			{
				systemChat("aprooved");
			}
			else
			{
				systemChat("nop");
			};
		};
	};
	while {(true)} do
	{
		scopeName "breakable";
		breakOut "breakable";
	};
};
