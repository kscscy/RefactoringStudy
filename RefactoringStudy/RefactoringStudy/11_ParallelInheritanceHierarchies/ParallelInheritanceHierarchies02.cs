using System;
/// <summary>
/// 업무표준을 공개하면 사용자가 잘못된 업무표준을 연구원에게 set할 수도 있다.
/// 새로운 연구원이 추가된다면 그에 따른 업무표준도 같이 추가되어야만 한다.
/// 
/// 해결책 1 : 병렬 계층을 유지한채 익숙해진다
/// 장점 : SRP를 유지할 수 있다.
/// 단점 : 새 기능을 추가하려면 매번 두개의 클래스를 만들어야만 한다.
/// 
/// 해결책 2 : 부분 계층 구조로 만든다
/// 장점 : 한 계층만 유지보수를 한다 (책임에 대해 확신이 서지 않을 때 이 방법을 쓸것, 유연성 제공)
/// 단점 : SRP원칙에 어긋난다.
/// 
/// </summary>
/// 
namespace RefactoringStudy._11_ParallelInheritanceHierarchies
{
    public interface 연구원
    {
        string getType();
        void setType(string type);
        int getSalary();
        void setSalary(int salary);
        업무표준 get업무표준();
        void set업무표준(업무표준 표준);
    }

    public interface 업무표준
    {
        string 할일();
        string 목적();
    }

    //
    public class 개발자_연구원 : 연구원, 업무표준
    {
        private string type;
        private int salary;

        public int getSalary()
        {
            return salary;
        }

        public string getType()
        {
            return type;
        }

        public 업무표준 get업무표준()
        {
            return 표준;
        }

        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void set업무표준(업무표준 표준)
        {
            this.표준 = 표준;
        }

        public string 목적()
        {
            return "체계적인 개발";
        }

        public string 할일()
        {
            return "프로그래밍";
        }
    }



    //
    public class 기획자_연구원 : 연구원, 업무표준
    {
        private string type;
        private int salary;

        public int getSalary()
        {
            return salary;
        }

        public string getType()
        {
            return type;
        }

        public 업무표준 get업무표준()
        {
            return 표준;
        }

        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void set업무표준(업무표준 표준)
        {
            this.표준 = 표준;
        }

        public string 목적()
        {
            return "체계적인 기획";
        }

        public string 할일()
        {
            return "기획";
        }
    }

    public class 관리자
    {
        static void Main()
        {
            연구원 A = (개발자_연구원)Activator.CreateInstance("Namespace.연구원");
            A.setType("선임연구원");
            A.setSalary(100);
            A.set업무표준(new 개발자_업무표준());

            연구원 B = (기획자_연구원)Activator.CreateInstance("Namespace.연구원");
            B.setType("수석연구원");
            B.setSalary(1000);
            B.set업무표준(new 기획자_업무표준());
        }
    }
}
