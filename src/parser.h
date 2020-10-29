#pragma once
#include "cstnode.hpp"
#include "tokenizer.hpp"
#include "logging.h"

#include <optional>
#include <queue>
#include <vector>

namespace yaoosl::compiler
{
	class parser : public yaoosl::logging::can_log
	{
		class index_marker
		{
			parser& ref;
			size_t cur_indx;
		public:
			index_marker(parser& p) :
				ref(p),
				cur_indx(p.m_tokens_index)
			{}
			void rollback()
			{
				ref.m_tokens_index = cur_indx;
			}
		};
		tokenizer m_tokenizer;
		size_t m_tokens_index;
		std::vector<tokenizer::token> m_tokens;
		tokenizer::token next_token() {
			if (m_tokens_index == m_tokens.size())
			{
				auto token = m_tokenizer.next();
				m_tokens.push_back(token);
			}
			auto ret_token = m_tokens[m_tokens_index++];
			return ret_token;
		}
		tokenizer::token current_token() {
			auto ret_token = m_tokens[m_tokens_index];
			return ret_token;
		}
		tokenizer::token look_ahead_token(size_t len = 1) {
			auto index = m_tokens_index;
			tokenizer::token t;
			while (len-- > 0)
			{
				t = next_token();
			}
			m_tokens_index = index;
			return t;
		}
		void undo_token(size_t amount = 1) {
			if (m_tokens_index < amount)
			{
				m_tokens_index = 0;
			}
			else
			{
				m_tokens_index -= amount;
			}
		}
		index_marker mark()
		{
			return index_marker(*this);
		}
	public:
		parser(yaoosl::logging::logger logger, tokenizer&& tokenizer) :
			yaoosl::logging::can_log(logger),
			m_tokenizer(tokenizer),
			m_tokens_index(0),
			m_tokens()
		{}

		std::optional<yaoosl::compiler::cstnode> p_start(bool require);
		std::optional<yaoosl::compiler::cstnode> p_code_statements(bool require, bool allow_this);
		std::optional<yaoosl::compiler::cstnode> p_statements(bool require);
		std::optional<yaoosl::compiler::cstnode> p_file_statements(bool require);
		std::optional<yaoosl::compiler::cstnode> p_class(bool require);
		std::optional<yaoosl::compiler::cstnode> p_class_head(bool require, tokenizer::token* OUT_class_name_literal);
		std::optional<yaoosl::compiler::cstnode> p_class_body(bool require, tokenizer::token class_name_literal);
		std::optional<yaoosl::compiler::cstnode> p_class_statements(bool require, tokenizer::token class_name_literal);
		std::optional<yaoosl::compiler::cstnode> p_class_member_head(bool require, bool allow_instance, bool* OUT_is_unbound);
		std::optional<yaoosl::compiler::cstnode> p_namespace(bool require);
		std::optional<yaoosl::compiler::cstnode> p_property(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_property_body(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_property_get(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_property_set(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_method(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_method_arg(bool require, bool allow_default = true);
		std::optional<yaoosl::compiler::cstnode> p_method_arg_list(bool require);
		std::optional<yaoosl::compiler::cstnode> p_method_parameters(bool require);
		std::optional<yaoosl::compiler::cstnode> p_method_scope(bool require, bool allow_this);
		std::optional<yaoosl::compiler::cstnode> p_conversion(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_operator(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_operator_head(bool require);
		std::optional<yaoosl::compiler::cstnode> p_constructor(bool require, tokenizer::token class_name_literal);
		std::optional<yaoosl::compiler::cstnode> p_destructor(bool require, tokenizer::token class_name_literal);
		std::optional<yaoosl::compiler::cstnode> p_using(bool require);
		std::optional<yaoosl::compiler::cstnode> p_if_else(bool require);
		std::optional<yaoosl::compiler::cstnode> p_for(bool require);
		std::optional<yaoosl::compiler::cstnode> p_try_catch_finally(bool require);
		std::optional<yaoosl::compiler::cstnode> p_while(bool require);
		std::optional<yaoosl::compiler::cstnode> p_switch(bool require);
		std::optional<yaoosl::compiler::cstnode> p_label(bool require);
		std::optional<yaoosl::compiler::cstnode> p_case(bool require);
		std::optional<yaoosl::compiler::cstnode> p_enum(bool require);
		std::optional<yaoosl::compiler::cstnode> p_enum_head(bool require);
		std::optional<yaoosl::compiler::cstnode> p_enum_body(bool require);
		std::optional<yaoosl::compiler::cstnode> p_enum_statements(bool require);
		std::optional<yaoosl::compiler::cstnode> p_enum_value(bool require);
		std::optional<yaoosl::compiler::cstnode> p_template_definition(bool require);
		std::optional<yaoosl::compiler::cstnode> p_type_ident(bool require);
		std::optional<yaoosl::compiler::cstnode> p_type_list(bool require);
		std::optional<yaoosl::compiler::cstnode> p_type(bool require);
		std::optional<yaoosl::compiler::cstnode> p_encapsulation(bool require, bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_value(bool require);
		std::optional<yaoosl::compiler::cstnode> p_value_constant(bool require);
	};
}