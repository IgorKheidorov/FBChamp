async function getCourseTrainers(id) {
    try {
        const response = await fetch(`/api/courses/${id}/trainers`);
        const result = await response.json();
        fillTrainersSelect(result.trainers);
        fillTrainersTable(result.courseTrainers);
    } catch (e) {
        showError(e);
    }
}

async function addTrainerToCourse() {
    const courseId = document.getElementById("courseId").value;
    const trainerId = document.getElementById("trainersSelect").value;
    try {
        const response = await fetch(`/api/courses/${courseId}/trainers/${trainerId}`, { method: "POST" });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        fillTrainersSelect(result.trainers);
        fillTrainersTable(result.courseTrainers);
    } catch (e) {
        showError("Some errors. Please try again.");
    }
}

async function updateTrainerCourse(event) {
    const courseId = document.getElementById("courseId").value;
    const target = event.target.closest("tr");
    const trainerId = target.dataset.id;
    const order = Number(event.target.value);
    try {
        const response = await fetch(`/api/courses/${courseId}/trainers/${trainerId}?order=${order}`,
            { method: "PUT" });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        fillTrainersSelect(result.trainers);
        fillTrainersTable(result.courseTrainers);
    } catch (e) {
        showError("Can't update trainer!");
    }
}

async function deleteTrainerFromCourse(event) {
    if (!confirm("Do you want to delete this item?")) {
        return;
    }
    const courseId = document.getElementById("courseId").value;
    const target = event.target.closest("tr");
    const trainerId = target.dataset.id;
    try {
        const response = await fetch(`/api/courses/${courseId}/trainers/${trainerId}`, { method: "DELETE" });
        const result = await response.json();
        if (!response.ok) {
            throw result;
        }
        fillTrainersSelect(result.trainers);
        fillTrainersTable(result.courseTrainers);
    } catch (e) {
        showError("Can't delete trainer!");
    }
}

function fillTrainersSelect(list) {
    const select = document.getElementById("trainersSelect");
    select.options.length = 0;
    for (const trainer of list) {
        const option = document.createElement("option");
        option.value = trainer.id;
        option.text = trainer.name;
        select.add(option);
    }
}

function fillTrainersTable(list) {
    const table = document.getElementById("trainersTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    const tbody = table.getElementsByTagName("tbody")[0];
    for (const trainer of list) {
        const row = tbody.insertRow();
        row.dataset.id = trainer.trainerId;
        const fullName = row.insertCell();
        fullName.append(trainer.name);
        const visualOrder = row.insertCell();
        const input = document.createElement("input");
        input.setAttribute("type", "number");
        input.setAttribute("class", "form-control");
        input.setAttribute("title", "Visual Order");
        input.value = trainer.order;
        input.onchange = updateTrainerCourse;
        visualOrder.append(input);
        const action = row.insertCell();
        action.append(createDeleteCourseTrainerButton());
    }
}

function createDeleteCourseTrainerButton() {
    const div = document.createElement("div");
    div.className = "btn btn-outline-danger btn-sm";
    div.setAttribute("title", "Delete");
    div.innerHTML = `<i class="fas fa-trash-alt"></i>`;
    div.onclick = deleteTrainerFromCourse;
    return div;
}