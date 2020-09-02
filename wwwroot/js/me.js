
document.getElementById('me-popup-open').addEventListener('click', openMyProfile);

let popup = document.getElementById('mePopup');

let avatar = document.getElementById('my-avatar');

let myUserName = document.getElementById('my-name');

let myID= document.getElementById('my-id');

let fileInput = document.getElementById('avatar-input');

function openMyProfile() {

    getUserInfo(userID, function (user) {
        avatar.src = user.photo.imageHeaders + user.photo.imageBinary;
        myUserName.textContent = user.userName;
        myID.textContent = user.id;
        popup.style.display = 'block';
    });
}

document.getElementById('my-avatar').addEventListener('click', function () {
    fileInput.click();
});

fileInput.onchange = function (e) {
    e.preventDefault();
    let form = new FormData();
    form.append('photo', fileInput.files[fileInput.files.length - 1]);
    $.ajax({
        type: "POST",
        url: '/Chat/UpdateUserPhoto',
        contentType: false,
        processData: false,
        data: form,
        success: function (result) {
            if (result != null) {
                saveUser(result);
                avatar.src = result.photo.imageHeaders + result.photo.imageBinary;
                document.getElementById('me-popup-open').src = result.photo.imageHeaders + result.photo.imageBinary;
            }
        },
        error: function (xhr, status, p3) {
            console.log(status);
        }
    });
};
