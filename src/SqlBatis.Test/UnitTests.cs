using MySql.Data.MySqlClient;
using NUnit.Framework;
using SqlBatis.Attributes;
using Dapper;
using SqlBatis.Expressions;
using SqlBatis.Test.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SqlBatis.Test
{
    public class UnitTests
    {

        [Test]
        public void TestInsertAsync()
        {
            var builder = new DbContextBuilder
            {
                Connection = new MySqlConnection("server=127.0.0.1;user id=root;password=1024;database=test;"),
            };
            GlobalSettings.XmlCommandsProvider.Load(@"D:\SqlBatis\src\SqlBatis.Test\Student.xml");
            try
            {
                var sql = "insert into advert_banners(banner_img,banner_sort,banner_group) values(@img,@sort,@group)";
                var list = new List<int>();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < 30; i++)
                {
                    Regex.IsMatch(sql, $@"([\@,\:,\?]+{"fff"})");
                }
                stopwatch.Stop();
                list = list.Distinct().ToList();
                Debug.Write("��ʱ��"+stopwatch.ElapsedMilliseconds);
                using (var db = new DbContext(builder))
                {
                    var ff = db.Execute("insert into advert_banners(banner_img,banner_sort,banner_group) values(@img,@sort,@group)", new { img=(string)null,sort=20, group=1 });
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [Test]
        public void TestDynamicConvert()
        {
            var builder = new DbContextBuilder
            {
                Connection = new MySqlConnection("server=127.0.0.1;user id=root;password=1024;database=test;"),
            };
            try
            {
                var db = new DbContext(builder);
                var ids = new int[] { 1,2,3};
                var (list, count) = db.From<Student, StudentSchool, StudentLike>()
                    .Join<Student, StudentSchool>((a, b) => a.SchoolId == b.Id)
                    .LeftJoin<Student, StudentLike>((a,c)=>a.LikeId==c.Id)
                    .Page(1,2)
                    .SelectMany((a,b,c)=> new{a.Id,a.Sname,b.SchoolName,c.LikeName });
            

            }
            catch (Exception e)
            {

                throw;
            }
           
        }
    }
    [Table("student_school")]
    public class StudentSchool
    {
        public int Id { get; set; }
        [Column("school_name")]
        public string SchoolName { get; set; }
    }
    [Table("sutdent_like")]
    public class StudentLike
    {
        public int Id { get; set; }
        [Column("like_name")]
        public string LikeName { get; set; }
    }
    public class Student
    {
        public string Sname { get; set; }
        public int Id { get; set; }
        [Column("school_id")]
        public int SchoolId { get; set; }
        [Column("like_id")]
        public int LikeId { get; set; }
        [Column("age")]
        public int? Age { get; set; }
        [Column("del")]
        public bool Del { get; set; }
    }
    [Function]
    public static class DbFuns
    {

        public static int Count(object j)
        {
            return default;
        }
    }
}