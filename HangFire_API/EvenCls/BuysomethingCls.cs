using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
namespace HangFire_API.EvenCls
{
    public class BuysomethingCls
    {
        /// <summary>
        /// 由WEB傳來的參數轉換為中文
        /// </summary>
        private static readonly Dictionary<string, string> transString = new Dictionary<string, string>() {
            {"buyEgg","雞蛋"},
            {"buydaoyo","醬油"},
            {"buyfruit","水果"}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void GobuySomething(string name, string message)
        {
            //Do Somthing 買醬油,買雞蛋,買水果....

        }
        /// <summary>
        /// 當GobuySomething事件處理完成後呼叫,將訊息推往至SignalR
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task GoHome(string parentId, string name, string message)
        {
            string feedbackmessage = string.Format("阿母)))~{0},買回來了", transString.Where(w => w.Key == message).Select(s => s.Value).FirstOrDefault());
            using (HubConnection connection = new HubConnection("http://localhost:50925/habfireSR"))
            {
                IHubProxy myHub = connection.CreateHubProxy("SampleHUB");
                // 與SignalR連線
                await connection.Start().ContinueWith(task =>
                {
                    //連線成功時
                    if (!task.IsFaulted)
                    {
                        //呼叫SampleHUB中提供Send的方法,將TaskId,誰去買,跟訊息傳送過去
                        myHub.Invoke("Send", parentId, name, feedbackmessage);
                    }
                });

                connection.Stop();
            }
        }
    }
}