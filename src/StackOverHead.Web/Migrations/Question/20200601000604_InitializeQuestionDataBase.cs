﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverHead.Web.Migrations.Question
{
    public partial class InitializeQuestionDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QUESTION",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Votes = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ANSWER",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    KindOf = table.Column<int>(nullable: false),
                    Body = table.Column<string>(maxLength: 2147483647, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    AnswerId = table.Column<Guid>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: true),
                    Votes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANSWER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ANSWER_ANSWER_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "ANSWER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ANSWER_QUESTION_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QUESTION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONUSERVOTES",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONUSERVOTES", x => new { x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_QUESTIONUSERVOTES_QUESTION_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QUESTION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ANSWERUSERVOTES",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANSWERUSERVOTES", x => new { x.AnswerId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ANSWERUSERVOTES_ANSWER_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "ANSWER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ANSWER_AnswerId",
                table: "ANSWER",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ANSWER_QuestionId",
                table: "ANSWER",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANSWERUSERVOTES");

            migrationBuilder.DropTable(
                name: "QUESTIONUSERVOTES");

            migrationBuilder.DropTable(
                name: "ANSWER");

            migrationBuilder.DropTable(
                name: "QUESTION");
        }
    }
}
