﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StackOverHead.Question.Infra.Context;

namespace StackOverHead.Web.Migrations.Question
{
    [DbContext(typeof(StackOverHeadQuestionDbContext))]
    [Migration("20200601000604_InitializeQuestionDataBase")]
    partial class InitializeQuestionDataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.AnswerModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AnswerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .HasColumnType("text")
                        .HasMaxLength(2147483647);

                    b.Property<int>("KindOf")
                        .HasColumnType("integer");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Votes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("ANSWER");
                });

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.AnswerUserVotesModel", b =>
                {
                    b.Property<Guid>("AnswerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("AnswerId", "UserId");

                    b.ToTable("ANSWERUSERVOTES");
                });

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.QuestionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Tags")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Title")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("ViewCount")
                        .HasColumnType("integer");

                    b.Property<int>("Votes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("QUESTION");
                });

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.QuestionUserVotesModel", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("QuestionId", "UserId");

                    b.ToTable("QUESTIONUSERVOTES");
                });

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.AnswerModel", b =>
                {
                    b.HasOne("StackOverHead.Question.Infra.Models.AnswerModel", "Answer")
                        .WithMany("Comments")
                        .HasForeignKey("AnswerId");

                    b.HasOne("StackOverHead.Question.Infra.Models.QuestionModel", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.AnswerUserVotesModel", b =>
                {
                    b.HasOne("StackOverHead.Question.Infra.Models.AnswerModel", "Answer")
                        .WithMany("UserVotes")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StackOverHead.Question.Infra.Models.QuestionUserVotesModel", b =>
                {
                    b.HasOne("StackOverHead.Question.Infra.Models.QuestionModel", "Question")
                        .WithMany("UserVotes")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
