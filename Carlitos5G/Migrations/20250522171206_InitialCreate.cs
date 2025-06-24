using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Carlitos5G.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admin_ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_ticket", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profession = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TableName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Action = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecordId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Changes = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PerformedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "prequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contactno = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    services = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    others = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    query = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    posting_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    remark = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prequest", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    ticket_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    email_id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: true, collation: "ascii_general_ci"),
                    subject = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    task_type = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prioprity = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ticket = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attachment = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    admin_remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    posting_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    admin_remark_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tutors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profession = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alt_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: true),
                    posting_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_ticket", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, defaultValue: "1234")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    genero = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_asig = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    etapa_educativa = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    grado = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    tutor_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumb = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValue: "deactive")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoria = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValue: "nn")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iframe = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updation_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_diplomado = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_playlist_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "videogame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    carpeta = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    imagen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tutor_id = table.Column<Guid>(type: "char(36)", unicode: false, maxLength: 36, nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videogame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_videogame_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblresult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    student_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    class_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    subjects_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    marks = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    posting_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updation_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblresult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblresult_users_student_id",
                        column: x => x.student_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usercheck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    logindate = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logintime = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<Guid>(type: "char(36)", unicode: false, maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip = table.Column<byte[]>(type: "varbinary(16)", maxLength: 16, nullable: true),
                    mac = table.Column<byte[]>(type: "varbinary(16)", maxLength: 16, nullable: true),
                    city = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usercheck_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bookmark",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookmark", x => new { x.user_id, x.playlist_id });
                    table.ForeignKey(
                        name: "FK_bookmark_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_bookmark_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "certificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    certificate_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_certificates_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_certificates_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "content",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    tutor_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    media_path = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumbnail_path = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValue: "deactive")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, defaultValue: "Video")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_content_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_content_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "download",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_download_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_download_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "eventoscalendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    tutor_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    evento = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color_evento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_inicio = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_fin = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventoscalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventoscalendar_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_eventoscalendar_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_eventoscalendar_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flipbook",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    tutor_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pages = table.Column<int>(type: "int", nullable: true),
                    directory = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    media_path = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ThumbnailPath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, defaultValue: "Libro")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flipbook", x => x.id);
                    table.ForeignKey(
                        name: "FK_flipbook_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_flipbook_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ovas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(20)", maxLength: 20, nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iframe = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ovas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ovas_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblsubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    playlist_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    subject_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creationdate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updation_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblsubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblsubjects_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "avance",
                columns: table => new
                {
                    playlist_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_avance_content_content_id",
                        column: x => x.content_id,
                        principalTable: "content",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_avance_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalTable: "playlist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_avance_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    content_id = table.Column<Guid>(type: "char(36)", unicode: false, maxLength: 36, nullable: true, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", unicode: false, maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    tutor_id = table.Column<Guid>(type: "char(36)", unicode: false, maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    comment = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_content_content_id",
                        column: x => x.content_id,
                        principalTable: "content",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    tutor_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    content_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_likes_content_content_id",
                        column: x => x.content_id,
                        principalTable: "content",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_likes_tutors_tutor_id",
                        column: x => x.tutor_id,
                        principalTable: "tutors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_likes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Image", "Name", "Password", "Profession", "UpdationDate" },
                values: new object[,]
                {
                    { new Guid("3eb997b0-3c2e-4cd5-a9c5-419edac2caf6"), "luisa@carlitos.com", "admin2.jpg", "Luisa Manager", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "Gestora Educativa", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(963) },
                    { new Guid("94837ced-f138-462e-b5fb-84103adc7416"), "admin@carlitos.com", "admin1.jpg", "Carlos Admin", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "Coordinador", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(958) }
                });

            migrationBuilder.InsertData(
                table: "tutors",
                columns: new[] { "Id", "email", "image", "name", "password", "profession", "updationDate" },
                values: new object[,]
                {
                    { new Guid("33b6bfc6-c9a4-43ee-8326-48bd8e74b8d9"), "ana@carlitos.com", "/images/145d4cd3-a9c6-4ad7-aa36-3712fc918748.jpg", "Ana López", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "Física", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(1091) },
                    { new Guid("a7aa881b-36f9-4be3-bf34-9cfa31506fb4"), "mario@carlitos.com", "/images/145d4cd3-a9c6-4ad7-aa36-3712fc918747.jpg", "Mario Pérez", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "Matemático", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(1079) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "email", "etapa_educativa", "fecha_asig", "genero", "grado", "image", "name", "password", "telefono", "updationDate" },
                values: new object[,]
                {
                    { new Guid("2e4bde50-a245-4083-92af-9171352f9277"), "lucia@carlitos.com", "Secundaria", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(1128), "Femenino", "2do", "user2.jpg", "Lucía Alumna", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "555-5678", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(1129) },
                    { new Guid("4dbcb321-161c-4cb6-92a7-5c71774554e8"), "juan@carlitos.com", "Primaria", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(1124), "Masculino", "4to", "user1.jpg", "Juan Estudiante", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "555-1234", new DateTime(2025, 5, 22, 17, 12, 6, 42, DateTimeKind.Utc).AddTicks(1125) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_avance_content_id",
                table: "avance",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_avance_playlist_id",
                table: "avance",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_avance_user_id",
                table: "avance",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookmark_playlist_id",
                table: "bookmark",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificates_playlist_id",
                table: "certificates",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificates_user_id",
                table: "certificates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_content_id",
                table: "comments",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_tutor_id",
                table: "comments",
                column: "tutor_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_content_playlist_id",
                table: "content",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_content_tutor_id",
                table: "content",
                column: "tutor_id");

            migrationBuilder.CreateIndex(
                name: "IX_download_playlist_id",
                table: "download",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_download_user_id",
                table: "download",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_eventoscalendar_playlist_id",
                table: "eventoscalendar",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_eventoscalendar_tutor_id",
                table: "eventoscalendar",
                column: "tutor_id");

            migrationBuilder.CreateIndex(
                name: "IX_eventoscalendar_user_id",
                table: "eventoscalendar",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_flipbook_playlist_id",
                table: "flipbook",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_flipbook_tutor_id",
                table: "flipbook",
                column: "tutor_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_content_id",
                table: "likes",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_tutor_id",
                table: "likes",
                column: "tutor_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_user_id",
                table: "likes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ovas_playlist_id",
                table: "ovas",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_playlist_tutor_id",
                table: "playlist",
                column: "tutor_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblresult_student_id",
                table: "tblresult",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblsubjects_playlist_id",
                table: "tblsubjects",
                column: "playlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_tutors_email",
                table: "tutors",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usercheck_user_id",
                table: "usercheck",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_videogame_tutor_id",
                table: "videogame",
                column: "tutor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_ticket");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "avance");

            migrationBuilder.DropTable(
                name: "bookmark");

            migrationBuilder.DropTable(
                name: "certificates");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "download");

            migrationBuilder.DropTable(
                name: "eventoscalendar");

            migrationBuilder.DropTable(
                name: "flipbook");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "ovas");

            migrationBuilder.DropTable(
                name: "prequest");

            migrationBuilder.DropTable(
                name: "tblresult");

            migrationBuilder.DropTable(
                name: "tblsubjects");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "user_ticket");

            migrationBuilder.DropTable(
                name: "usercheck");

            migrationBuilder.DropTable(
                name: "videogame");

            migrationBuilder.DropTable(
                name: "content");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "playlist");

            migrationBuilder.DropTable(
                name: "tutors");
        }
    }
}
