using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 클래스 추출 (Extract Class)
/// 
/// 두 클래스가 처리해야할 기능이 하나의 클래스에 들어있을 땐 
/// 새 클래스를 만들고 기존 클래스의 관련 필드와 메소드를 새 클래스로 옮기자.
/// 
/// 클래스는 시간이 지날수록 방대해지기 마련이다. 그런 클래스에는 많은 메소드와 데이터가 들어있다.
/// 어느 부분을 분리할지 궁리해서 떼어내야 한다. 함께 변하거나 의존적인 데이터의 일부분도 클래스로 떼어내기 좋다.
/// 이것을 판단하는 좋은 방법은 데이터나 메소드를 하나 제거하면 어떻게 될지, 다른 필드와 메소드를 추가하는 것은 합리적인지 생각해보라.
/// 
/// </summary>

namespace RefactoringStudy._30_ExtractClass
{
    // before
    abstract class Shape
    {
        public void Draw()
        {
            try
            {
                // draw
            }
            catch (Exception e)
            {
                LogError(e);
            }
        }

        public static void LogError(Exception e)
        {
            File.WriteAllText(@"c:\Errors\Exception.txt", e.ToString());
        }
    }

    // after
    class Logger
    {
        public static void LogError(Exception e)
        {
            File.WriteAllText(@"c:\Errors\Exception.txt", e.ToString());
        }
    }

    abstract class Shape2
    {
        public void Draw()
        {
            try
            {
                // draw
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }
    }
}
