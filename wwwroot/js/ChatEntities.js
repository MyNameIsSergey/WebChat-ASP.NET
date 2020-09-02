

function createHTMLChatLine(chat) {

    let elem = document.createElement("li");
    elem.className = 'clearfix chat-preview';
    let img = document.createElement("img");
    img.src = chat.image.imageHeaders + chat.image.imageBinary;
    img.alt = 'avatar';
    img.className = 'chat-list-pic';

    let about = document.createElement("div");
    about.className = 'about';

    let name = document.createElement("div");
    name.className = 'name';
    name.textContent = chat.name;
    about.appendChild(name);

    elem.appendChild(img);
    elem.appendChild(about);
    elem.id = chat.id;

    return elem;
}

function createHTMLUser(user) {
    let elem = document.createElement("li");
    elem.className = 'clearfix chat-preview';
    let img = document.createElement("img");
    img.src = user.photo.imageHeaders + user.photo.imageBinary;
    img.alt = 'avatar';
    img.className = 'chat-list-pic';

    let about = document.createElement("div");
    about.className = 'about';

    let name = document.createElement("div");
    name.className = 'name';
    name.textContent = user.userName;

    about.appendChild(name);

    elem.appendChild(img);
    elem.appendChild(about);
    elem.id = user.id;

    return elem;
}

function createHTMLMessageLine(message, fromMe) {

    let elem = document.createElement("li");
    elem.className = 'clearfix';
    let data = document.createElement("div");
    data.className = 'message-data align-right';
    let date = document.createElement("span");
    date.className = 'message-data-time';
    date.innerHTML = (new Date(message.when)).toDateString();
    let name = document.createElement("span");
    name.className = 'message-data-name';
    name.innerHTML = message.senderName;
    data.appendChild(date);
    data.appendChild(document.createElement('br'));
    data.appendChild(name);
    let content = document.createElement("div");
    if(fromMe)
        content.className = 'message my-message';
    else
        content.className = 'message other-message';
    content.textContent = message.content;

    elem.appendChild(data);
    elem.appendChild(content);
    elem.id = message.id;

    return elem;
}

function makeCurrent(e) {
    e.classList.add('current');
}

function makeStandart(e) {
    e.classList.remove('current');
}

let headImage = document.getElementById('head-image');
let headChatName = document.getElementById('head-chat-name');

function changeHead(chat) {
    headImage.src = chat.image.imageHeaders + chat.image.imageBinary;
    headChatName.textContent = chat.name;
}

function showUserInfo(user) {
    document.getElementById('user-avatar').src = user.photo.imageHeaders + user.photo.imageBinary;
    document.getElementById('user-name').textContent = user.userName;
    document.getElementById('user-id').textContent = user.id;
    document.getElementById('userPopup').style.display = "block";
}