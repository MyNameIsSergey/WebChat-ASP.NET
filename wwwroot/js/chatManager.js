
let chats = [];

let messages = [];

let currentChatID;

let chatListHTML = document.getElementById('chat-list');

let messageListHTML = document.getElementById('chat-history');

let chatMembersList = document.getElementById('chat-mem');

let userID = "";

//=====================================

hubConnection.on("ChangeID", function (id) {
    userID = id;
    getUserInfo(userID, function (user) {
        document.getElementById('me-popup-open').src = user.photo.imageHeaders + user.photo.imageBinary;
    });
});

hubConnection.on("Send", function (message) {
    let msgList = messages.find(m => m.length > 0 && m[0].chatID === message.chatID);
    if (msgList === undefined) {
        hubConnection.invoke("GetMessages", Number(message.chatID));
        return;
    }
    if (Number(message.chatID) === Number(currentChatID)) {
        let elem = createHTMLMessageLine(message, userID === message.senderID);
        messageListHTML.append(elem);
        msgList.push(message);
        //location.href = "#" + message.id;
    }
});


document.getElementById("sendButton").addEventListener("click", function (e) {
    let message = document.getElementById("message-to-send").value;
    document.getElementById("message-to-send").value = '';
    if(message !== '')
        hubConnection.invoke("Send", message, Number(currentChatID));
});

hubConnection.on("UpdateChatList", updateChatList);

function updateChatList(chatList) {
    chatListHTML.innerHTML = "";
    chats = chatList;
    for (let i = 0; i < chatList.length; i++) {
        let elem = createHTMLChatLine(chatList[i]);
        elem.addEventListener("click", chatClick);
        chatListHTML.appendChild(elem);
    }
}

hubConnection.on("UpdateMessages", function (msgList, chatID) {
    messageListHTML.innerHTML = "";
if (msgList.length === 0)
        return;

    let index = messages.findIndex((m, index, array) => m.length > 0 &&  m[0].id === chatID);
    console.log(index);
    if (index >= 0)
        messages[index] = msgList;
    else
        messages.push(msgList);
    console.log(messages);
    if (chatID === Number(currentChatID))
        showMessages(msgList);
});

hubConnection.on("AddChat", function (chat) {
    chats.push(chat);
    let chatHTML = createHTMLChatLine(chat);
    chatListHTML.addEventListener("click", chatClick);
    chatListHTML.appendChild(chatHTML);
});

hubConnection.on("DeleteChat", function (id) {
    for (var i = chats.length - 1; i != -1; i--) {
        if (chats[i].id === id)
            chats.splice(i, 1);
    }
    updateChatList(chats);
});


function chatClick(e) {
    let element = e.target;
    while (element.id == '') {
        element = element.parentElement;
    }
    if (currentChatID === Number(element.id))
        return;
    let current = document.getElementById(currentChatID);
    if (current != null)
        makeStandart(current);
    makeCurrent(element);
    currentChatID = element.id;
    changeChat(currentChatID);
    showChatMembers(currentChatID);
     //let chat = findCurrentChat();
    //changeHead(chat);
}

function showChatMembers(chatID) {
    chatMembersList.innerHTML = "";
    getChatUsers(chatID, function (array, chat) {
        for (let i = 0; i < array.length; i++) {
            getUserInfo(array[i], function (user) {
                let userHTML = createHTMLUser(user);
                userHTML.addEventListener('click', userClick);
                chatMembersList.appendChild(userHTML);
            });
        }
    });
}

function userClick(e) {
    let element = e.target;
    while (element.id == '') {
        element = element.parentElement;
    }
    getUserInfo(element.id, user => showUserInfo(user));
}

function changeChat(chatID) {
    let numChatID = Number(chatID);
    console.log(messages);
    let msgList = messages.find(m => m.length > 0 && m[0].chatID === numChatID);
    if (msgList === null || msgList === undefined) {
        hubConnection.invoke("GetMessages", Number(chatID));
        return;
    }
    showMessages(msgList);
}

function showMessages(msgList) {
    messageListHTML.innerHTML = "";
    for (let i = 0; i < msgList.length; i++) {
        messageListHTML.appendChild(createHTMLMessageLine(msgList[i], userID === msgList[i].senderID));
    }
    //if (msgList.length > 0)
    //    location.href = "#" + msgList[msgList.length - 1].id;

}

function findCurrentChat() {
    return chats.find((c, index, array) => Number(c.id) === Number(currentChatID));
}

document.getElementById('createChatBtn').addEventListener('click', function() {
    let name = document.getElementById('chat-name').value;
    if (name !== '')
        hubConnection.invoke("CreateChat", name);
});

document.getElementById('quit-chat').addEventListener('click', function (e) {
    if(currentChatID !== undefined)
        hubConnection.invoke("LeaveChat", Number(currentChatID));
});


document.getElementById('add-mem-btn').addEventListener('click', function (e) {
    let id = document.getElementById('mem-id').value;
    if (currentChatID === undefined && value === '')
        return;
    hubConnection.invoke("AddChatMember", id, Number(currentChatID));
});
