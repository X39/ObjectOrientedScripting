oos_class_testObj_fnc_testFncStatic = {
	private["_return"];
	scopeName "oos_class_testObj_fnc_testFncStatic";
	_this call {
		diag_log str (_this select 1);
		_return = "";
		breakTo "oos_class_testObj_fnc_testFncStatic";
	};
	_return
};
preInit = {
	private["_obj"];
	_obj = [nil, {diag_log str (_this select 1)}];
	[_obj] call {
		_thisRef = _this select 0;
		_thisRef set [0, ""];
		if(!isNil "oos_class_testObj_var_testVarStatic") then
		{
			missionNamespace setVariable ["oos_class_testObj_var_testVarStatic", ""];
		};
		[_thisRef, "arg1", "arg2"] call (_thisRef select 2);
		["arg1", "arg2"] call (missionNamespace getVariable "oos_class_testObj_fnc_testFncStatic");
	};
	[_obj, "arg1", "arg2"] call (_obj select 2);
	["arg1", "arg2"] call (missionNamespace getVariable "oos_class_testObj_fnc_testFncStatic");
	["arg1", "arg2"] call (missionNamespace getVariable "oos_class_testObj_fnc_testFncStatic");
	_foo = [{diag_log "class foo" + (_this select 1)}];
	[_foo, "normal foo"] call (_foo select 0);
	_bar = [{diag_log "class foo" + "suberbar"; diag_log "class bar" + (_this select 1);}];
	[_bar, "normal bar"] call (_bar select 0);
};