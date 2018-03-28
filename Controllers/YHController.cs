using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using YHSchool.Core;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    [EnableCors("corA")]
    [Produces("application/json")]
    [Route("api/YH")]
    public class YHController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YHController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<Enroll> GetEnrolls()
        {
            return _context.Enrolls;
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnroll([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enroll = await _context.Enrolls.SingleOrDefaultAsync(m => m.ID == id);

            if (enroll == null)
            {
                return NotFound();
            }

            return Ok(enroll);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnroll([FromRoute] int id, [FromBody] Enroll enroll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enroll.ID)
            {
                return BadRequest();
            }

            _context.Entry(enroll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<IActionResult> PostEnroll([FromBody] Enroll enroll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enrolls.Add(enroll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnroll", new { id = enroll.ID }, enroll);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnroll([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enroll = await _context.Enrolls.SingleOrDefaultAsync(m => m.ID == id);
            if (enroll == null)
            {
                return NotFound();
            }

            _context.Enrolls.Remove(enroll);
            await _context.SaveChangesAsync();

            return Ok(enroll);
        }

        private bool EnrollExists(int id)
        {
            return _context.Enrolls.Any(e => e.ID == id);
        }

        #region postRobert 验证钉钉自定义机器人
        [HttpPost, Route("postRobert")]
        public IActionResult PostRobert([FromBody] Enroll item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var resMsg = DDHelper.SendMsg(item.Message);

            JObject jsonSearch = JObject.Parse(resMsg);

            string errcode = jsonSearch["errcode"].ToString();
            string errmsg = jsonSearch["errmsg"].ToString();

            var logObj = new EventLog();
            logObj.ActionType = "发送消息给钉钉机器人";
            logObj.LogLevel = LogLevel.Info.ToString();
            if (errmsg == "token is not exist")
            {
                logObj.LogLevel = LogLevel.Error.ToString();
            }
            logObj.CreateDate = DateTime.Now;
            logObj.Creator = "System";

            _context.Add(logObj);
            _context.SaveChangesAsync();

            return Json(new
            {
                item,
                errmsg
            });

            // return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
        #endregion

        #region postRobert 验证钉钉自定义机器人
        [HttpPost, Route("postYH")]
        public IActionResult PostYH([FromBody] dynamic msg_yh)
        {
            if (msg_yh == null)
            {
                return BadRequest();
            }

            var testJson = Json(msg_yh);

            JObject jsonSearch = JObject.Parse(msg_yh.ToString());

            string resCode = "200";
            string msg = "load data success";
            var _creator = "扬航用户";
            var msgTem = string.Empty;
            try
            {
                string formCode = jsonSearch["form"].ToString();
                var formObj = _context.YHForms.Where(x => x.FormCode == formCode).FirstOrDefault();

                if (formObj != null)
                {
                    msgTem = formObj.MsgTemplate;
                    if (!string.IsNullOrEmpty(msgTem))
                    {
                        // string str = "d {field_1} hdd {abc} {field_7} ok, {field_10} test";
                        var pattern = "{(.*?)}";  // 读取多个 {}中的字符串
                        var mc = Regex.Matches(msgTem, pattern);
                        var keys = new ArrayList();
                        var itemA = String.Empty;
                        foreach (Match ms in mc)
                        {
                            keys.Add(ms.Groups[1].Value);
                            itemA = jsonSearch[ConstraintStr.CON_YHEntryName][ms.Groups[1].Value].ToString();
                            msgTem = msgTem.Replace(ms.Value, itemA);
                        }

                        SendToDDing(msgTem);
                    }
                }
                
                Enroll enrollObj = new Enroll();
                enrollObj.FormCode = formCode;
                enrollObj.OriginMsg = msg_yh.ToString();
                enrollObj.Message = msgTem;
                enrollObj.CreateDate = DateTime.Now;
                enrollObj.Creator = _creator;

                _context.Enrolls.Add(enrollObj);
                _context.SaveChangesAsync();

            }
            catch (Exception er)
            {
                resCode = "201";
                msg = er.Message;
                _context.EventLogs.Add(new EventLog() {LogLevel=LogLevel.Error.ToString(),Comments= er.Message,CreateDate=DateTime.Now,Creator= _creator });
                _context.SaveChangesAsync();
               
            }           

            return Json(new
            {
                resCode,
                msg
            });
        }
        #endregion

        public async void SendToDDing(string msg)
        {
            if (String.IsNullOrEmpty(msg))
            {
                return;
            }

            var resMsg = DDHelper.SendMsg(msg);

            JObject jsonSearch = JObject.Parse(resMsg);

            string errcode = jsonSearch["errcode"].ToString();
            string errmsg = jsonSearch["errmsg"].ToString();

            var logObj = new EventLog();
            logObj.ActionType = "发送消息给钉钉机器人";
            logObj.LogLevel = LogLevel.System.ToString();

            logObj.CreateDate = DateTime.Now;
            logObj.Creator = "System";

            _context.Add(logObj);
            await _context.SaveChangesAsync();

            // return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
    }
}