using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace YHSchool.Migrations
{
    public partial class modifyYHForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "HookID",
            //    table: "Ent_YHForm",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "Modifier",
            //    table: "Ent_YHForm",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "ModifyDate",
            //    table: "Ent_YHForm",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "HookID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropColumn(
            //    name: "Modifier",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropColumn(
            //    name: "ModifyDate",
            //    table: "Ent_YHForm");
        }
    }
}
