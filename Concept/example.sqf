oos_fnc_class_cFoo____constructor___ = {
	_obj = [
		[nil, "foobar"],
		[{throw "UNKNOWN FUNCTION";}, {
			diag_log "class foo" + (_this select 0);
		}]
	];
	_obj
}
oos_fnc_class_cBar____constructor___ = {
	_obj = [
		[nil, "foobar"],
		[{throw "UNKNOWN FUNCTION";}, {
			diag_log "class foo" + (_this select 0);
			diag_log "class foo" + (_this select 0);
		}]
	];
	_obj
}
oos_fnc_class_testObj_testVarStatic = nil;
oos_fnc_class_testObj_testFncStatic = {
	private["___returnValue___"];
	scopeName "functionScope";
	_this call {
		//SQF::diag_log(SQF::str(arg2));
		diag_log str (_this select 1);
		//return "";
		___returnValue___ = "";
		breakTo "functionScope";
	};
	___returnValue___
};
oos_fnc_class_testObj____constructor___ = {
	_obj = [
		["testFncPublic", "testVarPublic"],
		[{throw "UNKNOWN FUNCTION";}, {
			diag_log str (_this select 0);
		}, nil]
	];
	//this.testVarPublic = ""
	_index = ((_obj select 0) find "testVarPublic");
	if(_index == -1) then {throw "Unknown object variable ""testVarPublic""";};
	_obj set[_index, ""];
	//if(!isset(testVarStatic))
	//{
	//	testVarStatic = "";
	//}
	if(isNil "oos_fnc_class_testObj_testVarStatic") then
	{
		oos_fnc_class_testObj_testVarStatic = "";
	};
	//this.testFncPublic("arg1", "arg2");
	["arg1", "arg2"] call ((_obj select 1) select (((_obj select 0) find "testFncPublic") + 1));
	//testFncStatic("arg1", "arg2");
	_obj
}
preInit = {
	private ["_obj", "_foo", "_bar"];
	//SQF::diag_log("preInit");
	diag_log "preInit";
	//var obj = new testObj();
	_obj = [] call oos_fnc_class_testObj____constructor___;
	//obj.testFncPublic("preInitArg1", "preInitArg2");
	[_obj, "preInitArg1", "preInitArg2"] call ((_obj select 1) select (((_obj select 0) find "testFncPublic") + 1));
	//testObj::testFncStatic("preInitArg1", "preInitArg2");
	[_obj, "preInitArg1", "preInitArg2"] call oos_fnc_class_testObj_testFncStatic;
	//var foo = new cFoo();
	_foo = [] call oos_fnc_class_cFoo____constructor___;
	//foo.foobar("normal foo");
	[_foo, "normal foo"] call ((_obj select 1) select (((_obj select 0) find "foobar") + 1));
	//var bar = new cBar();
	_bar = [] call oos_fnc_class_cBar____constructor___;
	//foo.foobar("normal bar");
	[_bar, "normal bar"] call ((_obj select 1) select (((_obj select 0) find "foobar") + 1));
};
