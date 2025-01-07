using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Controllers;
using StudentsApi.Data;
using StudentsApi.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StudentsApi.Tests
{
    public class StudentControllerTests
    {
        private readonly DbContextOptions<StudentDbContext> _options;

        public StudentControllerTests()
        {
            // Configure the in-memory database
            _options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        private StudentDbContext GetDbContext()
        {
            // Create a new context instance for each test
            var context = new StudentDbContext(_options);
            context.Database.EnsureDeleted(); // Clear the database
            context.Database.EnsureCreated(); // Recreate the database
            return context;
        }

        [Fact]
        public void GetStudents_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            using var context = GetDbContext();
            context.students.AddRange(new List<Student>
            {
                new Student { Id = 1, StudentName = "John Doe", Course = "Math", Age = 20, Email = "john@example.com" },
                new Student { Id = 2, StudentName = "Jane Smith", Course = "Science", Age = 22, Email = "jane@example.com" }
            });
            context.SaveChanges();

            var controller = new StudentController(context);

            // Act
            var result = controller.GetStudents().Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var students = result.Value as List<Student>;
            Assert.NotNull(students);
            Assert.Equal(2, students.Count);
        }

        [Fact]
        public void GetStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new StudentController(context);

            // Act
            var result = controller.GetStudent(99).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateItem_ReturnsCreatedAtActionResult_WhenStudentIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new StudentController(context);

            var student = new Student
            {
                Id = 1,
                StudentName = "New Student",
                Course = "Physics",
                Age = 21,
                Email = "newstudent@example.com"
            };

            // Act
            var result = controller.CreateItem(student).Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(student, result.Value);
        }

        [Fact]
        public void UpdateStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new StudentController(context);

            var student = new Student
            {
                Id = 99,
                StudentName = "Updated Name",
                Course = "Updated Course",
                Age = 22,
                Email = "updated@example.com"
            };

            // Act
            var result = controller.UpdateStudent(99, student).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateStudent_UpdatesStudentAndReturnsOk_WhenStudentExists()
        {
            // Arrange
            using var context = GetDbContext();
            context.students.Add(new Student
            {
                Id = 1,
                StudentName = "Original Name",
                Course = "Original Course",
                Age = 20,
                Email = "original@example.com"
            });
            context.SaveChanges();

            var controller = new StudentController(context);

            var updatedStudent = new Student
            {
                StudentName = "Updated Name",
                Course = "Updated Course",
                Age = 22,
                Email = "updated@example.com"
            };

            // Act
            var result = controller.UpdateStudent(1, updatedStudent).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var student = context.students.Find(1);
            Assert.NotNull(student);
            Assert.Equal("Updated Name", student.StudentName);
        }

        [Fact]
        public void DeleteStudent_RemovesStudentAndReturnsOk()
        {
            // Arrange
            using var context = GetDbContext();
            context.students.Add(new Student
            {
                Id = 1,
                StudentName = "Student to Delete",
                Course = "Course",
                Age = 20,
                Email = "delete@example.com"
            });
            context.SaveChanges();

            var controller = new StudentController(context);

            // Act
            var result = controller.DeleteStudent(1).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Null(context.students.Find(1));
        }

        [Fact]
        public void SearchStudents_ReturnsFilteredResults()
        {
            // Arrange
            using var context = GetDbContext();
            context.students.AddRange(new List<Student>
            {
                new Student { Id = 1, StudentName = "Alice", Course = "Math", Age = 20, Email = "alice@example.com" },
                new Student { Id = 2, StudentName = "Bob", Course = "Science", Age = 22, Email = "bob@example.com" }
            });
            context.SaveChanges();

            var controller = new StudentController(context);

            // Act
            var result = controller.SearchStudents(course: "Math", age: null, name: null, email: null).Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var students = result.Value as List<Student>;
            Assert.Single(students);
            Assert.Equal("Alice", students[0].StudentName);
        }
    }
}
