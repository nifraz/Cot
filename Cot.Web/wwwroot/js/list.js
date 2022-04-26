var selectAllCheckbox = document.querySelector("#selectAll");
var selectRecordCheckboxes = document.querySelectorAll(".select-record-checkbox");

selectAllCheckbox.addEventListener("click", (event) => {
    for (var i = 0; i < selectRecordCheckboxes.length; i++) { //for(let checkbox of checkboxes)
        selectRecordCheckboxes[i].checked = event.currentTarget.checked;
    }
}, false);

var deleteButton = document.querySelector("#deleteButton");
var selectedItemsCount = document.querySelectorAll("input[class='select-record'").length;
var selectedItemsCountSpan = document.querySelector("#selectedItemsCountSpan");

deleteButton.addEventListener("click", (event) => {
    selectedItemsCountSpan.textContent = " " + selectedItemsCount;
}, false);