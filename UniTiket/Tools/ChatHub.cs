using Microsoft.AspNetCore.SignalR;
using UniTiket.Repositories;

namespace UniTiket.Tools
{
    public class ChatHub : Hub
    {
        private readonly HttpContext _httpContext;
        private readonly IMessageRepository _mr;
        public ChatHub(IHttpContextAccessor httpContext, IMessageRepository mr)
        {
            _httpContext = httpContext.HttpContext;
            _mr = mr;
        }

        public override async Task OnConnectedAsync()
        {
           // await Clients.All.SendAsync("ReciveMessage", "amir", DateTime.Now, "jgjjgyjhfgvdcsfsef");

            //int userId = int.Parse(_httpContext.User.FindFirst("UserId").Value);
            //if (!await _gr.HasAnyChat(userId))
            //{
            //    var group = await _gr.AddAsync(new()
            //    {
            //        FirstUserId = userId,
            //        SecondUserId = userId,
            //        UpTime = DateTime.Now
            //    });
            //    await _gr.SaveChangesAsync();

            //    var message = await _mr.AddAsync(new()
            //    {
            //        CreatedTime = DateTime.Now,
            //        GroupId = group.GroupId,
            //        UserId = userId,
            //        Text = "Hello! This plase for your save message :) "
            //    });
            //    await _mr.SaveChangesAsync();

            //    await Clients.Caller.SendAsync("ReciveMessage",
            //     "Messager",
            //     message.CreatedTime,
            //     message.Text);
            //}

            //await base.OnConnectedAsync();
        }
        public async Task SendMessage(string tiketId, string text, string isAnser = "0")
        {
            var msg = await _mr.AddAsync(new()
            {
                TiketId = int.Parse(tiketId),
                Text = text,    
                CreatedTime = DateTime.Now,
                IsAnser = (isAnser != "0")? true : false
            });
            await _mr.SaveChangesAsync();

            await Clients.All.SendAsync("ReciveMessage", _httpContext.User.Identity.Name, msg.CreatedTime, msg.Text, msg.IsAnser);
        }
    }
}
