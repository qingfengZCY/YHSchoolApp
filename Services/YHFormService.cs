
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Services
{
    public class YHFormService
    {
        private readonly ApplicationDbContext _context;

        public YHFormService(ApplicationDbContext context)
        {
            _context = context;
        }

        public YHForm GetYHForm(string formCode)
        {
            return _context.YHForms.Where(x => x.FormCode == formCode).AsNoTracking().FirstOrDefault();
        }

        public int AddEnroll(Enroll enroll)
        {
            _context.Enrolls.Add(enroll);
            return _context.SaveChanges();
        }
    }
}
