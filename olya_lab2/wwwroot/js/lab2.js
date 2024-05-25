const uri = 'api/user';
let users = [];

function getUsers() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayUsers(data))
        .catch(error => console.error('Unable to get users.', error));
}

function addUser() {
    const addNameTextbox = document.getElementById('add-name');
    const addSurnameTextbox = document.getElementById('add-surname');
    const addEmailTextbox = document.getElementById('add-email');
    const addPasswordTextbox = document.getElementById('add-password');

    const user = {
        name: addNameTextbox.value.trim(),
        surname: addSurnameTextbox.value.trim(),
        email: addEmailTextbox.value.trim(),
        password: addPasswordTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then(() => {
            getUsers();
            addNameTextbox.value = '';
            addSurnameTextbox.value = '';
            addEmailTextbox.value = '';
            addPasswordTextbox.value = '';
        })
        .catch(error => console.error('Unable to add user.', error));
}

function deleteUser(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to delete user.', error));
}

function displayEditForm(id) {
    const user = users.find(user => user.id === id);
    document.getElementById('edit-id').value = user.id;
    document.getElementById('edit-name').value = user.name;
    document.getElementById('edit-surname').value = user.surname;
    document.getElementById('edit-email').value = user.email;
    document.getElementById('edit-password').value = user.password;
    document.getElementById('editUser').style.display = 'block';
}

function updateUser() {
    const userId = document.getElementById('edit-id').value;
    const user = {
        id: userId,
        name: document.getElementById('edit-name').value.trim(),
        surname: document.getElementById('edit-surname').value.trim(),
        email: document.getElementById('edit-email').value.trim(),
        password: document.getElementById('edit-password').value.trim()
    };

    fetch(`${uri}/${userId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to update user.', error));

    closeInput();
    return false;
}

function closeInput() {
    document.getElementById('editUser').style.display = 'none';
}

function _displayUsers(data) {
    const tBody = document.getElementById('users-table-body');
    tBody.innerHTML = '';

    data.forEach(user => {
        const editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm('${user.id}')`);

        const deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUser('${user.id}')`);

        const tr = tBody.insertRow();
        const td1 = tr.insertCell(0);
        const textNodeId = document.createTextNode(user.id);
        td1.appendChild(textNodeId);

        const td2 = tr.insertCell(1);
        const textNodeName = document.createTextNode(user.name);
        td2.appendChild(textNodeName);

        const td3 = tr.insertCell(2);
        const textNodeSurname = document.createTextNode(user.surname);
        td3.appendChild(textNodeSurname);

        const td4 = tr.insertCell(3);
        const textNodeEmail = document.createTextNode(user.email);
        td4.appendChild(textNodeEmail);

        const td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        const td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });


    users = data;
}

document.addEventListener('DOMContentLoaded', getUsers);
