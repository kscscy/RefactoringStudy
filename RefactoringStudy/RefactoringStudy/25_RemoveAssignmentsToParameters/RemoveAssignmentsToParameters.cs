using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 매개변수로의 값 대입 제거 (RemoveAssignments To Parameters)
/// 
/// 매개변수로 값을 대입하는 코드가 있을 땐,매개변수 대신 임시변수를 사용하게 수정한다.
/// 1. 매개변수 대신 사용할 임시변수를 선언한다.
/// 2. 매개변수로 값을 대입하는 코드 뒤에 나오는 매개변수 참조를 전부 임시변수로 수정한다.
/// 3. 매개변수로의 값 대입을 임시변수로의 값 대입으로 수정한다.
/// 
/// </summary>
namespace RefactoringStudy._25_RemoveAssignmentsToParameters
{
    class RemoveAssignmentsToParameters
    {
        public int Test(int a, int b)
        {
            b = a + b;
            return b;
        }

        public int Test2(int a, int b)
        {
            int rtn = a + b;
            return rtn;
        }
    }
}
