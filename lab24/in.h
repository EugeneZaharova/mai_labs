#ifndef _IN_H_
#define _IN_H_

#include "stack.h"

void get_string(Stack *out);
bool is_char_operator(char c);
bool is_char_operand(char c);
void io_free(Stack *out, Stack *tmp);

#endif