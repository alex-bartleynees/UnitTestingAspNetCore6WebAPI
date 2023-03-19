using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class CourseTests
    {
        [Fact]
        public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
        {
            // Act 
            var course = new Course("Disaster Management 101");

            // Assert
            Assert.True(course.IsNew);
        }
    }
}
