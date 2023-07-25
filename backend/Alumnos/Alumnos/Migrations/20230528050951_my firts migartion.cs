using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alumnos.Migrations
{
    /// <inheritdoc />
    public partial class myfirtsmigartion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tcourse",
                columns: table => new
                {
                    IdCourse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameCourse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tcourse", x => x.IdCourse);
                });

            migrationBuilder.CreateTable(
                name: "TinfoLimpieza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStudent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCourse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTeacher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSemester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoraInit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorEnd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinfoLimpieza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TprofessionalSchool",
                columns: table => new
                {
                    IdProfessionalSchool = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProfessionalSchool = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TprofessionalSchool", x => x.IdProfessionalSchool);
                });

            migrationBuilder.CreateTable(
                name: "Tstudent",
                columns: table => new
                {
                    IdStudent = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameStudent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tstudent", x => x.IdStudent);
                });

            migrationBuilder.CreateTable(
                name: "Tteacher",
                columns: table => new
                {
                    IdTeacher = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameTeacher = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tteacher", x => x.IdTeacher);
                });

            migrationBuilder.CreateTable(
                name: "Tcareer",
                columns: table => new
                {
                    IdCareer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameCareer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProfessionalSchool = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tcareer", x => x.IdCareer);
                    table.ForeignKey(
                        name: "FK_Tcareer_TprofessionalSchool_IdProfessionalSchool",
                        column: x => x.IdProfessionalSchool,
                        principalTable: "TprofessionalSchool",
                        principalColumn: "IdProfessionalSchool",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tsemester",
                columns: table => new
                {
                    IdSemester = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdCourse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdCareer = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tsemester", x => new { x.IdSemester, x.IdCourse });
                    table.ForeignKey(
                        name: "FK_Tsemester_Tcareer_IdCareer",
                        column: x => x.IdCareer,
                        principalTable: "Tcareer",
                        principalColumn: "IdCareer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tsemester_Tcourse_IdCourse",
                        column: x => x.IdCourse,
                        principalTable: "Tcourse",
                        principalColumn: "IdCourse",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TcourseStudents",
                columns: table => new
                {
                    IdStudent = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdCourse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdTeacher = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdSemester = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcourseStudents", x => new { x.IdStudent, x.IdCourse, x.IdSemester, x.IdTeacher });
                    table.ForeignKey(
                        name: "FK_TcourseStudents_Tsemester_IdSemester_IdCourse",
                        columns: x => new { x.IdSemester, x.IdCourse },
                        principalTable: "Tsemester",
                        principalColumns: new[] { "IdSemester", "IdCourse" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TcourseStudents_Tstudent_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Tstudent",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tschedule",
                columns: table => new
                {
                    IdSchedule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSemester = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdCourse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorInit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorEnd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tschedule", x => x.IdSchedule);
                    table.ForeignKey(
                        name: "FK_Tschedule_Tsemester_IdSemester_IdCourse",
                        columns: x => new { x.IdSemester, x.IdCourse },
                        principalTable: "Tsemester",
                        principalColumns: new[] { "IdSemester", "IdCourse" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TcourseAssignment",
                columns: table => new
                {
                    IdCarga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSchedule = table.Column<int>(type: "int", nullable: true),
                    IdTeacher = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcourseAssignment", x => x.IdCarga);
                    table.ForeignKey(
                        name: "FK_TcourseAssignment_Tschedule_IdSchedule",
                        column: x => x.IdSchedule,
                        principalTable: "Tschedule",
                        principalColumn: "IdSchedule",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TcourseAssignment_Tteacher_IdTeacher",
                        column: x => x.IdTeacher,
                        principalTable: "Tteacher",
                        principalColumn: "IdTeacher",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tcareer_IdProfessionalSchool",
                table: "Tcareer",
                column: "IdProfessionalSchool");

            migrationBuilder.CreateIndex(
                name: "IX_TcourseAssignment_IdSchedule",
                table: "TcourseAssignment",
                column: "IdSchedule",
                unique: true,
                filter: "[IdSchedule] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TcourseAssignment_IdTeacher",
                table: "TcourseAssignment",
                column: "IdTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_TcourseStudents_IdSemester_IdCourse",
                table: "TcourseStudents",
                columns: new[] { "IdSemester", "IdCourse" });

            migrationBuilder.CreateIndex(
                name: "IX_Tschedule_IdSemester_IdCourse",
                table: "Tschedule",
                columns: new[] { "IdSemester", "IdCourse" });

            migrationBuilder.CreateIndex(
                name: "IX_Tsemester_IdCareer",
                table: "Tsemester",
                column: "IdCareer");

            migrationBuilder.CreateIndex(
                name: "IX_Tsemester_IdCourse",
                table: "Tsemester",
                column: "IdCourse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TcourseAssignment");

            migrationBuilder.DropTable(
                name: "TcourseStudents");

            migrationBuilder.DropTable(
                name: "TinfoLimpieza");

            migrationBuilder.DropTable(
                name: "Tschedule");

            migrationBuilder.DropTable(
                name: "Tteacher");

            migrationBuilder.DropTable(
                name: "Tstudent");

            migrationBuilder.DropTable(
                name: "Tsemester");

            migrationBuilder.DropTable(
                name: "Tcareer");

            migrationBuilder.DropTable(
                name: "Tcourse");

            migrationBuilder.DropTable(
                name: "TprofessionalSchool");
        }
    }
}
