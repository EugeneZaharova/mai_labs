#include <iostream>

using namespace std;
using Tloong = unsigned long long;
const int MIN_CAP = 8;

class TElement {
public:
    Tloong key = 0;
    Tloong value = 0;
};

int MaxRadix(Tloong size, TElement *arr) {
    Tloong max = 0;

    for (Tloong i = 0; i < size; ++i) {
        arr[i].key > max ? max = arr[i].key : max;
    }

    int radix = 0;

    while (max / 10) {
        ++radix;
        max /= 10;
    }

    return radix + 1;
}

int Digit(TElement elem, int i) {
    
    Tloong tmp = elem.key;
 
    while (i) {
        if (tmp / 10) {
            if (i == 1) {
                return tmp % 10;
            }
            tmp /= 10;
        }
        else {            
			if (i == 1) {
				return tmp;
			}
			else {
				return 0;
			}
        }
		--i;
	}
}

void RarixSort(TElement *arr, Tloong n, Tloong cap)
{
    //for i = 1 to m
    int m = MaxRadix(n, arr);
    int k = 10;
    int *C = new int[k];
    TElement *B = new TElement[cap];

    for (int i = 1; i <= m; ++i) {
        //    for j = 0 to k - 1
        for (int j = 0; j <= k - 1; ++j) {
            //        C[j] = 0
            C[j] = 0;
        }

        //        for j = 0 to n - 1
        for (int j = 0; j <= n - 1; ++j) {
            //            d = digit(A[j], i)
            //            C[d]++
            int d = Digit(arr[j], i);
            ++C[d];
        }

        int count = 0;
    
        for (int j = 0; j <= k - 1; ++j) {
            int tmp = C[j];
            C[j] = count;
            count += tmp;
        }
      
        for (int j = 0; j <= n - 1; ++j) {
            int d = Digit(arr[j], i);
            B[C[d]] = arr[j];
            ++C[d];
        }

		for (int j = 0; j <= n - 1; ++j) {
			arr[j] = B[j];
		}
    }
}

int main(int argc, char const **argv) {
    
    TElement elem;
    Tloong capacity = MIN_CAP;
    Tloong size_of_arr = 0;
    TElement *arr = new TElement[capacity];


    for (Tloong i = 0; cin >> elem.key >> elem.value; ++i) {
        if (size_of_arr == capacity) {
            capacity *= 2;
            arr = (TElement*)realloc(arr, sizeof(TElement) * capacity);
        }

        arr[i] = elem;
        ++size_of_arr;
    }

    cout << "RESULT: " << endl;

	RarixSort(arr, size_of_arr, capacity);

    for (Tloong i = 0; i < size_of_arr; ++i) {
		printf("%0*d", MaxRadix(size_of_arr, arr), arr[i].key);
		cout << " " << arr[i].value << endl;
    }

    return 0;
}
