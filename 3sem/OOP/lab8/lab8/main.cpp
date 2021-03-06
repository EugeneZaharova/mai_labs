#include <cstdlib>
#include <iostream>
#include <memory>
#include "Triangle.h"
#include "TStackItem.h"
#include "TStack.h"
#include "figure.h"
#include "quadro.h"
#include "rectangle.h"
#include "allocator.h"
#include <random>
#include <ctime>

using namespace std;

enum General
{
	fin,
	add,
	del,
	print,
    srt,
    par_sort,
	start
};

void PrintLine()
{
	cout << "----------------------------------" << endl;
}
void PrintStars()
{
	cout << "**********************************" << endl;
}

void TipsForFirgurs() {

	PrintLine();
	cout << "1 - Triangle" << endl
		<< "2 - Quadro" << endl
		<< "3 - Rectangle" << endl
		<< "0 - stop" << endl;
	PrintLine();
}

void Tips() {
	PrintLine();

    cout << "1 - add elem to stack" << endl
        << "2 - delete elem from stack" << endl
        << "3 - print stack" << endl
        << "4 - sort stack" << endl
        << "5 - parallel sort" << endl
		<< "0 - end" << endl;
	PrintLine();
}

int main(int argc, char** argv) {

    TStack<Figure> stack;
    int state = start;
    while (1)
    {
        switch (state)
        {
        case start:
        {
            Tips();
            cin >> state;
            break;
        }
        case add:
        {

            PrintLine();
            cout << "Which figure you want to add?" << endl;

            TipsForFirgurs();

            int fig = 0;
            cin >> fig;
            PrintStars();

            if (fig == 1)
            {
                stack.push(shared_ptr<Figure>(new Triangle(cin)));
                PrintStars();
                break;
            }
            if (fig == 2)
            {
                stack.push(shared_ptr<Figure>(new Quadro(cin)));
                PrintStars();
                break;
            }
            if (fig == 3)
            {
                stack.push(shared_ptr<Figure>(new Rectangle(cin)));
                PrintStars();
                break;
            }

            if (fig == 0)
            {
                state = start;
                PrintStars();
                break;
            }

            cout << "Wrong number" << endl;
            PrintStars();
            break;
        }

        case del:
        {
            PrintStars();
            stack.pop();
            PrintStars();
            state = start;
            break;
        }

        case print:
        {
            PrintStars();
            for (auto i : stack)
                i->Print();
            PrintStars();
            state = start;
            break;
        }

        case srt:
        {
            clock_t time;
            double duration;

            time = clock();

            cout << "Sort -------------" << endl;
            stack.sort();
            cout << "Done -------------" << endl;
            duration = (clock() - time) / (double)CLOCKS_PER_SEC;
            cout << "Time of sort: " << duration << endl;
            state = start;
            break;
        }

        case par_sort:
        {
            clock_t time;
            double duration;

            time = clock();

            cout << "Parallel Sort ----" << endl;
            stack.sort_parallel();
            cout << "Done -------------" << endl;
            duration = (clock() - time) / (double)CLOCKS_PER_SEC;
            cout << "Time of parallel sort: " << duration << endl;
            
            state = start;
            break;
        }

        case fin:
            return 0;
        }
    }

    /*TStack<Figure> stack;
    std::default_random_engine generator;
    uniform_int_distribution<int> distribution(1, 14);
    for (int i = 0; i < 14; i++) {
        int side = distribution(generator);
        stack.push(shared_ptr<Figure>(new Triangle(side, side, side)));
        stack.push(shared_ptr<Figure>(new Quadro(side)));
        stack.push(shared_ptr<Figure>(new Rectangle(side, side + 1)));

    }

    TStack<Figure> stack1 = stack;

    std::cout << "Sort -------------" << std::endl;
    stack1.sort();
    stack.sort_parallel();
    std::cout << "Done -------------" << std::endl;
    std::cout << stack << std::endl;
    std::cout << stack1 << std::endl;*/

}