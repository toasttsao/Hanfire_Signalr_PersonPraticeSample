using Hangfire;
using HangFire_API.EvenCls;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
namespace HangFire_API.Controllers
{
    [RoutePrefix("api/BuyOrder")]
    public class BuyOrderController : ApiController
    {
        [HttpGet]
        [Route("hangfire/{name}/{message}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult hangefire( string name, string message)
        {
            BackgroundJobClient _jobs = new BackgroundJobClient();
            //新增一個作業
            var parentId = _jobs.Enqueue<BuysomethingCls>(x => x.GobuySomething(name, message));
            //作業完成後執行
            _jobs.ContinueWith<BuysomethingCls>(parentId, x => x.GoHome(parentId, name, message));
            //回傳TaskId
            return Ok(parentId);
        }



        [HttpGet]
        [Route("taskcontinuation/{name}/{message}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult taskcontinuation(string name, string message)
        {

            BuysomethingCls buyCls = new BuysomethingCls();
            //新增一個作業
            var firstTask = Task.Run(() => {
                buyCls.GobuySomething(name, message);
            });

            //作業完成後執行
            Task continuationTask = firstTask.ContinueWith(async (antecedent) =>
            {
                int parentId = antecedent.Id;
                await buyCls.GoHome(parentId.ToString(), name, message);
            });

            //回傳TaskId
            return Ok(firstTask.Id);
        }
    }
}