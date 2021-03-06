#ifndef TRIANGLE_H
#define TRIANGLE_H
#include <cstdlib>
#include <iostream>
#include "figure.h"
class Triangle : public Figure {
public:
    Triangle(size_t a, size_t b, size_t c);
    Triangle(std::istream &is);
    double Square() override;
    void Print() override;
    virtual ~Triangle();
private:
    size_t side_a;
    size_t side_b;
    size_t side_c;
};
#endif