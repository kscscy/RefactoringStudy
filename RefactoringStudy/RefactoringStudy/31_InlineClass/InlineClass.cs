using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 클래스 내용 직접 삽입 (Inline Class)
/// 
/// 클래스에 기능이 너무 적을 땐 그 클래스의 모든 기능을 다른 클래스로 합쳐 넣고 원래의 클래스는 삭제하자.
/// 
/// ExtractClass와 반대되는 내용이다. 클래스가 더 이상 제 역할을 수행하지 못하여 존재할 이유가 없을 때 실시한다.
/// 리팩토링을 실시해서 남은 기능이 거의 없어졌을 때 나타난다. 
/// 이럴 때는 작아진 클래스를 가장 많이 사용하는 다른 클래스를 하나 고른 후 이클래스를 거기에 합쳐야 한다.
/// </summary>
namespace RefactoringStudy._31_InlineClass
{
    // before
    class Painter
    {
        private Color myColor;

        public Painter(Color c)
        {
            myColor = c;
            InitPainter(myColor);
        }

        private void InitPainter(Color color)
        {
            //init painter
        }
    }

    class Circle
    {
        private Painter myPainter;

        public Circle(Color c)
        {
            myPainter = new Painter(c);
        }
    }

    // after
    class Circle
    {
        private Color myColor;

        public Circle(Color c)
        {
            myColor = c;
            InitPainter(myColor);
        }

        private void InitPainter(Color color)
        {
            //init painter
        }
    }
}
