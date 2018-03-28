
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Services
{
    public class EventLogService
    {
        private readonly ApplicationDbContext _context;

        public EventLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddEvent(int enrollID, string comments, string actionType, LogLevel logLevel, string creator)
        {
            var logObj = new EventLog() { EnrollID = enrollID, LogLevel = logLevel.ToString(), Comments = comments, ActionType = actionType, CreateDate = DateTime.Now, Creator = creator };
            
            _context.EventLogs.Add(logObj);
            _context.SaveChangesAsync();
        }

        public IList<EventLog> GetAllEvent()
        {
            return _context.EventLogs.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get eventLog by enrollID
        /// </summary>
        /// <param name="enrollID"></param>
        /// <returns></returns>
        public IList<EventLog> GetEvent(int enrollID)
        {
            return _context.EventLogs.Where(x => x.EnrollID == enrollID).AsNoTracking().ToList();
        }
    }
}
