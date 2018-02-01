using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 컬렉션 캡슐화 (Encapsulate Collection)
/// 
/// 메소드가 컬렉션을 반환할 땐 
/// 그 메소드가 읽기전용 뷰를 반환하게 수정하고 추가 메소드와 삭제 메소드를 작성하자.
/// 
/// 클래스에 여러 인스턴스로 구성된 컬렉션이 있는 경우를 볼 수 있다.
/// 그럴 땐 컬렉션을 읽고 쓸 수 있는 읽기/쓰기 메소드가 있기 마련이다.
/// 
/// 컬렉션은 다른 종류의 데이터와는 다른 읽기/쓰기 방식을 사용해야 한다.
/// 
/// 컬렉션 읽기 메소드는 컬렉션 객체 자체를 반환해서는 안된다. 
/// 왜냐하면 컬렉션 참조 부분이 컬렉션의 내용을 조작해도 컬렉션이 든 클래스는 무슨일이 일어나는지 모르기 때문이다.
/// 이로 인해 컬렉션 참조 코드에 객체의 데이터 구조가 지나치게 노출된다.
/// 값이 여러 개인 속성을 읽기 메소드는 컬렉션 조작이 불가능한 형식을 반환하고 불필요하게 자세한 컬렉션 구조 정보는 감춰야 한다.
/// 
/// 컬렉션 쓰기 메소드는 절대 있으면 안되므로, 원소를 추가하는 메소드와 삭제하는 메소드를 대신 사용해야 한다.
/// 이렇게 하면 컬렉션이 든 객체가 컬렉션의 원소 추가와 삭제를 통제할 수 있다.
/// 
/// </summary>

namespace RefactoringStudy._46_EncapsulateCollection
{
    // before
    class Person
    {
        public List<Course> Courses { get; set; }
    }

    class Course
    {
        public string Name { get; set; }
        public bool IsAdvanced { get; set; }
    }

    class Example
    {
        public void ManipulatingCourses()
        {
            var kent = new Person { Courses = new List<Course>() };
            kent.Courses.Add(new Course { Name = "Smalltalk Programming", IsAdvanced = false });
            kent.Courses.Add(new Course { Name = "TDD", IsAdvanced = true });

            var refactoringCourse = new Course { Name = "Refactoring", IsAdvanced = true };
            kent.Courses.Add(refactoringCourse);

            kent.Courses.Add(new Course { Name = "Jasmine", IsAdvanced = true });
            kent.Courses.Remove(refactoringCourse);
        }

        public void GetCourseInfo()
        {
            var kent = new Person();

            var advancedCourses = kent.Courses.Count(c => c.IsAdvanced);
        }
    }

    // after
    class Person2
    {
        private List<Course2> _courses;
        public IReadOnlyList<Course2> Courses
        {
            get { return _courses.AsReadOnly(); }
        }

        public Person2()
        {
            _courses = new List<Course2>();
        }

        //public Person2(IEnumerable<Course2> courses)
        //    :this()
        //{
        //    _courses.AddRange(courses);
        //}

        public void Addcourse(Course2 course)
        {
            _courses.Add(course);
        }

        public void RemoveCourse(Course2 course)
        {
            _courses.Remove(course);
        }
    }

    class Course2
    {
        public string Name { get; set; }
        public bool IsAdvanced { get; set; }
    }

    class Example2
    {
        public void ManipulatingCourses()
        {
            var kent = new Person2 ();
            kent.Addcourse(new Course2 { Name = "Smalltalk Programming", IsAdvanced = false });
            kent.Addcourse(new Course2 { Name = "TDD", IsAdvanced = true });

            var refactoringCourse = new Course2 { Name = "Refactoring", IsAdvanced = true };
            kent.Addcourse(refactoringCourse);

            kent.Addcourse(new Course2 { Name = "Jasmine", IsAdvanced = true });
            kent.RemoveCourse(refactoringCourse);
        }

        public void GetCourseInfo()
        {
            var kent = new Person2();

            var advancedCourses = kent.Courses.Count(c => c.IsAdvanced);
        }
    }
}
