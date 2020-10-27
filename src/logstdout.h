#pragma once
#include "logging.h"
#include <iostream>
#include <iomanip>

namespace yaoosl::logging
{
	class logstdout : public logger
	{
	protected:
		// Inherited via logger
		virtual void do_log(std::string_view source, severity s, position p, std::string message) override
		{
			std::cout << "[" << source << "]" <<
				"[" << to_string(s, to_string_mod::upercase | to_string_mod::short_name) << "]" <<
				"[" << "L" << std::setw(5) << p.line << "|" << "C" << std::setw(5) << p.column << "|" << "F'" << p.file << "'" << "]" <<
				"  " << message << "\n";
		}
	};
}