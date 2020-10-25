#pragma once
#include "cstnode.hpp"
#include "tokenizer.hpp"

#include <optional>
#include <queue>
#include <vector>

namespace yaoosl::compiler
{
	class parser
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
		parser(tokenizer&& tokenizer) :
			m_tokenizer(tokenizer),
			m_tokens_index(0),
			m_tokens()
		{}

		std::optional<yaoosl::compiler::cstnode> p_start();
		std::optional<yaoosl::compiler::cstnode> p_file_statements();
		std::optional<yaoosl::compiler::cstnode> p_class();
		std::optional<yaoosl::compiler::cstnode> p_class_head();
		std::optional<yaoosl::compiler::cstnode> p_class_body();
		std::optional<yaoosl::compiler::cstnode> p_class_statements();
		std::optional<yaoosl::compiler::cstnode> p_namespace();
		std::optional<yaoosl::compiler::cstnode> p_method(bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_method_head();
		std::optional<yaoosl::compiler::cstnode> p_method_parameters();
		std::optional<yaoosl::compiler::cstnode> p_method_body();
		std::optional<yaoosl::compiler::cstnode> p_conversion(bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_operator(bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_constructor(tokenizer::token class_name_literal);
		std::optional<yaoosl::compiler::cstnode> p_using();
		std::optional<yaoosl::compiler::cstnode> p_enum();
		std::optional<yaoosl::compiler::cstnode> p_enum_head();
		std::optional<yaoosl::compiler::cstnode> p_enum_body();
		std::optional<yaoosl::compiler::cstnode> p_enum_statements();
		std::optional<yaoosl::compiler::cstnode> p_enum_value();
		std::optional<yaoosl::compiler::cstnode> p_template_definition();
		std::optional<yaoosl::compiler::cstnode> p_type_ident();
		std::optional<yaoosl::compiler::cstnode> p_type_list();
		std::optional<yaoosl::compiler::cstnode> p_type();
		std::optional<yaoosl::compiler::cstnode> p_encapsulation(bool allow_instance);
		std::optional<yaoosl::compiler::cstnode> p_value_constant();
	};
}