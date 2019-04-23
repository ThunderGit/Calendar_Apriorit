$(function () {
    $("#CreateNewEvent").click(function () {
        var EventVM;

        var RepeatInfoVM;
        var EventInfoVM;
        if ($("#IsRepeated").prop("checked")) {
            RepeatInfoVM = JSON.stringify({
                'WeekDayForRepeat': $('#DayRepeat').val(),
                'TypeRepeat': $('#TypeRepeat').val(),
                'QuantityRepeats': $('#QuantityRepeats').val(),
            });
            EventInfoVM = JSON.stringify({
                'Description': $('#Description').val(),
                'EndTime': $('#EndTime').val(),
                'StartTime': $('#StartTime').val(),
                'RepeatInfoVM': RepeatInfoVM,
                'IsRepeated': true
            });
        }
        else {
            EventInfoVM = JSON.stringify({
                'Description': $('#Description').val(),
                'EndTime': $('#EndTime').val(),
                'StartTime': $('#StartTime').val(),
                'RepeatInfoVM': RepeatInfoVM,
                'IsRepeated': false
            });

        }
        EventVM = JSON.stringify({
            'Id': null,
            'Title': $('#Title').val(),

        });
        console.log("3");
        $.ajax({
            type: "POST",
            url: "/Calendar/CreateNewEvent",
            data: { EventVM, EventInfoVM, RepeatInfoVM },
            beforeSend: function () {


            },
            success: function (response) {
                alert("Sucsses");
                window.location.href = "/Calendar/Calendar";

            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    });
});
