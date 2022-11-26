var objDiv = document.getElementById("messagesdiv");
objDiv.scrollTop = objDiv.scrollHeight;

//connect signalR
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chatHub')
    .build();

connection.on('ReciveMessage', renderMessage);

connection.start();

function ready() {
    var MessageForm = document.getElementById('MessageForm');
    MessageForm.addEventListener('submit',
        function (e) {
            e.preventDefault();
            var text = e.target[0].value;
            e.target[0].value = '';
            sendMessage(text, e.target[1].value, e.target[2].value);
        })
}

function sendMessage(text, tiketId, isAnser) {
    if (text && text.length) {
        connection.invoke('SendMessage', tiketId, text, isAnser);
    }
}

function renderMessage(name, time, message, isAnser) {
    let div = document.createElement('div');
    div.className = 'row';
    if (!isAnser) {
        div.innerHTML =
            '<div class="col-md-4"></div><div class="col-md-8"><div class="bdr-callout bdr-callout-primary"><span class="fw-semibold">'
            + name + '</span><p>'
            + message + '</p><span style="font-size:small">'
            + time + '</span></div></div>';
    }
    else {
        div.innerHTML =
            '<div class="col-md-8"><div class="bd-callout bd-callout-info"><span class="fw-semibold">Admin</span><p>'
            + message + '</p><span style="font-size:small">'
        + time + '</span></div></div><div class="col-md-4"></div>';
    }
    document.getElementById('messagesdiv').appendChild(div);

    objDiv.scrollTop = objDiv.scrollHeight;
}

document.addEventListener('DOMContentLoaded', ready);

