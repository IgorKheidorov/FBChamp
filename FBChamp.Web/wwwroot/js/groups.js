const editGroupModal = new bootstrap.Modal(document.getElementById("editGroupModal"));

async function getGroups() {
    try {
        const response = await fetch(apiUri);
        const result = await response.json();
        fillTable(result);
    } catch (e) {
        showError(e);
    }
}

async function addGroup() {
    const name = document.getElementById("name").value;
    const description = document.getElementById("description").value;
    const visualOrder = Number(document.getElementById("visualOrder").value);
    const model = { name, description, visualOrder };
    try {
        const response = await fetch(apiUri,
            {
                headers: { "Content-Type": "application/json" },
                method: "POST",
                body: JSON.stringify(model)
            });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        editGroupModal.hide();
        fillTable(result);
    } catch (err) {
        fillErrors(err);
    }
}

async function editGroup() {
    const id = document.getElementById("id").value;
    const name = document.getElementById("name").value;
    const description = document.getElementById("description").value;
    const visualOrder = Number(document.getElementById("visualOrder").value);
    const model = { id, name, description, visualOrder };
    try {
        const response = await fetch(apiUri,
            {
                headers: { "Content-Type": "application/json" },
                method: "PUT",
                body: JSON.stringify(model)
            });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        editGroupModal.hide();
        fillTable(result);
    } catch (err) {
        fillErrors(err);
    }
}

async function deleteGroup(id) {
    if (!confirm("Do you want to delete this item?")) {
        return;
    }
    try {
        const response = await fetch(apiUri + id, { method: "DELETE" });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        fillTable(result);
    } catch (e) {
        if (e.error) {
            showError(e.error);
        }
    }
}

function createAddWindow() {
    document.getElementById("title").innerHTML = "Create Group";
    document.getElementById("errorsList").innerHTML = "";
    document.getElementById("name").value = null;
    document.getElementById("visualOrder").value = null;
    document.getElementById("description").value = null;
    document.getElementById("saveButton").onclick = addGroup;
    editGroupModal.show();
}

function createEditWindow(group) {
    document.getElementById("title").innerHTML = "Edit Group";
    document.getElementById("errorsList").innerHTML = "";
    document.getElementById("id").value = group.id;
    document.getElementById("name").value = group.name;
    document.getElementById("visualOrder").value = group.visualOrder;
    document.getElementById("description").value = group.description;
    document.getElementById("saveButton").onclick = editGroup;
    editGroupModal.show();
}

function fillTable(groups) {
    const table = document.getElementById("groupsTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }

    const tbody = table.getElementsByTagName("tbody")[0];
    for (const group of groups) {
        const row = tbody.insertRow();
        row.style.verticalAlign = "middle";

        const name = row.insertCell();
        name.append(group.name);
        name.classList.add("fw-bold");

        const description = row.insertCell();
        if (group.description != null) {
            description.append(group.description);
        }

        const order = row.insertCell();
        order.append(group.visualOrder);

        const actions = row.insertCell();

        const editButton = document.createElement("div");
        editButton.setAttribute("title", "Edit");
        editButton.className = "btn btn-outline-warning btn-sm";
        editButton.innerHTML = `<i class="fas fa-pencil-alt"></i>`;
        editButton.onclick = () => onEditClick(group.id);
        actions.appendChild(editButton);

        const deleteButton = document.createElement("div");
        deleteButton.setAttribute("title", "Delete");
        deleteButton.className = "btn btn-outline-danger btn-sm";
        deleteButton.innerHTML = `<i class="fas fa-trash-alt"></i>`;
        deleteButton.style.marginLeft = "6px";
        deleteButton.onclick = () => deleteGroup(group.id);
        actions.appendChild(deleteButton);
    }
}

function fillErrors(err) {
    if (err.errors) {
        if (err.errors["Name"]) {
            addError(err.errors["Name"]);
        }
        if (err.errors["Description"]) {
            addError(err.errors["Description"]);
        }
    }
    if (err["Name"]) {
        addError(err["Name"]);
    }
}

function addError(errors) {
    const div = document.getElementById("errorsList");
    div.innerHTML = "";
    for (const error of errors) {
        const p = document.createElement("p");
        p.append(error);
        div.append(p);
    }
}

async function onEditClick(id) {
    const response = await fetch(apiUri + id);
    const result = await response.json();
    createEditWindow(result);
}