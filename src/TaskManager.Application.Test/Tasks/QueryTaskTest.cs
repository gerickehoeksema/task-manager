using FluentAssertions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Tasks.Query;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Test.Tasks
{
    public class QueryTaskTest : QueryTestBase
    {
        [Test]
        public async Task Should_Handle_GetTask()
        {
            // Arrange
            await SeedAsync().ConfigureAwait(false);
            var query = new GetTaskQuery { TaskId = 1 };

            // Act
            var handler = new GetTaskQuery.GetTaskQueryHandler(Context, Mapper);
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task Should_Handle_GetTaskList()
        {
            // Arrange
            await SeedAsync().ConfigureAwait(false);
            var query = new GetTaskListQuery();

            // Act
            var handler = new GetTaskListQuery.GetTaskListQueryHandler(Context, Mapper);
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        internal async Task SeedAsync()
        {
            await Context.Tasks
                .AddRangeAsync(
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 1", Description = "Task 1 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 2", Description = "Task 2 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 3", Description = "Task 3 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 4", Description = "Task 4 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 5", Description = "Task 5 description", Status = TaskStatus_Enum.Unassigned, IsActive = true }
                )
                .ConfigureAwait(false);

            await Context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }
}
