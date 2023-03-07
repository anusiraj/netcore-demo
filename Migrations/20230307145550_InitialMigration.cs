using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using NETCoreDemo.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NETCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:course_status", "not_started,on_going,ended")
                .Annotation("Npgsql:Enum:project_role", "project_manager,project_coordinator,developer,designer");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    street = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    zipcode = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp without time zone", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    startdate = table.Column<DateTime>(name: "start_date", type: "timestamp without time zone", nullable: false),
                    status = table.Column<Course.CourseStatus>(type: "course_status", nullable: false),
                    size = table.Column<short>(type: "smallint", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp without time zone", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    tags = table.Column<ICollection<string>>(type: "jsonb", nullable: false),
                    deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "timestamp without time zone", nullable: false),
                    enddate = table.Column<DateTime>(name: "end_date", type: "timestamp without time zone", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp without time zone", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    addressid = table.Column<int>(name: "address_id", type: "integer", nullable: true),
                    courseid = table.Column<int>(name: "course_id", type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.id);
                    table.ForeignKey(
                        name: "fk_students_addresses_address_id",
                        column: x => x.addressid,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_students_courses_course_id",
                        column: x => x.courseid,
                        principalTable: "courses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "assignment_student",
                columns: table => new
                {
                    assignmentsid = table.Column<int>(name: "assignments_id", type: "integer", nullable: false),
                    studentsid = table.Column<int>(name: "students_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_student", x => new { x.assignmentsid, x.studentsid });
                    table.ForeignKey(
                        name: "fk_assignment_student_assignment_assignments_id",
                        column: x => x.assignmentsid,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignment_student_students_students_id",
                        column: x => x.studentsid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_students",
                columns: table => new
                {
                    studentid = table.Column<int>(name: "student_id", type: "integer", nullable: false),
                    projectid = table.Column<int>(name: "project_id", type: "integer", nullable: false),
                    joinedat = table.Column<DateTime>(name: "joined_at", type: "timestamp without time zone", nullable: false),
                    role = table.Column<ProjectRole>(type: "project_role", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_students", x => new { x.projectid, x.studentid });
                    table.ForeignKey(
                        name: "fk_project_students_projects_project_id",
                        column: x => x.projectid,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_students_students_student_id",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_student_students_id",
                table: "assignment_student",
                column: "students_id");

            migrationBuilder.CreateIndex(
                name: "ix_courses_name",
                table: "courses",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_project_students_student_id",
                table: "project_students",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_address_id",
                table: "students",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_students_course_id",
                table: "students",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_email",
                table: "students",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignment_student");

            migrationBuilder.DropTable(
                name: "project_students");

            migrationBuilder.DropTable(
                name: "assignment");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
