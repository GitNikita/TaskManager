using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tExecutionStatusesTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tExecutionStatusesTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tUserTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionStatusTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tUserTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tUserTasks_tExecutionStatusesTask_ExecutionStatusTaskId",
                        column: x => x.ExecutionStatusTaskId,
                        principalTable: "tExecutionStatusesTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tUserFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier ROWGUIDCOL", nullable: false, defaultValueSql: "newid()"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max) FILESTREAM", nullable: false),
                    UserTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tUserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tUserFiles_tUserTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalTable: "tUserTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tUserFiles_UserTaskId",
                table: "tUserFiles",
                column: "UserTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_tUserTasks_ExecutionStatusTaskId",
                table: "tUserTasks",
                column: "ExecutionStatusTaskId");

            migrationBuilder.Sql("insert into dbo.tExecutionStatusesTask (ExecutionStatusName) values ('Создана'), ('Выполняется'), ('Выполнена')");
            migrationBuilder.Sql("insert into dbo.tUserTasks (Name, ExecutionStatusTaskId) values ('Задача1', 1), ('Задача2', 2), ('Задача3', 3)");
            var rootPath = @$"{Environment.CurrentDirectory}\FilesForTest\";
            migrationBuilder.Sql($"declare @File1 varbinary(MAX); set @File1 = (select cast(bulkcolumn as varbinary(max)) from OPENROWSET(BULK '{rootPath}100mb-file.txt', SINGLE_BLOB) as file1); insert into dbo.tUserFiles ([FileName], [FileData], UserTaskId) values('100mb-file.txt', @File1, 1)");
            migrationBuilder.Sql($"declare @File2 varbinary(MAX); set @File2 = (select cast(bulkcolumn as varbinary(max)) from OPENROWSET(BULK '{rootPath}flowers.png', SINGLE_BLOB) as file2); insert into dbo.tUserFiles ([FileName], [FileData], UserTaskId) values('flowers.png', @File2, 2)");
            migrationBuilder.Sql($"declare @File3 varbinary(MAX); set @File3 = (select cast(bulkcolumn as varbinary(max)) from OPENROWSET(BULK '{rootPath}sample.pdf', SINGLE_BLOB)as file3); insert into dbo.tUserFiles ([FileName], [FileData], UserTaskId) values('sample.pdf', @File3, 3)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tUserFiles");

            migrationBuilder.DropTable(
                name: "tUserTasks");

            migrationBuilder.DropTable(
                name: "tExecutionStatusesTask");
        }
    }
}
