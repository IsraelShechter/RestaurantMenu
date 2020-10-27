
$(document).ready(function () {
    let selectedMeal = 0;

    $("#mainSelect").on('change', (event) => {
        selectedMeal = event.target.value;
    });

    $("#showMenu").on('click', () => {
        getMeal(false);
    });

    $("#showMenuEditable").on('click', () => {
        getMeal(true);
    });

    $('#newName').on("input", () => {
        $('#nameError').hide();
    });

    $('#editName').on("input", () => {
        $('#nameErrorEdit').hide();
    });


    $('#addMealBtn').on("click", () => {
        $('#nameError').hide();
        const meal = $('#newMeal')[0].value;
        const name = $('#newName')[0].value;
        const price = $('#newPrice')[0].value;
        if (!name) {
            $('#nameError').show();
        } else {

            addMeal(parseInt(meal), name, parseFloat(price), () => {
                $('#addServingModal').modal('hide');
            });
        }
    });

    $('#editMealBtn').on("click", (i) => {
        $('#nameErrorEdit').hide();
        const meal = $('#editMeal')[0].value;
        const name = $('#editName')[0].value;
        const price = $('#editPrice')[0].value;
        const id = $('#servingId')[0].value;
        if (!name) {
            $('#nameErrorEdit').show();
        } else {

            editMeal(parseInt(meal), name, parseFloat(price), id, () => {
                $('#editServingModal').modal('hide');
            });
        }
    });

    $('#addServingModal').on('shown.bs.modal', function () {
        $('#newMeal').val(0);
        $('#newName').val("");
        $('#newPrice').val(0);
        $('#newMeal').trigger('focus');
        
    });


    const getMeal = (isEditable) => {
        $.ajax({
            url: `/api/serving/${selectedMeal}`,
            contentType: "application/json",
            method: "GET",
            success: function (data) {
                populateTable(data, isEditable);
            }
        });
    };


    const populateTable = (data, isEditable) => {
        $("table tbody").replaceWith(document.createElement('tbody'));
        if (data) {
            data.forEach(s => {
                const editablrBtns = `<td><button id="${s.id}-edit" class="btn btn-link">ערוך</button><button id="${s.id}" class="btn btn-link">מחק</button></td>`;
                $("table tbody").append(
                    `<tr>
                    <td> ${s.name} </td>
                    <td> ${s.price.toFixed(2)} </td>
                    ${isEditable ? editablrBtns : ''}
                </tr>`);
                if (isEditable) {
                    $(`#${s.id}`).on('click', () => {
                        remove(s.id);
                    });
                    $(`#${s.id}-edit`).on("click", () => {
                        $('#servingId').val(s.id);
                        $('#editMeal').val(s.meal);
                        $('#editName').val(s.name);
                        $('#editPrice').val(s.price);
                        $('#editServingModal').modal('show');
                    });
                }
            });
        } else {
            $("table tbody").append(
                `<tr>
                    <td> לא נמצאו מנות</td>
                </tr>`);
        }

    };



    const addMeal = (meal, name, price,  callback) => {
        $.ajax({
            url: `/api/serving`,
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({ meal, name, price }),
            success: function () {
                callback();
                getMeal(true);
            }
        });

    };

    const editMeal = (meal, name, price, id, callback) => {
        $.ajax({
            url: `/api/serving/${id}`,
            contentType: "application/json",
            method: "PATCH",
            data: JSON.stringify({ meal, name, price }),
            success: function () {
                callback();
                getMeal(true);
            }
        });

    };

    const remove = (id) => {
        $.ajax({
            url: `/api/serving/${id}`,
            contentType: "application/json",
            method: "DELETE",
            success: function () {
                getMeal(true);
            }
        });
    };

});