#pragma once
#include <string_view>
#include <algorithm>

namespace yaoosl::logging
{
	
	class can_log
	{
		logger& m_logger;
	public:
		can_log(logger& logger) : m_logger(logger) { }
		template<class T>
		void log(T&& msg) { m_logger.log(msg); }
	};
	enum class severity
	{
		error,
		warning,
		info,
		verbose,
		trace
	};
	enum class to_string_mod
	{
		M_CASING = 0b0001,
		lowercase = 0b0000,
		upercase = 0b0001,
		M_NAME = 0b0010,
		full_name = 0b0000,
		short_name = 0b0010
	};
	constexpr to_string_mod operator|(const to_string_mod l, const to_string_mod r) { return static_cast<to_string_mod>(static_cast<uint16_t>(l) | static_cast<uint16_t>(r)); }
	constexpr to_string_mod operator&(const to_string_mod l, const to_string_mod r) { return static_cast<to_string_mod>(static_cast<uint16_t>(l) & static_cast<uint16_t>(r)); }
	constexpr std::string_view to_string(severity s, to_string_mod mod = to_string_mod::lowercase | to_string_mod::full_name)
	{
		using namespace std::string_view_literals;
		if ((mod & to_string_mod::M_CASING) == to_string_mod::lowercase && (mod & to_string_mod::M_NAME) == to_string_mod::full_name)
		{
			switch (s)
			{
			case yaoosl::logging::severity::error: return "error"sv;
			case yaoosl::logging::severity::warning: return "warning"sv;
			case yaoosl::logging::severity::info: return "info"sv;
			case yaoosl::logging::severity::verbose: return "verbose"sv;
			case yaoosl::logging::severity::trace: return "trace"sv;
			default: return "unknown"sv;
			}
		}
		else if ((mod & to_string_mod::M_CASING) == to_string_mod::lowercase && (mod & to_string_mod::M_NAME) == to_string_mod::short_name)
		{
			switch (s)
			{
			case yaoosl::logging::severity::error: return "err"sv;
			case yaoosl::logging::severity::warning: return "wrn"sv;
			case yaoosl::logging::severity::info: return "inf"sv;
			case yaoosl::logging::severity::verbose: return "vbs"sv;
			case yaoosl::logging::severity::trace: return "trc"sv;
			default: return "???"sv;
			}
		}
		else if ((mod & to_string_mod::M_CASING) == to_string_mod::upercase && (mod & to_string_mod::M_NAME) == to_string_mod::full_name)
		{
			switch (s)
			{
			case yaoosl::logging::severity::error: return "ERROR"sv;
			case yaoosl::logging::severity::warning: return "WARNING"sv;
			case yaoosl::logging::severity::info: return "INFO"sv;
			case yaoosl::logging::severity::verbose: return "VERBOSE"sv;
			case yaoosl::logging::severity::trace: return "TRACE"sv;
			default: return "UNKNOWN"sv;
			}
		}
		else if ((mod & to_string_mod::M_CASING) == to_string_mod::upercase && (mod & to_string_mod::M_NAME) == to_string_mod::short_name)
		{
			switch (s)
			{
			case yaoosl::logging::severity::error: return "ERR"sv;
			case yaoosl::logging::severity::warning: return "WRN"sv;
			case yaoosl::logging::severity::info: return "INF"sv;
			case yaoosl::logging::severity::verbose: return "VBS"sv;
			case yaoosl::logging::severity::trace: return "TRC"sv;
			default: return "???"sv;
			}
		}
		else
		{
			return "???"sv;
		}
	}

	struct position
	{
		size_t line;
		size_t column;
		size_t offset;
		size_t length;
		std::string_view file;
	};
	class message
	{
		severity m_severity;
		position m_position;
	public:
		message(severity severity, position position) : m_severity(severity), m_position(position) {}
		position position() const { return m_position; }
		severity severity() const { return m_severity; }
		virtual std::string get_message() const = 0;
		
	};
	class logger
	{
	protected:
		virtual void do_log(std::string_view source, severity s, position p, std::string message) = 0;
	public:
		template<class T>
		void log(std::string_view source, T&& msg)
		{
			static_assert(std::is_base_of<message, T>::value);
			do_log(source, msg.severity(), msg.position(), msg.get_message());
		}
	};
}