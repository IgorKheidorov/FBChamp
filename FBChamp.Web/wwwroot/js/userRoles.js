async function getUserRoles(id) {
    try {
        const response = await fetch(`/api/users/${id}/roles`);
        const result = await response.json();
        fillRolesTable(result.userRoles);
        fillRolesSelect(result.roles);
    } catch (e) {
        showError(e);
    }
}

async function addRoleToUser() {
    const userId = document.getElementById("userId").value;
    const roleId = document.getElementById("rolesSelect").value;
    try {
        const response = await fetch(`/api/users/${userId}/roles/${roleId}`, { method: "POST" });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        fillRolesTable(result.userRoles);
        fillRolesSelect(result.roles);
    } catch (e) {
        showError("Nothing to add!");
    }
}

async function deleteRoleFromUser(event) {
    if (!confirm("Do you want to delete this item?")) {
        return;
    }
    const userId = document.getElementById("userId").value;
    const target = event.target.closest("tr");
    const roleId = target.dataset.id;
    try {
        const response = await fetch(`/api/users/${userId}/roles/${roleId}`, { method: "DELETE" });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        fillRolesTable(result.userRoles);
        fillRolesSelect(result.roles);
    } catch (e) {
        showError("Sorry, some errors... Can't delete role!");
    }
}

function fillRolesTable(roles) {
    const table = document.getElementById("rolesTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    const tbody = table.getElementsByTagName("tbody")[0];
    for (const role of roles) {
        const row = tbody.insertRow();
        row.dataset.id = role.id;
        const name = row.insertCell();
        name.append(role.name);
        const action = row.insertCell();
        action.append(createDeleteRoleButton());
    }
}

function fillRolesSelect(roles) {
    const select = document.getElementById("rolesSelect");
    select.options.length = 0;
    if (roles.length === 0) {
        document.getElementById("rolesDiv").style.display = "none";
    } else {
        document.getElementById("rolesDiv").style.display = "flex";
    }

    for (const role of roles) {
        const option = document.createElement("option");
        option.value = role.id;
        option.text = role.name;
        select.add(option);
    }
}

function createDeleteRoleButton() {
    const div = document.createElement("div");
    div.className = "btn btn-outline-danger btn-sm";
    div.setAttribute("title", "Delete");
    div.innerHTML = "<i class='fas fa-trash-alt'></i>";
    div.onclick = deleteRoleFromUser;
    return div;
}