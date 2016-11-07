using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Hangfire_Signalr.Hubs
{
    public class SampleHUB : Hub
    {
        /// <summary>
        /// 提供HangFire_API作業完成後呼叫
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="who"></param>
        /// <param name="message"></param>
        public void Send(string taskId, string who, string message)
        {
            //提供receiveMessage,當作業完成讓Client端接收訊息
            Clients.All.receiveMessage(taskId, who, message);
        }

        #region 連線狀態

        /// <summary>
        /// 連線
        /// </summary>
        /// <returns></returns>

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        /// <summary>
        /// 重新連線
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        /// <summary>
        /// 斷線
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        #endregion 連線狀態
    }
}