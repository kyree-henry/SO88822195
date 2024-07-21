// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const dateTimeInputs = document.querySelectorAll('input[type="datetime-local"]');
    const now = new Date();
    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
    const formattedDateTime = now.toISOString().slice(0, 16);
    dateTimeInputs.forEach(function (input) {
        input.min = formattedDateTime;
    });
});

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/reminderHub")
    .build();

connection.on("ReceiveReminder", function (title) {
    const reminderSound = document.getElementById('reminderSound');
    reminderSound.play();
    fhToastr.info(`Sending email notification for '${title}' as a reminder.`);
});

connection.on("ReceiveUpdatedReminders", function (reminders) {

    let reminderTableBody = document.getElementById('reminderTableBody');
    reminderTableBody.innerHTML = '';

    reminders.forEach(function (reminder) {
        let newRow = document.createElement('tr');
        let status = "Pending";
        let now = new Date();
        let reminderDate = new Date(reminder.dateTime);
        if (reminderDate <= now) {
            status = "Completed";
        }
        newRow.innerHTML = `<td>${reminder.title}</td>
                                <td>${new Date(reminder.dateTime).toLocaleString()}</td>
                                <td><span class="badge ${status === "Completed" ? 'badge-success' : 'badge-warning'}">${status}</span></td>`; reminderTableBody.appendChild(newRow);
    });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});