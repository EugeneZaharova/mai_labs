#ifndef __TREE_H__
#define __TREE_H__

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

typedef int Value;
typedef int Key;

typedef struct {
	Value value;
	Key key;
} Data;

typedef struct Node_ {
    struct Node_ *left;
    struct Node_ *right;
    Data *data;
} Node;

typedef struct {
	Node *root;
} Tree;

Tree *tree_create();
Node *node_create();
Data *data_create();
void node_add(Data *data, Node *node);
int max(int a, int b);
int tree_depht(Tree *tree);
int node_depht(Node *node, int d);
Node *tree_find_elem(Node *node, Key key);
void tree_delete_elem(Tree *tree, Node *node);
void tree_print_2(Node *node, int lvl);

#endif