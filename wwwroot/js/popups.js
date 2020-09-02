
let buttonClass = document.getElementById('create-popup-open').className;


document.getElementById('create-popup-open').addEventListener('click', popUpCreate);
document.getElementById('popup-create-close').addEventListener('click', popUpCreateHide);
document.getElementById('popup-user-close').addEventListener('click', popUpUserHide);
document.getElementById('popup-me-close').addEventListener('click', popUpMeHide);
document.getElementById('me-popup-open').addEventListener('click', popUpMe);

function popUpCreate() {
	document.getElementById('createChatPopup').style.display = "block";
}
function popUpCreateHide() {
	document.getElementById('createChatPopup').style.display = "none";
}

function popUpUserHide() {
	document.getElementById('userPopup').style.display = "none";
}

function popUpMe() {
	document.getElementById('mePopup').style.display = "block";
}

function popUpMeHide() {
	document.getElementById('mePopup').style.display = "none";
}